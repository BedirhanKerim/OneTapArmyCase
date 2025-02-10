using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using static OneTapArmyCore.Enums;

namespace OneTapArmyCore
{
    public class SoldierMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody rb;
        public SoldierBehaviourState soldierBehaviour;
        public SoldierAttack soldierAttack;
        public Vector3 targetLocation;
        public bool movementIsDone;
        private bool canMove = true;

        public TeamType teamType;
        private float rangeBetweentarget;
        private float _baseSpeed = 3;
        private float speed = 3;
        [SerializeField] private Animator animator;
        [SerializeField] private Transform targetEnemySoldier;
        [SerializeField] private Animator horseAnimator;
        [SerializeField] private bool haveHorse = false;

        private void OnEnable()
        {
            canMove = true;
        }

        void FixedUpdate()
        {
            Move();
        }

        public void SetSpeed(float extraBuffSpeed)
        {
            speed = (extraBuffSpeed / 100 * _baseSpeed) + _baseSpeed;
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
            if (!canMove)
            {
                return;
            }

            Vector3 moveDirection = Vector3.zero;
            Quaternion targetRotation;

            if (movementIsDone)
            {
                targetRotation = (teamType == TeamType.Player)
                    ? Quaternion.LookRotation(Vector3.forward)
                    : Quaternion.LookRotation(Vector3.back);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 2 * Time.deltaTime);
                return;
            }

            moveDirection = (targetLocation - transform.position).normalized;
            if (moveDirection == Vector3.zero)
            {
                moveDirection = Vector3.forward; // Varsayılan yön
            }
            targetRotation = Quaternion.LookRotation(moveDirection);

            if (soldierBehaviour == SoldierBehaviourState.Walking || soldierBehaviour == SoldierBehaviourState.Waiting)
            {
                HandleMovement(moveDirection, targetRotation);
            }
            else if (soldierBehaviour == SoldierBehaviourState.WaitingOnBase)
            {
                HandleMovement(moveDirection, targetRotation);
            }
            else if (soldierBehaviour == SoldierBehaviourState.Charging)
            {
                if (targetEnemySoldier != null)
                {
                    targetLocation = targetEnemySoldier.position;
                    HandleMovement(moveDirection, targetRotation);
                }
            }
            else if (soldierBehaviour == SoldierBehaviourState.Attacking)
            {
                HandleAttack(moveDirection);
            }
        }

        private void HandleMovement(Vector3 moveDirection, Quaternion targetRotation)
        {
            moveDirection.y = 0;
            float distance = (targetLocation - transform.position).sqrMagnitude;
            Vector3 newPosition = Vector3.MoveTowards(rb.position, targetLocation, speed * Time.fixedDeltaTime);
            rb.MovePosition(newPosition);
            rb.MoveRotation(Quaternion.Lerp(rb.rotation, targetRotation, 5 * Time.fixedDeltaTime));

            animator.SetBool("isWalk", true);
            if (haveHorse) horseAnimator.SetBool("isWalk", true);

            if (distance < 0.001f || ((targetLocation.x < 4 || targetLocation.x > 13) && distance < 2f))
            {
                movementIsDone = true;
                animator.SetBool("isWalk", false);
                animator.SetBool("isAttack", false);
                if (haveHorse) horseAnimator.SetBool("isWalk", false);
            }
        }

        private void HandleAttack(Vector3 moveDirection)
        {
            rb.velocity = Vector3.zero;
            animator.SetBool("isAttack", true);
            if (haveHorse) horseAnimator.SetBool("isAttack", true);

            if (targetEnemySoldier != null)
            {
                moveDirection = (targetEnemySoldier.position - transform.position).normalized;
                moveDirection.y = 0;
            }

            Quaternion attackRotation = (moveDirection == Vector3.zero)
                ? Quaternion.Euler(Vector3.one * Mathf.Epsilon)
                : Quaternion.LookRotation(moveDirection);
            rb.MoveRotation(Quaternion.Lerp(rb.rotation, attackRotation, 5 * Time.fixedDeltaTime));
        }


        public void GetTargetTransform(Transform target, float distanceValue, IDamagable damagableRef)
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

        public void Die()
        {
            canMove = false;
            animator.Play("Die");
            if (haveHorse)
            {
                horseAnimator.Play("Die");
            }
        }
    }
}