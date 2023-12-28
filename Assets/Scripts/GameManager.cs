using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    //结束界面UI
    public GameObject endUI;
    //结束界面弹窗
    public Text text;
    //提示页面弹窗
    public Text Inf;

    // 单例模式
    public static GameManager Instance;
    private EnemySpawner enemySpawner;
    public void Awake()
    {
        Instance = this;
        enemySpawner = GetComponent<EnemySpawner>();
    }
    // Use this for initialization
    public void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Win()
    {
        endUI.SetActive(true);
        text.text = "VECTORY";
    }
    public void Failed()
    {
        enemySpawner.Stop();
        endUI.SetActive(true);
        text.text = "GAME OVER";
    }

    public void OnButtonRetry()
    {
        //重新加载当前场景
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void OnButtonMenu()
    {
        //返回菜单
        SceneManager.LoadScene(0);
    }

    //任务简介
    public void CloseInf()
    {
        Inf.transform.Find("Button/Text").GetComponent<Text>().text = "";
        Inf.enabled = false;

    }
}
