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
        //Monster�� ������ �� ��Ȱ��ȭ�� Monster�� ������ ��Ȱ��
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
