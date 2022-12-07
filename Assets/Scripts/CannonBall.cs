using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    private static readonly int Exploded = Animator.StringToHash("Exploded");
    protected Rigidbody ballRigidBody;

    [SerializeField] protected Animator animator;

    [Header("Explosion Settings")]
    [SerializeField] private float _explosionRadius = 9.0f;
    [SerializeField] private float _explosionForce = 12f;
    [SerializeField] private float _explosionUpwardsModifier = 1f;

    void Awake()
    {
        ballRigidBody = GetComponent<Rigidbody>();
    }

    public virtual void Setup(Vector3 fireForce)
    {
        ballRigidBody.AddForce(fireForce, ForceMode.Impulse);
        ballRigidBody.angularVelocity = new Vector3(
            Random.Range(-10f, 10f),
            Random.Range(-10f, 10f), 
            Random.Range(-10f, 10f));
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        transform.rotation = Quaternion.FromToRotation(transform.up, collision.GetContact(0).normal);

        ballRigidBody.velocity = Vector3.zero;
        ballRigidBody.angularVelocity = Vector3.zero;
        ballRigidBody.isKinematic = true;

        animator.SetTrigger(Exploded);

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
