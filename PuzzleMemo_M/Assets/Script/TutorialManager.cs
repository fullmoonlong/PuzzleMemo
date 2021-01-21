using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    //여러개의 팝업을 집어넣을수 있게만듬
    public GameObject[] popUps;
    public GameObject[] Clicks;
    //현재 팝업 번호
    private int popUpIndex;

    //튜토리얼텍스트 연결용
    //TutorialText tutoText;

    //카메라 정보
    private Camera cam;
    public GameObject Player1;
    public GameObject Player2;
    public GameObject PlayerA_1;//Index 31
    public GameObject PlayerB_1;//Index 31
    public GameObject PlayerA_2;//Index 32

    public Sprite AddPlayer;
    public Sprite CurrentPlayer;

    Vector3 originCamPos;//초기 위치
    float originCamSize;//초기 사이즈

    public Text Playertext;

    public GameObject ClickText;
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
    public GameObject mainmenuPos;
    public GameObject cardPos;
    public GameObject boardPos;
    public GameObject centerPos;
    public GameObject NextPos;
    public GameObject OutPos;
    public GameObject PlayerA_2_Backpos;//A_2뒤로 갈 위치

    private float cameraSpeed = 5f;
    public float ZoomSpeed = 3f;

    //카메라이동용 함수
    static private int cardCnt = 100;

    bool[] check = new bool[cardCnt];
    bool[] complete = new bool[cardCnt];

    //간격용
    float popUpInterval;
    static float IntervalTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        //0에서 시작, 16에 scene 2
        //popUpIndex = 0;
        if(PlayerPrefs.HasKey("PopIndex"))
        {
            popUpIndex = PlayerPrefs.GetInt("PopIndex");
        }
        //간격 시간
        popUpInterval = IntervalTime;

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

    private void Update()
    {
        popUpInterval -= Time.deltaTime;
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

            //트리거 전에 간격 주기
            if (popUpInterval >= 0){ ClickText.SetActive(false); return;}
            if (popUpInterval < 0){ ClickText.SetActive(true);}
            //if (popUpInterval < 0) { Click.SetActive(true); }
            //특정 트리거로 다음 설명으로
            if (Input.GetButtonDown("Fire1"))
            {
                //카메라 정보 초기화
                CameraReset();

                popUpInterval = IntervalTime;
                popUpIndex++;
            }
        }

        else if (popUpIndex == 1)//scene1-1.1
        {
            ExplainText.text = "게임 방법에 대해 궁금하신가요?\n설명에 따라 천천히 룰을 익혀보도록 합니다.";
            //트리거 전에 간격 주기
            if (popUpInterval >= 0){ ClickText.SetActive(false); return;}
            if (popUpInterval < 0){ ClickText.SetActive(true);}
            //특정 트리거로 다음 설명으로
            if (Input.GetButtonDown("Fire1"))
            {
                

                //카메라 정보 초기화
                CameraReset();

                popUpInterval = IntervalTime;
                popUpIndex++;
            }
        }

        else if (popUpIndex == 2)//scene1-1.2
        {
            ExplainText.text = "게임의 구성품에 대해 알아볼까요?";
            //트리거 전에 간격 주기
            if (popUpInterval >= 0){ ClickText.SetActive(false); return;}
            if (popUpInterval < 0) { ClickText.SetActive(true); }

            //특정 트리거로 다음 설명으로
            if (Input.GetButtonDown("Fire1"))
            {
                

                //카메라 정보 초기화
                CameraReset();

                popUpInterval = IntervalTime;
                popUpIndex++;
            }
        }

        else if (popUpIndex == 3)//scene1-1.3 (보드판 설명)
        {
            ExplainText.text = "Puzzle Memo의 구성품은 게임이 진행될 보드판과";
            //3초 뒤?
            popUps[0].SetActive(true);
            cam.transform.position = boardsoloPos.transform.position;
            //cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 3, ZoomSpeed);

            //트리거 전에 간격 주기
            if (popUpInterval >= 0){ ClickText.SetActive(false); return;}
            if (popUpInterval < 0){ ClickText.SetActive(true);}
            if (Input.GetButtonDown("Fire1"))
            {
                
                popUpInterval = IntervalTime;
                popUpIndex++;
            }
        }

        else if (popUpIndex == 4)//scene1-1.4 (말 설명)
        {
            //나중에 말판 위치 추가시 넣을거
            //cam.transform.position = Vector3.Lerp(cam.transform.position, boardPos.transform.position, cameraSpeed * Time.deltaTime);

            ExplainText.text = "점수를 따라 이동할 수 있는 4종류의 말";

            popUps[0].SetActive(false);

            //트리거 전에 간격 주기
            if (popUpInterval >= 0){ ClickText.SetActive(false); return;}
            if (popUpInterval < 0){ ClickText.SetActive(true);}
            if (Input.GetButtonDown("Fire1"))
            {
                
                popUpInterval = IntervalTime;
                popUpIndex++;
            }
        }

        else if (popUpIndex == 5)//scene1-1.5 (일반카드 설명)
        {
            ExplainText.text = "그리고 40장의 일반카드와";
            
            popUps[1].SetActive(true);
            cam.transform.position = cardsoloPos.transform.position;

            //트리거 전에 간격 주기
            if (popUpInterval >= 0){ ClickText.SetActive(false); return;}
            if (popUpInterval < 0){ ClickText.SetActive(true);}
            if (Input.GetButtonDown("Fire1"))
            {
                
                popUpInterval = IntervalTime;
                popUpIndex++;
            }
        }

        else if (popUpIndex == 6)//scene1-1.6 (특수 카드 설명)
        {
            ExplainText.text = "5장의 특수카드로 이루어져 있습니다.";
            popUps[1].SetActive(false);
            popUps[2].SetActive(true);
            cam.transform.position = speicalPos.transform.position;

            //트리거 전에 간격 주기
            if (popUpInterval >= 0){ ClickText.SetActive(false); return;}
            if (popUpInterval < 0){ ClickText.SetActive(true);}
            if (Input.GetButtonDown("Fire1"))
            {
                
                popUpInterval = IntervalTime;
                popUpIndex++;
            }
        }

        else if (popUpIndex == 7)//scene1-2.1
        {
            ExplainText.text = "말판의 이동에 관한 설명을 시작합시다.";
            popUps[2].SetActive(false);
            cam.transform.position = centerPos.transform.position;

            //트리거 전에 간격 주기
            if (popUpInterval >= 0){ ClickText.SetActive(false); return;}
            if (popUpInterval < 0){ ClickText.SetActive(true);}
            if (Input.GetButtonDown("Fire1"))
            {
                
                popUpInterval = IntervalTime;
                popUpIndex++;
            }
        }

        else if (popUpIndex == 8)//scene1-2.2
        {
            ExplainText.text = "동물을 완성한 후, 맞춰진 동물이 가진 카드의 수만큼 나뭇잎을 따라 말을 이동하게 됩니다.";
            //트리거 전에 간격 주기
            if (popUpInterval >= 0){ ClickText.SetActive(false); return;}
            if (popUpInterval < 0){ ClickText.SetActive(true);}
            if (Input.GetButtonDown("Fire1"))
            {
                
                popUpInterval = IntervalTime;
                popUpIndex++;
            }
        }

        else if (popUpIndex == 9)//scene1-2.3 (카드 클릭, 플레이어 움직이기)
        {
            Clicks[0].SetActive(true);
            ExplainText.text = "왼쪽  카드를 클릭하여 보세요";
            if (GameObject.FindWithTag("card6").GetComponent<CardControl>().ishitted == true && complete[6] == false)
            {
                Clicks[0].SetActive(false);
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
                        Player2.GetComponent<SpriteRenderer>().sprite = AddPlayer;
                        Player1.SetActive(false);
                        complete[6] = true;
                    }
                }
            }

            //트리거 전에 간격 주기
            if (popUpInterval >= 0) { ClickText.SetActive(false); return;}
            if (complete[6]) // 말위로 올라타기 기능 설명
            {
                Debug.Log("complete");
                
                popUpInterval = IntervalTime;
                popUpIndex++;
            }
        }

        else if (popUpIndex == 10)//scene1-2.4
        {
            ExplainText.text = "뒷사람이 앞사람을 따라 잡았을 경우, 말 위로 올라타는 것이 가능합니다.";
            //트리거 전에 간격 주기
            if (popUpInterval >= 0){ ClickText.SetActive(false); return;}
            if (popUpInterval < 0) { ClickText.SetActive(true); }
            if (Input.GetButtonDown("Fire1"))
            {
                
                popUpInterval = IntervalTime;
                popUpIndex++;
            }
        }

        else if (popUpIndex == 11)//scene1-2.5
        {
            ExplainText.text = "아랫사람이 이동을 하면 올라탄 사람은 같이 이동을 하게 됩니다.";
            cam.transform.position = Vector3.Lerp(cam.transform.position, centerPos.transform.position, cameraSpeed * Time.deltaTime);

            //트리거 전에 간격 주기
            if (popUpInterval >= 0){ ClickText.SetActive(false); return;}
            if (popUpInterval < 0) { ClickText.SetActive(true); }
            if (Input.GetButtonDown("Fire1"))
            {
                
                popUpInterval = IntervalTime;
                popUpIndex++;
            }

        }

        else if (popUpIndex == 12)//scene1-2.6
        {
            ExplainText.text = "하지만 업혀있는 사람이 이동하게 되면 아랫사람은 같이 이동하지 않습니다.";
            //트리거 전에 간격 주기
            if (popUpInterval >= 0){ ClickText.SetActive(false); return;}
            if (popUpInterval < 0){ ClickText.SetActive(true);}
            if (Input.GetButtonDown("Fire1"))
            {
                
                popUpInterval = IntervalTime;
                popUpIndex++;
            }
        }

        else if (popUpIndex == 13)//scene1-2.7 (카드 클릭, 플레이어 같이 이동)
        {
            Clicks[1].SetActive(true);
            ExplainText.text = "오른쪽 카드를 클릭해보세요";
            Playertext.text = "Player2";
            if (GameObject.FindWithTag("card18").GetComponent<CardControl>().ishitted == true && complete[18] == false)
            {
                Clicks[1].SetActive(false);
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
                    Player1.SetActive(true);
                    Player2.transform.position = Vector3.Lerp(Player2.transform.position, NextPos.transform.position, cameraSpeed * Time.deltaTime);
                    Player1.transform.position = Vector3.Lerp(Player1.transform.position, OutPos.transform.position, cameraSpeed * Time.deltaTime);

                    Player2.transform.GetComponent<SpriteRenderer>().sprite = CurrentPlayer;
                 
                    if (Player2.transform.position.x >= NextPos.transform.position.x - 0.001)
                    {
                        complete[18] = true;
                    }
                }
            }

            //트리거 전에 간격 주기
            if (popUpInterval >= 0) { ClickText.SetActive(false); return;}
            if (complete[18]) // 말위로 올라타기 기능 설명
            {
                popUpInterval = IntervalTime;
                popUpIndex++;
            }
        }

        else if (popUpIndex == 14)//scene1-2.8 (내리는 모션 )
        {
            ExplainText.text = "또한, 업혀있는 사람은 덩굴이 내려운 장소에서 무조건 내리게 됩니다.";
            Player1.transform.position = Vector3.Lerp(Player1.transform.position, OutPos.transform.position, cameraSpeed * Time.deltaTime);

            //트리거 전에 간격 주기
            if (popUpInterval >= 0){ ClickText.SetActive(false); return;}
            if (popUpInterval < 0){ ClickText.SetActive(true);}
            if (Input.GetButtonDown("Fire1"))
            {
                
                popUpInterval = IntervalTime;
                popUpIndex++;
            }
        }

        else if (popUpIndex == 15)//scene1-2.9
        {
            ExplainText.text = "말판의 이동은 이러한 방법으로 진행되며, 모든 퍼즐이 맞춰 졌을 때 가장 멀리 이동한 사람이 승리합니다.";
            popUps[3].SetActive(true);

            //트리거 전에 간격 주기
            if (popUpInterval >= 0){ ClickText.SetActive(false); return;}
            if (popUpInterval < 0){ ClickText.SetActive(true);}
            if (Input.GetButtonDown("Fire1"))
            {
                popUps[3].SetActive(false);

                popUpInterval = IntervalTime;
                popUpIndex++;
            }
        }

        //Scene2 시작
        else if (popUpIndex == 16)//scene2-1
        {
            ExplainText.text = "말판에 대한 설명이 끝났으니 다음은 카드로 넘어가도록 하겠습니다.";
            //룰 설명 위해 화면 이동
            cam.transform.position = cardrulePos.transform.position;

            //트리거 전에 간격 주기
            if (popUpInterval >= 0){ ClickText.SetActive(false); return;}
            if (popUpInterval < 0){ ClickText.SetActive(true);}
            if (Input.GetButtonDown("Fire1"))
            {
                
                popUpInterval = IntervalTime;
                popUpIndex++;
            }
        }

        else if (popUpIndex == 17)
        {
            ExplainText.text = " 일반 카드의 설명을 먼저 진행하며, 기본 플레이 룰에 대해 말씀드리겠습니다.";
            //트리거 전에 간격 주기
            if (popUpInterval >= 0){ ClickText.SetActive(false); return;}
            if (popUpInterval < 0){ ClickText.SetActive(true);}
            if (Input.GetButtonDown("Fire1"))
            {
                
                popUpInterval = IntervalTime;
                popUpIndex++;
            }
        }

        else if (popUpIndex == 18)
        {
            ExplainText.text = "Puzzle Memo는 40장의 일반 카드를 가지고 있습니다.";
            //트리거 전에 간격 주기
            if (popUpInterval >= 0){ ClickText.SetActive(false); return;}
            if (popUpInterval < 0){ ClickText.SetActive(true);}
            if (Input.GetButtonDown("Fire1"))
            {

                
                popUpInterval = IntervalTime;
                popUpIndex++;
            }
        }

        else if (popUpIndex == 19)
        {
            ExplainText.text = "카드는 골고루 섞어 뒷면이 보이도록 뒤집은 후, 보드 판을 기준으로 아래에서부터 맞춰지게 됩니다.";
            //트리거 전에 간격 주기
            if (popUpInterval >= 0){ ClickText.SetActive(false); return;}
            if (popUpInterval < 0){ ClickText.SetActive(true);}
            if (Input.GetButtonDown("Fire1"))
            {

                popUpInterval = IntervalTime;
                popUpIndex++;
            }
        }

        else if (popUpIndex == 20)
        {
            Clicks[2].SetActive(true);
            ExplainText.text = "우측 아래 카드를 클릭해 보세요";
            //트리거 전에 간격 주기
            if (popUpInterval >= 0){ ClickText.SetActive(false); return;}
            if (GameObject.FindWithTag("card22").GetComponent<CardControl>().ishitted == true)
            {
                Clicks[2].SetActive(false);
                ExplainText.text = "잘하셨습니다!";
                popUpInterval = IntervalTime;
                popUpIndex++;
            }
        }

        else if (popUpIndex == 21)
        {
            //트리거 전에 간격 주기
            if (popUpInterval >= 0){ ClickText.SetActive(false); return;}
            if (popUpInterval < 0){ ClickText.SetActive(true);}
            if (Input.GetButtonDown("Fire1"))
            {
                popUpInterval = IntervalTime;
                popUpIndex++;
            }
        }

        else if (popUpIndex == 22)
        {
            Clicks[3].SetActive(true);
            ExplainText.text = "왼쪽 위 카드를 선택해 보세요";
            //트리거 전에 간격 주기
            if (popUpInterval >= 0){ ClickText.SetActive(false); return;}
      
            if (GameObject.FindWithTag("card15").GetComponent<CardControl>().isOpen == true)
            {
                Clicks[3].SetActive(false);
                ExplainText.text = "아래 칸이 맞춰지지 않은 상태에서는 위쪽 칸을 맞출 수 없습니다.";

                popUpInterval = IntervalTime;
                popUpIndex++;
            }
        }

        else if (popUpIndex == 23)
        {
            //트리거 전에 간격 주기
            if (popUpInterval >= 0){ ClickText.SetActive(false); return;}
            if (popUpInterval < 0){ ClickText.SetActive(true);}
            if (GameObject.FindWithTag("card15").GetComponent<CardControl>().isOpen == false)
            {
                popUpInterval = IntervalTime;
                popUpIndex++;
            }
        }

        else if (popUpIndex == 24)
        {
            popUps[4].SetActive(true);

            //트리거 전에 간격 주기
            if (popUpInterval >= 0){ ClickText.SetActive(false); return;}
            if (popUpInterval < 0){ ClickText.SetActive(true);}
            if (Input.GetButtonDown("Fire1"))
            {
                popUpInterval = IntervalTime;
                popUpIndex++;
            }
        }

        else if (popUpIndex == 25)
        {
            ExplainText.text = "뒤집힌 카드를 클릭해서 남은 카드들을 모두 맞춰봅시다.";
            popUps[4].SetActive(false);

            //트리거 전에 간격 주기
            if (popUpInterval >= 0){ ClickText.SetActive(false); return;}
            if (GameObject.FindWithTag("card15").GetComponent<CardControl>().ishitted == true &&
                GameObject.FindWithTag("card16").GetComponent<CardControl>().ishitted == true &&
                GameObject.FindWithTag("card21").GetComponent<CardControl>().ishitted == true &&
                GameObject.FindWithTag("card22").GetComponent<CardControl>().ishitted == true)
            {
                popUpInterval = IntervalTime;
                popUpIndex++;
            }
        }

        else if (popUpIndex == 26)//scene2-2
        {
            ExplainText.text = "모든 칸을 맞추게 되면 완성된 동물이 가진 카드의 수만큼 점수를 획득합니다.";
            popUps[5].SetActive(true);

            //트리거 전에 간격 주기
            if (popUpInterval >= 0){ ClickText.SetActive(false); return;}
            if (popUpInterval < 0){ ClickText.SetActive(true);}
            if (Input.GetButtonDown("Fire1"))
            {
                popUpInterval = IntervalTime;
                popUpIndex++;
            }
        }

        else if (popUpIndex == 27)
        {
            ExplainText.text = "이번에는 특수 카드에 대한 설명을 시작하겠습니다.";
            cam.transform.position = speicalPos.transform.position;

            popUps[5].SetActive(false);

            //트리거 전에 간격 주기
            if (popUpInterval >= 0){ ClickText.SetActive(false); return;}
            if (popUpInterval < 0){ ClickText.SetActive(true);}
            if (Input.GetButtonDown("Fire1"))
            {
                popUpInterval = IntervalTime;
                popUpIndex++;
            }
        }

        else if (popUpIndex == 28)
        {
            ExplainText.text = "Puzzle Memo는 5장의 특수 카드를 보유하고 있습니다.";
            popUps[6].SetActive(true);

            //트리거 전에 간격 주기
            if (popUpInterval >= 0) { ClickText.SetActive(false); return; }
            if (popUpInterval < 0) { ClickText.SetActive(true); }
            if (Input.GetButtonDown("Fire1"))
            {
                popUpInterval = IntervalTime;
                popUpIndex++;
            }
        }

        else if (popUpIndex == 29)
        {
            ExplainText.text = "거미줄 카드만 2장, 나머지 카드 1장씩으로 총 5장이 존재합니다.";

            //트리거 전에 간격 주기
            if (popUpInterval >= 0) { ClickText.SetActive(false); return; }
            if (popUpInterval < 0) { ClickText.SetActive(true); }
            if (Input.GetButtonDown("Fire1"))
            {
                popUpInterval = IntervalTime;
                popUpIndex++;
            }
        }

        else if (popUpIndex == 30)
        {
            ExplainText.text = "먼저 소개시켜드릴 카드는 원숭이 그림이 그려진 카드입니다.";
            popUps[6].SetActive(false);

            //트리거 전에 간격 주기
            if (popUpInterval >= 0) { ClickText.SetActive(false); return; }
            if (popUpInterval < 0) { ClickText.SetActive(true); }
            if (Input.GetButtonDown("Fire1"))
            {
                popUpInterval = IntervalTime;
                popUpIndex++;
            }
        }

        else if (popUpIndex == 31)
        {
            ExplainText.text = "앞으로 튀어 오르는 원숭이가 그려진 카드는 앞사람의 머리 위로 올라타게 합니다";
            popUps[7].SetActive(true);

            //트리거 전에 간격 주기
            if (popUpInterval >= 0) { ClickText.SetActive(false); return; }
            if (popUpInterval < 0) { ClickText.SetActive(true); }
            if (Input.GetButtonDown("Fire1"))
            {
                popUpInterval = IntervalTime;
                popUpIndex++;
            }
        }

        else if (popUpIndex == 32)
        {
            ExplainText.text = "앞으로 튀어 오르는 원숭이가 그려진 카드는 앞사람의 머리 위로 올라타게 합니다";

            if (complete[32] == false)
            {
                //플레이어 A_1을 플레이어B_1 위로 올리고 합쳐짐
                PlayerA_1.transform.position = Vector3.Lerp(PlayerA_1.transform.position, PlayerB_1.transform.position, cameraSpeed * Time.deltaTime);

                if (PlayerA_1.transform.position.x >= PlayerB_1.transform.position.x - 0.001)
                {
                    PlayerB_1.GetComponent<SpriteRenderer>().sprite = AddPlayer;
                    PlayerA_1.SetActive(false);
                    complete[32] = true;
                }
            }

            //트리거 전에 간격 주기
            if (popUpInterval >= 0) { ClickText.SetActive(false); return; }
            if (popUpInterval < 0) { ClickText.SetActive(true); }
            if (Input.GetButtonDown("Fire1"))
            {
                popUpInterval = IntervalTime;
                popUpIndex++;
            }
        }

        else if (popUpIndex == 33)
        {
            ExplainText.text = "뒤로 튕겨나간 원숭이가 그려진 카드는 뒤로 1칸 말을 이동하게 합니다";
            popUps[7].SetActive(false);
            popUps[8].SetActive(true);

            //트리거 전에 간격 주기
            if (popUpInterval >= 0) { ClickText.SetActive(false); return; }
            if (popUpInterval < 0) { ClickText.SetActive(true); }
            if (Input.GetButtonDown("Fire1"))
            {
                popUpInterval = IntervalTime;
                popUpIndex++;
            }
        }

        else if (popUpIndex == 34)
        {
            ExplainText.text = "뒤로 튕겨나간 원숭이가 그려진 카드는 뒤로 1칸 말을 이동하게 합니다"; 

            if (complete[34] == false)
            {
                //플레이어 A_2를 뒤로 한칸 이동시킴
                PlayerA_2.transform.position = Vector3.Lerp(PlayerA_2.transform.position, PlayerA_2_Backpos.transform.position, cameraSpeed * Time.deltaTime);

                if (PlayerA_2.transform.position.x <= PlayerA_2_Backpos.transform.position.x - 0.001)
                {
                    complete[34] = true;
                }
            }

            //트리거 전에 간격 주기
            if (popUpInterval >= 0) { ClickText.SetActive(false); return; }
            if (popUpInterval < 0) { ClickText.SetActive(true); }
            if (Input.GetButtonDown("Fire1"))
            {
                
                popUpInterval = IntervalTime;
                popUpIndex++;
            }
        }

        else if (popUpIndex == 35)
        {
            ExplainText.text = "다음으로 보실 카드는 거미줄 카드입니다. 거미줄 카드는 자신의 차례를 강제로 한번 쉬게 하는 함정카드입니다.";
            popUps[8].SetActive(false);
            popUps[9].SetActive(true);

            //트리거 전에 간격 주기
            if (popUpInterval >= 0) { ClickText.SetActive(false); return; }
            if (popUpInterval < 0) { ClickText.SetActive(true); }
            if (Input.GetButtonDown("Fire1"))
            {
                
                popUpInterval = IntervalTime;
                popUpIndex++;
            }
        }

        else if (popUpIndex == 36)
        {
            ExplainText.text = "마지막 카드는 연꽃이 그려진 카드입니다. 이 카드는 뽑은 것으로 다음 차례로 넘어가는 카드입니다.";
            popUps[9].SetActive(false);
            popUps[10].SetActive(true);

            //트리거 전에 간격 주기
            if (popUpInterval >= 0) { ClickText.SetActive(false); return; }
            if (popUpInterval < 0) { ClickText.SetActive(true); }
            if (Input.GetButtonDown("Fire1"))
            {

                popUpInterval = IntervalTime;
                popUpIndex++;
            }
        }

        //Scene 3
        else if (popUpIndex == 37)//설명 종료
        {
            ExplainText.text = "특수 카드이지만 무난하게 차례를 넘긴다는 느낌이 강한 카드입니다.";
            popUps[10].SetActive(false);

            //트리거 전에 간격 주기
            if (popUpInterval >= 0) { ClickText.SetActive(false); return; }
            if (popUpInterval < 0) { ClickText.SetActive(true); }
            if (Input.GetButtonDown("Fire1"))
            {
                
                popUpInterval = IntervalTime;
                popUpIndex++;
            }
        }

        else if (popUpIndex == 38)
        {
            ExplainText.text = "이로써 Puzzle Memo의 모든 설명을 종료합니다.";
            cam.transform.position = mainmenuPos.transform.position;

            //트리거 전에 간격 주기
            if (popUpInterval >= 0) { ClickText.SetActive(false); return; }
            if (popUpInterval < 0) { ClickText.SetActive(true); }
            if (Input.GetButtonDown("Fire1"))
            {

                popUpInterval = IntervalTime;
                popUpIndex++;
            }
        }

        else if (popUpIndex == 39)
        {
            ExplainText.text = "메인 화면의 튜토리얼 버튼을 누른 후 원하시는 목차의 설명을 다시 보실 수 있으며,";
            popUps[11].SetActive(true);

            //트리거 전에 간격 주기
            if (popUpInterval >= 0) { ClickText.SetActive(false); return; }
            if (popUpInterval < 0) { ClickText.SetActive(true); }
            if (Input.GetButtonDown("Fire1"))
            {

                popUpInterval = IntervalTime;
                popUpIndex++;
            }
        }

        else if (popUpIndex == 40)
        {
            ExplainText.text = "마찬가지로 메인 화면의 플레이 버튼을 통하여 혼자서도 룰을 연습하실 수 있습니다.";
            popUps[11].SetActive(false);
            popUps[12].SetActive(true);

            //트리거 전에 간격 주기
            if (popUpInterval >= 0) { ClickText.SetActive(false); return; }
            if (popUpInterval < 0) { ClickText.SetActive(true); }
            if (Input.GetButtonDown("Fire1"))
            {

                popUpInterval = IntervalTime;
                popUpIndex++;
            }
        }

        else if (popUpIndex == 41)
        {
            ExplainText.text = "다음 클릭시 메인화면으로 이동됩니다.";
            popUps[12].SetActive(false);

            //트리거 전에 간격 주기
            if (popUpInterval >= 0) { ClickText.SetActive(false); return; }
            if (popUpInterval < 0) { ClickText.SetActive(true); }
            if (Input.GetButtonDown("Fire1"))
            {
                //메인메뉴로
                SceneManager.LoadScene("MainMenu");
            }
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
