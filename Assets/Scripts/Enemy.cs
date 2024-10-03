using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int hp = 3;

    public float speed = 3;

    public Collider2D frontCollider;
    public Collider2D frontBottomCollider;  // ��������
    public CompositeCollider2D terrainCollider;


    public Vector2 vx;  // ������ �̵����� �����ϱ� ����


    // Start is called before the first frame update
    void Start()
    {
        vx = Vector2.right * speed;  // ������ �� ���������� �����δ�.
    }

    // Update is called once per frame
    void Update()
    {
        if (frontCollider.IsTouching(terrainCollider) || !frontBottomCollider.IsTouching(terrainCollider))  // !�� ���� ���´� frontBottomCollider�� ���� ��� �ִٴ� �ǹ̰� �ȴ�. �׷��� �̷� ������ �ƴϴ�. �׷��� !�� �ٿ��ش�.
        {                                                                                                   // ������ ��������, ������ �� ����
         
            vx = -vx; // ������ �ٲ���
            transform.localScale = new Vector2(-transform.localScale.x, 1);  // �����̴� ���⵵ �ٲ�� ������ ǥ�õǴ� ���⵵ �ٲ�� �ȴ�. ������ �÷��̾� ���� �ٲٴ°� SpriteRenderer�� flipx�� ����߾���.
                                                                             // ���⼭�� �÷��̾� ����� ������� �ʴ´�. frontBottomCollider�� �� �ڽ� collider�� �پ� �ֱ� �����̴�. 
                                                                             // �� �ڵ忡�� �ڱ� �ڽ��� ���� transform.localScale�� x�� �ݴ�� �ٲ� ���̴�.
        }
        
    }


    private void FixedUpdate()
    {
        transform.Translate(vx * Time.fixedDeltaTime);
    }


    public void Hit(int damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            GetComponent<Rigidbody2D>().angularVelocity = 720;
            GetComponent<Collider2D>().enabled = false;  // BoxCollider2D �Ⱦ� ������ ��ü ������ �پ缺�� ���ؼ� Collider2D�� �ҷ��� �� Collider2D�� �ش��ϴ� �� �������� �Ǵµ� BoxCollider2D ���� Collider2D�� �����̱� �����̴�.
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 10), ForceMode2D.Impulse);

            Invoke("DestroyThis", 2.0f);    // 2�� �Ŀ� �θ���.

        }
    }

    private void DestroyThis()
    {
        Destroy(gameObject);
    }

}
