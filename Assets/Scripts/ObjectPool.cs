using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool : MonoBehaviour
{

    // Instantiate�� ���� ���� �÷��� ����, ���� ������ ���� �ִ� ��Ȳ���� ���� ����ϱ⿡ ���� �Լ��� �ƴϴ�. (���� ���� ����� ��)
    // Instantiate�� prefab�� �������� �ؼ� ���ο� ������Ʈ�� ���µ� Instantiate�� ���ٰ� ���� ���� ������ ���� ���Ǹ� ������ ���� ������ �ȴ�.
    // �׷��� ���� �����ڴ� �� prefab�� �̸� Instantiate�� ���� ���� �صΰ� �����ٰ� �ʿ��� ������ ������ �Ѵ� �Ŵ�.
    // �׷��� ����� ������Ʈ�� ���ӻ��� ���ϸ� Destroy�ϴ� �� �ƴ϶� ��� ������ �Ŵ�.���� �ٽ� �Ѱ� ���� �ϴ� ��. ��, �̸� Instantiate�� �ؼ� ���� ���� �������� ���� ���δ�.
    // �̰��� ObjectPooling �̶�� �Ѵ�.


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
