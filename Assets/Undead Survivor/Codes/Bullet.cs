using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per; //관통변수

    public void Init(float damage,int per)
    {
        this.damage = damage; //this는 해당 클래스의 변수로 접근 즉 this.damage는 위에 선언한 변수
        this.per = per;
    }

    
}
