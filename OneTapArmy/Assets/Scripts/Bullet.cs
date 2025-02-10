using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    private IDamagable targetDamagable;
    private float damage;
    protected float Distance;
    [SerializeField] private float speed,curve;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target != null&&targetDamagable.IsAlive())
        {
            Move();
            
        
            if (Vector3.Distance(new Vector3(transform.position.x,0,transform.position.z), new Vector3(target.position.x,0,target.position.z)) <= .2f)
            {
                HitEnemy();                return;
            }
        }
        else
        {
            //gameObject.SetActive(false);
            // PoolingManager.Instance.BulletDestroy(gameObject,PoolingManager.EBulletType.PistolBullet);
            //Destroy(gameObject);
            //var g0 = gameObject;
            //PoolingManager.Instance.Despawn(ref g0);
            Destroy(gameObject);
        }
    }

    private void Move()
    {
        Distance = Vector3.Distance(
            new Vector3(transform.position.x, 0, transform.position.z),
            new Vector3(target.position.x, 0, target.position.z));
        transform.position =
            Vector3.MoveTowards(
                new Vector3(transform.position.x, transform.position.y, transform.position.z),
                new Vector3(target.position.x, Distance * curve, target.position.z),
                Time.deltaTime * speed
            );


        Vector3 direction = (target.position - transform.position).normalized;
           
        Quaternion lookRotation;
        if (direction == Vector3.zero)
        {
            lookRotation = Quaternion.Euler(Vector3.one * Mathf.Epsilon);
        }
        else
        {
            lookRotation = Quaternion.LookRotation(direction);

        }
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 2f);
    }
    void HitEnemy()
    {

        targetDamagable.TakeDamage(damage);

       // var gO = this.gameObject;
       // PoolingManager.Instance.Despawn(ref gO);
       var gObj = this.gameObject;
        LeanPool.Despawn(gObj);
        gObj.SetActive(false);

       //Destroy(gameObject);
    }
    public void SetBullet(IDamagable targetSoldier, float _damage)
    {
        if (targetSoldier == null) return;
        {

            target = targetSoldier.GetTransform();
            targetDamagable = targetSoldier;
            //  targetLoc=targetEnemy.GetComponent<EnemyMovement>().locationType;
            damage = _damage;
            gameObject.SetActive(true);
            transform.LookAt(target);
        }
    }
}
