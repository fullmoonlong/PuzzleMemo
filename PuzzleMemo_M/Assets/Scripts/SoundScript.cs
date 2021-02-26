using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SoundScript : MonoBehaviour
{
    public static SoundScript Inst;

    AudioSource Myaudio;

    public AudioClip Menusound;
    //버튼 클릭 소리 보류
    //public AudioClip Clicksound;

    public Text AnimalName;

    public GameObject Dic;
    enum AnimalOrder
    {
        개구리,
        아나콘다,
        아프리카거북,
        알락꼬리원숭이,
        재규어,
        투칸,
        하마,
        원숭이,
        뱀,
        낙타,
        여우,
        펭귄,
        북극곰
    }

    public AudioClip[] AnimalSound;
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

    public void SoundPlay()
    {
        //근데 엑스창을 닫아도 사운드가 계속 들림
        switch (AnimalName.text)
        {
            case "개구리":
                Myaudio.PlayOneShot(AnimalSound[(int)AnimalOrder.개구리]);
                break;

            case "아나콘다":
                Myaudio.PlayOneShot(AnimalSound[(int)AnimalOrder.아나콘다]);
                break;

            case "아프리카거북":
                Myaudio.PlayOneShot(AnimalSound[(int)AnimalOrder.아프리카거북]);
                break;

            case "알락꼬리원숭이":
                Myaudio.PlayOneShot(AnimalSound[(int)AnimalOrder.알락꼬리원숭이]);
                break;

            case "재규어":
                Myaudio.PlayOneShot(AnimalSound[(int)AnimalOrder.재규어]);
                break;

            case "투칸":
                Myaudio.PlayOneShot(AnimalSound[(int)AnimalOrder.투칸]);
                break;

            case "하마":
                Myaudio.PlayOneShot(AnimalSound[(int)AnimalOrder.하마]);
                break;

            case "원숭이":
                Myaudio.PlayOneShot(AnimalSound[(int)AnimalOrder.원숭이]);
                break;

            case "뱀":
                Myaudio.PlayOneShot(AnimalSound[(int)AnimalOrder.뱀]);
                break;

            case "낙타":
                Myaudio.PlayOneShot(AnimalSound[(int)AnimalOrder.낙타]);
                break;

            case "여우":
                Myaudio.PlayOneShot(AnimalSound[(int)AnimalOrder.여우]);
                break;

            case "펭귄":
                Myaudio.PlayOneShot(AnimalSound[(int)AnimalOrder.펭귄]);
                break;

            case "북극곰":
                Myaudio.PlayOneShot(AnimalSound[(int)AnimalOrder.북극곰]);
                break;
        }

    }
}
