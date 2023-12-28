using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //初始敌人个数
    public static int CountEnemyAlive = 0;
    //移动点位
    public Wave[] waves;
    //开始位置
    public Transform START;
    //移动速率
    public float waveRate = 0.2f;
    //线程
    private Coroutine coroutine;

    //敌人生成类
    public void Start()
    {
        //开启线程
        coroutine = StartCoroutine(SpawnEnemy());
    }

    //停止线程
    public void Stop()
    {
        StopCoroutine(coroutine);
    }

    //生成敌人
    IEnumerator SpawnEnemy()
    {
        foreach (Wave wave in waves)
        {
            for (int i = 0; i < wave.count; i++)
            {
                //生成
                GameObject.Instantiate(wave.enemyPrefab, START.position, Quaternion.identity);
                CountEnemyAlive++;
                //暂停
                if (i != wave.count - 1)
                {
                    yield return new WaitForSeconds(wave.rate);
                }
            }
            //上一波物体是否全部死亡
            while (CountEnemyAlive > 0)
            {
                yield return 0;
            }
            //开始下一波
            yield return new WaitForSeconds(waveRate);
        }
        //再也没有敌人 通关成功
        while (CountEnemyAlive > 0)
        {
            yield return 0;
        }
        GameManager.Instance.Win();
    }

}
