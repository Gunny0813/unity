using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per; //���뺯��

    public void Init(float damage,int per)
    {
        this.damage = damage; //this�� �ش� Ŭ������ ������ ���� �� this.damage�� ���� ������ ����
        this.per = per;
    }

    
}
