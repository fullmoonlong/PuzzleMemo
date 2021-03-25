using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class DoubleModeCardControl : MonoBehaviour
{
    #region Singleton
    public static DoubleModeCardControl instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    //이미지 번호
    int imgNum = 1;

    //오픈된 카드 판별 여부
    public bool isOpen = false;
    public bool ishitted = false;

    //물체 이동을 위해서
    public Vector3 cardPos;
    public Vector3 targetPosition;


    //변수
    static public bool isMyTurn;//true가 player1, false가 player2
    static public int cardCnt = 24;
    public float cardInterval = 1.2f;

    public Animator anim;
    public static string DoubleMap;

    // Start is called before the first frame update
    void Start()
    {
        DoubleMap = PlayerPrefs.GetString("DoubleMap");
        anim = GetComponent<Animator>();
        SetIsMyTurn(true);

        //이미지 위치용 정보
        targetPosition = new Vector3(0, 0, -2);

        if (tag.Substring(0, 5) == "board")
        {
            //Color color = transform.GetComponent<Renderer>().material.color;

            //transform.GetComponent<MeshRenderer>().material.color = new Color(0.3f, 0.3f, 0.3f, 0f); //흐리게 만들기

            //보드 번호 가져오기
            int boardNum = int.Parse(transform.tag.Substring(5));

            if (DoubleMap == "Antarctica")
            {
                transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("card_back" + boardNum.ToString());
                //transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("Puzzle/Basic/single_puzzle2_" + boardNum.ToString());
            }
            else if (DoubleMap == "Desert")
            {
                transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("medal" + boardNum.ToString());
                //transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("Puzzle/Basic/single_puzzle2_" + boardNum.ToString());
            }
            else
            {
                //transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("Puzzle/Basic/single_puzzle2_" + boardNum.ToString());
                transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("Puzzle/New/퍼즐_뉴2_" + boardNum.ToString());
            }
        }

        if (tag.Substring(0, 4) == "card")//특수카드일 경우에, 20위치에 총 카드 갯수
        {
            //카드 번호 가져오기
            imgNum = int.Parse(transform.tag.Substring(4));
        }
    }

    // Update is called once per frame
    void Update()
    {
        //윈도우(왼버튼), 모바일(터치)
        if (Input.GetButtonDown("Fire1"))
        {
            ClickCheck();
        }

        //클릭 후 카드 움직임
        CardMove();
        CardBoardMatch();
    }

    void ClickCheck()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            string tag = hit.transform.tag;

            if (tag.Substring(0, 4) == "card")
            {
                //카드에 있는 OpenCard()함수 실행하기
                hit.transform.GetComponent<DoubleModeCardControl>().OpenCard();

                //다음사람 턴으로 변경
                DoubleModeManager.turnCount++;

                if ((DoubleModeManager.turnCount / 24) % 2 == 1)
                {
                    SetIsMyTurn(false);
                }
                else if ((DoubleModeManager.turnCount / 24) % 2 == 0)
                {
                    SetIsMyTurn(true);
                }
            }
        }
    }

    public void OpenCard()
    {
        //이미 열려있으면 무시
        if (isOpen) return;

        isOpen = true;
        transform.position = new Vector3(transform.position.x, transform.position.y, -1.1f);
        MatchInfo();
        anim.Play("SingleAniOpen");
    }

    public void CloseCard()
    {
        if (isOpen == false)
        {
            return;
        }

        isOpen = false;
        ishitted = false;
        transform.position = new Vector3(transform.position.x, transform.position.y, -1f);
        anim.Play("SingleAniClose");
    }

    public void ShowImage()
    {
        if(DoubleMap == "Antarctica")
        {
            transform.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("card_back");
            //transform.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("single_puzzle_" + imgNum.ToString());
        }
        else if (DoubleMap == "Sea")
        {
            transform.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("medal");
            //transform.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("single_puzzle_" + imgNum.ToString());
        }
        else
        {
            //transform.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Puzzle/Basic/single_puzzle_" + imgNum.ToString());
            transform.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Puzzle/New/퍼즐_뉴_" + imgNum.ToString());
        }
    }

    public void HideImage()
    {
        transform.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Card_B");
    }

    public void CardMove()
    {
        if (this.ishitted == true && this.tag.Substring(0, 4) == "card")
        {
            int hittedcardNum = int.Parse(transform.tag.Substring(4));
            BoardMatch(hittedcardNum);
        }
    }

    public void BoardMatch(int _hittedCardNum)
    {
        GameObject.FindWithTag("card" + _hittedCardNum).transform.position = GameObject.FindWithTag("board" + _hittedCardNum).transform.position;
    }

    public void CardBoardMatch()
    {
        if (this.isOpen == false || this.ishitted == true)
        {
            this.cardInterval = 1.2f;
            return;
        }
        else if (this.isOpen == true)
        {
            this.cardInterval -= Time.deltaTime;
        }

        if (this.cardInterval <= 0f)
        {
            {
                if ((imgNum + 6) <= cardCnt && GameObject.FindWithTag("card" + (imgNum + 6)).transform.GetComponent<DoubleModeCardControl>().ishitted == false)
                {
                    CloseCard();

                    return;
                }
                else
                {
                    this.ishitted = true;

                    return;
                }
            }
        }
    }

    public void SetIsMyTurn(bool _isMyTurn)
    {
        isMyTurn = _isMyTurn;
    }

    public bool GetIsMyTurn()
    {
        return isMyTurn;
    }

    public void MatchInfo()
    {
        //제일 밑줄 카드이거나 밑에 칸이 맞춰졌다면
        if ((imgNum + 6) > cardCnt || GameObject.FindWithTag("card" + (imgNum + 6)).transform.GetComponent<DoubleModeCardControl>().ishitted == true)
        {
            if (this.tag.Substring(0, 4) == "card")
            {
                //동물 정보에 의한 점수 넘겨주기
                if (imgNum == 1 || imgNum == 2 || imgNum == 3 || imgNum == 7 || imgNum == 8 || imgNum == 13)
                {
                    DoubleModeManager.Animals[0] += 1;

                    if (DoubleModeManager.Animals[0] == 6)
                    {
                        if (isMyTurn == true)
                        {
                            MalManager.A += 6;
                            DoubleModeManager.matchOver += 6;
                        }
                        else
                        {
                            MalManager.B += 6;
                            DoubleModeManager.matchOver += 6;
                        }
                    }
                }
                else if (imgNum == 4 || imgNum == 9 || imgNum == 10)
                {
                    DoubleModeManager.Animals[1] += 1;

                    if (DoubleModeManager.Animals[1] == 3)
                    {
                        if (isMyTurn == true)
                        {
                            MalManager.A += 3;
                            DoubleModeManager.matchOver += 3;
                        }
                        else
                        {
                            MalManager.B += 3;
                            DoubleModeManager.matchOver += 3;
                        }
                    }
                }
                else if (imgNum == 5 || imgNum == 6 || imgNum == 11 || imgNum == 12)
                {
                    DoubleModeManager.Animals[2] += 1;

                    if (DoubleModeManager.Animals[2] == 4)
                    {
                        if (isMyTurn == true)
                        {
                            MalManager.A += 4;
                            DoubleModeManager.matchOver += 4;
                        }
                        else
                        {
                            MalManager.B += 4;
                            DoubleModeManager.matchOver += 4;
                        }
                    }
                }
                else if (imgNum == 14)
                {
                    DoubleModeManager.Animals[3] += 1;

                    if (DoubleModeManager.Animals[3] == 1)
                    {
                        if (isMyTurn == true)
                        {
                            MalManager.A += 1;
                            DoubleModeManager.matchOver += 1;
                        }
                        else
                        {
                            MalManager.B += 1;
                            DoubleModeManager.matchOver += 1;
                        }
                    }
                }
                else if (imgNum == 15 || imgNum == 16 || imgNum == 17 || imgNum == 21 || imgNum == 22 || imgNum == 23)
                {
                    DoubleModeManager.Animals[4] += 1;

                    if (DoubleModeManager.Animals[4] == 6)
                    {
                        if (isMyTurn == true)
                        {
                            MalManager.A += 6;
                            DoubleModeManager.matchOver += 6;
                        }
                        else
                        {
                            MalManager.B += 6;
                            DoubleModeManager.matchOver += 6;
                        }
                    }
                }
                else if (imgNum == 18 || imgNum == 24)
                {
                    DoubleModeManager.Animals[5] += 1;

                    if (DoubleModeManager.Animals[5] == 2)
                    {
                        if (isMyTurn == true)
                        {
                            MalManager.A += 2;
                            DoubleModeManager.matchOver += 2;
                        }
                        else
                        {
                            MalManager.B += 2;
                            DoubleModeManager.matchOver += 2;
                        }
                    }
                }
                else if (imgNum == 19 || imgNum == 20)
                {
                    DoubleModeManager.Animals[6] += 1;

                    if (DoubleModeManager.Animals[6] == 2)
                    {
                        if (isMyTurn == true)
                        {
                            MalManager.A += 2;
                            DoubleModeManager.matchOver += 2;
                        }
                        else
                        {
                            MalManager.B += 2;
                            DoubleModeManager.matchOver += 2;
                        }
                    }
                }
            }
        }
    }
}
