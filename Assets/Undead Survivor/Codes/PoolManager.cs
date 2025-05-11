using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    //프리펩들을 보관할 변수:N
    public GameObject[] prefabs;

    //풀 담당을 하는 리스트들:N
    List<GameObject>[] pools;
    //List<GameObject>는 int와 같은 하나의 타입으로 볼 수 있음
    //즉 위의 pools 변수선언은 GameObject들을 담는 List들을 담을 수 있는 배열 pools변수 선언

    void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];
        //리스트 담을 배열을 할당하기 위해 new가 필요함

        for (int index=0; index<pools.Length; index++)
        {
            pools[index] = new List<GameObject>();
            //배열하나에 GameObject를 담는 List 할당.

        }
        
    }

    public GameObject Get(int index)
    {
        GameObject select = null;
        //... 선택한 풀의 놀고 있는 (비활성화된)게임오브젝트 접근
        //foreach문은 pool[index]안에 있는 GameObject수만큼 반복됨
        foreach(GameObject item in pools[index])
        {
            if (!item.activeSelf)
            {
                //...발견하면 select변수에 할당
                select = item;
                select.SetActive(true);
                break;
            }
        }
        if(select == null) 
        {
            select = Instantiate(prefabs[index],transform);
            pools[index].Add(select);
        }
        

        //...못 찾으면 -> 새롭게 생성하고 select에 할당
        return select;
    }
    //게임 오브젝트 반환하는 함수 선언


}
