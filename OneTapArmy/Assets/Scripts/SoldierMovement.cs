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
        public SoldierAttack soldierAttack;
        public Vector3 targetLocation;
        public bool movementIsDone;
        public TeamType teamType;
        private float rangeBetweentarget;
        [SerializeField] private Animator animator;
        [SerializeField] private Transform targetEnemySoldier;
        void FixedUpdate()
        {
            Move();
        }

        public void SetMovementData(Vector3 targetLoc)
        {
            targetLocation = targetLoc;
            movementIsDone = false;
            soldierBehaviour = SoldierBehaviourState.Walking;
        }
        public void SetMovementBaseData(Vector3 targetLoc)
        {
            targetLocation = targetLoc;
            movementIsDone = false;
            soldierBehaviour = SoldierBehaviourState.WaitingOnBase;
        }
        public void Move()
        {    
            Vector3 direction = Vector3.zero;
                     Quaternion targetRotation;
            if (movementIsDone)
            {


                 if (teamType==TeamType.Player)
                 {
                     targetRotation = Quaternion.LookRotation(Vector3.forward);
                    
                 }
                 else
                 {
                     targetRotation = Quaternion.LookRotation(Vector3.back);

                 }
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 2 * Time.deltaTime);           
               
                return;
            }
            float speed = 0f;
            Quaternion smoothedRotation;
//Debug.Log(rb.velocity.magnitude);
           
            direction = (targetLocation - transform.position).normalized;

       
            {
                targetRotation = Quaternion.LookRotation(direction);
            }

            if (soldierBehaviour==SoldierBehaviourState.Walking||
                soldierBehaviour==SoldierBehaviourState.Waiting)
            {
                direction.y = 0;
                speed = 3f;
                var distance=(targetLocation - transform.position).sqrMagnitude;
                Vector3 newPosition = Vector3.MoveTowards(rb.position, targetLocation, speed * Time.fixedDeltaTime);
                rb.MovePosition(newPosition);
                smoothedRotation = Quaternion.Lerp(rb.rotation, targetRotation, 5 * Time.fixedDeltaTime);
                rb.MoveRotation(smoothedRotation);
                animator.SetBool("isWalk", true);

                    if (distance<.001f)
                    {
                        movementIsDone = true; // Hareketi durdur
                        animator.SetBool("isWalk", false);
                        animator.SetBool("isAttack",false);
                    }
                
              
            }
            else if (soldierBehaviour==SoldierBehaviourState.WaitingOnBase)
            {
                direction.y = 0;
                speed = 3f;
                var distance=(targetLocation - transform.position).sqrMagnitude;
                Vector3 newPosition = Vector3.MoveTowards(rb.position, targetLocation, speed * Time.fixedDeltaTime);
                rb.MovePosition(newPosition);
                smoothedRotation = Quaternion.Lerp(rb.rotation, targetRotation, 5 * Time.fixedDeltaTime);
                rb.MoveRotation(smoothedRotation);
                animator.SetBool("isWalk", true);

              
//                Debug.Log(distance);
                if (distance<.001f)
                {
                    movementIsDone = true; // Hareketi durdur
                    animator.SetBool("isWalk", false);
                    animator.SetBool("isAttack",false);
                }
            }
            else if (soldierBehaviour == SoldierBehaviourState.Charging)
            {
                if (targetEnemySoldier != null)
                {
                    targetLocation = targetEnemySoldier.position;
                    var distance=(targetLocation - transform.position).sqrMagnitude;

                    direction.y = 0;
                    speed = 3f;
              

                //animator.SetBool("isAttack", false);

            /*    if (direction == Vector3.zero)
                {
                    targetRotation = Quaternion.Euler(Vector3.one * Mathf.Epsilon);
                }
                else
                {
                    targetRotation = Quaternion.LookRotation(direction);
                }*/
            Vector3 newPosition = Vector3.MoveTowards(rb.position, targetLocation, speed * Time.fixedDeltaTime);
            rb.MovePosition(newPosition);
                smoothedRotation = Quaternion.Lerp(rb.rotation, targetRotation, 5 * Time.fixedDeltaTime);
                rb.MoveRotation(smoothedRotation);
                animator.SetBool("isWalk", true);

                if (distance<.01f)
                {
                    movementIsDone = true; // Hareketi durdur
                    animator.SetBool("isWalk", false);
                   // animator.SetBool("isAttack",true);
                }  }
            }
            else if (soldierBehaviour == SoldierBehaviourState.Attacking)
            {
                rb.velocity = Vector3.zero;
                animator.SetBool("isAttack", true);
                if (targetEnemySoldier != null)
                {
                    direction = (targetEnemySoldier.position - transform.position).normalized;
                    direction.y = 0;
                }

                if (direction == Vector3.zero)
                {
                    targetRotation = Quaternion.Euler(Vector3.one * Mathf.Epsilon);
                }
                else
                {
                    targetRotation = Quaternion.LookRotation(direction);
                }

                smoothedRotation = Quaternion.Lerp(rb.rotation, targetRotation, 5 * Time.fixedDeltaTime);
                rb.MoveRotation(smoothedRotation);

                return; // Attacking durumunda başka işlem yapmıyoruz
            }
        }
        
        public void GetTargetTransform(Transform target, float distanceValue,IDamagable damagableRef)
        {
            rangeBetweentarget = distanceValue;
            targetEnemySoldier = target;

            if (target != null)
            {
                if (distanceValue < soldierAttack.range)
                {
                    //mySoldierAttack.targetSoldier = target.GetComponent<IDamagable>();
                    soldierAttack.targetSoldier = damagableRef;

                   soldierAttack.CheckRange(distanceValue);
                   soldierBehaviour = SoldierBehaviourState.Attacking;
                }
                else
                {
                    soldierBehaviour = SoldierBehaviourState.Charging;
                    movementIsDone = false; 
                }
            }
            else
            {
               // animator.SetBool("isAttack", false);
               soldierBehaviour = SoldierBehaviourState.Waiting;
            }
        }
        
        
    }
    
    
}