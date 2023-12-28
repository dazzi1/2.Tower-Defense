using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//UI类
public class BuildManager : MonoBehaviour
{
    //三种炮台
    public TurretData laserTurretData;
    public TurretData missileTurretData;
    public TurretData standardTurretData;
    //地板
    private MapCube selectedMapCube;
    //金钱
    public Text moneyText;
    //金钱动画
    public Animator moneyAnimator;
    //初始金额
    private int money = 500;
    //升级UI页面
    public GameObject upgradeCanvas;
    //升级动画
    private Animator upgradeCanvasAnimator;
    //升级按钮
    public Button buttonUpgrade;

    void Start()
    {
        upgradeCanvasAnimator = upgradeCanvas.GetComponent<Animator>();
    }

    //改变显示金钱
    void ChangeMoney(int change)
    {
        money += change;
        moneyText.text = money + "R";
    }

    //当前选择的炮台
    private TurretData selectedTurretData;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //判断是否点击在UI上
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {
                //开发炮台的建造
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                //判断是否射线是否与物体相交
                bool isCollider = Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("MapCube"));
                if (isCollider)
                {
                    //得到点击的MapCube
                    MapCube mapCube = hit.collider.GetComponent<MapCube>();
                    //当前点击地面为空 
                    if (selectedTurretData != null && mapCube.turretGo == null)
                    {
                        // 可以创建
                        if (money >= selectedTurretData.cost)
                        {
                            //创建炮台
                            mapCube.BuildTurret(selectedTurretData);
                            //改变金钱
                            ChangeMoney(-selectedTurretData.cost);
                        }
                        else
                        {
                            //播放金钱不够动画
                            moneyAnimator.SetTrigger("flicker");
                        }
                    }
                    else if (mapCube.turretGo != null)
                    {
                        //升级处理
                        if (mapCube == selectedMapCube && upgradeCanvas.activeInHierarchy)
                        {
                            //升级完成隐藏UI
                            StartCoroutine(HideUpgradeUI());
                        }
                        else
                        {
                            //显示升级UI
                            ShowUpgradeUI(mapCube.transform.position, mapCube.isUpgraded);
                        }
                        //保存升级位置
                        selectedMapCube = mapCube;
                    }
                }
            }
        }
    }

    //选择不同炮台
    public void OnLaserSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = laserTurretData;
        }
    }

    public void OnMissileSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = missileTurretData;
        }
    }

    public void OnStandardSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = standardTurretData;
        }
    }
    // 显示升级UI
    void ShowUpgradeUI(Vector3 pos, bool isDisableUpgrade = false)
    {
        StopCoroutine("HideUpgradeUI");
        upgradeCanvas.SetActive(false);
        upgradeCanvas.SetActive(true);
        pos.y += 5;
        upgradeCanvas.transform.position = pos;
        buttonUpgrade.interactable = !isDisableUpgrade;
    }
    //隐藏升级UI
    IEnumerator HideUpgradeUI()
    {
        upgradeCanvasAnimator.SetTrigger("Hide");

        yield return new WaitForSeconds(0.8f);
        upgradeCanvas.SetActive(false);
    }
    //升级
    public void OnUpgradeButtonDown()
    {
        if (money >= selectedMapCube.turretData.costUpgraded)
        {
            ChangeMoney(-selectedMapCube.turretData.costUpgraded);
            selectedMapCube.UpgradeTurret();
        }
        else
        {
            moneyAnimator.SetTrigger("flicker");
        }
        StartCoroutine(HideUpgradeUI());
    }
    //拆
    public void OnDestroyButtonDown()
    {
        selectedMapCube.DestroyTurret();
        StartCoroutine(HideUpgradeUI());
    }
}
