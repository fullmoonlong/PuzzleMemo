using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        BasicPlay,
        SpPlay,
        SinglePlay,
        BasicPuzzle,
        BasicJungle,
        Desert,
        Antarctica,
        Easy,
        Normal,
        Hard,
        SpPattern
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

        Btn[(int)Button.BasicPlay].SetActive(true);
        Btn[(int)Button.SpPlay].SetActive(true);
        Btn[(int)Button.SinglePlay].SetActive(true);
    }

    public void BasicPlay()
    {
        Btn[(int)Button.BasicPlay].SetActive(false);
        Btn[(int)Button.SpPlay].SetActive(false);
        Btn[(int)Button.SinglePlay].SetActive(false);

        Btn[(int)Button.BasicPuzzle].SetActive(true);
        Btn[(int)Button.BasicJungle].SetActive(true);
        Btn[(int)Button.Desert].SetActive(true);
        Btn[(int)Button.Antarctica].SetActive(true);
    }

    public void SpPlay()
    {
        Btn[(int)Button.BasicPlay].SetActive(false);
        Btn[(int)Button.SpPlay].SetActive(false);
        Btn[(int)Button.SinglePlay].SetActive(false);

        Btn[(int)Button.SpPattern].SetActive(true);
    }

    public void Stage()
    {
        Btn[(int)Button.BasicPuzzle].SetActive(false);
        Btn[(int)Button.BasicJungle].SetActive(false);
        Btn[(int)Button.Desert].SetActive(false);
        Btn[(int)Button.Antarctica].SetActive(false);

        Btn[(int)Button.Easy].SetActive(true);
        Btn[(int)Button.Normal].SetActive(true);
        Btn[(int)Button.Hard].SetActive(true);
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
