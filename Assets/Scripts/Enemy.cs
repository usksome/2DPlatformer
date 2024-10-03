using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int hp = 3;

    public float speed = 3;

    public Collider2D frontCollider;
    public Collider2D frontBottomCollider;  // 절벽관련
    public CompositeCollider2D terrainCollider;


    public Vector2 vx;  // 현재의 이동상태 저장하기 위함


    // Start is called before the first frame update
    void Start()
    {
        vx = Vector2.right * speed;  // 시작할 때 오른쪽으로 움직인다.
    }

    // Update is called once per frame
    void Update()
    {
        if (frontCollider.IsTouching(terrainCollider) || !frontBottomCollider.IsTouching(terrainCollider))  // !가 없는 상태는 frontBottomCollider가 땅과 닿아 있다는 의미가 된다. 그러면 이런 절벽이 아니다. 그래서 !을 붙여준다.
        {                                                                                                   // 우측이 절벽관련, 좌측이 땅 관련
         
            vx = -vx; // 방향을 바꾸자
            transform.localScale = new Vector2(-transform.localScale.x, 1);  // 움직이는 방향도 바꿔야 되지만 표시되는 방향도 바꿔야 된다. 이전에 플레이어 방향 바꾸는건 SpriteRenderer의 flipx로 사용했었다.
                                                                             // 여기서는 플레이어 방식을 사용하지 않는다. frontBottomCollider가 내 자식 collider로 붙어 있기 때문이다. 
                                                                             // 이 코드에서 자기 자신의 방향 transform.localScale의 x가 반대로 바뀔 것이다.
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
            GetComponent<Collider2D>().enabled = false;  // BoxCollider2D 안쓴 이유는 개체 지향의 다양성에 의해서 Collider2D를 불렀을 때 Collider2D에 해당하는 걸 가져오게 되는데 BoxCollider2D 역시 Collider2D의 일종이기 때문이다.
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 10), ForceMode2D.Impulse);

            Invoke("DestroyThis", 2.0f);    // 2초 후에 부르다.

        }
    }

    private void DestroyThis()
    {
        Destroy(gameObject);
    }

}
