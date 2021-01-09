using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialText : MonoBehaviour
{
    public Text ExplainText;
    private string m_text = "안녕 만나서 반가워! 먼저 게임 룰을 설명해 줄게";
    bool ClickCheck = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(_typing());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            ClickCheck = true;
        }
    }

    IEnumerator _typing()
    {
        for(int i = 0; i<= m_text.Length; i++)
        {
            if(ClickCheck == false)
            {
                ExplainText.text = m_text.Substring(0, i);
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
