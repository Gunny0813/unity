using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 inputVec; //
    public float speed;
    public Scanner scanner;


    Rigidbody2D rigid;  
    SpriteRenderer spriter;
    Animator anim;
    void Awake() //기본 초기화
    {
        rigid = GetComponent<Rigidbody2D>(); 
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();

    }
     void Update()//프레임 마다 시행됨(1초에 60회)
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");
    }


    void FixedUpdate()//물리연산을 다룰떄 씀
    {
        

        //Time.fixedDeltaTime은 한 프레임이 쓰인 시간
        Vector2 nextVec = inputVec.normalized * speed *Time.fixedDeltaTime;
        //3.위치 이동
        rigid.MovePosition (rigid.position +nextVec);

    }
    void LateUpdate()//update가 끝난후 다음 프레임으로 넘어갈떄 시행되는 생명주기
    {
        anim.SetFloat("Speed", inputVec.magnitude);
        //"플레이어의 입력 크기에 따라 Animator에 'Speed' 값을 실시간으로 넘겨주는 코드"
        //파라미터 이름,이에 넣을 값   
        if (inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0;  
        }   

    }


    /*void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }
    */

}
