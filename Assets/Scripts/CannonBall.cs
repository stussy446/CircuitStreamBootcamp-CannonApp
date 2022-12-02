using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    private static readonly int Exploded = Animator.StringToHash("Exploded");
    private Rigidbody _ballRigidBody;

    [SerializeField] private Animator _animator;

    [Header("Explosion Settings")]
    [SerializeField] private float _explosionRadius = 9.0f;
    [SerializeField] private float _explosionForce = 12f;
    [SerializeField] private float _explosionUpwardsModifier = 1f;

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

        Vector3 explosionPosition = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, _explosionRadius, LayerMask.GetMask("Targets"));

        foreach (Collider hit in colliders)
        {
            Rigidbody collidedRigidBody = hit.GetComponent<Rigidbody>();

            if (collidedRigidBody !=  null)
            {
                collidedRigidBody.AddExplosionForce(
                    _explosionForce,
                    explosionPosition,
                    _explosionRadius,
                    _explosionUpwardsModifier,
                    ForceMode.Impulse
                    );
            }
        }
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
