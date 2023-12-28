using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//敌人移动点位
public class WayPoints : MonoBehaviour
{

    public static Transform[] positions;

    void Awake()
    {
        //获取子对象位置列表
        positions = new Transform[transform.childCount];
        for (int i = 0; i < positions.Length; i++)
        {
            //根据子对象坐标不断变换位置
            positions[i] = transform.GetChild(i);
        }
    }
}
