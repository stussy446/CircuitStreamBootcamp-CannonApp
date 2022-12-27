using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericChallenge1 : MonoBehaviour
{
    [SerializeField] private GameObject _genericprefab;

    private void Start()
    {
        InstantiateWithComponent<Animator>();
        InstantiateWithComponent<Target>();
    }

    /// <summary>
    /// Instantiates a genericprefab game object and adds whatever component type is passed into the method. After it is added, sets the component to false and 
    /// logs the components addition to the gameobject to the console.
    /// </summary>
    /// <typeparam name="T">Type of component to be added to the game object</typeparam>
    private void InstantiateWithComponent<T>() where T : Component
    {
        var newObject = Instantiate(_genericprefab, transform);
        var newComponent = newObject.AddComponent<T>();

        newComponent.gameObject.SetActive(false);
        Debug.Log($"Object initialized, component {typeof(T)} has been added to the object");
    }
}
