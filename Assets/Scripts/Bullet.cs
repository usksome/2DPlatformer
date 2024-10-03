using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public Vector2 velocity = new Vector2 (10, 0);


    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void FixedUpdate()
    {
        transform.Translate(velocity * Time.fixedDeltaTime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Terrain")
        {
            gameObject.SetActive(false);  // 활성화되지 않은 상태로 만들면 ObjectPooling 에서 다시 꺼내서 쓸 수 있게 되는 거다.
        }

        else if (collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().Hit(1);    // Enemy component를 가지고 와야 한다. Enemy 클래스의 인스턴스를 가지고 온 거기 때문에 Hit 그리고 damage 1로...
            gameObject.SetActive(false);  // 총알이 적에 맞았으면 총알은 일은 다했기 때문에 SetActive를 false로 만들어서 오브젝트 풀로 돌려 놓는다.


        }

    }

}
