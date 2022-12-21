using CannonApp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private void Start()
    {
        GameServices.GetService<LevelController>().RegisterTarget();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.layer.Equals(LayerMask.NameToLayer("Water Trigger")))
        {
            return;
        }

        GameServices.GetService<LevelController>().TargetDestroyed();
        Destroy(gameObject);
    }


}
