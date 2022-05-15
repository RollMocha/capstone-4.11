using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 범위에 적이 있는지 확인하는 코드
public class EnemyDetect : MonoBehaviour
{
    public List<GameObject> enemyList; // 적 리스트
    public Slime parentTower; // 부모의 타워
    public GameObject target;
    //Queue<GameObject> enemyList;


    void Start()
    {
        enemyList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyList.Count <= 0)
        {

        }
    }

    // 적이 범위에 들어오면 정보 추가
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Monster")
        {
            enemyList.Add(other.gameObject);
        }
    }

    // 적이 범위에서 나가면 정보 제거
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Monster")
        {
            foreach (GameObject go in enemyList)
            {
                if (go == other.gameObject)
                {
                    enemyList.Remove(other.gameObject);
                    break;
                }
            }
        }
    }

    // 제일 먼저 들어온 적 정보 가져오기
    public GameObject GetTarget()
    {
        return FindEnemyClosestToTower();
    }

    // 적이 있는지 확인
    public bool EnemyDetectCheck()
    {

        if (enemyList.Count <= 0)
        {
            return false;
        }

        return true;
    }

    // 타워와 가장 가까운 적 찾기
    public GameObject FindEnemyClosestToTower()
    {
        GameObject target_ = null;
        float minDir = -1;

        foreach (GameObject enemy in enemyList)
        {
            // 아이템이 들어있는 리스트에서 아이템과 플레이어의 거리를 계산
            float dir = Vector3.Distance(transform.position, enemy.transform.position);

            // 첫 계산 또는 최소 거리보다 가까우면 해당 아이템으로 변경
            if (minDir > dir || minDir == -1)
            {
                minDir = dir;
                target_ = enemy;
            }
        }

        return target_;
    }
}

