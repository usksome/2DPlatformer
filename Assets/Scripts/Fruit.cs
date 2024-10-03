using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuit : MonoBehaviour
{

    public float timeAdd = 10;  // 아이템 먹을 시 점수 추가

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameManager.Instance.AddTime(timeAdd);

            GetComponent<Animator>().SetTrigger("Eaten");

            Invoke("DestroyThis", 0.3f);

        }
    }


    private void DestroyThis()
    {
        Destroy(gameObject);
    }


}
