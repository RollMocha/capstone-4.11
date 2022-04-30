using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isRoad;
    public bool isSlime = false; // 슬라임이 있는지 확인

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 GetPosition()
    {
        Vector3 tilePosition = this.gameObject.transform.position;
        return tilePosition;
    }

    public bool SlimeCheck()
    {
        return isSlime;
    }
}
