using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    float aiTurnTimer, passedTime;

    // Update is called once per frame
    void Update()
    {
        Debug.Log(SoloModeCardControl.instance.GetIsMyTurn());

        if (SoloModeCardControl.instance.GetIsMyTurn() == false)
        {
            passedTime += Time.deltaTime;
            aiTurnTimer = Random.Range(2, 4);

            if (passedTime >= aiTurnTimer)
            {
                if (SoloModeManager.matchOver < 24)
                {
                    AIClickCheck();
                    passedTime = 0f;
                }
                else
                {
                    Debug.Log("FINISHED");
                }
            }

            //카드 움직임
            SoloModeCardControl.instance.CardMove();
            SoloModeCardControl.instance.CardBoardMatch();
        }
    }

    void AIClickCheck()
    {
        //AI단계별 구현
        int randomCard = Random.Range(1, 28);

        if (GameObject.FindWithTag("card" + randomCard).GetComponent<SoloModeCardControl>().ishitted == true)
        {
            while (GameObject.FindWithTag("card" + randomCard).GetComponent<SoloModeCardControl>().ishitted == true)//<- false를 true로 바꿈
            {
                if (SoloModeManager.turnCount <= 5)
                {
                    Debug.Log("Phase1");
                    randomCard = Random.Range(1, 28);
                }
                else if (SoloModeManager.turnCount <= 10 && SoloModeManager.turnCount > 5)
                {
                    Debug.Log("Phase2");
                    randomCard = Random.Range(7, 28);
                    if (GameObject.FindWithTag("card" + randomCard).GetComponent<SoloModeCardControl>().ishitted == true)
                    {
                        while (GameObject.FindWithTag("card" + randomCard).GetComponent<SoloModeCardControl>().ishitted == true)
                        {
                            randomCard = Random.Range(1, 28);
                        }
                    }
                }
                else if (SoloModeManager.turnCount <= 20 && SoloModeManager.turnCount > 10)
                {
                    Debug.Log("Phase3");
                    randomCard = Random.Range(13, 28);
                    if (GameObject.FindWithTag("card" + randomCard).GetComponent<SoloModeCardControl>().ishitted == true)
                    {
                        while (GameObject.FindWithTag("card" + randomCard).GetComponent<SoloModeCardControl>().ishitted == true)
                        {
                            randomCard = Random.Range(7, 28);
                            if (GameObject.FindWithTag("card" + randomCard).GetComponent<SoloModeCardControl>().ishitted == true)
                            {
                                randomCard = Random.Range(1, 28);
                            }
                        }
                    }
                }
                else
                {
                    Debug.Log("Phase4");
                    randomCard = Random.Range(19, 28);
                    if (GameObject.FindWithTag("card" + randomCard).GetComponent<SoloModeCardControl>().ishitted == true)
                    {
                        while (GameObject.FindWithTag("card" + randomCard).GetComponent<SoloModeCardControl>().ishitted == true)
                        {
                            randomCard = Random.Range(13, 28);
                            if (GameObject.FindWithTag("card" + randomCard).GetComponent<SoloModeCardControl>().ishitted == true)
                            {
                                randomCard = Random.Range(1, 28);
                            }
                        }
                    }
                }
            }

            //넣어야 위에서 찾은 후에 바로 될꺼 같아서 넣어봄 - 규식
            if (GameObject.FindWithTag("card" + randomCard).GetComponent<SoloModeCardControl>().ishitted == false)
            {
                GameObject.FindWithTag("card" + randomCard).GetComponent<SoloModeCardControl>().OpenCard();
                SoloModeManager.turnCount++;
                SoloModeCardControl.instance.SetIsMyTurn(true);
            }
        }
        else
        {
            if (SoloModeManager.turnCount <= 5)
            {
                Debug.Log("Phase1");
                randomCard = Random.Range(1, 28);
            }
            else if (SoloModeManager.turnCount <= 10 && SoloModeManager.turnCount > 5)
            {
                Debug.Log("Phase2");
                randomCard = Random.Range(7, 28);
                if (GameObject.FindWithTag("card" + randomCard).GetComponent<SoloModeCardControl>().ishitted == true)
                {
                    while (GameObject.FindWithTag("card" + randomCard).GetComponent<SoloModeCardControl>().ishitted == true)
                    {
                        randomCard = Random.Range(1, 28);
                    }
                }
            }
            else if (SoloModeManager.turnCount <= 20 && SoloModeManager.turnCount > 10)
            {
                Debug.Log("Phase3");
                randomCard = Random.Range(13, 28);
                if (GameObject.FindWithTag("card" + randomCard).GetComponent<SoloModeCardControl>().ishitted == true)
                {
                    while (GameObject.FindWithTag("card" + randomCard).GetComponent<SoloModeCardControl>().ishitted == true)
                    {
                        randomCard = Random.Range(7, 28);
                        if (GameObject.FindWithTag("card" + randomCard).GetComponent<SoloModeCardControl>().ishitted == true)
                        {
                            randomCard = Random.Range(1, 28);
                        }
                    }
                }
            }
            else
            {
                Debug.Log("Phase4");
                randomCard = Random.Range(19, 28);
                if (GameObject.FindWithTag("card" + randomCard).GetComponent<SoloModeCardControl>().ishitted == true)
                {
                    while (GameObject.FindWithTag("card" + randomCard).GetComponent<SoloModeCardControl>().ishitted == true)
                    {
                        randomCard = Random.Range(13, 28);
                        if (GameObject.FindWithTag("card" + randomCard).GetComponent<SoloModeCardControl>().ishitted == true)
                        {
                            randomCard = Random.Range(1, 28);
                        }
                    }
                }
            }
            GameObject.FindWithTag("card" + randomCard).GetComponent<SoloModeCardControl>().OpenCard();

            //플레이어의 턴으로 변경
            SoloModeManager.turnCount++;
            SoloModeCardControl.instance.SetIsMyTurn(true);
        }
    }
}