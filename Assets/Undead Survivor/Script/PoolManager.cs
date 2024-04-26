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
        //Monster를 생성할 때 비활성화된 Monster가 있으면 재활용
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
