using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPManager : MonoBehaviour
{
    static public int CurrentHP; //플레이어 현재 체력
    public int MaxHP = 20; //플레이어 최대 체력
    string CurrentHPString = CurrentHP.ToString();

    public Text PlayerHP;

   void Start()
    {
        CurrentHP = MaxHP; //현재체력을 최대체력으로 초기화
    }

    void Update()
    {
        CurrentHPString = CurrentHP.ToString();
        PlayerHP.text = "HP: " + CurrentHPString;
        //화면에 현재체력 표시
    }
}
