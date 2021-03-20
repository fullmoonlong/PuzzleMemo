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

        SpecialRule();
        Shuffle();
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

    void Shuffle()
    {
        int cardCnt = SpRuleCardControl.cardCnt;//일반 카드 수
        int S_cardCnt = 0; // 특수 카드 수
        
        //카드 위치 정하기
        /*for (int i = 1; i <= cardCnt + S_cardCnt; i++)
        {
            GameObject card = GameObject.FindWithTag("card" + i);

            //시작위치부터 순서대로 생성
            card.transform.position = new Vector3(cardX + (((i - 1) % 6) * cardGapX),
                cardY - (((i - 1) / 6) * (cardGapY)), card.transform.position.z);
        }*/

        //카드 위치 섞기
        for (int i = 1; i <= cardCnt + S_cardCnt; i++)
        {
            Debug.Log(i);
            GameObject card = GameObject.FindWithTag("card" + i);
            GameObject other_card = GameObject.FindWithTag("card" + UnityEngine.Random.Range(1, cardCnt + S_cardCnt + 1));

            //자기 위치 저장
            Vector3 cardPos = card.transform.position;

            //서로 위치 바꾸기
            card.transform.position = other_card.transform.position;
            other_card.transform.position = cardPos;

            //카드위치 정보 넘기기
            card.GetComponent<SpRuleCardControl>().cardPos = card.transform.position;
            other_card.GetComponent<SpRuleCardControl>().cardPos = other_card.transform.position;
        }
    }

    void SpecialRule()
    {
        if (SpRuleManager.SpRuleNum == 1)
        {
            SpRuleManager.Animals[0] += 3;
            SpRuleManager.Animals[1] += 1;
            SpRuleManager.Animals[2] += 2;

            for (int i = 1; i <= 24; i++)
            {
                GameObject.FindWithTag("card" + i).transform.GetComponent<SpRuleCardControl>().ishitted = false;
                GameObject.FindWithTag("card" + i).transform.GetComponent<SpRuleCardControl>().isOpen = false;
                GameObject.FindWithTag("card" + i).transform.GetComponent<SpRuleCardControl>().CloseCard();
            }

            for (int i = 1; i <= 6; i++)
            {
                GameObject.FindWithTag("card" + i).transform.GetComponent<SpRuleCardControl>().ishitted = true;
                GameObject.FindWithTag("card" + i).transform.GetComponent<SpRuleCardControl>().isOpen = true;
                GameObject.FindWithTag("card" + i).transform.GetComponent<SpRuleCardControl>().anim.Play("SingleAniOpen");
            }
        }
        else if (SpRuleManager.SpRuleNum == 2)
        {
            SpRuleManager.Animals[2] += 2;
            SpRuleManager.Animals[5] = 0; // 완성이므로 제외

            for (int i = 1; i <= 24; i++)
            {
                GameObject.FindWithTag("card" + i).transform.GetComponent<SpRuleCardControl>().ishitted = false;
                GameObject.FindWithTag("card" + i).transform.GetComponent<SpRuleCardControl>().isOpen = false;
                GameObject.FindWithTag("card" + i).transform.GetComponent<SpRuleCardControl>().CloseCard();
            }

            for (int i = 6; i <= 24;)
            {
                GameObject.FindWithTag("card" + i).transform.GetComponent<SpRuleCardControl>().ishitted = true;
                GameObject.FindWithTag("card" + i).transform.GetComponent<SpRuleCardControl>().isOpen = true;
                GameObject.FindWithTag("card" + i).transform.GetComponent<SpRuleCardControl>().anim.Play("SingleAniOpen");

                i += 6;
            }
        }
    }
}