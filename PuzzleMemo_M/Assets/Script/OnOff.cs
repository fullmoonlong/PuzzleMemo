using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnOff : MonoBehaviour
{
    public GameObject MenuSet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //메뉴 온오프
    public void OnClickMenu()
    {

        if (MenuSet.activeSelf)
        {
            MenuSet.SetActive(false);
        }
        else
        {
            MenuSet.SetActive(true);
        }
    }
}
