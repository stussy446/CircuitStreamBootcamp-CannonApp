using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    private static readonly int Exploded = Animator.StringToHash("Exploded");
    private Rigidbody _ballRigidBody;

    [SerializeField] private Animator _animator;

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

    private void OnCollisionEnter(Collision collision)
    {
        transform.rotation = Quaternion.FromToRotation(transform.up, collision.GetContact(0).normal);

        _ballRigidBody.velocity = Vector3.zero;
        _ballRigidBody.angularVelocity = Vector3.zero;
        _ballRigidBody.isKinematic = true;

        _animator.SetTrigger(Exploded);
    }

    public void OnFinishedExplosionAnimation()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
