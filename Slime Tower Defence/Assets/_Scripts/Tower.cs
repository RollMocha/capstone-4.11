using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    float attackTimer;

    bool isAttack = true; // 공격이 가능한지 확인

    public EnemyDetect enemyDetect; // 적 확인을 위한 컴포넌트
    public GameObject bulletPrefab; // 공격 프리팹

    GameObject targetEnemy = null; // 적 정보

    public float attackSpeed = 2f; // 공격 속도

    float timer = 0;

    void Start()
    {
        
    }

    void Update()
    {
        timer += Time.deltaTime;

        // 적이 범위안에 있는지를 매번 체크
        if (enemyDetect.EnemyDetectCheck())
        {
            targetEnemy = enemyDetect.GetTarget(); // 적이 범위안에 있으면 추가
            if (targetEnemy != null)
            {
                // 적을 바라봄
                RotateToTarget(targetEnemy);

                
                if (timer > attackSpeed)
                {
                    Attack(targetEnemy);
                    timer = 0;
                }
            }
        }
    }

    IEnumerator AttackCheck()
    {
        yield return new WaitForSeconds(attackSpeed);

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

            bullet.GetComponent<Bullet>().SetTarget(target); // 공격 프리팹에 적 정보 전달

            //isAttack = true; // 공격 종료
        }

    }

    // 적을 바라보는 함수
    void RotateToTarget(GameObject target)
    {
        transform.LookAt(target.transform);
    }
}
