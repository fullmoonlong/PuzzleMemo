using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialText : MonoBehaviour
{
    public Text ExplainText;
    public string m_text = "안녕 만나서 반가워! 먼저 게임 룰을 설명해 줄게";
    bool ClickCheck = false;

    //현재 글자 위치용으로 추가 - 규식
    public int n;

    // Start is called before the first frame update
    void Start()
    {
        n = 0;// 글자 처음부터

        StartCoroutine(_typing());
    }

    // Update is called once per frame
    void Update()
    {
        //카드 넘어가면서 클릭하면 설명 바로 다나와서 임시로없앰 ㅠㅠ
        /*if(Input.GetMouseButtonDown(0))
        {
            ClickCheck = true;
        }*/
    }

    //설명 넘어가면서 새로 불러오려고 추가 - 규식
    public void TextStart()
    {
        n = 0;//글자 처음부터

        StartCoroutine(_typing());
    }

    IEnumerator _typing()
    {
        for(n = 0; n <= m_text.Length; n++)
        {
            if(ClickCheck == false)
            {
                Debug.Log(n);
                ExplainText.text = m_text.Substring(0, n);
                yield return new WaitForSeconds(0.1f);
            }

            else
            {
                ExplainText.text = m_text;
                ClickCheck = false;
                yield break;
            }
        }
    }
}
