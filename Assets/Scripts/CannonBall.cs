using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    private Rigidbody _ballRigidBody;

    void Awake()
    {
        _ballRigidBody = GetComponent<Rigidbody>();
    }

    public void Setup(Vector3 fireForce)
    {
        _ballRigidBody.AddForce(fireForce, ForceMode.Impulse);
        _ballRigidBody.angularVelocity = new Vector3(
            Random.Range(-10f, 10f),
            Random.Range(-10f, 10f), 
            Random.Range(-10f, 10f));
    }
}
