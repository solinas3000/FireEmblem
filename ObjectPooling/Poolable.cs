using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poolable : MonoBehaviour {

    public bool isPooled;
    public bool TryPool()
    {
        if (!isPooled)
            isPooled = ObjectPoolManager.GetInstance().TryPool(gameObject);
        gameObject.SetActive(!isPooled);
        return isPooled;
    }

    public GameObject Unpool(Vector3 position, Quaternion rotation)
    {
        GameObject obj =  ObjectPoolManager.GetInstance().Unpool(gameObject);
        obj.GetComponent<Poolable>().isPooled = false;
        obj.SetActive(true);
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        return obj;
    }

    public static GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        Poolable poolable = prefab.GetComponent<Poolable>();
        if (poolable)
            return poolable.Unpool(position, rotation);
        return GameObject.Instantiate(prefab, position, rotation);
    }

    public static void Destroy(GameObject obj)
    {
        Poolable poolable = obj.GetComponent<Poolable>();
        if (poolable)
            poolable.TryPool();
        else
            Object.Destroy(obj);
    }
}
