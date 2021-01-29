using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MalManager : MonoBehaviour
{
    //말
    public GameObject Player_A, Player_B;
    //말 정보
    public static int A, B;
    
    [Space]
    [Space]
    //판 정보
    public GameObject[] Plates = new GameObject[SoloModeCardControl.cardCnt];

    //올라타기 정보, true = 올라탐/false = 일반상태
    bool TopOnEnemy_A, TopOnEnemy_B;

    // Start is called before the first frame update
    void Start()
    {
        //처음 위치
        A = 0;
        B = 0;

        //일반 상태
        TopOnEnemy_A = false;
        TopOnEnemy_B = false;

        //해당위치로 이동
        Player_A.transform.position = Plates[A].transform.position + new Vector3(0.1f, 0, 0);
        Player_B.transform.position = Plates[B].transform.position - new Vector3(0.1f, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        MalMovement();
    }

    void MalMovement()
    {
        //이동할 블럭이 존재하지 않으면 return
        if(Plates[A] == null || Plates[B] == null)
        {
            return;
        }

        //A 움직임에 변화가 있을경우
        if (Player_A.transform.position != Plates[A].transform.position + new Vector3(0.1f, 0, 0))
        {
            //움직였으니 올라타기 초기화
            TopOnEnemy_A = false;

            //A가 움직였는데, B가 올라타있지 않고 위치가 같을 경우
            if ( A == B && TopOnEnemy_B == false)
            {
                //올라탐
                TopOnEnemy_A = true;
            }

            //B가 A에 올라타있으면
            if (TopOnEnemy_B == true)
            {
                //덩굴 체크와 이동
                B = BindCheck(B, A);
            }

            //움직임
            Player_A.transform.position = Plates[A].transform.position + new Vector3(0.1f, 0, 0);
        }

        //B 움직임에 변화가 있을경우
        if (Player_B.transform.position != Plates[B].transform.position - new Vector3(0.1f, 0, 0))
        {
            //움직였으니 올라타기 초기화
            TopOnEnemy_B = false;

            //A가 움직였는데, B가 올라타있지 않고 위치가 같을 경우
            if (A == B && TopOnEnemy_A == false)
            {
                //올라탐
                TopOnEnemy_B = true;
            }

            //A가 B 올라타있으면
            if (TopOnEnemy_A == true)
            {
                //덩굴 체크와 이동
                A = BindCheck(A, B);
            }

            //움직임
            Player_B.transform.position = Plates[B].transform.position - new Vector3(0.1f, 0, 0);
        }
    }

    int BindCheck(int Upper, int Downer)
    {
        //덩굴이 5-6, 11-12 사이에 있을 경우
        if(Downer >= 6 && Upper <= 5)
        {
            Upper = 5;
        }
        else if (Downer >= 12 && Upper <= 11)
        {
            Upper = 11;
        }
        else//덩굴 없을때
        {
            Upper = Downer;
        }

        return Upper;
    }
}
