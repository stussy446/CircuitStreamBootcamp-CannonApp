using System.Collections.Generic;
using UnityEngine;

public class GenericChallenge1 : MonoBehaviour
{
    [SerializeField] private GameObject _genericprefab;
    [SerializeField] private int _amountToInstantiate = 1;

    private void Start()
    {
        List<Animator> animList = InstantiateWithComponent<Animator>(_amountToInstantiate);
        List<Target> targetList = InstantiateWithComponent<Target>(_amountToInstantiate);

        for (int i = 0; i < _amountToInstantiate; i++)
        {
            Debug.Log($"elements {i}: {animList[i]}, {targetList[i]}");
        }
    }

    /// <summary>
    /// Instantiates genericprefab game objects and adds whatever component type is passed into the method. After it is added, sets the component to false and 
    /// adds the new object of type T to the list, and returns the list 
    /// </summary>
    /// <typeparam name="T">Type of component to be added to the game object</typeparam>
    /// <returns>List of type T</returns>
    private List<T> InstantiateWithComponent<T>(int amount) where T : Component
    {
        List<T> genericObjectsList = new List<T>(amount);

        for (int i = 0; i < amount; i++)
        {
            var newObject = Instantiate(_genericprefab, transform);
            var newComponent = newObject.AddComponent<T>();

            genericObjectsList.Add(newComponent);
            newComponent.gameObject.SetActive(false);

            Debug.Log($"Object initialized, component {typeof(T)} has been added to the object");
        }

        return genericObjectsList;
    }
}
