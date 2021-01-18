using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    //여러개의 팝업을 집어넣을수 있게만듬
    public GameObject[] popUps;
    //현재 팝업 번호
    private int popUpIndex;

    //튜토리얼텍스트 연결용
    //TutorialText tutoText;

    //카메라 정보
    private Camera cam;
    public GameObject Player1;
    public GameObject Player2;
    Vector3 originCamPos;//초기 위치
    float originCamSize;//초기 사이즈

    public Text Playertext;

    [Space]
    [Space]
    public Text ExplainText;

    [Space]
    [Space]
    //카메라 이동용
    public GameObject cardsoloPos;
    public GameObject boardsoloPos;
    public GameObject speicalPos;
    public GameObject cardrulePos;
    public GameObject cardPos;
    public GameObject boardPos;
    public GameObject centerPos;
    public GameObject NextPos;
    public GameObject OutPos;

    private float cameraSpeed = 5f;
    public float ZoomSpeed = 3f;

    //카메라이동용 함수
    static private int cardCnt = 100;

    bool[] check = new bool[cardCnt];
    bool[] complete = new bool[cardCnt];

    // Start is called before the first frame update
    void Start()
    {
        //0에서 시작, 16에 scene 2
        popUpIndex = 0;

        //튜토리얼 텍스트 스크립트 연결
        //tutoText = GameObject.Find("Panel_Explanation").transform.Find("Text").GetComponent<TutorialText>();

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
            ExplainText.text = "Puzzle Memo의 세계에 오신 것을 환영합니다.";
            //특정 트리거로 다음 설명으로
            if (Input.GetButtonDown("Fire1"))
            {
                ExplainText.text = "게임 방법에 대해 궁금하신가요? 설명에 따라 천천히 룰을 익혀보도록 합니다.";
                

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
                ExplainText.text = "게임의 구성품에 대해 알아볼까요?";
                

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
                ExplainText.text = "Puzzle Memo의 구성품은 게임이 진행될 보드판과";
                

                //카메라 정보 초기화
                CameraReset();

                popUpIndex++;
            }
        }

        else if (popUpIndex == 3)//scene1-1.3 (보드판 설명)
        {
            //3초 뒤?
            popUps[0].SetActive(true);
            cam.transform.position = boardsoloPos.transform.position;
            //cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 3, ZoomSpeed);

            if (Input.GetButtonDown("Fire1"))
            {
                ExplainText.text = "점수를 따라 이동할 수 있는 4종류의 말";
                
                popUpIndex++;
            }
        }

        else if (popUpIndex == 4)//scene1-1.4 (말 설명)
        {
            //나중에 말판 위치 추가시 넣을거
            //cam.transform.position = Vector3.Lerp(cam.transform.position, boardPos.transform.position, cameraSpeed * Time.deltaTime);

            if (Input.GetButtonDown("Fire1"))
            {
                ExplainText.text = "그리고 40장의 일반카드와";
                
                popUpIndex++;
            }
        }

        else if (popUpIndex == 5)//scene1-1.5 (일반카드 설명)
        {
            popUps[0].SetActive(false);
            popUps[1].SetActive(true);
            cam.transform.position = cardsoloPos.transform.position;

            if (Input.GetButtonDown("Fire1"))
            {
                ExplainText.text = "5장의 특수카드로 이루어져 있습니다.";
                
                popUpIndex++;
            }
        }

        else if (popUpIndex == 6)//scene1-1.6 (특수 카드 설명)
        {
            popUps[1].SetActive(false);
            popUps[2].SetActive(true);
            cam.transform.position = speicalPos.transform.position;

            if (Input.GetButtonDown("Fire1"))
            {
                ExplainText.text = "말판의 이동에 관한 설명을 시작합시다.";
                
                popUpIndex++;
            }
        }

        else if (popUpIndex == 7)//scene1-2.1
        {
            popUps[2].SetActive(false);
            cam.transform.position = centerPos.transform.position;

            if (Input.GetButtonDown("Fire1"))
            {
                ExplainText.text = "동물을 완성한 후, 맞춰진 동물이 가진 카드의 수만큼 나뭇잎을 따라 말을 이동하게 됩니다.";
                
                popUpIndex++;
            }
        }

        else if (popUpIndex == 8)//scene1-2.2
        {
            if (Input.GetButtonDown("Fire1"))
            {
                ExplainText.text = "왼쪽  카드를 클릭하여 보세요";
                
                popUpIndex++;
            }
        }

        else if (popUpIndex == 9)//scene1-2.3 (카드 클릭, 플레이어 움직이기)
        {
            popUps[3].SetActive(true);

            if (GameObject.FindWithTag("card6").GetComponent<CardControl>().ishitted == true && complete[6] == false)
            {
                //카메라를 보드판 위치로 이동
                cam.transform.position = Vector3.Lerp(cam.transform.position, boardPos.transform.position, cameraSpeed * Time.deltaTime);
                //위치이동이 완료되었으면 체크
                if (cam.transform.position.y >= boardPos.transform.position.y - 0.001)
                {
                    check[6] = true;
                }

                //체크됐을 때 플레이어1이 플레이어2로 갔을 때 complete 체크
                if (check[6])
                {
                    Player1.transform.position = Vector3.Lerp(Player1.transform.position, Player2.transform.position, cameraSpeed * Time.deltaTime);
                    if (Player1.transform.position.x >= Player2.transform.position.x - 0.001)
                    {
                        complete[6] = true;
                    }
                }
            }

            if (complete[6]) // 말위로 올라타기 기능 설명
            {
                Debug.Log("complete");
                ExplainText.text = "뒷사람이 앞사람을 따라 잡았을 경우, 말 위로 올라타는 것이 가능합니다.";
                
                popUpIndex++;
            }
        }

        else if (popUpIndex == 10)//scene1-2.4
        {
            if (Input.GetButtonDown("Fire1"))
            {
                ExplainText.text = "아랫사람이 이동을 하면 올라탄 사람은 같이 이동을 하게 됩니다.";
                
                popUpIndex++;
            }
        }

        else if (popUpIndex == 11)//scene1-2.5
        {
            cam.transform.position = Vector3.Lerp(cam.transform.position, centerPos.transform.position, cameraSpeed * Time.deltaTime);

            if (Input.GetButtonDown("Fire1"))
            {
                ExplainText.text = "하지만 업혀있는 사람이 이동하게 되면 아랫사람은 같이 이동하지 않습니다.";
                
                popUpIndex++;
            }

        }

        else if (popUpIndex == 12)//scene1-2.6
        {
            if (Input.GetButtonDown("Fire1"))
            {
                ExplainText.text = "오른쪽 카드를 클릭해보세요";
                
                popUpIndex++;
            }
        }

        else if (popUpIndex == 13)//scene1-2.7 (카드 클릭, 플레이어 같이 이동)
        {
            Playertext.text = "Player2";
            if (GameObject.FindWithTag("card18").GetComponent<CardControl>().ishitted == true && complete[18] == false)
            {
                print("들어옴");
                //카메라를 보드판 위치로 이동
                cam.transform.position = Vector3.Lerp(cam.transform.position, boardPos.transform.position, cameraSpeed * Time.deltaTime);
                //위치이동이 완료되었으면 체크
                if (cam.transform.position.y >= boardPos.transform.position.y - 0.001)
                {
                    check[18] = true;
                }

                //체크됐을 때 플레이어1이 플레이어2로 갔을 때 complete 체크
                if (check[18])
                {
                    Player2.transform.position = Vector3.Lerp(Player2.transform.position, NextPos.transform.position, cameraSpeed * Time.deltaTime);
                    Player1.transform.position = Vector3.Lerp(Player1.transform.position, NextPos.transform.position, cameraSpeed * Time.deltaTime);

                    if (Player2.transform.position.x >= NextPos.transform.position.x - 0.001)
                    {
                        complete[18] = true;
                    }
                }
            }

            if (complete[18]) // 말위로 올라타기 기능 설명
            {
                popUps[3].SetActive(false);
                ExplainText.text = "또한, 업혀있는 사람은 덩굴이 내려운 장소에서 무조건 내리게 됩니다.";
                
                popUpIndex++;
            }
        }

        else if (popUpIndex == 14)//scene1-2.8 (내리는 모션 )
        {
            Player1.transform.position = Vector3.Lerp(Player1.transform.position, OutPos.transform.position, cameraSpeed * Time.deltaTime);

            if (Input.GetButtonDown("Fire1"))
            {
                ExplainText.text = "말판의 이동은 이러한 방법으로 진행되며, 모든 퍼즐이 맞춰 졌을 떄 가장 멀리 이동한 사람이 승리합니다.";
                
                popUpIndex++;
            }
        }

        else if (popUpIndex == 15)//scene1-2.9
        {
            //아래 코드 안써도 말 나와서 제외
            //ExplainText.text = "말판의 이동은 이러한 방법으로 진행되며, 모든 퍼즐이 맞춰 졌을 떄 가장 멀리 이동한 사람이 승리합니다.";

            if (Input.GetButtonDown("Fire1"))
            {
                ExplainText.text = "말판에 대한 설명이 끝났으니 다음은 카드로 넘어가도록 하겠습니다.";
                
                popUpIndex++;
            }
        }

        //Scene2 시작
        else if (popUpIndex == 16)//scene2-1
        {
            //룰 설명 위해 화면 이동
            cam.transform.position = cardrulePos.transform.position;

            if (Input.GetButtonDown("Fire1"))
            {
                ExplainText.text = " 일반 카드의 설명을 먼저 진행하며, 기본 플레이 룰에 대해 말씀드리겠습니다.";
                
                popUpIndex++;
            }
        }

        else if (popUpIndex == 17)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                ExplainText.text = "Puzzle Memo는 40장의 일반 카드를 가지고 있습니다.";
                
                popUpIndex++;
            }
        }

        else if (popUpIndex == 18)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                GameObject.FindWithTag("card22").GetComponent<CardControl>().OpenCard();//22번 카드 뒤집기

                ExplainText.text = "카드는 골고루 섞어 뒷면이 보이도록 뒤집은 후, 보드 판을 기준으로 아래에서부터 맞춰지게 됩니다.";
                
                popUpIndex++;
            }
        }

        else if (popUpIndex == 19)
        {
            if (GameObject.FindWithTag("card22").GetComponent<CardControl>().ishitted == true)
            {
                popUpIndex++;
            }
        }

        else if (popUpIndex == 20)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                ExplainText.text = "아래 칸이 맞춰지지 않은 상태에서는 위쪽 칸을 맞출 수 없습니다.";
                
                popUpIndex++;
            }
        }

        else if (popUpIndex == 21)
        {
            popUps[4].SetActive(true);

            if (Input.GetButtonDown("Fire1"))
            {
                GameObject.FindWithTag("card15").GetComponent<CardControl>().OpenCard();//15번 카드 뒤집기
                GameObject.FindWithTag("card16").GetComponent<CardControl>().OpenCard();//16번 카드 뒤집기
                GameObject.FindWithTag("card21").GetComponent<CardControl>().OpenCard();//21번 카드 뒤집기

                ExplainText.text = "모든 칸을 맞추게 되면 완성된 동물이 가진 카드의 수만큼 점수를 획득합니다.";
                
                popUpIndex++;
            }
        }

        else if (popUpIndex == 22)
        {
            if (GameObject.FindWithTag("card15").GetComponent<CardControl>().ishitted == true ||
                GameObject.FindWithTag("card16").GetComponent<CardControl>().ishitted == true ||
                GameObject.FindWithTag("card21").GetComponent<CardControl>().ishitted == true)
            {
                popUpIndex++;
            }
        }

        else if (popUpIndex == 23)//scene2-2
        {
            popUps[4].SetActive(false);
            popUps[5].SetActive(true);

            if (Input.GetButtonDown("Fire1"))
            {
                ExplainText.text = "이번에는 특수 카드에 대한 설명을 시작하겠습니다.";
                
                popUpIndex++;
            }
        }

        else if (popUpIndex == 24)
        {
            cam.transform.position = speicalPos.transform.position;

            popUps[5].SetActive(false);

            if (Input.GetButtonDown("Fire1"))
            {
                ExplainText.text = "Puzzle Memo는 5장의 특수 카드를 보유하고 있습니다.";
                
                popUpIndex++;
            }
        }

        else if (popUpIndex == 25)
        {


            if (Input.GetButtonDown("Fire1"))
            {
                ExplainText.text = "연꽃카드를 뒤집었을 땐, 다른 카드를 뒤집을 수 있습니다";
                
                popUpIndex++;
            }
        }

        else if (popUpIndex == 26)
        {


            if (Input.GetButtonDown("Fire1"))
            {
                ExplainText.text = "거미줄 카드는 꽝 카드입니다. 차례를 넘깁니다.";
                
                popUpIndex++;
            }
        }

        else if (popUpIndex == 27)
        {


            if (Input.GetButtonDown("Fire1"))
            {
                ExplainText.text = "원숭이가 묘기를 부리는 이 카드는 앞사람의 머리 위로 올라탑니다.";
                
                popUpIndex++;
            }
        }

        else if (popUpIndex == 28)
        {


            if (Input.GetButtonDown("Fire1"))
            {
                ExplainText.text = "원숭이가 미끄러지는 카드는 말판을 뒤로 한칸 이동시킵니다.";
                
                popUpIndex++;
            }
        }

        else if (popUpIndex == 29)
        {


            if (Input.GetButtonDown("Fire1"))
            {
                ExplainText.text = "불리한 상황에도 포기하지 않는다면 역전할 수 있습니다. ";
                
                popUpIndex++;
            }
        }

        //Scene 3
        else if (popUpIndex == 30)//설명 종료
        {


            if (Input.GetButtonDown("Fire1"))
            {
                ExplainText.text = "이로써 Puzzle Memo의 모든 설명을 종료합니다.";
                
                popUpIndex++;
            }
        }
        //if (popUpIndex == 0)//첫번째 설명 (사족)
        //{
        //    //설명 텍스트
        //    ExplainText.text = "Puzzle Memo는 40장의 일반 카드를 가지고 있습니다. 하지만 설명을 위해서 일단 4장의 카드만 보여드리도록 하겠습니다.";

        //    //특정 트리거로 다음 설명으로
        //    if (Input.GetButtonDown("Fire1"))
        //    {
        //        //설명변경 후 다음 설명으로
        //        //ExplainText.text = "두번째 설명/ 아래쪽 두개 맞추면 넘어감";
        //        ExplainText.text = "카드는 골고루 섞어 뒷면이 보이도록 뒤집은 후, 보드 판을 기준으로 아래에서부터 맞춰지게 됩니다.";
        //        

        //        //카메라 정보 초기화
        //        CameraReset();

        //        popUpIndex++;
        //    }
        //}

        //else if (popUpIndex == 1)//두번째 설명
        //{
        //    if (Input.GetButtonDown("Fire1"))
        //    {
        //        ExplainText.text = "오른쪽 아래의 카드를 클릭 해 보세요";
        //        

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
        //    //    ExplainText.text = "세번째 설명/ 위쪽 두개 맞추면 넘어감";
        //    //    

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
        //        ExplainText.text = "네번째 설명/ 현재 끝. 우측 위 버튼을 통해 나가기";
        //        

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
