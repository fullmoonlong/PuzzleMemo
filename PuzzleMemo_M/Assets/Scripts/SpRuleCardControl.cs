using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SpRuleCardControl : MonoBehaviour
{
    #region Singleton
    public static SpRuleCardControl instance;

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
    static bool isMyTurn;
    static public int cardCnt = 24;
    public float cardInterval = 1.2f;

    Animator anim;

    public List<Animal> AllAnimalList, CurAnimalList;

    string filePath;

    enum AnimalOrder
    {
        개구리,
        아나콘다,
        아프리카거북,
        알락꼬리원숭이,
        재규어,
        투칸,
        하마,
        원숭이,
        뱀,
        낙타,
        여우,
        펭귄,
        북극곰
    }

    // Start is called before the first frame update
    void Start()
    {
        //파일 위치 지정
        filePath = Application.persistentDataPath + "/AllAnimal.txt";
        print(filePath);
        LoadFile();

        anim = GetComponent<Animator>();
        SetIsMyTurn(true);

        //이미지 위치용 정보
        targetPosition = new Vector3(0, 0, -2);

        if (tag.Substring(0, 5) == "board")
        {
            Color color = transform.GetComponent<Renderer>().material.color;

            //transform.GetComponent<MeshRenderer>().material.color = new Color(0.3f, 0.3f, 0.3f, 0f); //흐리게 만들기

            //보드 번호 가져오기
            int boardNum = int.Parse(transform.tag.Substring(5));

            transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("Puzzle/Basic/single_puzzle2_" + boardNum.ToString());
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
        if (isMyTurn == true)
        {
            //윈도우(왼버튼), 모바일(터치)
            if (Input.GetButtonDown("Fire1"))
            {
                ClickCheck();
            }
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

            if (tag.Substring(0, 4) == "card" && hit.transform.GetComponent<SpRuleCardControl>().isOpen == false)
            {
                //카드에 있는 OpenCard()함수 실행하기
                hit.transform.GetComponent<SpRuleCardControl>().OpenCard();

                //AI의 턴으로 변경
                SpRuleManager.turnCount++;
                SetIsMyTurn(false);
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
        transform.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Puzzle/Basic/single_puzzle_" + imgNum.ToString());
    }

    public void HideImage()
    {
        transform.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("single_card_back");
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
                if ((imgNum + 6) <= cardCnt && GameObject.FindWithTag("card" + (imgNum + 6)).transform.GetComponent<SpRuleCardControl>().ishitted == false)
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
        if ((imgNum + 6) > cardCnt || GameObject.FindWithTag("card" + (imgNum + 6)).transform.GetComponent<SpRuleCardControl>().ishitted == true)
        {
            if (this.tag.Substring(0, 4) == "card")
            {
                //동물 정보에 의한 점수 넘겨주기
                if (imgNum == 1 || imgNum == 2 || imgNum == 3 || imgNum == 7 || imgNum == 8 || imgNum == 13)
                {
                    SpRuleManager.Animals[0] += 1;

                    if (SpRuleManager.Animals[0] == 6)
                    {
                        if (isMyTurn == true)
                        {
                            MalManager.A += 6;
                            SpRuleManager.matchOver += 6;
                            Debug.Log("아나콘다");
                            AllAnimalList[(int)AnimalOrder.아나콘다].IsHaving = true;
                            SaveFile();
                        }
                        else
                        {
                            MalManager.B += 6;
                            SpRuleManager.matchOver += 6;
                        }
                    }
                }
                else if (imgNum == 4 || imgNum == 9 || imgNum == 10)
                {
                    SpRuleManager.Animals[1] += 1;

                    if (SpRuleManager.Animals[1] == 3)
                    {
                        if (isMyTurn == true)
                        {
                            MalManager.A += 3;
                            SpRuleManager.matchOver += 3;
                            Debug.Log("알락꼬리원숭이");
                            AllAnimalList[(int)AnimalOrder.알락꼬리원숭이].IsHaving = true;
                            SaveFile();
                        }
                        else
                        {
                            MalManager.B += 3;
                            SpRuleManager.matchOver += 3;
                        }
                    }
                }
                else if (imgNum == 5 || imgNum == 6 || imgNum == 11 || imgNum == 12)
                {
                    SpRuleManager.Animals[2] += 1;

                    if (SpRuleManager.Animals[2] == 4)
                    {
                        if (isMyTurn == true)
                        {
                            MalManager.A += 4;
                            SpRuleManager.matchOver += 4;
                            Debug.Log("하마");
                            AllAnimalList[(int)AnimalOrder.하마].IsHaving = true;
                            SaveFile();
                        }
                        else
                        {
                            MalManager.B += 4;
                            SpRuleManager.matchOver += 4;
                        }
                    }
                }
                else if (imgNum == 14)
                {
                    SpRuleManager.Animals[3] += 1;

                    if (SpRuleManager.Animals[3] == 1)
                    {
                        if (isMyTurn == true)
                        {
                            MalManager.A += 1;
                            SpRuleManager.matchOver += 1;
                            Debug.Log("개구리");
                            AllAnimalList[(int)AnimalOrder.개구리].IsHaving = true;
                            SaveFile();
                        }
                        else
                        {
                            MalManager.B += 1;
                            SpRuleManager.matchOver += 1;
                        }
                    }
                }
                else if (imgNum == 15 || imgNum == 16 || imgNum == 17 || imgNum == 21 || imgNum == 22 || imgNum == 23)
                {
                    SpRuleManager.Animals[4] += 1;

                    if (SpRuleManager.Animals[4] == 6)
                    {
                        if (isMyTurn == true)
                        {
                            MalManager.A += 6;
                            SpRuleManager.matchOver += 6;
                            Debug.Log("재규어");
                            AllAnimalList[(int)AnimalOrder.재규어].IsHaving = true;
                            SaveFile();
                        }
                        else
                        {
                            MalManager.B += 6;
                            SpRuleManager.matchOver += 6;
                        }
                    }
                }
                else if (imgNum == 18 || imgNum == 24)
                {
                    SpRuleManager.Animals[5] += 1;

                    if (SpRuleManager.Animals[5] == 2)
                    {
                        if (isMyTurn == true)
                        {
                            MalManager.A += 2;
                            SpRuleManager.matchOver += 2;
                            Debug.Log("투칸");
                            AllAnimalList[(int)AnimalOrder.투칸].IsHaving = true;
                            SaveFile();
                        }
                        else
                        {
                            MalManager.B += 2;
                            SpRuleManager.matchOver += 2;
                        }
                    }
                }
                else if (imgNum == 19 || imgNum == 20)
                {
                    SpRuleManager.Animals[6] += 1;

                    if (SpRuleManager.Animals[6] == 2)
                    {
                        if (isMyTurn == true)
                        {
                            MalManager.A += 2;
                            SpRuleManager.matchOver += 2;
                            Debug.Log("아프리카거북");
                            AllAnimalList[(int)AnimalOrder.아프리카거북].IsHaving = true;
                            SaveFile();
                        }
                        else
                        {
                            MalManager.B += 2;
                            SpRuleManager.matchOver += 2;
                        }
                    }
                }
            }
        }
    }
    

    void SaveFile()
    {
        //AllAnimalList 직렬화
        string jdata = JsonUtility.ToJson(new Serialization<Animal>(AllAnimalList));
        File.WriteAllText(filePath, jdata);

    }

    void LoadFile()
    {
        string jdata = File.ReadAllText(filePath);

        AllAnimalList = JsonUtility.FromJson<Serialization<Animal>>(jdata).target;
    }

}