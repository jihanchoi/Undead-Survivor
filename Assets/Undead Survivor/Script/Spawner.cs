using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public LevelSpawnData[] levelData;

    float spawnTimer;
    int level;

    private void Update()
    {
        //���� �ð� ���� ���͸� ����
        spawnTimer += Time.deltaTime;
        level = Mathf.FloorToInt(GameManager.instance.gameTime / 10f);
        if(spawnTimer > levelData[level].spawnTime)
        {
            MonsterSpawn();
            spawnTimer = 0f;
        }
    }

    void MonsterSpawn()
    {
        //������ ���͸� Ư���� ��ġ�� ����
        GameObject monster = GameManager.instance.poolManager.GetPrefab(0);
        monster.transform.position = SpawnPosition();
        monster.GetComponent<Monster>().Init(levelData[level]);
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
[System.Serializable]
public class LevelSpawnData
{
    public int spriteType;
    public float spawnTime;
    public int health;
    public float speed;
}
