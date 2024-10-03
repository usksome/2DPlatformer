using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool : MonoBehaviour
{

    // Instantiate는 원래 게임 플레이 도중, 게임 루프가 돌고 있는 상황에서 자주 사용하기에 좋은 함수는 아니다. (이전 강의 사용한 것)
    // Instantiate는 prefab을 원본으로 해서 새로운 오브젝트를 찍어내는데 Instantiate를 갖다가 게임 루프 내에서 자주 사용되면 오히려 렉의 원인이 된다.
    // 그래서 게임 제작자는 이 prefab을 미리 Instantiate를 여러 개를 해두고 꺼놨다가 필요할 때마다 꺼내서 켜는 거다.
    // 그러다 사용한 오브젝트에 쓰임새가 다하면 Destroy하는 게 아니라 잠시 꺼놓는 거다.사용시 다시 켜고 끄고 하는 것. 즉, 미리 Instantiate를 해서 게임 루프 내에서의 렉을 줄인다.
    // 이것을 ObjectPooling 이라고 한다.


    public static ObjectPool Instance;

    public GameObject bulletPrefab;

    public int bulletLimit = 30;

    List<GameObject> bullets;



    private void Awake()
    {
        Instance = this;
    }



    // Start is called before the first frame update
    void Start()
    {
        bullets = new List<GameObject>();


        for (int i = 0; i < bulletLimit; i++) 
        { 

            GameObject go = Instantiate(bulletPrefab, transform);
            go.SetActive(false);

        }

    }


    public GameObject GetBullet()
    {
        foreach (GameObject go in bullets)
            
        {

            if (!go.activeSelf)
            {
                go.SetActive(true);
                return go;
            }

        }

        GameObject obj = Instantiate(bulletPrefab, transform);
        bullets.Add(obj);
        return obj;

    }


  

 
}
