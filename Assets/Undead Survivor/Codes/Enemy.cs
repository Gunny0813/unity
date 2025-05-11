using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float speed;
    public float health;
    public float maxHealth;
    public RuntimeAnimatorController[] animCon;


    Animator animator;
    public Rigidbody2D target;//���������� ���󰡴� ��ǥ�� ����
    Rigidbody2D rigid;
    SpriteRenderer spriter;
    bool islive;
    void Awake()
    {
        animator = GetComponent<Animator>();    
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
    }

    
    void FixedUpdate()
    {
        if (!islive)
            return;
        
        Vector2 dirVec = target.position - rigid.position;//Ÿ�� ��ġ - ���� ��ġ = ��ġ����
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime; // ������ ��ġ normallized�� �������ͷ� �������
        rigid.MovePosition(rigid.position+nextVec);
        rigid.linearVelocity = Vector2.zero;//������ Rigidbody2D�� ���� �ִ� velocity�� �ִٸ�,
        //MovePosition �ܿ��� �� velocity �������� ��� �̵��Ϸ��� �ϰ� �˴ϴ�.
    }

    void LateUpdate()//update�� ������ ���� ���������� �Ѿ�� ����Ǵ� �����ֱ�
    {
        if (!islive)
            return;
        spriter.flipX = target.position.x < rigid.position.x;
    }

    void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        islive = true;
        health = maxHealth;
    }

    public void Init(SpawnData data)
    {
        animator.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //collision�� �ε��� ������Ʈ�� ������ ��� ����
        if (!collision.CompareTag("Bullet"))

        {
            return;
        }
        health -=collision.GetComponent<Bullet>().damage;
        if(health > 0)
        {
            //live
        }
        else
        {
            //die
            Dead();
        }
    }
    void Dead()
    {
        gameObject.SetActive(false);
    }
}
