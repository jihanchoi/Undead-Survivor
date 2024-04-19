using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    float spawnTimer;
    int level;

    private void Update()
    {
        //일정 시간 마다 몬스터를 생성
        spawnTimer += Time.deltaTime;
        if(spawnTimer > 1f)
        {
            MonsterSpawn();
            spawnTimer = 0f;
        }
    }

    void MonsterSpawn()
    {
        //랜덤한 몬스터를 특정한 위치에 생성
        GameObject monster = GameManager.instance.poolManager.MonsterGet
             (Random.Range(0, GameManager.instance.poolManager.monsterPrefab.Length));
        monster.transform.position = SpawnPosition();
    }

    public Vector3 SpawnPosition()
    {
        float vel = GameManager.instance.areaLengh;

        //스폰좌표가 펼쳐지는 거리 구하기
        float spawnDis = Random.Range(-vel, vel);

        //스폰좌표로 설정할 방위 구하기
        float spawnPoint = Random.value > 0.5 ? vel : -vel; //동:서 , 북:남
        Vector3 spawnDefense = Random.value > 0.5 ?         //북:동 , 남:서
            new Vector3(spawnPoint, spawnDis) : new Vector3(spawnDis, spawnPoint);

        return spawnDefense + transform.position;
    }
}
