using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Beginning()
    {
        PlayerPrefs.SetInt("PopIndex", 0);
        SceneManager.LoadScene("Tutorial");
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

    //주경이가 정한 씬네임 따라적기
    public void Solo()
    {
        SceneManager.LoadScene("Solo");
    }
}
