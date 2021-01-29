using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoloModeManager : MonoBehaviour
{

    #region Singleton
    public static SoloModeManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    public int turnCount = 0;

    static int animalCnt = 7;
    public static int[] Animals = new int[animalCnt];

    // Use this for initialization
    void Start()
    {
        turnCount = 0;

        for (int i = 0; i < animalCnt; i++)
        {
            Animals[i] = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}