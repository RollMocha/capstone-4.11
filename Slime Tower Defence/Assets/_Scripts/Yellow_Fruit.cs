using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Yellow_Fruit : MonoBehaviour
{
    private float spawntime;

    void Start()
    {
    }

    void Update()
    {
        spawntime += Time.deltaTime;//카운트 다운

        DestroyFruits();
    }

    private void DestroyFruits()
    {
        if (spawntime >= 1)//1초 넘게 된다면 사라짐
        {
            Destroy(gameObject);      
        }
    }
}
