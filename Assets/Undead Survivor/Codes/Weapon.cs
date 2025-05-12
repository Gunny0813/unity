using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabid;
    public float damage;
    public int count;
    public float speed;
    
    float timer;
    Player player;

    void Start()
    {
        Init();    
    }
    void Awake()
    {
        player = GetComponentInParent<Player>();    
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
                timer += Time.deltaTime;
                if(timer > speed)
                {
                    timer = 0f;
                    Fire();
                }
                break;
        }
        if (Input.GetButtonDown("Jump"))
        {
            LevelUp(10, 1);
            Debug.Log("레벨업");
        }
    }
    public void LevelUp(float damage,int count)
    {
        this.damage = damage;
        this.count += count;
        if(id== 0)
        {
            Batch();
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
                speed = 0.3f;
                break;
        }
        
    }
    

    void Batch()
    {
        for(int index=0; index<count; index++)
        {
            Transform bullet;
            
            //bullet은 pool에서 가져오기에 poolmanager의 자식이됨. 그럼 위치를 따라가지 못함
            if(index<transform.childCount)
            {
                bullet = transform.GetChild(index);
            }
            else
            {
                bullet = GameManager.instance.pool.Get(prefabid).transform;
                bullet.parent = transform;
            }
            
            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * 360 * index / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World);
            bullet.GetComponent<Bullet>().Init(damage, -1,Vector3.zero);//-1 is infinity per


        }
    }

    void Fire()
    {
        if (!player.scanner.nearestTarget)
        {
            return;
        }
        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;//방향만을 나타내도록 단위벡터로 바꿈
        Transform bullet = GameManager.instance.pool.Get(prefabid).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Init(damage, count, dir);



    }
}

