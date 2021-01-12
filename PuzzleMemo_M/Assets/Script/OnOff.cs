using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnOff : MonoBehaviour
{
    public GameObject MenuSet;

    public AudioSource audioSource;

    public Text musictext;

    // Start is called before the first frame update
    void Start()
    {
        musictext.text = "Music ON";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //메뉴 온오프
    public void OnClickMenu()
    {
        SoundScript.Inst.ClickMenu();
        if (MenuSet.activeSelf)
        {
            MenuSet.SetActive(false);
        }
        else
        {
            MenuSet.SetActive(true);
        }
    }

    //노래 온오프
    public void OnClickMusic()
    {
        SoundScript.Inst.ClickMenu();
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
            musictext.text = "Music OFF";
        }

        else
        {
            audioSource.Play();
            musictext.text = "Music ON";
        }
    }

    //나가기
    public void OnClickExit()
    {
        SoundScript.Inst.ClickMenu();
        Application.Quit();
    }
}
