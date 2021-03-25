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

    public static int DoubleModeSpNum;//0이 Default, 1 제일 윗줄, 2 가장 우측

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

        DoubleModeSpNum = PlayerPrefs.GetInt("DoubleModeSpNum");

        DoubleSpRule();
        Shuffle();
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

    void Shuffle()
    {
        int cardCnt = DoubleModeCardControl.cardCnt;//일반 카드 수
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
            card.GetComponent<DoubleModeCardControl>().cardPos = card.transform.position;
            other_card.GetComponent<DoubleModeCardControl>().cardPos = other_card.transform.position;
        }
    }

    void DoubleSpRule()
    {
        if (DoubleModeManager.DoubleModeSpNum == 1)
        {
            DoubleModeManager.Animals[0] += 3;
            DoubleModeManager.Animals[1] += 1;
            DoubleModeManager.Animals[2] += 2;

            for (int i = 1; i <= 24; i++)
            {
                GameObject.FindWithTag("card" + i).transform.GetComponent<DoubleModeCardControl>().ishitted = false;
                GameObject.FindWithTag("card" + i).transform.GetComponent<DoubleModeCardControl>().isOpen = false;
                GameObject.FindWithTag("card" + i).transform.GetComponent<DoubleModeCardControl>().CloseCard();
            }

            for (int i = 1; i <= 6; i++)
            {
                GameObject.FindWithTag("card" + i).transform.GetComponent<DoubleModeCardControl>().ishitted = true;
                GameObject.FindWithTag("card" + i).transform.GetComponent<DoubleModeCardControl>().isOpen = true;
                GameObject.FindWithTag("card" + i).transform.GetComponent<DoubleModeCardControl>().anim.Play("SingleAniOpen");
            }
        }
        else if (DoubleModeManager.DoubleModeSpNum == 2)
        {
            DoubleModeManager.Animals[2] += 2;
            DoubleModeManager.Animals[5] = 0; // 투칸 완성이므로 제외

            for (int i = 1; i <= 24; i++)
            {
                GameObject.FindWithTag("card" + i).transform.GetComponent<DoubleModeCardControl>().ishitted = false;
                GameObject.FindWithTag("card" + i).transform.GetComponent<DoubleModeCardControl>().isOpen = false;
                GameObject.FindWithTag("card" + i).transform.GetComponent<DoubleModeCardControl>().CloseCard();
            }

            for (int i = 6; i <= 24;)
            {
                GameObject.FindWithTag("card" + i).transform.GetComponent<DoubleModeCardControl>().ishitted = true;
                GameObject.FindWithTag("card" + i).transform.GetComponent<DoubleModeCardControl>().isOpen = true;
                GameObject.FindWithTag("card" + i).transform.GetComponent<DoubleModeCardControl>().anim.Play("SingleAniOpen");

                i += 6;
            }
        }
        else if (DoubleModeManager.DoubleModeSpNum == 3)
        {
            DoubleModeManager.Animals[0] += 3;
            DoubleModeManager.Animals[1] += 1;
            DoubleModeManager.Animals[2] += 3;
            DoubleModeManager.Animals[5] = 0; // 투칸 완성이므로 제외

            for (int i = 1; i <= 24; i++)
            {
                GameObject.FindWithTag("card" + i).transform.GetComponent<DoubleModeCardControl>().ishitted = false;
                GameObject.FindWithTag("card" + i).transform.GetComponent<DoubleModeCardControl>().isOpen = false;
                GameObject.FindWithTag("card" + i).transform.GetComponent<DoubleModeCardControl>().CloseCard();
            }

            for (int i = 1; i <= 5; i++)//제일 윗줄 - 6은 제일 우측줄과 겹쳐서 제외함
            {
                GameObject.FindWithTag("card" + i).transform.GetComponent<DoubleModeCardControl>().ishitted = true;
                GameObject.FindWithTag("card" + i).transform.GetComponent<DoubleModeCardControl>().isOpen = true;
                GameObject.FindWithTag("card" + i).transform.GetComponent<DoubleModeCardControl>().anim.Play("SingleAniOpen");
            }
            for (int i = 6; i <= 24;)//제일 우측줄
            {
                GameObject.FindWithTag("card" + i).transform.GetComponent<DoubleModeCardControl>().ishitted = true;
                GameObject.FindWithTag("card" + i).transform.GetComponent<DoubleModeCardControl>().isOpen = true;
                GameObject.FindWithTag("card" + i).transform.GetComponent<DoubleModeCardControl>().anim.Play("SingleAniOpen");

                i += 6;
            }
        }
        else if (DoubleModeManager.DoubleModeSpNum == 4)
        {
            DoubleModeManager.Animals[0] += 3;
            DoubleModeManager.Animals[2] += 2;
            DoubleModeManager.Animals[4] += 3;
            DoubleModeManager.Animals[5] += 1;
            DoubleModeManager.Animals[6] = 0;//거북이 완성

            for (int i = 1; i <= 24; i++)
            {
                GameObject.FindWithTag("card" + i).transform.GetComponent<DoubleModeCardControl>().ishitted = false;
                GameObject.FindWithTag("card" + i).transform.GetComponent<DoubleModeCardControl>().isOpen = false;
                GameObject.FindWithTag("card" + i).transform.GetComponent<DoubleModeCardControl>().CloseCard();
            }

            for (int i = 20; i <= 5; i++)//제일 윗줄 - 19는 제일 좌측줄과 겹쳐서 제외함
            {
                GameObject.FindWithTag("card" + i).transform.GetComponent<DoubleModeCardControl>().ishitted = true;
                GameObject.FindWithTag("card" + i).transform.GetComponent<DoubleModeCardControl>().isOpen = true;
                GameObject.FindWithTag("card" + i).transform.GetComponent<DoubleModeCardControl>().anim.Play("SingleAniOpen");
            }
            for (int i = 1; i <= 24;)
            {
                GameObject.FindWithTag("card" + i).transform.GetComponent<DoubleModeCardControl>().ishitted = true;
                GameObject.FindWithTag("card" + i).transform.GetComponent<DoubleModeCardControl>().isOpen = true;
                GameObject.FindWithTag("card" + i).transform.GetComponent<DoubleModeCardControl>().anim.Play("SingleAniOpen");

                i += 6;
            }
        }
    }
}