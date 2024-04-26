using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] monsterPrefab;
    List<GameObject>[] monsterList;

    private void Awake()
    {
        monsterList = new List<GameObject>[monsterPrefab.Length];
        for(int i = 0; i < monsterList.Length; i++)
            monsterList[i] = new List<GameObject>();
    }

    public GameObject GetPrefab(int i)
    {
        GameObject select = null;
        //Monster�� ������ �� ��Ȱ��ȭ�� Monster�� ������ ��Ȱ��
        foreach (GameObject monster in monsterList[i])
        {
            if(!monster.activeSelf)
            {
                select = monster;
                select.SetActive(true);
                break;
            }
        }
        if(!select)
        {
            select = Instantiate(monsterPrefab[i], transform);
            monsterList[i].Add(select);
        }
        return select;
    }
}
