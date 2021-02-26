using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpRuleManager : MonoBehaviour
{

    #region Singleton
    public static SpRuleManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    public static int turnCount = 0;
    public static int matchOver = 0;
    static int animalCnt = 7;
    public static int[] Animals = new int[animalCnt];

    public GameObject gameOverPanel;

    public static int SpRuleNum;//0이 Default, 1 제일 윗줄, 2 가장 우측

    // Use this for initialization
    void Start()
    {
        turnCount = 0;
        matchOver = 0;
        gameOverPanel.SetActive(false);

        for (int i = 0; i < animalCnt; i++)
        {
            Animals[i] = 0;
        }

        SpRuleNum = PlayerPrefs.GetInt("SpRuleNum");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            SpRuleNum = 1;
            Debug.Log(SpRuleNum);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            SpRuleNum = 2;
            Debug.Log(SpRuleNum);
        }


        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log(turnCount);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log(matchOver);
        }
        if(matchOver == 24)
        {
            gameOverPanel.SetActive(true);
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene("SpRulePlayScene");
    }

    public void Exit()
    {
        Application.Quit();
    }
}