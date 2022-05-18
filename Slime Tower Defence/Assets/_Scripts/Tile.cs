using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isRoad;
    public bool isSlime = false; // 슬라임이 있는지 확인
    public Slime attachSlime; // 배치된 슬라임
    public Vector3 towerPosition; // 슬라임을 배치할 위치

    // Start is called before the first frame update
    void Start()
    {
        // 슬라임을 배치하기 위한 정보를 처음에 설정
        Vector3 tilePosition = transform.position;
        towerPosition = new Vector3(tilePosition.x, tilePosition.y + 3f,
            tilePosition.z);
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
    public bool CheckSlime()
    {
        return isSlime;
    }

    // 슬라임 배치
    public void SetSlime(Slime slimePrefab)
    {
        // 슬라임 정보가 있는지 확인
        if (slimePrefab == null)
        {
            return;
        }

        isSlime = true; // 슬라임 정보 변경
        attachSlime = slimePrefab;

    }

    // 슬라임 정보 가져오기
    public Slime GetSlime()
    {
        return attachSlime;
    }

    // 타일에 배치되어 있는 슬라임 제거
    public void DestroySlime()
    {
        PromoteSlimeSpawnManager.promoteSlimeSpawnManager.RemoveSlimeAtList(attachSlime.gameObject);
        Destroy(attachSlime.gameObject);
    }

}
