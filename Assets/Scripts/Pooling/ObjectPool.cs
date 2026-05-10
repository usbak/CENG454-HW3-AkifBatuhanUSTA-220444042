using System.Collections.Generic;
using UnityEngine;


public class ObjectPool<T> where T : Component
{
    private readonly T prefab;
    private readonly Transform parent;
    private readonly Queue<T> available = new();
    private readonly HashSet<T> active = new();

    public int AvailableCount => available.Count;
    public int ActiveCount => active.Count;

    public ObjectPool(T prefab, int initialSize, Transform parent = null)
    {
        this.prefab = prefab;
        this.parent = parent;
        for (int i = 0; i < initialSize; i++)
            CreateNew();
    }

    private T CreateNew()
    {
        T obj = GameObject.Instantiate(prefab, parent);
        obj.gameObject.SetActive(false);
        available.Enqueue(obj);
        return obj;
    }

    
    public T Get(Vector3 position, Quaternion rotation)
    {
        T obj = available.Count > 0 ? available.Dequeue() : CreateNew();
        if (available.Count > 0) available.Dequeue();  // safety

        obj.transform.SetPositionAndRotation(position, rotation);
        obj.gameObject.SetActive(true);
        active.Add(obj);

        
        if (obj is IPoolable poolable) poolable.OnSpawn();

        return obj;
    }

    
    public void Return(T obj)
    {
        if (obj == null || !active.Contains(obj)) return;

        
        if (obj is IPoolable poolable) poolable.OnDespawn();

        obj.gameObject.SetActive(false);
        active.Remove(obj);
        available.Enqueue(obj);
    }
}
