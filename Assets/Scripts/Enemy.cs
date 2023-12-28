using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//敌人类
public class Enemy : MonoBehaviour
{
    //点位
    private Transform[] positions;
    //初始HP
    public float hp = 150;
    //当前HP
    private float totalHp;
    //击杀特效
    public GameObject explosionEffect;
    //血条
    private Slider hpSlider;

    private int index = 0;
    public int speed = 10;

    void Start()
    {
        //初始化敌人
        positions = WayPoints.positions;
        totalHp = hp;
        hpSlider = GetComponentInChildren<Slider>();
    }

    void Update()
    {
        Move();
    }
    void Move()
    {
        //防止数组越界
        if (index > positions.Length - 1) return;
        //逐步移动到指定位置
        transform.Translate((positions[index].position - transform.position).normalized * Time.deltaTime * speed);
        //去下一个位置
        if (Vector3.Distance(positions[index].position, transform.position) < 0.2f)
        {
            index++;
        }
        //到达目的地销毁
        if (index > positions.Length - 1)
        {
            ReachDestination();
        }
    }
    //到达终点
    void ReachDestination()
    {
        //提示失败
        GameManager.Instance.Failed();
        GameObject.Destroy(this.gameObject);
    }

    void OnDestroy()
    {
        EnemySpawner.CountEnemyAlive--;
    }

    //敌人扣血
    public void TakeDamage(float damage)
    {
        if (hp <= 0)
        {
            return;
        }
        hp -= damage;
        //血条减少
        hpSlider.value = (float)hp / totalHp;
        if (hp <= 0)
        {
            Die();
        }
    }
    //敌人死亡
    void Die()
    {
        GameObject effect = GameObject.Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(effect, 1.5f);
        Destroy(this.gameObject);
    }
}
