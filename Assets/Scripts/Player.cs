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

        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;  // ���ٰ� �Ʒ� �ٵ��� ���� �ε����� ���鼭 ���� ������ �� �ٽ� ����ġ�� �ϴ� �κ�
        GetComponent<Rigidbody2D>().angularVelocity = 0;       
        GetComponent<BoxCollider2D>().enabled = true;
        
        transform.eulerAngles = Vector3.zero;  // ���� ���ư��� ���� ���·� ���ƿ´�. (eulerAngle)�� �� ���� ���ư� �ִ����� �˷��ִ� ���ʹ�. 0 0 0 ���� ����� �ϱ� ������ ���͸� zero�� �Ѵ�.

        transform.position = originPosition;

        GetComponent<Rigidbody2D>().velocity = Vector2.zero;  // ������ �̵��ϴٰ� �׾��� �ϴ��� �ٽ� ������ ���� ���� ���� ���·� ���ƿ��� �ȴ�.

    }


    // Update is called once per frame
    void Update()
    {
        vx = Input.GetAxisRaw("Horizontal");   // GetAxis(�߰����� �ڵ����� ���� �ε巴�� �������), GetAxisRaw (���� �� ������ ��, ���ӵ��� ������� �ʱ� ������ ���⼭�� GetAxisRaw �������)


        if (vx < 0)  // vx�� 0���� �۴ٸ� ������ ���� �ִٴ� �ǹ�
        {
            GetComponent<SpriteRenderer>().flipX = true;  // flipX�� true�� �����ϸ� �¿����
        }


        if (vx > 0) 
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }


        // ������ else �� ������� �ʴ� ����: ĳ���Ͱ� �������� ���� ���߸� ������ ���� �ִ� ���¿��� �ϴµ� else�� �ϸ� �ٽ� ������ �ٶ�
        // vx�� 0�� �� ���� ���¸� ��ȭ���� �ʵ��� �ϱ� ����


        if (bottomCollider.IsTouching(terrainCollider))  // ���� �پ� �ִ�. ���� ���� �پ� �ֳ���?
        {
            if (!isGrounded)  // ���� �پ� �־���.
            {
                //����
                if (vx == 0)
                {

                    GetComponent<Animator>().SetTrigger("Idle");

                }


                else
                {
                    GetComponent<Animator>().SetTrigger("Run");
                }

            }


            else   // �������� ������ �پ� �ְ� ���ݵ� �پ� �ִ� ����, �̷� ���¿����� �ӵ��� �ٲ������ �ִϸ��̼��� �ٲ�� �Ѵ�.
            {
                // ��� �ȴ� ��
                if (prevVx != vx)   // ���� �ӵ��� ���� �ӵ��� �ٸ��ٸ�
                {
                    if (vx == 0)
                    {
                        GetComponent<Animator>().SetTrigger("Idle");   // �׸��� ���� �ӵ��� 0�̶�� �Ѵٸ�Idle ���´�.
                    }

                    else
                    {
                        GetComponent<Animator>().SetTrigger("Run");  // vx�� 0�� �ƴϸ� ������ ���´�.
                    }

                }

            }

            isGrounded = true; // �ٴڿ� �پ� �ֱ� ������ true

        }

        else
        {

            if (isGrounded)
            {
                // ���� ����
                GetComponent<Animator>().SetTrigger("Jump");

            }


            isGrounded= false;
        }



        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpSpeed;  // �̹����� �ٷ� �ӵ��� �ִ°� �ƴ϶� Rigidbody 2D�� �̿�
        }

        prevVx = vx;


        //�Ѿ� �߻�
        if (Input.GetButtonDown("Fire1") && lastShoot + 0.5f < Time.time)  //      (�Ѿ� ������) ���������� ���� �� �ð� �÷��� 0.5�� ���� ���� �ð��� �̷����� �Ѵ�.
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

            GameObject bullet = ObjectPool.Instance.GetBullet();  // �Ѿ� �߻�
            bullet.transform.position = transform.position;
            bullet.GetComponent<Bullet>().velocity = bulletV;
            lastShoot = Time.time;   // �Ѿ��� �߻� ���� �� ���� �ð��� �־��ش�. (�Ѿ� �߻� ������ �ֱ� ����)
            
        }

    }


    private void FixedUpdate()    // vx���� ���� ĳ���Ͱ� ������ �ǵ� ������ ĳ���ʹ� Rigidbody���� Dynamic�� �����߱� ������ ����Ƽ ���� ������ �Բ� �����δ�. �׷��� ������ ���� ĳ������ ��ġ�� �ٲ� ����
    {                             // ���� ������Ʈ���� �ٲ��� ���� FixedUpdate �ؼ� �ٲٴ� �� ����.

        transform.Translate(Vector2.right*vx*speed*Time.fixedDeltaTime); // ĳ���� ������

    }


    private void OnCollisionEnter2D(Collision2D collision)   // ���� �÷��̾� ������ Ʈ���Ű� �� ���� �ֱ� ������ OnCollisionEnter�� ����Ѵ�.
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
