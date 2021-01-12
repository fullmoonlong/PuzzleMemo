using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour
{
    public static SoundScript Inst;

    AudioSource Myaudio;

    public AudioClip Menusound;
    //버튼 클릭 소리 보류
    //public AudioClip Clicksound;

    // Start is called before the first frame update
    void Start()
    {
        if (Inst == null)
        {
            Inst = this;
        }

        Myaudio = gameObject.AddComponent<AudioSource>();
    }

    public void ClickMenu()
    {
        Myaudio.PlayOneShot(Menusound);
    }

    public void ClickBtn()
    {
        //Myaudio.PlayOneShot(Clicksound);
    }
}
