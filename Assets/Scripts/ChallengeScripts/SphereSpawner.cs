using System;
using UnityEngine;

public class SphereSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _spherePrefab;
    [SerializeField][Range(2f, 5f)] private float _maxDelay;

    private float _spawnRate;

    public event Action onSphereSpawned;

    private void Awake()
    {
        _spawnRate = UnityEngine.Random.Range(1f, 3f);
    }

    private void Update()
    {
        // if spawnrate reaches 0, spawn a sphere and reset the spawnrate to random interval 
        if(_spawnRate < 0f)
        {
            SpawnSphere();
            _spawnRate = UnityEngine.Random.Range(1f, _maxDelay);
        }
        else
        {
            _spawnRate -= Time.deltaTime;
        }
    }

    /// <summary>
    /// Instantiates sphere at the spawners position and invokes the onSphereSpawned Action
    /// </summary>
    private void SpawnSphere()
    {
        Instantiate(_spherePrefab, transform.position, Quaternion.identity);
        onSphereSpawned?.Invoke();
    }
}
