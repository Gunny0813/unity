using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per; 

    Rigidbody2D rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();     
    }

    [System.Obsolete]
    public void Init(float damage,int per,Vector3 dir)
    {
        this.damage = damage; 
        this.per = per;
        
        if(per >-1)
        {
            rigid.velocity = dir *15f;
        }
    }

    
}
