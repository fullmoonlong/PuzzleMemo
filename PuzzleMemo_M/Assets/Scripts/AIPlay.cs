using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AIPlay : MonoBehaviour
{
    //종류별로 ㄱ,ㄴ,ㅣ,ㅡ자
    public Text TypeTitleText;
    //public Text TypeExplainText;

    public string[] typetitle;
    public string[] typeexplain;

    //맵별로 맵이름과 등장동물
    public Text MapTitleText;
    public Text MapExplainText;

    public string[] maptitle;
    public string[] mapexplain;

    //첫번째배열에는 타입, 두번째 배열은 맵종류 이차원 배열이 안되서 일차원 배열로 변경
    public Sprite[] TypeMapImage; 

    public int CurType;
    public int CurMap;

    int TypeLength;
    int MapLength;

    public Image CurImage;

    public Image MapImage;
    // Start is called before the first frame update
    void Start()
    {
        //처음 타입과 맵은 0으로 시작
        CurType = 0;
        CurMap = 0;

        TypeLength = 4;
        MapLength = 4;

        //처음이미지도 처음 타입 기본맵으로 시작
        CurImage.sprite = TypeMapImage[TypeLength * CurType + CurMap];

        //처음 타입의 타이틀과 설명 시작
        TypeTitleText.text = typetitle[CurType];
        //TypeExplainText.text = typeexplain[CurType];
    }

    // Update is called once per frame
    void Update()
    {
        //타입보여주는 이미지
        CurImage.sprite = TypeMapImage[CurType + CurMap * TypeLength];

        TypeTitleText.text = typetitle[CurType];
        //TypeExplainText.text = typeexplain[CurType];

        //맵 보여주는 이미지
        MapImage.sprite = TypeMapImage[TypeLength * CurType + CurMap];

        MapTitleText.text = maptitle[CurMap];
        MapExplainText.text = mapexplain[CurMap];
    }

    public void TypeNextBtn()
    {
        CurType += 1;
        if (CurType >= TypeLength)
        {
            CurType = 0;
        }
    }

    public void TypeBackBtn()
    {
        CurType -= 1;
        if (CurType < 0)
        {
            CurType = TypeLength - 1;
        }  
    }

    public void MapNextBtn()
    {
        CurMap += 1;
        if (CurMap >= MapLength)
        {
            CurMap = 0;
        }
    }

    public void MapBackBtn()
    {
        CurMap -= 1;
        if (CurMap < 0)
        {
            CurMap = MapLength - 1;
        }
    }
}
