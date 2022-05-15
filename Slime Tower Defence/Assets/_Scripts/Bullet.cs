using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    GameObject attackTarget; // 공격해야할 적 정보
    public float destroyTime = 10f; // 공격이 맞지않을 경우 대비
    public float speed;

    public bool isSplash = false;

    void Start()
    {
        Destroy(this.gameObject, destroyTime);
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (attackTarget == null)
        {
            Destroy(this.gameObject, 0.1f);
        }
        else
        {
            MoveToTarget();
        }
    }

    // 적의 정보를 세팅하는 함수
    public void SetTarget(GameObject target, float bulletSpeed)
    {
        attackTarget = target;
        speed = bulletSpeed;
        
    }

    // 적을 향해 날라가는 함수
    void MoveToTarget()
    {
        if (attackTarget == null)
        {
            return;
        }

        if (speed <= 0)
        {
            return;
        }

        // 적 위치로 날라감
        transform.position = 
            Vector3.MoveTowards(this.transform.position, attackTarget.transform.position, speed);

    }

    // 적과 부딪힐 때 사라지는 함수
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Monster hit");

        if (other.tag == "Monster")
        {
            // 스플래쉬 데미지 구현
            Collider[] hitCol = Physics.OverlapSphere(transform.position, 3.0f);
            Debug.Log("Monster out");

            foreach (Collider hit in hitCol)
            {
                if (hit.gameObject.tag != "Monster") break;

                hit.gameObject.GetComponent<Enemy_1>().Damage(5);

                Debug.Log("Monster Damage");
            }

            // 스플래쉬 데미지 끝

            Destroy(this.gameObject, 0.1f);
        }
    }
}
