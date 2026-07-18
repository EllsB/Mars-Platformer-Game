using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject prefab;
    public int poolSize = 12;

    private List<GameObject> pool;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        pool = new List<GameObject>();

        for(int i = 0; 1 < poolSize; i++)
        {
            CreateNewObj();
        }
    }

    public GameObject GetPooledObject()
    {
        foreach(GameObject obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }

        return CreateNewObj();

    }

    private GameObject CreateNewObj()
    {
        GameObject obj = Instantiate(prefab, transform);
        obj.SetActive(false);
        pool.Add(obj);
        return obj;
    }
}
