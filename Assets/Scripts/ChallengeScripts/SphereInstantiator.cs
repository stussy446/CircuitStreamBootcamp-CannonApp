using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereInstantiator : MonoBehaviour
{
    [SerializeField] private GameObject _sphere;
    [SerializeField] private Transform _parentTransform;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void InstantiateUnparentedSphere()
    {
        Instantiate(_sphere, Vector3.up * 100, Quaternion.Euler(0, 45, 0));
    }

    private void InstantiateChildSphere()
    {
        //TODO: add local rotation to sphere
        Instantiate(_sphere, transform);
    }

    private void InstantiateChildOfTransform(Transform parent)
    {
        Instantiate(_sphere, parent);
    }

    private void InstantiateRandom()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            float randomX = Random.Range(0f, 100f);
            float randomY = Random.Range(0f, 100f);
            float randomZ = Random.Range(0f, 100f);

            Vector3 randomPosition = new Vector3(randomX, randomY, randomZ);
            Instantiate(_sphere, randomPosition, Quaternion.identity);
        }
    }
}
