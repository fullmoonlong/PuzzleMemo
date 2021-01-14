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
    public GameObject speicalPos;
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
        //for (int i = 0; i < popUps.Length; i++)
        //{

        //    if (i == popUpIndex)
        //    {
        //        popUps[i].SetActive(true);
        //    }
        //    else
        //    {
        //        popUps[i].SetActive(false);
        //    }
        //}

        if (popUpIndex == 0)//scene1.1
        {
            tutoText.m_text = "Puzzle Memo의 세계에 오신 것을 환영합니다.";
            //특정 트리거로 다음 설명으로
            if (Input.GetButtonDown("Fire1"))
            {
                tutoText.m_text = "게임 방법에 대해 궁금하신가요? 설명에 따라 천천히 룰을 익혀보도록 합니다.";
                tutoText.TextStart();

                //카메라 정보 초기화
                CameraReset();

                popUpIndex++;
            }
        }

        else if (popUpIndex == 1)//scene1-1.1
        {
            //특정 트리거로 다음 설명으로
            if (Input.GetButtonDown("Fire1"))
            {
                tutoText.m_text = "게임의 구성품에 대해 알아볼까요?";
                tutoText.TextStart();

                //카메라 정보 초기화
                CameraReset();

                popUpIndex++;
            }
        }

        else if (popUpIndex == 2)//scene1-1.2
        {
            //특정 트리거로 다음 설명으로
            if (Input.GetButtonDown("Fire1"))
            {
                tutoText.m_text = "Puzzle Memo의 구성품은 게임이 진행될 보드판과";
                tutoText.TextStart();

                //카메라 정보 초기화
                CameraReset();

                popUpIndex++;
            }
        }

        else if (popUpIndex == 3)//scene1-1.3 (보드판 설명)
        {
            //3초 뒤?
            popUps[0].SetActive(true);
            cam.transform.position = Vector3.Lerp(cam.transform.position, boardPos.transform.position, cameraSpeed * Time.deltaTime);
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 3, ZoomSpeed);
            if (Input.GetButtonDown("Fire1"))
            {
                tutoText.m_text = "점수를 따라 이동할 수 있는 4종류의 말";
                tutoText.TextStart();

                popUpIndex++;
            }
        }

        else if (popUpIndex == 4)//scene1-1.4 (말 설명)
        {
            //나중에 말판 위치 추가시 넣을거
            //cam.transform.position = Vector3.Lerp(cam.transform.position, boardPos.transform.position, cameraSpeed * Time.deltaTime);
            
            if (Input.GetButtonDown("Fire1"))
            {
                tutoText.m_text = "그리고 40장의 일반카드와";
                tutoText.TextStart();
                popUpIndex++;
            }
        }

        else if (popUpIndex == 5)//scene1-1.5 (일반카드 설명)
        {
            popUps[0].SetActive(false);
            popUps[1].SetActive(true);
            cam.transform.position = Vector3.Lerp(cam.transform.position, cardPos.transform.position, cameraSpeed * Time.deltaTime);

            if (Input.GetButtonDown("Fire1"))
            {
                tutoText.m_text = "5장의 특수카드로 이루어져 있습니다.";
                tutoText.TextStart();
                CameraReset();
                popUpIndex++;
            }
        }

        else if (popUpIndex == 6)//scene1-1.6 (특수 카드 설명)
        {
            popUps[1].SetActive(false);
            popUps[2].SetActive(true);
            cam.transform.position = Vector3.Lerp(cam.transform.position, speicalPos.transform.position, cameraSpeed * Time.deltaTime);
        }
        //if (popUpIndex == 0)//첫번째 설명 (사족)
        //{
        //    //설명 텍스트
        //    tutoText.m_text = "Puzzle Memo는 40장의 일반 카드를 가지고 있습니다. 하지만 설명을 위해서 일단 4장의 카드만 보여드리도록 하겠습니다.";

        //    //특정 트리거로 다음 설명으로
        //    if (Input.GetButtonDown("Fire1"))
        //    {
        //        //설명변경 후 다음 설명으로
        //        //tutoText.m_text = "두번째 설명/ 아래쪽 두개 맞추면 넘어감";
        //        tutoText.m_text = "카드는 골고루 섞어 뒷면이 보이도록 뒤집은 후, 보드 판을 기준으로 아래에서부터 맞춰지게 됩니다.";
        //        tutoText.TextStart();

        //        //카메라 정보 초기화
        //        CameraReset();

        //        popUpIndex++;
        //    }
        //}

        //else if (popUpIndex == 1)//두번째 설명
        //{
        //    if (Input.GetButtonDown("Fire1"))
        //    {
        //        tutoText.m_text = "오른쪽 아래의 카드를 클릭 해 보세요";
        //        tutoText.TextStart();

        //        //카메라 정보 초기화
        //        CameraReset();

        //        popUpIndex++;
        //    }
        //}

        //else if (popUpIndex == 2)//두번째 설명
        //{
        //    //카메라 확대
        //    cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 3, ZoomSpeed);
        //    //만약 카드12를 맞췄으면
        //    //3초뒤에

        //    if (GameObject.FindWithTag("card12").GetComponent<CardControl>().ishitted == true && complete[12] == false)
        //    {
        //        //카메라를 보드판 위치로 이동
        //        cam.transform.position = Vector3.Lerp(cam.transform.position, boardPos.transform.position, cameraSpeed * Time.deltaTime);

        //        //위치이동이 완료되었으면 체크
        //        if (cam.transform.position.y >= boardPos.transform.position.y - 0.001)
        //        {
        //            check[12] = true;
        //        }

        //        //체크됐을 때 다시 카드판 위치로 이동
        //        if (check[12])
        //        {
        //            cam.transform.position = Vector3.Lerp(cam.transform.position, cardPos.transform.position, cameraSpeed * Time.deltaTime);

        //            if (cam.transform.position.y <= -0.63)
        //            {
        //                complete[12] = true;
        //            }
        //        }
        //    }

        //    //if (complete[11] || complete[12])
        //    //{
        //    //    //설명 텍스트
        //    //    tutoText.m_text = "세번째 설명/ 위쪽 두개 맞추면 넘어감";
        //    //    tutoText.TextStart();

        //    //    //카메라 정보 초기화
        //    //    CameraReset();

        //    //    popUpIndex++;
        //    //}
        //}

        //else if (popUpIndex == 3)//세번째 설명
        //{
        //    //카메라 확대
        //    cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 3, ZoomSpeed);

        //    if (GameObject.FindWithTag("card5").GetComponent<CardControl>().ishitted == true && complete[5] == false)
        //    {
        //        //카메라를 보드판 위치로 이동
        //        cam.transform.position = Vector3.Lerp(cam.transform.position, boardPos.transform.position, cameraSpeed * Time.deltaTime);

        //        //위치이동이 완료되었으면 체크
        //        if (cam.transform.position.y >= boardPos.transform.position.y - 0.001)
        //        {
        //            check[5] = true;
        //        }

        //        //체크됐을 때 다시 카드판 위치로 이동
        //        if (check[5])
        //        {
        //            cam.transform.position = Vector3.Lerp(cam.transform.position, cardPos.transform.position, cameraSpeed * Time.deltaTime);

        //            if (cam.transform.position.y <= -0.63)
        //            {
        //                complete[5] = true;
        //            }
        //        }
        //    }

        //    //만약 카드12를 맞췄으면
        //    if (GameObject.FindWithTag("card6").GetComponent<CardControl>().ishitted == true && complete[6] == false)
        //    {
        //        //카메라를 보드판 위치로 이동
        //        cam.transform.position = Vector3.Lerp(cam.transform.position, boardPos.transform.position, cameraSpeed * Time.deltaTime);

        //        //위치이동이 완료되었으면 체크
        //        if (cam.transform.position.y >= boardPos.transform.position.y - 0.001)
        //        {
        //            check[6] = true;
        //        }

        //        //체크됐을 때 다시 카드판 위치로 이동
        //        if (check[6])
        //        {
        //            cam.transform.position = Vector3.Lerp(cam.transform.position, cardPos.transform.position, cameraSpeed * Time.deltaTime);

        //            if (cam.transform.position.y <= -0.63)
        //            {
        //                complete[6] = true;
        //            }
        //        }
        //    }
        //    if (complete[5] && complete[6])
        //    {
        //        //설명 텍스트
        //        tutoText.m_text = "네번째 설명/ 현재 끝. 우측 위 버튼을 통해 나가기";
        //        tutoText.TextStart();

        //        //카메라 정보 초기화
        //        CameraReset();

        //        popUpIndex++;
        //    }
        //}
        //else if (popUpIndex == 4)//네번째 설명
        //{
        //    //쓸모없는 오브젝트 제외
        //    if (GameObject.Find("Cards")) GameObject.Find("Cards").SetActive(false);
        //    if (GameObject.Find("Boards")) GameObject.Find("Boards").SetActive(false);
        //}
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
