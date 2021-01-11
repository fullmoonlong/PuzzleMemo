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

        if (tag.Substring(0, 5) == "board")//보드
        {
            //보드 번호 가져오기
            int boardNum = int.Parse(transform.tag.Substring(5));

            //이미지 적용
            transform.GetComponent<Renderer>().material.mainTexture = Resources.Load("puzzle2_" + boardNum.ToString()) as Texture2D;
        }

        if (tag.Substring(0, 4) == "card")//카드
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

        //맞춘 카드인지 체크

        //카드 움직임
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
        if (!isOpen) return;

        isOpen = false;
        transform.position = new Vector3(transform.position.x, transform.position.y, -1f);
        anim.Play("aniClose");
    }

    void ShowImage()
    {
        Debug.Log(imgNum);
        //transform.GetComponent<Renderer>().material.mainTexture = Resources.Load("puzzle_" + imgNum.ToString()) as Texture2D;
        transform.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("puzzle_" + imgNum.ToString());
    }

    void HideImage()
    {
        transform.GetComponent<Renderer>().material.mainTexture = Resources.Load("card_back") as Texture2D;
    }

    void CardMove()
    {
        if (this.isOpen == true)//특수카드일 경우에
        {
            return;
        }

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