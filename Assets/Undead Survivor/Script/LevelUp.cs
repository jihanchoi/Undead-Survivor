using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelUp : MonoBehaviour
{
    RectTransform rect;
    Item[] items;

    int itemLength;
    public int ranLength;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        items = GetComponentsInChildren<Item>(true);
        itemLength = items.Length;
    }

    public void Show()
    {
        rect.localScale = Vector3.one;
        GameManager.instance.Stop();
        Next();
    }

    public void Hide()
    {
        rect.localScale = Vector3.zero;
        GameManager.instance.Resume();
    }

    public void Select(int index)
    {
        //ĳ���� �� �⺻ ���� index�� ���� �ѹ�
        items[index].OnClick();
    }

    void Next()
    {
        foreach (Item item in items)
        {
            item.gameObject.SetActive(false);
        }

        int[] ran = new int[ranLength]; //Ȱ��ȭ ��ų �������� ������ŭ �ߺ����� ���� ���� ����
        for(int i = 0; i < ranLength; i++)
        {
            ran[i] = Random.Range(0, itemLength);
            if (i > 0)
            {
                for (int j = 0; j < i; j++)
                {
                    if (ran[i] == ran[j])
                    {
                        i--;
                    }
                }
            }
        }
        if(ran.Length > 1)  //Ȱ��ȭ ��ų ������ ������ �����ϰ� �ϱ����� �������� ����
        {
            for (int i = 0; i < ran.Length - 1; i++)
            {
                for(int j = i+1; j < ranLength; j++)
                {
                    if(ran[i] > ran[j])
                    {
                        int num = ran[j];
                        ran[j] = ran[i];
                        ran[i] = num;
                    }
                }
            }
        }

        for(int index = 0; index < ran.Length; index++)
        {
            if (ran[index] >= itemLength) //�������� ������ �پ� ������ index�� �迭�� ������ ����� ���
            {
                int lastRan = Random.Range(0, itemLength);   //������ ���� �ٽ� ���� �� �ߺ� ����
                if(ran.Length <= itemLength)
                {
                    while (ran.Contains(lastRan))
                    {
                        lastRan = Random.Range(0, itemLength);
                    }
                    ran[index] = lastRan;
                }
                else
                {
                    break;
                }

            }

            Item ranItem = items[ran[index]];   //Ȱ��ȭ ��ų �������� ����

            if(ranItem.level == ranItem.data.damages.Length)    //�������� �������� ���̻� �������� �� ���� ��� �迭���� ���� �� �迭�� ���� ����
            {
                items = items.Where(item => item != items[ran[index]]).ToArray();
                itemLength--;
                ranItem = items[ran[index]];
                if (ranLength > itemLength)     //Ȱ��ȭ �� �������� ���� Ȱ��ȭ �� �� �ִ� �������� �� ���� ���� ����� ���� ����
                {
                    ranLength = itemLength;
                }
            }

            if(ranItem != null)
                ranItem.gameObject.SetActive(true); //������ Ȱ��ȭ
        }
    }
}
