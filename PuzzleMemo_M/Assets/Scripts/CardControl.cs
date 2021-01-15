using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardControl : MonoBehaviour
{
    //이미지 번호
    int imgNum = 1;

    //오픈된 카드 판별 여부
    bool isOpen = false;
    public bool ishitted = false;

    //물체 이동을 위해서
    public Vector3 cardPos;
    public Vector3 targetPosition;

    public float card_term = 1.2f;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        //이미지 위치용 정보
        targetPosition = new Vector3(0, 0, -2);

        if (tag.Substring(0, 5) == "board")
        {
            Color color = transform.GetComponent<Renderer>().material.color;

            //transform.GetComponent<MeshRenderer>().material.color = new Color(0.3f, 0.3f, 0.3f, 0f); //흐리게 만들기

            //보드 번호 가져오기
            int boardNum = int.Parse(transform.tag.Substring(5));

            transform.GetComponent<Renderer>().material.mainTexture = Resources.Load("puzzle2_" + boardNum.ToString()) as Texture2D;
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
        CardMove();
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
                Debug.Log("card");
                //카드에 있는 OpenCard()함수 실행하기
                hit.transform.GetComponent<CardControl>().OpenCard();
            }
        }
    }

    void OpenCard()
    {
        //이미 열려있으면 무시
        if (isOpen) return;

        isOpen = true;
        transform.position = new Vector3(transform.position.x, transform.position.y, -1.1f);
        anim.Play("aniOpen");
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
        anim.Play("aniClose");
    }

    void ShowImage()
    {
        transform.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("puzzle_" + imgNum.ToString());
    }

    void HideImage()
    {
        transform.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("card_back");
    }

    void CardMove()
    {
        if (this.ishitted == true && this.tag.Substring(0, 4) == "card")
        {
            int hittedcardNum = int.Parse(transform.tag.Substring(4));
            BoardMatch(hittedcardNum);
        }
    }

    void BoardMatch(int _hittedCardNum)
    {
        GameObject.FindWithTag("card" + _hittedCardNum).transform.position = GameObject.FindWithTag("board" + _hittedCardNum).transform.position;
    }
}