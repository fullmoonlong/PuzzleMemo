using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu" && Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }


    public void TestInToSpGame()
    {
        int Map = GetComponent<AIPlay>().CurMap;
        int Type = GetComponent<AIPlay>().CurType;

        //맵
        if (Map == 0)
        {
            PlayerPrefs.SetString("DoubleMap", "Jungle");
        }
        else if (Map == 1)
        {
            PlayerPrefs.SetString("DoubleMap", "Antarctica");
        }
        else if (Map == 2)
        {
            PlayerPrefs.SetString("DoubleMap", "Desert");
        }

        //타입
        PlayerPrefs.SetInt("SpRuleNum", Type + 1);

        //씬 이동
        SceneManager.LoadScene("SpRulePlayScene");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Beginning()
    {
        PlayerPrefs.SetInt("PopIndex", 0);
        SceneManager.LoadScene("Tutorial");
    }

    public void NormalCard()
    {
        SceneManager.LoadScene("Tutorial");
        PlayerPrefs.SetInt("PopIndex", 18);
    }

    public void SpeicalCard()
    {
        SceneManager.LoadScene("Tutorial");
        PlayerPrefs.SetInt("PopIndex", 27);
    }

    public void GameRule()
    {
        SceneManager.LoadScene("Tutorial");
        PlayerPrefs.SetInt("PopIndex", 16);
    }

    public void Solo()
    {
        SceneManager.LoadScene("SoloPlayScene");
    }

    public void Double_default()
    {
        PlayerPrefs.SetString("DoubleMap", "Jungle");
        SceneManager.LoadScene("DoubleModeScene");
    }

    public void Double_Antarctica()
    {
        PlayerPrefs.SetString("DoubleMap", "Antarctica");
        SceneManager.LoadScene("DoubleModeScene");
    }

    public void Double_Desert()
    {
        PlayerPrefs.SetString("DoubleMap", "Desert");
        SceneManager.LoadScene("DoubleModeScene");
    }

    public void Collection()
    {
        SceneManager.LoadScene("Collection");
    }

    public void AIPlay()
    {
        SceneManager.LoadScene("AIPlay");
    }

    public void SinglePlay()
    {
        SceneManager.LoadScene("SinglePlay");
    }
}
