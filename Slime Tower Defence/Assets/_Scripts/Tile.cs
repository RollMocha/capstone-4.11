using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isRoad;
    public bool isSlime = false; // 슬라임이 있는지 확인
    public Tower attachSlime; // 배치된 슬라임

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 타일의 위치정보 전달, Drag 이벤트에 사용
    public Vector3 GetPosition()
    {
        Vector3 tilePosition = this.gameObject.transform.position;
        return tilePosition;
    }

    // 슬라임이 있는지 체크
    public bool SlimeCheck()
    {
        return isSlime;
    }
}
