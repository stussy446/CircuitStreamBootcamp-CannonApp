using CannonApp;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ObjectsPool 
{
    [SerializeField] private List<GameObject> _poolPrefabs;

    private Dictionary<PoolObjectId, List<IPoolObject>> _pools;
    private Dictionary<PoolObjectId, IPoolObject> _idToPrefab;

    public void Setup(int prewarmCount)
    {
        InitializeIdToPrefabDictionary();

        _pools = new Dictionary<PoolObjectId, List<IPoolObject>>(_poolPrefabs.Count);

        foreach (var prefabInterface in _idToPrefab.Values)
        {
            var pool = new List<IPoolObject>(prewarmCount);

            for(int i = 0; i < prewarmCount; i++)
            {
                pool.Add(Instantiate(prefabInterface));
            }

            _pools[prefabInterface.PoolId] = pool;
        }
    }

    private IPoolObject Instantiate(IPoolObject prefabInterface)
    {
        var prefab = (MonoBehaviour)prefabInterface;
        var newObject = (IPoolObject)UnityEngine.Object.Instantiate(prefab);
        newObject.Deactivate();

        return newObject;
    }

    public T GetObject<T>(PoolObjectId id)
    {
        var pool = _pools[id];
        IPoolObject poolObject;

        if (pool.Count == 0)
        {
            var prefabInterface = _idToPrefab[id];
            poolObject = Instantiate(prefabInterface);
        }
        else
        {
            poolObject = pool[0];
            pool.RemoveAt(0);
        }

        poolObject.Activate();
        return (T)poolObject;
    }

    public void ReleaseObject(IPoolObject poolObject)
    {
        _pools[poolObject.PoolId].Add(poolObject);
        poolObject.Deactivate();
    }

    private void InitializeIdToPrefabDictionary()
    {
        _idToPrefab = new Dictionary<PoolObjectId, IPoolObject>(_poolPrefabs.Count);

        foreach (var  obj in _poolPrefabs)
        {
            var poolObjectComponent = obj.GetComponent<IPoolObject>();
            if (poolObjectComponent == null) { throw new Exception($"PoolPrefab is not an IPoolObject"); }

            _idToPrefab[poolObjectComponent.PoolId] = poolObjectComponent;
        }
    }
}
