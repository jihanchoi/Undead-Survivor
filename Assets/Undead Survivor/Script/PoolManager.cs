using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] prefab;
    List<GameObject>[] prefabList;

    private void Awake()
    {
        prefabList = new List<GameObject>[prefab.Length];
        for(int i = 0; i < prefabList.Length; i++)
            prefabList[i] = new List<GameObject>();
    }

    public GameObject GetPrefab(int i)
    {
        GameObject select = null;
        //Monster를 생성할 때 비활성화된 Monster가 있으면 재활용
        foreach (GameObject Prefab in prefabList[i])
        {
            if(!Prefab.activeSelf)
            {
                select = Prefab;
                select.SetActive(true);
                break;
            }
        }
        if(!select)
        {
            select = Instantiate(prefab[i], transform);
            prefabList[i].Add(select);
        }
        return select;
    }
}
