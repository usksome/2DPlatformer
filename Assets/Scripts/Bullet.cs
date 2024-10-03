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
            gameObject.SetActive(false);  // Ȱ��ȭ���� ���� ���·� ����� ObjectPooling ���� �ٽ� ������ �� �� �ְ� �Ǵ� �Ŵ�.
        }

        else if (collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().Hit(1);    // Enemy component�� ������ �;� �Ѵ�. Enemy Ŭ������ �ν��Ͻ��� ������ �� �ű� ������ Hit �׸��� damage 1��...
            gameObject.SetActive(false);  // �Ѿ��� ���� �¾����� �Ѿ��� ���� ���߱� ������ SetActive�� false�� ���� ������Ʈ Ǯ�� ���� ���´�.


        }

    }

}
