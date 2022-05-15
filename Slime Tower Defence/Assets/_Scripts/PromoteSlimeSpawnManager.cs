using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 상위 슬라임 소환을 위해 맵에 배치된 슬라임 정보를 관리하는 매니저 클래스
public class PromoteSlimeSpawnManager : MonoBehaviour
{
    List<Slime> slimeOnTile; // 맵에 배치된 슬라임 리스트

    static public PromoteSlimeSpawnManager promoteSlimeSpawnManager;

    private void Awake()
    {
        promoteSlimeSpawnManager = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        slimeOnTile = new List<Slime>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddSlimeAtList(Slime slime)
    {
        slimeOnTile.Add(slime);
    }

    // 배치할 상위 슬라임에 필요한 슬라임 구분 
    public int CheckPromoteSlime(SlimeState buildSlimeState)
    {
        int checkNum; // 함수가 성공했는지 확인을 위한 변수

        switch (buildSlimeState) // 소환할 슬라임에 맞는 함수 실행
        {
            case SlimeState.VINE: // 넝쿨 슬라임 : 얼음 슬라임과 번개 슬라임
                checkNum = CheckCanBulidPromoteSlime(SlimeState.ICE, SlimeState.THUNDER);
                break;
            case SlimeState.WATER: // 물 슬라임 : 불 슬라임과 얼음 슬라임
                checkNum = CheckCanBulidPromoteSlime(SlimeState.FIRE, SlimeState.WATER);
                break;
            case SlimeState.WIND: // 바람 슬라임 : 불 슬라임과 번개 슬라임
                checkNum = CheckCanBulidPromoteSlime(SlimeState.FIRE, SlimeState.THUNDER);
                break;
            default: // 정보가 없는 상위 슬라임이 들어올 경우
                Debug.Log("no information from PromoreSime");
                checkNum = -2;
                break;
        }

        return checkNum;
    }

    // 상위 슬라임을 만들 조건이 되는지 확인
    public int CheckCanBulidPromoteSlime(SlimeState firstSlimeState, SlimeState secondSlimeState)
    {
        Slime firstSlime = null; // 재료 슬라임 1
        Slime secondSlime = null; // 재료 슬라임 2

        foreach (Slime slime in slimeOnTile) // 리스트에서 소환에 필요한 슬라임 검색
        {
            if (slime.state == firstSlimeState)
            {
                firstSlime = slime;
                continue;
            }

            if (slime.state == secondSlimeState)
            {
                secondSlime = slime;
                continue;
            }
        }

        if (firstSlime == null || secondSlime == null) // 재료 슬라임이 하나라도 없는 경우
        {
            Debug.Log("no have correct Slime in Game");
            return -1;
        }
        // 슬라임 소환 조건 만족

        firstSlime.ChangeTileCheckInfomation();
        Destroy(firstSlime.gameObject);

        secondSlime.ChangeTileCheckInfomation();
        Destroy(secondSlime.gameObject);

        Debug.Log("Promote Slime in Game");
        return 1;
    }
}
