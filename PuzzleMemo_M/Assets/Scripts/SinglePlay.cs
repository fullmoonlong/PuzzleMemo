using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SinglePlay : MonoBehaviour
{
    public Text TitleText;
    public Text ExplainText;

    public string[] title;
    public string[] explain;

    int CurMap;

    int MapLength;

    public Sprite[] MapImage;

    public Image CurMapImage;

    // Start is called before the first frame update
    void Start()
    {
        CurMap = 0;
        MapLength = 4;
        CurMapImage.sprite = MapImage[CurMap];
        ExplainText.text = explain[CurMap];
        TitleText.text = title[CurMap];
    }

    // Update is called once per frame
    void Update()
    {
        CurMapImage.sprite = MapImage[CurMap];
        ExplainText.text = explain[CurMap];
        TitleText.text = title[CurMap];
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

    public void MapBasic()
    {
        CurMap = 0;
    }

    public void MapJungle()
    {
        CurMap = 1;
    }

    public void MapDesert()
    {
        CurMap = 2;
    }

    public void MapA()
    {
        CurMap = 3;
    }
}
