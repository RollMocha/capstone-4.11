using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1 : MonoBehaviour
{
    public int enemy_hp = 10;
    public float speed = 10f;//몬스터 속도
    public float destroy_time = 0.1f;//최종도착과 디스폰 사이 시간
    public Transform[] fruits = new Transform[fruitsindex];//
    public int fruitspawnrandom = 20;

    Rigidbody E1_rigidbody; //Rigidbody를 저장하는 변수
    public int rotatespeed = 5; //회전속도
    private static int fruitsindex = 3;
    private Transform target;//Transform
    private int wavepointIndex = 0;//OneWaypoints의 인덱스

    void Start()
    {
        target = OneWaypoints.opoints[0];//첫번째 OneWaypoint 설정
        E1_rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        Vector3 dir = target.position - transform.position;// 목적지 방향을 구하는 식
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);//이동관련 함수

        if (Vector3.Distance(transform.position, target.position) <= 0.4f)//Vector3 a(transform.position)와 Vector3 b(target.position)의 사이의 거리가 0.4f보다 낮으면...
        {
            GetNextWaypoint();//다음 목적지 탐색하는 함수
        }

        Quaternion newRotation = target.rotation;
        E1_rigidbody.rotation = Quaternion.Slerp(E1_rigidbody.rotation, newRotation,
            rotatespeed * Time.deltaTime);
        //몬스터가 이동하는 방향으로 회전(바라봄)
    }

    private void GetNextWaypoint()
    {
        if (wavepointIndex >= OneWaypoints.opoints.Length - 1)//현재 목적지(wavepointIndex)가 마지막 목적지(Waypoints.points.Length -1)이라면
        {
            Destroy(gameObject, destroy_time);//이 스크랩트를 가지고 있는 게임 객체를 파괴
            return;
        }

        wavepointIndex++;
        target = OneWaypoints.opoints[wavepointIndex];//목적지를 다음 목적지로 대입
    }

    private void SpawnFruit()
    {
        int random = UnityEngine.Random.Range(0, 100);


        if (random < fruitspawnrandom)
        {
            int fruit_random = UnityEngine.Random.Range(0, 2);

            Instantiate(fruits[fruit_random], transform.position, transform.rotation);
        }
    }
}