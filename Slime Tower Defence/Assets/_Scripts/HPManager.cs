using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPManager : MonoBehaviour
{
    static public HPManager hPManager;

    static public int CurrentHP; //플레이어 현재 체력
    public int MaxHP = 20; //플레이어 최대 체력
    string CurrentHPString = CurrentHP.ToString();

    public GameObject GameOverobj;
    public Text PlayerHP;

    private void Awake()
    {
        hPManager = this;
    }

    void Start()
    {
        CurrentHP = MaxHP; //현재체력을 최대체력으로 초기화
    }

    void Update()
    {
        CurrentHPString = CurrentHP.ToString();
        PlayerHP.text = "HP: " + CurrentHPString;
        //화면에 현재체력 표시
        CheckGameOver();//게임오버 판별
    }

    void CheckGameOver() //플레이어 체력이 0 이하로 내려가면 게임오버
    {
        if (CurrentHP < 0)
        {
            GameOverobj.SetActive(true);//게임오버 버튼 및 텍스트 활성화
        }
    }
}
