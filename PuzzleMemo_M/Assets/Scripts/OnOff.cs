using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class OnOff : MonoBehaviour
{
    //추가창 넣을 것들
    public GameObject MenuSet;
    public GameObject MenuTuto;
    public GameObject MenuGame;

    public AudioSource audioSource;
    AudioSource Myaudio;

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
        //SoundScript.Inst.ClickMenu();
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
        //SoundScript.Inst.ClickMenu();
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
        //SoundScript.Inst.ClickMenu();
        Application.Quit();
    }

    //튜토 온오프
    public void OnClickTuto()
    {
        //SoundScript.Inst.ClickMenu();
        if (MenuTuto.activeSelf)
        {
            MenuTuto.SetActive(false);
        }
        else
        {
            MenuTuto.SetActive(true);
        }
    }

    //게임 온오프
    public void OnClickGame()
    {
        //SoundScript.Inst.ClickMenu();
        if (MenuGame.activeSelf)
        {
            MenuGame.SetActive(false);
            if(!MenuGame.activeSelf)
            {
                //동물울음소리끄기
                Debug.Log("꺼짐");
                GameObject.Find("SoundManager").GetComponent<AudioSource>().Stop();
            }
        }
        else
        {
            MenuGame.SetActive(true);
        }
    }
}
