using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    float spawnTimer;
    int level;

    private void Update()
    {
        //���� �ð� ���� ���͸� ����
        spawnTimer += Time.deltaTime;
        if(spawnTimer > 1f)
        {
            MonsterSpawn();
            spawnTimer = 0f;
        }
    }

    void MonsterSpawn()
    {
        //������ ���͸� Ư���� ��ġ�� ����
        GameObject monster = GameManager.instance.poolManager.MonsterGet
             (Random.Range(0, GameManager.instance.poolManager.monsterPrefab.Length));
        monster.transform.position = SpawnPosition();
    }

    public Vector3 SpawnPosition()
    {
        float vel = GameManager.instance.areaLengh;

        //������ǥ�� �������� �Ÿ� ���ϱ�
        float spawnDis = Random.Range(-vel, vel);

        //������ǥ�� ������ ���� ���ϱ�
        float spawnPoint = Random.value > 0.5 ? vel : -vel; //��:�� , ��:��
        Vector3 spawnDefense = Random.value > 0.5 ?         //��:�� , ��:��
            new Vector3(spawnPoint, spawnDis) : new Vector3(spawnDis, spawnPoint);

        return spawnDefense + transform.position;
    }
}
