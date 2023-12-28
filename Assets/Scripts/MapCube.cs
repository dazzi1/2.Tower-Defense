using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
//建立炮台类
public class MapCube : MonoBehaviour
{
    [HideInInspector]
    //保存当前Cube上的炮台
    public GameObject turretGo;
    [HideInInspector]
    //炮台数据
    public TurretData turretData;
    [HideInInspector]
    //是否升级
    public bool isUpgraded = false;
    //动画效果
    public GameObject buildEffect;
    //激光
    private Renderer renderer;
    void Start()
    {
        renderer = GetComponent<Renderer>();
    }
    public void BuildTurret(TurretData turretData)
    {
        this.turretData = turretData;
        isUpgraded = false;
        //实例化炮台
        turretGo = GameObject.Instantiate(turretData.turretPrefab, transform.position, Quaternion.identity);
        //实例化特效
        GameObject effect = GameObject.Instantiate(buildEffect, transform.position, Quaternion.identity);
        //摧毁特效
        Destroy(effect, 1.5f);
    }
    //升级炮台
    public void UpgradeTurret()
    {
        if (isUpgraded == true) return;
        Destroy(turretGo);
        isUpgraded = true;
        //实例化炮台
        turretGo = GameObject.Instantiate(turretData.turretUpgradedPrefab, transform.position, Quaternion.identity);
        //实例化特效
        GameObject effect = GameObject.Instantiate(buildEffect, transform.position, Quaternion.identity);
        //摧毁特效
        Destroy(effect, 1.5f);
    }
    //摧毁炮台
    public void DestroyTurret()
    {
        Destroy(turretGo);
        isUpgraded = false;
        turretGo = null;
        turretData = null;
        //实例化特效
        GameObject effect = GameObject.Instantiate(buildEffect, transform.position, Quaternion.identity);
        //摧毁特效
        Destroy(effect, 1.5f);

    }
    //鼠标移动后变色
    void OnMouseEnter()
    {
        if (turretGo == null && EventSystem.current.IsPointerOverGameObject() == false && this.tag != "Way")
        {
            renderer.material.color = Color.blue;
        }
    }
    void OnMouseExit()
    {
        if (turretGo == null && EventSystem.current.IsPointerOverGameObject() == false && this.tag != "Way")
        {
            renderer.material.color = Color.white;
        }
    }
}
