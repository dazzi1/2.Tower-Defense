using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//子弹类
public class Turret : MonoBehaviour
{

    private List<GameObject> enemys = new List<GameObject>();
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
        {
            //检测到敌人进入范围，将敌人加入列表
            enemys.Add(col.gameObject);
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Enemy")
        {
            //检测到敌人离开范围，将敌人删除列表
            enemys.Remove(col.gameObject);
        }
    }
    //多少秒攻击一次
    public float attackRateTime = 1;
    //计时器 每次到达attackRateTime就开始攻击
    private float timer = 0;
    //子弹
    public GameObject bulletPrefab;
    //攻击方向
    public Transform FirePosition;
    //炮塔头部
    public Transform Head;
    //激光
    public LineRenderer laserRenderer;
    //激光特效
    public GameObject laserEffect;
    //是否使用激光
    public bool useLaser = false;
    public float damageRate = 70;
    void Start()
    {
        timer = attackRateTime;
    }

    void Update()
    {
        //头部转向敌人
        if (enemys.Count > 0 && enemys[0] != null)
        {
            Vector3 targetPosition = enemys[0].transform.position;
            targetPosition.y = Head.position.y;
            Head.LookAt(targetPosition);
        }
        //不使用激光
        if (useLaser == false)
        {
            timer += Time.deltaTime;
            if (enemys.Count > 0 && timer >= attackRateTime)
            {
                timer = 0;
                Attack();
            }
        }
        //使用激光
        else if (enemys.Count > 0)
        {
            if (laserRenderer.enabled == false)
                laserRenderer.enabled = true;

            laserEffect.SetActive(true);

            if (enemys[0] == null)
            {
                UpdateEnemys();
            }

            if (enemys.Count > 0)
            {
                //发射激光
                laserRenderer.SetPositions(new Vector3[] { FirePosition.position, enemys[0].transform.position });
                //造成伤害
                enemys[0].GetComponent<Enemy>().TakeDamage(damageRate * Time.deltaTime);
                //播放特效
                laserEffect.transform.position = enemys[0].transform.position;
                //控制特效朝向
                Vector3 pos = transform.position;
                pos.y = enemys[0].transform.position.y;
                laserEffect.transform.LookAt(pos);

            }
            //范围内击杀
            if (enemys.Count == 0)
            {
                laserEffect.SetActive(false);
                laserRenderer.enabled = false;
            }
        }
        else
        {
            //超出范围关闭特效
            laserEffect.SetActive(false);
            //超出范围关闭激光
            laserRenderer.enabled = false;
        }
    }
    void Attack()
    {
        if (enemys[0] == null)
        {
            UpdateEnemys();
        }
        if (enemys.Count > 0)
        {
            //生成子弹
            GameObject bullet = GameObject.Instantiate(bulletPrefab, FirePosition.position, FirePosition.rotation);
            //发射子弹
            bullet.GetComponent<Bullet>().SetTarget(enemys[0].transform);
        }
        else
        {
            timer = attackRateTime;
        }

    }

    //更新敌人集合
    void UpdateEnemys()
    {
        List<GameObject> newEnemys = new List<GameObject>();
        for (int index = 0; index < enemys.Count; index++)
        {
            if (enemys[index] != null)
            {
                newEnemys.Add(enemys[index]);
            }
        }
        enemys = newEnemys;
    }
}
