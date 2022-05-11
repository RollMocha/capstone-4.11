using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromoteSlimeSpawnManager : MonoBehaviour
{
    List<Slime> slimePrefabList;

    List<Slime> slimeOnTile;

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

    // 배치하려는 슬라임과 배치에 필요한 슬라임 확인
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
    public int CheckCanBulidPromoteSlime(SlimeState firstSlimeState, SlimeState secondSlimeState)
    {
        Slime firstSlime = null;
        Slime secondSlime = null;

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
}
