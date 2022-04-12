using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZoneDetcet : MonoBehaviour
{
    void OnTriggerEnter(Collider other) //몬스터와 충돌 감지, 플레이어 체력 감소
    {
        if (other.tag == "Monster")
        {
            Destroy(other.gameObject);
            HPManager.CurrentHP = HPManager.CurrentHP - 1;
        }
    }
}
