using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    //여러개의 팝업을 집어넣을수 있게만듬
    public GameObject[] popUps;
    //현재 팝업 번호
    private int popUpIndex;

    //튜토리얼텍스트 연결용
    TutorialText tutoText;

    //카메라 정보
    private Camera cam;
    Vector3 originCamPos;//초기 위치
    float originCamSize;//초기 사이즈

    [Space]
    [Space]
    public GameObject cardPos;
    public GameObject boardPos;

    private float cameraSpeed = 5f;

    public float ZoomSpeed = 3f;

    //카메라이동용 함수
    static private int cardCnt = 50;

    bool[] check = new bool[cardCnt];
    bool[] complete = new bool[cardCnt];

    // Start is called before the first frame update
    void Start()
    {
        //0에서 시작
        popUpIndex = 0;

        //튜토리얼 텍스트 스크립트 연결
        tutoText = GameObject.Find("Panel_Explanation").transform.Find("Text").GetComponent<TutorialText>();

        //카메라 정보 세팅
        cam = Camera.main;

        //처음 카메라 정보
        originCamPos = cam.transform.position;
        originCamSize = cam.orthographicSize;

        //어레이 값 지정
        for(int i = 0; i < cardCnt; i++)
        {
            check[i] = false;
            complete[i] = false;
        }
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
            tutoText.m_text = "첫번째 설명/ 클릭하면 넘어감";

            //특정 트리거로 다음 설명으로
            if (Input.GetButtonDown("Fire1"))
            {
                //설명변경 후 다음 설명으로
                tutoText.m_text = "두번째 설명/ 아래쪽 두개 맞추면 넘어감";
                tutoText.TextStart();

                //카메라 정보 초기화
                CameraReset();

                popUpIndex++;
            }
        }
        else if (popUpIndex == 1)//두번째 설명
        {
            //카메라 확대
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 3, ZoomSpeed);
            
            //만약 카드11을 맞췄으면
            if (GameObject.FindWithTag("card11").GetComponent<CardControl>().ishitted == true && complete[11] == false)
            {
                //카메라를 보드판 위치로 이동
                cam.transform.position = Vector3.Lerp(cam.transform.position, boardPos.transform.position, cameraSpeed * Time.deltaTime);

                //위치이동이 완료되었으면 체크
                if (cam.transform.position.y >= boardPos.transform.position.y - 0.001)
                {
                    check[11] = true;
                }

                //체크됐을 때 다시 카드판 위치로 이동
                if (check[11])
                {
                    cam.transform.position = Vector3.Lerp(cam.transform.position,cardPos.transform.position, cameraSpeed * Time.deltaTime);

                    if (cam.transform.position.y <= -0.63)
                    {
                        complete[11]= true;
                    }
                }
            }

            //만약 카드12를 맞췄으면
            if (GameObject.FindWithTag("card12").GetComponent<CardControl>().ishitted == true && complete[12] == false)
            {
                //카메라를 보드판 위치로 이동
                cam.transform.position = Vector3.Lerp(cam.transform.position, boardPos.transform.position, cameraSpeed * Time.deltaTime);
                
                //위치이동이 완료되었으면 체크
                if (cam.transform.position.y >= boardPos.transform.position.y - 0.001)
                {
                    check[12] = true;
                }

                //체크됐을 때 다시 카드판 위치로 이동
                if (check[12])
                {
                    cam.transform.position = Vector3.Lerp(cam.transform.position, cardPos.transform.position, cameraSpeed * Time.deltaTime);

                    if (cam.transform.position.y <= -0.63)
                    {
                        complete[12] = true;
                    }
                }
            }

            if (complete[11] && complete[12])
            {
                //설명 텍스트
                tutoText.m_text = "세번째 설명/ 위쪽 두개 맞추면 넘어감";
                tutoText.TextStart();

                //카메라 정보 초기화
                CameraReset();

                popUpIndex++;
            }
        }

        else if (popUpIndex == 2)//세번째 설명
        {
            //카메라 확대
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 3, ZoomSpeed);

            if (GameObject.FindWithTag("card5").GetComponent<CardControl>().ishitted == true && complete[5] == false)
            {
                //카메라를 보드판 위치로 이동
                cam.transform.position = Vector3.Lerp(cam.transform.position, boardPos.transform.position, cameraSpeed * Time.deltaTime);

                //위치이동이 완료되었으면 체크
                if (cam.transform.position.y >= boardPos.transform.position.y - 0.001)
                {
                    check[5] = true;
                }

                //체크됐을 때 다시 카드판 위치로 이동
                if (check[5])
                {
                    cam.transform.position = Vector3.Lerp(cam.transform.position, cardPos.transform.position, cameraSpeed * Time.deltaTime);

                    if (cam.transform.position.y <= -0.63)
                    {
                        complete[5] = true;
                    }
                }
            }

            //만약 카드12를 맞췄으면
            if (GameObject.FindWithTag("card6").GetComponent<CardControl>().ishitted == true && complete[6] == false)
            {
                //카메라를 보드판 위치로 이동
                cam.transform.position = Vector3.Lerp(cam.transform.position, boardPos.transform.position, cameraSpeed * Time.deltaTime);

                //위치이동이 완료되었으면 체크
                if (cam.transform.position.y >= boardPos.transform.position.y - 0.001)
                {
                    check[6] = true;
                }

                //체크됐을 때 다시 카드판 위치로 이동
                if (check[6])
                {
                    cam.transform.position = Vector3.Lerp(cam.transform.position, cardPos.transform.position, cameraSpeed * Time.deltaTime);

                    if (cam.transform.position.y <= -0.63)
                    {
                        complete[6] = true;
                    }
                }
            }
            if (complete[5] && complete[6])
            {
                //설명 텍스트
                tutoText.m_text = "네번째 설명/ 현재 끝. 우측 위 버튼을 통해 나가기";
                tutoText.TextStart();

                //카메라 정보 초기화
                CameraReset();

                popUpIndex++;
            }
        }
        else if (popUpIndex == 3)//네번째 설명
        {
            //쓸모없는 오브젝트 제외
            if (GameObject.Find("Cards")) GameObject.Find("Cards").SetActive(false);
            if (GameObject.Find("Boards")) GameObject.Find("Boards").SetActive(false);
        }
    }

    //카메라 정보 초기화
    void CameraReset()
    {
        cam.transform.position = originCamPos;
        cam.orthographicSize = originCamSize;
    }

    public int GetPopUpIndex()
    {
        return popUpIndex;
    }
}
