using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SinglePlay : MonoBehaviour
{
    public Text ExplainText;

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
    }

    // Update is called once per frame
    void Update()
    {
        CurMapImage.sprite = MapImage[CurMap];
        ExplainText.text = explain[CurMap];
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
