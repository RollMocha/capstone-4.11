using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1 : MonoBehaviour
{
    public int enemy_hp = 10;
    public float defaultSpeed = 10f;//몬스터 속도

    public float destroy_time = 0.1f;//최종도착과 디스폰 사이 시간
    public Transform[] fruits;//
    public int fruitspawnrandom = 20;

    Rigidbody E1_rigidbody; //Rigidbody를 저장하는 변수
    public int rotatespeed = 5; //회전속도
    public int fruitsindex = 3;
    private Transform target;//Transform
    private int wavepointIndex = 0;//OneWaypoints의 인덱스
    private int Waypoint;
    private int wayPointCount; //이동 경로 갯수
    private RedFruitUI redFruitUI;
    private WaveSpawner waveSpawner;

    public float speed;
    float timer = 0;
    int slowPercent;
    int knockbackPower;
    float[] debuffCheckTimer;
    bool[] debuffCheck;

    public void Start()
    {
        Waypoint = UnityEngine.Random.Range(0, 2);

        fruits = new Transform[fruitsindex];

        switch (Waypoint)
        {
            case 0:
                target = OneWaypoints.opoints[0];//첫번째 OneWaypoint 설정
                break;
            case 1:
                target = TwoWaypoints.tpoints[0];//두번째 twoWaypoint 설정
                break;
        }


        speed = defaultSpeed;
        debuffCheckTimer = new float[3] { 0, 0, 0 };
        debuffCheck = new bool[3] { false, false, false };

        E1_rigidbody = GetComponent<Rigidbody>();
        //FixedUpdate();
    }

    private void Update()
    {
        
        DieCheck();
        
        DebuffCheck();
        Slow();
        Bondage();
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
        if (Waypoint == 0)
        {
            if (wavepointIndex >= OneWaypoints.opoints.Length - 1)//현재 목적지(wavepointIndex)가 마지막 목적지(Waypoints.points.Length -1)이라면
            {
                WaveSpawner.waveSpawner.DestroyEnemy(this); // 이 스크랩트를 가지고 있는 게임 객체를 파괴
                return;
            }

            wavepointIndex++;
            target = OneWaypoints.opoints[wavepointIndex];//목적지를 다음 목적지로 대입
        }
        else if (Waypoint == 1)
        {
            if (wavepointIndex >= TwoWaypoints.tpoints.Length - 1)//현재 목적지(wavepointIndex)가 마지막 목적지(Waypoints.points.Length -1)이라면
            {
                WaveSpawner.waveSpawner.DestroyEnemy(this); // 이 스크랩트를 가지고 있는 게임 객체를 파괴
                return;
            }

            wavepointIndex++;
            target = TwoWaypoints.tpoints[wavepointIndex];//목적지를 다음 목적지로 대입
        }
    }
    public void OnDie()
    {
        waveSpawner.DestroyEnemy(this);
    }

    public void Damage(int damage)
    {
        enemy_hp -= damage;
    }

    public void DieCheck()
    {
        if (enemy_hp <= 0)
        {
            WaveSpawner.waveSpawner.EnemyList_1.Remove(this);
            Destroy(this.gameObject);
        }
    }

    // 슬로우 디버프
    public void SlowDebuff(int slowPercent_, int slowTime)
    {
        debuffCheckTimer[0] = slowTime;
        debuffCheck[0] = true;
        slowPercent = slowPercent_;
    }

    // 슬로우 실행
    void Slow()
    {
        if (!debuffCheck[0])
        {
            return;
        }

        if (slowPercent <= 0)
        {
            return;
        }

        if (speed < defaultSpeed || speed <= 0)
        {
            return;
        }

        speed = speed / slowPercent;
    }

    // 속박 디버프
    public void BondageDebuff(int stopTime)
    {
        debuffCheckTimer[1] = stopTime;
        debuffCheck[1] = true;
        Debug.Log("Bondage Start");
    }

    // 속박 실행
    void Bondage()
    {
        if (!debuffCheck[1])
        {
            return;
        }

        if (speed <= 0)
        {
            return;
        }

        speed = 0;
    }

    // 넉백 디버프
    public void KnockBackDebuff(Vector3 bulletPosition)
    {

        Vector3 knockbackPosition = transform.position - bulletPosition;
        knockbackPosition = knockbackPosition.normalized;
        knockbackPosition += Vector3.back;

        transform.Translate(knockbackPosition * 5);

        Debug.Log("NnockBack");
    }

    void KnockBack()
    {
        if (!debuffCheck[1])
        {
            return;
        }

        if (speed <= 0)
        {
            return;
        }

        speed = 0;
    }

    public void DebuffCheck()
    {
        if (!debuffCheck[0] && !debuffCheck[1] && !debuffCheck[2])
        {
            speed = defaultSpeed;
            return;
        }

        for (int i = 0; i < debuffCheckTimer.Length; i++)
        {
            if (!debuffCheck[i])
            {
                continue;
            }

            debuffCheckTimer[i] -= Time.deltaTime;
            
            if (debuffCheckTimer[i] <= 0)
            {
                debuffCheckTimer[i] = 0;
                debuffCheck[i] = false;
            }
        }
    }
}