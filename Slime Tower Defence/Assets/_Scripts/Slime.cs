using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SlimeState
{
    DEFAULT,
    ICE,
    FIRE,
    THUNDER,
    VINE,
    WATER,
    WIND
};

public class Slime : MonoBehaviour
{
    bool isAttack = true; // 공격이 가능한지 확인

    public EnemyDetect enemyDetect; // 적 확인을 위한 컴포넌트
    public List<GameObject> enemyList; // 적 리스트
    public GameObject bulletPrefab; // 공격 프리팹

    GameObject targetEnemy = null; // 목표 적 정보

    public float attackSpeed; // 공격 속도
    public float bulletSpeed; // 투사체 속도

    float timer = 0; // 공격 속도 조절을 위해 사용

    public SlimeState state; // 슬라임의 종류 확인
    Tile tile; // 본인이 소환된 타일 정보

    void Start()
    {

    }

    void Update()
    {
        timer += Time.deltaTime; // 공격속도를 위해 사용

        if (enemyList.Count > 0)
        {
            if (targetEnemy == null)
            {
                targetEnemy = FindEnemyClosestToTower();
            }

            RotateToTarget(targetEnemy);

            if (timer > attackSpeed)
            {
                Attack(targetEnemy);
                timer = 0;
            }
        }
    }

    // 공격을 위한 함수
    void Attack(GameObject target)
    {
        if (isAttack) // 공격이 가능한지 확인
        {
            //isAttack = false; // 공격이 이미 실행 중이므로 막기

            //RotateToTarget(target); // 적 바라보기
            GameObject bullet = Instantiate(bulletPrefab, new Vector3(transform.position.x,
                transform.position.y, transform.position.z), Quaternion.identity); // 공격 프리팹 생성

            bullet.GetComponent<Bullet>().SetTarget(target, bulletSpeed); // 공격 프리팹에 적 정보 전달

            //isAttack = true; // 공격 종료
        }

    }

    // 적을 바라보는 함수
    void RotateToTarget(GameObject target)
    {
        Vector3 targetPosition = new Vector3(target.transform.position.x, transform.position.y, 
            target.transform.position.z);

        transform.LookAt(targetPosition);
    }

    // 본인이  배치된 타일의 정보를 설정
    public void AttachTileInfomation(Tile tile_)
    {
        if (tile_ == null)
        {
            Debug.Log("no tile infomation");
            return;
        }

        tile = tile_;
    }

    public void ChangeTileCheckInfomation()
    {
        tile.isSlime = false;
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
                    if (other.gameObject == targetEnemy)
                    {
                        targetEnemy = null;
                    }
                    enemyList.Remove(other.gameObject);
                    break;
                }
            }
        }
    }

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
