using System.Collections.Generic;
using UnityEngine;

public class ComponentPoolManager<T> where T : Component
{
    private readonly T prefab;
    private readonly Queue<T> objects = new();
    private readonly Transform parent;

    public ComponentPoolManager(T prefab, int initialSize, Transform parent = null)
    {
        this.prefab = prefab;
        this.parent = parent;

        for (int i = 0; i < initialSize; i++)
        {
            AddObjectToPool();
        }
    }

    private T AddObjectToPool()
    {
        T obj = Object.Instantiate(prefab, parent);
        obj.gameObject.SetActive(false);
        objects.Enqueue(obj);
        return obj;
    }

    public T Get()
    {
        if (objects.Count == 0)
        {
            AddObjectToPool();
        }

        T obj = objects.Dequeue();
        obj.gameObject.SetActive(true);
        return obj;
    }

    public void ReturnToPool(T obj)
    {
        obj.gameObject.SetActive(false);
        objects.Enqueue(obj);
    }
}

public class ObjectPoolManager
{
    private readonly GameObject prefab;
    private readonly Queue<GameObject> objects = new();
    private readonly Transform parent;

    public ObjectPoolManager(GameObject prefab, int initialSize, Transform parent = null)
    {
        this.prefab = prefab;
        this.parent = parent;

        for (int i = 0; i < initialSize; i++)
        {
            AddObjectToPool();
        }
    }

    private GameObject AddObjectToPool()
    {
        GameObject obj = Object.Instantiate(prefab, parent);
        obj.SetActive(false);
        objects.Enqueue(obj);
        return obj;
    }

    public GameObject Dequeue()
    {
        if (objects.Count == 0)
        {
            AddObjectToPool();
        }

        GameObject obj = objects.Dequeue();
        obj.SetActive(true);
        return obj;
    }

    public void Enqueue(GameObject obj)
    {
        obj.SetActive(false);
        objects.Enqueue(obj);
    }
}