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
        //캐릭터 별 기본 무기 index는 무기 넘버
        items[index].OnClick();
    }

    void Next()
    {
        foreach (Item item in items)
        {
            item.gameObject.SetActive(false);
        }

        int[] ran = new int[ranLength]; //활성화 시킬 아이템의 갯수만큼 중복없는 랜덤 숫자 생성
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
        if(ran.Length > 1)  //활성화 시킬 아이템 관리를 용이하게 하기위한 오름차순 정리
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
            if (ran[index] >= itemLength) //아이템의 종류가 줄어 마지막 index가 배열의 범위를 벗어나질 경우
            {
                int lastRan = Random.Range(0, itemLength);   //마지막 값을 다시 설정 및 중복 제거
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

            Item ranItem = items[ran[index]];   //활성화 시킬 아이템을 배정

            if(ranItem.level == ranItem.data.damages.Length)    //배정받은 아이템이 더이상 레벨업할 수 없는 경우 배열에서 제거 및 배열의 길이 조정
            {
                items = items.Where(item => item != items[ran[index]]).ToArray();
                itemLength--;
                ranItem = items[ran[index]];
                if (ranLength > itemLength)     //활성화 할 아이템의 수가 활성화 할 수 있는 아이템의 수 보다 많을 경우의 오류 방지
                {
                    ranLength = itemLength;
                }
            }

            if(ranItem != null)
                ranItem.gameObject.SetActive(true); //아이템 활성화
        }
    }
}
