using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour//적 생성하는 코드
{
    public Transform[] enemyPrefab_1 = new Transform[5];//Monster_1Prefab의 Transform
    public Transform[] enemyPrefab_2 = new Transform[5];//Monster_2Prefab의 Transform
    public Transform spawnPoint;//MonsterSpawnWaypoint(적 스폰 GameObject)의 Transform
    public float timeBetweenWaves = 5f;//적들이 생성되는 시간 간격

    private float countdown = 2f;//시간
    private int waveIndex = 0;//스테이지 번호 

    private void Update()//5초마다 SpawnWave() 함수를 호출
    {
        if (countdown <= 0f)//countdown이 0인 경우 SpawnWave()를 호출하고 countdown은 저희가 선언한  5f로 초기화
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }
    }

    IEnumerator SpawnWave()// waveIndex 만큼  0.3초마다 SpawnEnemy_1()과 SpawnEnemy_2를 호출
    {
        waveIndex++;
        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy_1();
            SpawnEnemy_2();
            yield return new WaitForSeconds(0.5f);//0.5f동안 기다리는 함수
        }

    }

    private void SpawnEnemy_1()//Monster_1Prefab 생성
    {
        int enemy_random = UnityEngine.Random.Range(0, 5);
        Instantiate(enemyPrefab_1[enemy_random], spawnPoint.position, spawnPoint.rotation);
    }
    private void SpawnEnemy_2()//Monster_2Prefab 생성
    {
        int enemy_random = UnityEngine.Random.Range(0, 5);
        Instantiate(enemyPrefab_2[enemy_random], spawnPoint.position, spawnPoint.rotation);
    }
}
