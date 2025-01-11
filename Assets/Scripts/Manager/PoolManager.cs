using System.Collections.Generic;
using UnityEngine;

public class ComponentPoolManager<T> where T : Component
{
    private readonly Queue<T> objects = new();
    private readonly Transform parent;
    private readonly T prefab;

    public ComponentPoolManager(T prefab, int initialSize, Transform parent = null)
    {
        this.prefab = prefab;
        this.parent = parent;

        for (var i = 0; i < initialSize; i++) AddObjectToPool();
    }

    private T AddObjectToPool()
    {
        var obj = Object.Instantiate(prefab, parent);
        obj.gameObject.SetActive(false);
        objects.Enqueue(obj);
        return obj;
    }

    public T Get()
    {
        if (objects.Count == 0) AddObjectToPool();

        var obj = objects.Dequeue();
        obj.gameObject.SetActive(true);
        return obj;
    }

    public void ReturnToPool(T obj)
    {
        obj.gameObject.SetActive(false);
        objects.Enqueue(obj);
    }

    public int GetPoolCount()
    {
        return objects.Count;
    }
}

public class ObjectPoolManager
{
    private readonly Queue<GameObject> objects = new();
    private readonly Transform parent;
    private readonly GameObject prefab;

    public ObjectPoolManager(GameObject prefab, int initialSize, Transform parent = null)
    {
        this.prefab = prefab;
        this.parent = parent;

        for (var i = 0; i < initialSize; i++) AddObjectToPool();
    }

    private GameObject AddObjectToPool()
    {
        var obj = Object.Instantiate(prefab, parent);
        obj.SetActive(false);
        objects.Enqueue(obj);
        return obj;
    }

    public GameObject Dequeue()
    {
        if (objects.Count == 0) AddObjectToPool();

        var obj = objects.Dequeue();
        obj.SetActive(true);
        return obj;
    }

    public void Enqueue(GameObject obj)
    {
        obj.SetActive(false);
        objects.Enqueue(obj);
    }
}