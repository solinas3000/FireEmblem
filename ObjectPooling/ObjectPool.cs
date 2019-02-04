using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool {

    private Queue<GameObject> pool;
    private GameObject pooledPrefab;

    public ObjectPool(GameObject prefab)
    {
        pool = new Queue<GameObject>();
        pooledPrefab = prefab;
    }

    public bool TryPool(GameObject obj)
    {
        if (obj.name != pooledPrefab.name)
            return false;
        pool.Enqueue(obj);
        return true;
    }

    public GameObject Unpool()
    {
        if (pool.Count > 0)
            return pool.Dequeue();
        GameObject obj = GameObject.Instantiate(pooledPrefab);
        obj.name = pooledPrefab.name;
        return obj;
    }
}
