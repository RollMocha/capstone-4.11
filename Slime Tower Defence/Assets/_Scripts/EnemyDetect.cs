using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 범위에 적이 있는지 확인하는 코드
public class EnemyDetect : MonoBehaviour
{
    public List<GameObject> enemyList; // 적 리스트

    void Start()
    {
        Renderer renderer;
        renderer = GetComponent<Renderer>();
        Color color = renderer.material.color;
        color.a = 0.3f;
        renderer.material.color = color;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // 적이 범위에 들어오면 정보 추가
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("other enter tag : " + other.tag);

        if (other.tag == "Monster")
        {
            enemyList.Add(other.gameObject);
        }
    }

    // 적이 범위에서 나가면 정보 제거
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("other exit tag : " + other.tag);

        if (other.tag == "Monster")
        {
            enemyList.Remove(other.gameObject);
        }
    }

    // 제일 먼저 들어온 적 정보 가져오기
    public GameObject GetTarget()
    {
        return enemyList[0];
    }

    // 적이 있는지 확인
    public bool EnemyDetectCheck()
    {
        Debug.Log(enemyList.Count);

        if (enemyList.Count <= 0)
        {
            return false;
        }

        return true;
    }
}
