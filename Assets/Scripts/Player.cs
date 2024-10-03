using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Player : MonoBehaviour
{

    
    public float speed = 7;

    public float jumpSpeed = 15;

    public Collider2D bottomCollider;

    public CompositeCollider2D terrainCollider;

 

    float vx = 0;

    float prevVx = 0;

    bool isGrounded;

    Vector2 originPosition;

    float lastShoot;


    // Start is called before the first frame update
    void Start()
    {
        originPosition = transform.position;
    }


    public void Restart()
    {

        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;  // 이줄과 아래 줄들은 적과 부딪혀서 돌면서 죽은 상태일 때 다시 원위치로 하는 부분
        GetComponent<Rigidbody2D>().angularVelocity = 0;       
        GetComponent<BoxCollider2D>().enabled = true;
        
        transform.eulerAngles = Vector3.zero;  // 전혀 돌아가지 않은 상태로 돌아온다. (eulerAngle)은 몇 도로 돌아가 있는지를 알려주는 벡터다. 0 0 0 으로 해줘야 하기 때문에 벡터를 zero로 한다.

        transform.position = originPosition;

        GetComponent<Rigidbody2D>().velocity = Vector2.zero;  // 옆으로 이동하다가 죽었다 하더라도 다시 리스톤 했을 때는 멈춘 상태로 돌아오게 된다.

    }


    // Update is called once per frame
    void Update()
    {
        vx = Input.GetAxisRaw("Horizontal");   // GetAxis(중간값이 자동으로 들어와 부드럽게 만들어줌), GetAxisRaw (조금 더 날것의 값, 가속도를 사용하지 않기 때문에 여기서는 GetAxisRaw 사용했음)


        if (vx < 0)  // vx가 0보다 작다면 왼쪽을 보고 있다는 의미
        {
            GetComponent<SpriteRenderer>().flipX = true;  // flipX를 true로 설정하면 좌우반전
        }


        if (vx > 0) 
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }


        // 위에서 else 를 사용하지 않는 이유: 캐릭터가 왼쪽으로 가다 멈추면 왼쪽을 보고 있는 상태여야 하는데 else로 하면 다시 정면을 바라봄
        // vx가 0일 때 현재 상태를 변화하지 않도록 하기 위함


        if (bottomCollider.IsTouching(terrainCollider))  // 땅에 붙어 있다. 지금 땅에 붙어 있나요?
        {
            if (!isGrounded)  // 땅에 붙어 있었다.
            {
                //착지
                if (vx == 0)
                {

                    GetComponent<Animator>().SetTrigger("Idle");

                }


                else
                {
                    GetComponent<Animator>().SetTrigger("Run");
                }

            }


            else   // 이전까지 전에도 붙어 있고 지금도 붙어 있는 상태, 이런 상태에서는 속도가 바뀌었으면 애니메이션을 바꿔야 한다.
            {
                // 계속 걷는 중
                if (prevVx != vx)   // 이전 속도와 지금 속도가 다르다면
                {
                    if (vx == 0)
                    {
                        GetComponent<Animator>().SetTrigger("Idle");   // 그리고 현재 속도가 0이라고 한다면Idle 상태다.
                    }

                    else
                    {
                        GetComponent<Animator>().SetTrigger("Run");  // vx가 0가 아니면 움직인 상태다.
                    }

                }

            }

            isGrounded = true; // 바닥에 붙어 있기 때문에 true

        }

        else
        {

            if (isGrounded)
            {
                // 점프 시작
                GetComponent<Animator>().SetTrigger("Jump");

            }


            isGrounded= false;
        }



        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpSpeed;  // 이번에는 바로 속도를 주는게 아니라 Rigidbody 2D를 이용
        }

        prevVx = vx;


        //총알 발사
        if (Input.GetButtonDown("Fire1") && lastShoot + 0.5f < Time.time)  //      (총알 딜레이) 마지막으로 총을 쏜 시간 플러스 0.5초 보다 현재 시간이 미래여야 한다.
        {
            Vector2 bulletV = Vector2.zero;

            if (GetComponent<SpriteRenderer>().flipX)
            {
                bulletV = new Vector2(-10, 0);
            }

            else
            {
                bulletV = new Vector2(10, 0);
            }

            GameObject bullet = ObjectPool.Instance.GetBullet();  // 총알 발사
            bullet.transform.position = transform.position;
            bullet.GetComponent<Bullet>().velocity = bulletV;
            lastShoot = Time.time;   // 총알을 발사 했을 때 현재 시간을 넣어준다. (총알 발사 딜레이 주기 위함)
            
        }

    }


    private void FixedUpdate()    // vx값에 따라 캐릭터가 움직일 건데 실제로 캐릭터는 Rigidbody에서 Dynamic을 선택했기 때문에 유니티 물리 엔진과 함께 움직인다. 그렇기 때문에 실제 캐릭터의 위치를 바꿀 때는
    {                             // 위에 업데이트에서 바꾸지 말고 FixedUpdate 해서 바꾸는 게 좋다.

        transform.Translate(Vector2.right*vx*speed*Time.fixedDeltaTime); // 캐릭터 움직임

    }


    private void OnCollisionEnter2D(Collision2D collision)   // 적과 플레이어 양쪽이 트리거가 안 켜져 있기 때문에 OnCollisionEnter를 사용한다.
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Die();
        }
    }



    void Die()
    {

        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        GetComponent<Rigidbody2D>().angularVelocity = 720;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
        GetComponent<BoxCollider2D>().enabled = false;

        GameManager.Instance.Die();
    }


}
