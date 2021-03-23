using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnManagerment : MonoBehaviour
{
    public GameObject[] Btn;

    public GameObject Ready;

    enum Button
    {
        Tutorial,
        Play,
        Collection,
        Beginning,
        GameRule,
        NormalCard,
        SpeicalCard,
        AIPlay,
        SinglePlay,
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Tutorial()
    {
        Btn[(int)Button.Tutorial].SetActive(false);
        Btn[(int)Button.Play].SetActive(false);
        Btn[(int)Button.Collection].SetActive(false);
        
        Btn[(int)Button.Beginning].SetActive(true);
        Btn[(int)Button.GameRule].SetActive(true);
        Btn[(int)Button.NormalCard].SetActive(true);
        Btn[(int)Button.SpeicalCard].SetActive(true);
    }

    public void Play()
    {
        Btn[(int)Button.Tutorial].SetActive(false);
        Btn[(int)Button.Play].SetActive(false);
        Btn[(int)Button.Collection].SetActive(false);

        Btn[(int)Button.AIPlay].SetActive(true);
        Btn[(int)Button.SinglePlay].SetActive(true);
    }

    public void ReadyPanel()
    {
        Ready.SetActive(true);
    }

    public void ReadyOut()
    {
        Ready.SetActive(false);
    }
}
