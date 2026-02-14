using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance; 

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int poolSize = 20;

    private List<GameObject> bulletPool;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        bulletPool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bulletPool.Add(bullet);
        }
    }
    public GameObject GetBullet()
    {
        for (int i = 0; i < bulletPool.Count; i++)
        {
            if (!bulletPool[i].activeInHierarchy)
            {
                return bulletPool[i];
            }
        }

        GameObject newBullet = Instantiate(bulletPrefab);
        newBullet.SetActive(false);
        bulletPool.Add(newBullet);
        return newBullet;
    }
}