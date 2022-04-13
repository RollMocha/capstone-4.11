using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    GameObject attackTarget; // 공격해야할 적 정보
    public float destroyTime = 5f; // 공격이 맞지않을 경우 대비

    void Start()
    {

    }

    void Update()
    {
        MoveToTarget();
    }

    // 적의 정보를 세팅하는 함수
    public void SetTarget(GameObject target)
    {
        attackTarget = target;
    }

    // 적을 향해 날라가는 함수
    void MoveToTarget()
    {
        if (attackTarget != null)
        {
            // 적 위치로 날라감
            transform.position = 
                Vector3.MoveTowards(this.transform.position, attackTarget.transform.position, 0.1f);
        }
        else
        {
            Destroy(this);
        }
    }

    // 적과 부딪힐 때 사라지는 함수
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Monster")
        {
            Destroy(this.gameObject);
        }
    }
}
