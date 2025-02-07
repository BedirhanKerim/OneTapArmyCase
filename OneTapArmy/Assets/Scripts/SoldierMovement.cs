using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using static OneTapArmyCore.Enums;

namespace OneTapArmyCore
{
    public class SoldierMovement : MonoBehaviour
    {  
        [SerializeField]private Rigidbody rb;
        public SoldierBehaviourState soldierBehaviour;
        public Vector3 targetLocation;
        public bool movementIsDone;
        
        void FixedUpdate()
        {
            Move();
        }

        public void SetMovementData(Vector3 targetLoc)
        {
            targetLocation = targetLoc;
            movementIsDone = false;
            soldierBehaviour = SoldierBehaviourState.Walking;
            GameManager.Instance.movementManager.totalWaitingSoldier = 0;
        }
        public void SetMovementBaseData(Vector3 targetLoc)
        {
            targetLocation = targetLoc;
            movementIsDone = false;
            soldierBehaviour = SoldierBehaviourState.WaitingOnBase;
        }
        public void Move()
        {
            if (movementIsDone)
            {
                rb.velocity=Vector3.zero;
                return;
            }
            Vector3 direction = Vector3.zero;
            float speed = 0f;
            Quaternion targetRotation;
            Quaternion smoothedRotation;

            if (rb.velocity.magnitude < 1f)
            {
              //  animator.SetBool("isWalk", false);
                //animator.SetBool("isAttack",false);
            }
            else
            {
               // animator.SetBool("isWalk", true);
            }
            direction = (targetLocation - transform.position).normalized;

            if (direction == Vector3.zero)
            {
                targetRotation = Quaternion.Euler(Vector3.one * Mathf.Epsilon);
            }
            else
            {
                targetRotation = Quaternion.LookRotation(direction);
            }
            smoothedRotation = Quaternion.Lerp(rb.rotation, targetRotation, 8 * Time.fixedDeltaTime);

            if (soldierBehaviour==SoldierBehaviourState.Walking||
                soldierBehaviour==SoldierBehaviourState.Waiting)
            {
                direction.y = 0;
                speed = 3f;
                var distance=(rb.position - transform.position).sqrMagnitude;
                Vector3 newPosition = Vector3.MoveTowards(rb.position, targetLocation, speed * Time.fixedDeltaTime);
                rb.MovePosition(newPosition);
                smoothedRotation = Quaternion.Lerp(rb.rotation, targetRotation, 5 * Time.fixedDeltaTime);
                rb.MoveRotation(smoothedRotation);

                    if (distance<.1f)
                    {
                        movementIsDone = false; // Hareketi durdur

                    }
                
              
            }
            else if (soldierBehaviour==SoldierBehaviourState.WaitingOnBase)
            {
                direction.y = 0;
                speed = 3f;
                var distance=(rb.position - transform.position).sqrMagnitude;
                Vector3 newPosition = Vector3.MoveTowards(rb.position, targetLocation, speed * Time.fixedDeltaTime);
                rb.MovePosition(newPosition);
                smoothedRotation = Quaternion.Lerp(rb.rotation, targetRotation, 5 * Time.fixedDeltaTime);
                rb.MoveRotation(smoothedRotation);

                if (distance<.1f)
                {
                    movementIsDone = false; // Hareketi durdur

                }
            }
        }
    }
}