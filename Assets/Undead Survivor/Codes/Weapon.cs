using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabid;
    public float damage;
    public int count;
    public float speed;


    void Start()
    {
        Init();    
    }

    // Update is called once per frame
    void Update()
    {
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;

            default:
                break;
        }
    }

    public void Init()
    {
        switch (id)
        {
            case 0:
                speed = 150;
                Batch();
                break;
            
            default:
                break;
        }
    }

    void Batch()
    {
        for(int index=0; index<count; index++)
        {
            Transform bullet = GameManager.instance.pool.Get(prefabid).transform;
            //bullet�� pool���� �������⿡ poolmanager�� �ڽ��̵�. �׷� ��ġ�� ������ ����
            bullet.parent = transform;
            bullet.GetComponent<Bullet>().Init(damage, -1);//-1 is infinity per


        }
    }
}
