using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    //여러개의 팝업을 집어넣을수 있게만듬
    public GameObject[] popUps;
    //현재 팝업 번호
    private int popUpIndex;

    // Start is called before the first frame update
    void Start()
    {
        //0에서 시작
        popUpIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //해당 단계의 설명이 나오게하기
        for (int i = 0; i < popUps.Length; i++)
        {
            
            if (i == popUpIndex)
            {
                popUps[i].SetActive(true);
            }
            else
            {
                popUps[i].SetActive(false);
            }
        }

        if (popUpIndex == 0)//첫번째 설명
        {
            //설명 텍스트
            GameObject.Find("Panel_Explanation").transform.Find("Text").GetComponent<TutorialText>().m_text = "첫번째 설명/ 클릭하면 넘어감";

            //특정 트리거로 다음 설명으로
            if (Input.GetButtonDown("Fire1"))
            {
                //설명변경 후 다음 설명으로
                GameObject.Find("Panel_Explanation").transform.Find("Text").GetComponent<TutorialText>().m_text = "두번째 설명/ 아래쪽 두개 맞추면 넘어감";
                GameObject.Find("Panel_Explanation").transform.Find("Text").GetComponent<TutorialText>().TextStart();

                popUpIndex++;
            }
        }
        else if (popUpIndex == 1)//두번째 설명
        {
            if (GameObject.FindWithTag("card11").GetComponent<CardControl>().ishitted == true
                && GameObject.FindWithTag("card12").GetComponent<CardControl>().ishitted == true)
            {
                //설명 텍스트
                GameObject.Find("Panel_Explanation").transform.Find("Text").GetComponent<TutorialText>().m_text = "세번째 설명/ 위쪽 두개 맞추면 넘어감";
                GameObject.Find("Panel_Explanation").transform.Find("Text").GetComponent<TutorialText>().TextStart();

                popUpIndex++;
            }
        }
        else if (popUpIndex == 2)//세번째 설명
        {
            if (GameObject.FindWithTag("card5").GetComponent<CardControl>().ishitted == true
                && GameObject.FindWithTag("card6").GetComponent<CardControl>().ishitted == true)
            {
                //설명 텍스트
                GameObject.Find("Panel_Explanation").transform.Find("Text").GetComponent<TutorialText>().m_text = "네번째 설명/ 현재 끝";
                GameObject.Find("Panel_Explanation").transform.Find("Text").GetComponent<TutorialText>().TextStart();

                popUpIndex++;
            }
        }
        else if (popUpIndex == 3)//네번째 설명
        {

        }
    }

    public int GetPopUpIndex()
    {
        return popUpIndex;
    }
}
