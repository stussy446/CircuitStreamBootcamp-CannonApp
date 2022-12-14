using CannonApp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private LayerMask _waterTriggerLayer;

    private LevelController _levelController;

    public void Setup(LevelController controller) 
    { 
        _levelController = controller;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.layer.Equals(LayerMask.NameToLayer("Water Trigger")))
        {
            return;
        }

        _levelController.TargetDestroyed();
        Destroy(gameObject);

        //// uses bitwise operation to check if the other colliders layer is a layer that can be collided with 
        //if ((_waterTriggerLayer.value & (1 << other.gameObject.layer)) > 0)
        //{
        //    Destroy(gameObject);
        //}
    }


}
