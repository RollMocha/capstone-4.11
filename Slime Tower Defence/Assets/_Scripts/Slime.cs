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

    Enemy_1 targetEnemy = null; // 목표 적 정보

    public float attackSpeed; // 공격 속도
    public float bulletSpeed; // 투사체 속도
    public float attackRange; // 공격 범위 
    public int attackDamage; // 공격력

    float timer = 0; // 공격 속도 조절을 위해 사용

    public SlimeState state; // 슬라임의 종류 확인
    Tile tile; // 본인이 소환된 타일 정보

    void Start()
    {

    }

    void Update()
    {
        timer += Time.deltaTime; // 공격속도를 위해 사용

        // 적이 맵에 있는지 확인
        if (WaveSpawner.waveSpawner.EnemyList_1.Count <= 0)
        {
            return;
        }

        // 추적 중인 적이 없으면 추가
        if (targetEnemy == null)
        {
            targetEnemy = FindEnemyClosestToTower();
            return;
        }
        else
        {
            // 적이 사거리 안에 있는지 확인
            float dir = Vector3.Distance(transform.position, targetEnemy.transform.position);

            if (dir > attackRange)
            {
                targetEnemy = null;
                return;
            }

            // 적 바라보기
            RotateToTarget(targetEnemy);

            // 공격 시간 체크
            if (timer > attackSpeed)
            {
                Attack(targetEnemy);
                timer = 0;
            }
        }
    }

    // 공격을 위한 함수
    void Attack(Enemy_1 target)
    {
        if (isAttack) // 공격이 가능한지 확인
        {
            //isAttack = false; // 공격이 이미 실행 중이므로 막기

            //RotateToTarget(target); // 적 바라보기
            GameObject bullet = Instantiate(bulletPrefab, new Vector3(transform.position.x,
                transform.position.y, transform.position.z), Quaternion.identity); // 공격 프리팹 생성

            bullet.GetComponent<Bullet>().SetTarget(target.gameObject, bulletSpeed, 
                attackDamage); // 공격 프리팹에 적 정보 전달

            //isAttack = true; // 공격 종료
        }

    }

    // 적을 바라보는 함수
    void RotateToTarget(Enemy_1 target)
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

    // 가장 가까운 적 추적
    public Enemy_1 FindEnemyClosestToTower()
    {
        Enemy_1 target_ = null; // 반환할 적 정보
        float minDir = -1; // 가장 가까운 거리 저장용

        // 적 리스트에서 가장 가까운 적 찾기
        foreach (Enemy_1 enemy in WaveSpawner.waveSpawner.EnemyList_1)
        {
            // 적과의 거리 계산
            float dir = Vector3.Distance(transform.position, enemy.transform.position);

            // 적이 사거리 안에 있는지 확인
            if (dir > attackRange)
            {
                continue;
            }

            // 적이 제일 가까이 있는지 확인
            if (dir > minDir)
            {
                minDir = dir;
                target_ = enemy;
            }
        }

        return target_; // 마지막으로 적 반환
    }
}
