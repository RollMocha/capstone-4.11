using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSpawnManager : MonoBehaviour
{
    List<Slime> slimePrefabList;

    static public SlimeSpawnManager slimeSpawnManager;

    private void Awake()
    {
        slimeSpawnManager = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
