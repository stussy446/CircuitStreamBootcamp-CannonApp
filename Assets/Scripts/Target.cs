using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private LayerMask _waterTriggerLayer;
    private void OnTriggerEnter(Collider other)
    {
        // uses bitwise operation to check if the other colliders layer is a layer that can be collided with 
        if ((_waterTriggerLayer.value & (1 << other.gameObject.layer)) > 0)
        {
            Destroy(gameObject);
        }
    }
}
