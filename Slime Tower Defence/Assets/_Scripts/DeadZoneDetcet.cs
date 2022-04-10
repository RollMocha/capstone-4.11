using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZoneDetcet : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision) //몬스터와 충돌 감지, 플레이어 체력 감소
  {
     if (collision.collider.gameObject.CompareTag("Monster"))
     {
          HPManager.CurrentHP=HPManager.CurrentHP - 1;
          Debug.Log("CurrentHP= " + HPManager.CurrentHP);
     }
  }
}
