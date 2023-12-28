using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//键盘控制摄像机位置类
public class MoveCamera : MonoBehaviour
{

    private float speed = 25;
    private float mouseSpeed = 60;

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float mouse = Input.GetAxis("Mouse ScrollWheel");
        //更改摄像机位置
        transform.Translate(new Vector3(h * speed, mouse * mouseSpeed * speed, v * speed) * Time.deltaTime, Space.World);

    }
}
