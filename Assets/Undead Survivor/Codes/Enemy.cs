using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float speed;
    public float health;
    public float maxHealth;
    public RuntimeAnimatorController[] animCon;


    Animator animator;
    public Rigidbody2D target;//물리적으로 따라가는 목표물 설정
    Rigidbody2D rigid;
    Collider2D col;
    SpriteRenderer spriter;
    WaitForFixedUpdate wait;
    bool islive;
    void Awake()
    {
        animator = GetComponent<Animator>();    
        rigid = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();   
        spriter = GetComponent<SpriteRenderer>();
        wait = new WaitForFixedUpdate();
    }

    
    void FixedUpdate()
    {
        if (!islive || animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            return;
        
        Vector2 dirVec = target.position - rigid.position;//타겟 위치 - 나의 위치 = 위치차이
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime; // 가야할 위치 normallized는 단위벡터로 만들어줌
        rigid.MovePosition(rigid.position+nextVec);
        rigid.linearVelocity = Vector2.zero;//이전에 Rigidbody2D에 남아 있는 velocity가 있다면,
        //MovePosition 외에도 그 velocity 방향으로 계속 이동하려고 하게 됩니다.
    }

    void LateUpdate()//update가 끝난후 다음 프레임으로 넘어갈떄 시행되는 생명주기
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
        islive = true;
        col.enabled = true;
        rigid.simulated = true;
        spriter.sortingOrder = 2;
        animator.SetBool("Dead", false);

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
        //collision은 부딪힌 오브젝트의 정보를 담고 있음
        if (!collision.CompareTag("Bullet")|| !islive)

        {
            return;
        }
        health -=collision.GetComponent<Bullet>().damage;
        StartCoroutine(KnonkBack());
        if(health > 0)
        {
            //live,hit action
            animator.SetTrigger("Hit");
        }
        else
        {
            islive = false;
            col.enabled = false;
            rigid.simulated = false;
            spriter.sortingOrder = 1;
            animator.SetBool("Dead",true);
            GameManager.instance.kill++;
            GameManager.instance.GetExp();
        }
    }


    IEnumerator KnonkBack()
    {
        yield return wait; //next frame delay
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dirvec = transform.position - playerPos;
        rigid.AddForce(dirvec.normalized*3,ForceMode2D.Impulse);
    }

    void Dead()
    {

        gameObject.SetActive(false);
       
    }
}
