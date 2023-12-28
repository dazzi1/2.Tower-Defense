using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//子弹攻击AI类
public class Bullet : MonoBehaviour
{

    //子弹伤害
    public int damage = 50;
    //子弹速度
    public float speed = 20;
    //子弹特效
    public GameObject explosionEffectPrefab;

    //private float distanceArriveTarget = 1.3f;

    private Transform target;

    public void SetTarget(Transform _target)
    {
        this.target = _target;
    }

    void Update()
    {
        //目标不存在 销毁自身
        if (target == null) { Die(); return; }
        //面向目标位置
        transform.LookAt(target.position);
        //向前移动
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        //Vector3 dir = target.position - transform.position;
        //if (dir.magnitude < distanceArriveTarget) {
        //	//扣血
        //	target.GetComponent<Enemy>().TakeDamage(damage);
        //	//爆炸效果
        //	GameObject.Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
        //	Destroy(this.gameObject);
        //}
    }
    //触发检测碰撞
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
        {
            //扣血扣完
            col.GetComponent<Enemy>().TakeDamage(damage);
            Die();
        }
    }

    void Die()
    {
        //爆炸效果
        GameObject effect = GameObject.Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
        //销毁
        Destroy(effect, 1);
        Destroy(this.gameObject);
    }
}
