using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DoubleModeManager : MonoBehaviour
{
    #region Singleton
    public static DoubleModeManager instance;

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

    public Text turnText;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log(turnCount);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log(matchOver);
        }
        if (matchOver == 24)
        {
            gameOverPanel.SetActive(true);
        }

        if (DoubleModeCardControl.isMyTurn == true)
        {
            turnText.text = "Player A";
        }
        else if (DoubleModeCardControl.isMyTurn == false)
        {
            turnText.text = "Player B";
        }

    }

    public void Retry()
    {
        SceneManager.LoadScene("DoubleModeScene");
    }

    public void Exit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}