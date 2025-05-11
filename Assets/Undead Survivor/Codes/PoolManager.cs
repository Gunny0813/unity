using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    //��������� ������ ����:N
    public GameObject[] prefabs;

    //Ǯ ����� �ϴ� ����Ʈ��:N
    List<GameObject>[] pools;
    //List<GameObject>�� int�� ���� �ϳ��� Ÿ������ �� �� ����
    //�� ���� pools ���������� GameObject���� ��� List���� ���� �� �ִ� �迭 pools���� ����

    void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];
        //����Ʈ ���� �迭�� �Ҵ��ϱ� ���� new�� �ʿ���

        for (int index=0; index<pools.Length; index++)
        {
            pools[index] = new List<GameObject>();
            //�迭�ϳ��� GameObject�� ��� List �Ҵ�.

        }
        
    }

    public GameObject Get(int index)
    {
        GameObject select = null;
        //... ������ Ǯ�� ��� �ִ� (��Ȱ��ȭ��)���ӿ�����Ʈ ����
        //foreach���� pool[index]�ȿ� �ִ� GameObject����ŭ �ݺ���
        foreach(GameObject item in pools[index])
        {
            if (!item.activeSelf)
            {
                //...�߰��ϸ� select������ �Ҵ�
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
        

        //...�� ã���� -> ���Ӱ� �����ϰ� select�� �Ҵ�
        return select;
    }
    //���� ������Ʈ ��ȯ�ϴ� �Լ� ����


}
