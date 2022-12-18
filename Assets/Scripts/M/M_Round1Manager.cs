using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Round1Manager : MonoBehaviour
{
    int brokenMachine = 0; //부숴진 장치 수
    public GameObject portal; //씬 이동 포탈
    public static M_Round1Manager instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
    }

    
    void Update()
    {
        SetMission();
    }

    void SetMission()
    {
        string missionText = "장치를 파괴하세요\n(" + brokenMachine + "/3)";
        GameManager.instance.MissionChange("Main", missionText);
    }

    void Round1Set()
    {
    }

    public void BreakMachine()
    {
        brokenMachine++;
        if (brokenMachine == 3)
        {
            portal.SetActive(true);
        }
    }
}
