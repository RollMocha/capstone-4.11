using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromoteSlimeSpawnManager : MonoBehaviour
{
    List<Slime> slimePrefabList;

    List<Slime> slimeOnTile;

    Slime firstSlime;
    Slime secondSlime;

    SlimeState firstSlimeState;
    SlimeState secondSlimeState;

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

    public int CheckPromoteSlime(SlimeState buildSlimeState)
    {
        int checkNum = -2;
        switch (buildSlimeState)
        {
            case SlimeState.VINE:
                checkNum = CheckCanBulidPromoteSlime(SlimeState.ICE, SlimeState.THUNDER);
                break;
            case SlimeState.WATER:
                checkNum = CheckCanBulidPromoteSlime(SlimeState.FIRE, SlimeState.WATER);
                break;
            case SlimeState.WIND:
                checkNum = CheckCanBulidPromoteSlime(SlimeState.FIRE, SlimeState.THUNDER);
                break;
        }

        return checkNum;
    }

    // 상위 슬라임을 만들 조건이 되는지 확인
    public int CheckCanBulidPromoteSlime(SlimeState firstSlimeState_, SlimeState secondSlimeState_)
    {
        firstSlime = null;
        secondSlime = null;

        firstSlimeState = firstSlimeState_;
        secondSlimeState = secondSlimeState_;

        foreach (Slime slime in slimeOnTile)
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

        if (firstSlime == null || secondSlime == null)
        {
            Debug.Log("no have correct Slime in Game");
            return -1;
        }

        Destroy(firstSlime.gameObject);
        Destroy(secondSlime.gameObject);

        Debug.Log("Promote Slime in Game");
        return 1;
    }

    // 상위 슬라임을 만들 때 실행
    void FindSlimeStateAtList()
    {
        foreach (Slime slime in slimeOnTile)
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
    }
}
