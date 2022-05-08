using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSpawnManager : MonoBehaviour
{
    List<Slime> slimePrefabList;

    List<Slime> slimeOnTile;

    Slime firstSlime;
    Slime secondSlime;

    static public SlimeSpawnManager slimeSpawnManager;

    private void Awake()
    {
        slimeSpawnManager = this;
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

    void AddSlimeAtList(Slime slime)
    {
        slimeOnTile.Add(slime);
    }

    // 상위 슬라임을 만들 때 실행
    void FindSlimeStateAtList(SlimeState firstSlimeState, SlimeState secondSlimeState)
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
