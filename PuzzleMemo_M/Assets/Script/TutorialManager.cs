using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    //여러개의 팝업을 집어넣을수 있게만듬
    public GameObject[] popUps;
    //현재 팝업 번호
    private int popUpIndex;

    private Camera cam;

    public GameObject cardPos;
    public GameObject boardPos;
    float cardposY;

    private float cameraSpeed = 5f;

    public float ZoomSpeed = 3f;

    bool check11 = false;
    bool check12 = false;
    bool complete11 = false;
    bool complete12 = false;

    bool check5 = false;
    bool check6 = false;
    bool complete5 = false;
    bool complete6 = false;
    // Start is called before the first frame update
    void Start()
    {
        //0에서 시작
        popUpIndex = 0;
        cam = Camera.main;
        cardposY = -1.37f;
    }

    // Update is called once per frame
    void FixedUpdate()
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
            //카메라 확대
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 3, ZoomSpeed);
            
            //만약 카드11을 맞췄으면
            if (GameObject.FindWithTag("card11").GetComponent<CardControl>().ishitted == true && complete11 == false)
            {
                //카메라를 보드판 위치로 이동
                cam.transform.position = Vector3.Lerp(cam.transform.position, boardPos.transform.position, cameraSpeed * Time.deltaTime);

                //위치이동이 완료되었으면 체크
                if (cam.transform.position.y >= boardPos.transform.position.y - 0.001)
                {
                    check11 = true;
                }

                //체크됐을 때 다시 카드판 위치로 이동
                if (check11)
                {
                    cam.transform.position = Vector3.Lerp(cam.transform.position,cardPos.transform.position, cameraSpeed * Time.deltaTime);

                    if (cam.transform.position.y <= -0.63)
                    {
                        complete11 = true;
                    }
                }
            }

            //만약 카드12를 맞췄으면
            if (GameObject.FindWithTag("card12").GetComponent<CardControl>().ishitted == true && complete12 == false)
            {
                //카메라를 보드판 위치로 이동
                cam.transform.position = Vector3.Lerp(cam.transform.position, boardPos.transform.position, cameraSpeed * Time.deltaTime);
                
                //위치이동이 완료되었으면 체크
                if (cam.transform.position.y >= boardPos.transform.position.y - 0.001)
                {
                    check12 = true;
                }

                //체크됐을 때 다시 카드판 위치로 이동
                if (check12)
                {
                    cam.transform.position = Vector3.Lerp(cam.transform.position, cardPos.transform.position, cameraSpeed * Time.deltaTime);

                    if (cam.transform.position.y <= -0.63)
                    {
                        complete12 = true;
                    }
                }
            }

            if (complete11 && complete12)
            {
                //설명 텍스트
                GameObject.Find("Panel_Explanation").transform.Find("Text").GetComponent<TutorialText>().m_text = "세번째 설명/ 위쪽 두개 맞추면 넘어감";
                GameObject.Find("Panel_Explanation").transform.Find("Text").GetComponent<TutorialText>().TextStart();

                popUpIndex++;
            }
        }

        else if (popUpIndex == 2)//세번째 설명
        {
            if (GameObject.FindWithTag("card5").GetComponent<CardControl>().ishitted == true && complete5 == false)
            {
                //카메라를 보드판 위치로 이동
                cam.transform.position = Vector3.Lerp(cam.transform.position, boardPos.transform.position, cameraSpeed * Time.deltaTime);

                //위치이동이 완료되었으면 체크
                if (cam.transform.position.y >= boardPos.transform.position.y - 0.001)
                {
                    check5 = true;
                }

                //체크됐을 때 다시 카드판 위치로 이동
                if (check5)
                {
                    cam.transform.position = Vector3.Lerp(cam.transform.position, cardPos.transform.position, cameraSpeed * Time.deltaTime);

                    if (cam.transform.position.y <= -0.63)
                    {
                        complete5 = true;
                    }
                }
            }

            //만약 카드12를 맞췄으면
            if (GameObject.FindWithTag("card6").GetComponent<CardControl>().ishitted == true && complete6 == false)
            {
                //카메라를 보드판 위치로 이동
                cam.transform.position = Vector3.Lerp(cam.transform.position, boardPos.transform.position, cameraSpeed * Time.deltaTime);

                //위치이동이 완료되었으면 체크
                if (cam.transform.position.y >= boardPos.transform.position.y - 0.001)
                {
                    check6 = true;
                }

                //체크됐을 때 다시 카드판 위치로 이동
                if (check6)
                {
                    cam.transform.position = Vector3.Lerp(cam.transform.position, cardPos.transform.position, cameraSpeed * Time.deltaTime);

                    if (cam.transform.position.y <= -0.63)
                    {
                        complete6 = true;
                    }
                }
            }
            if (complete5 && complete6)
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
