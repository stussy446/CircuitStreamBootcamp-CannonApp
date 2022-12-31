using UnityEngine;
using CannonApp;

public class CannonBall : MonoBehaviour, IPoolObject
{
    private static readonly int Exploded = Animator.StringToHash("Exploded");
    public virtual CannonBallType BallType => CannonBallType.Normal;

    public PoolObjectId PoolId => throw new System.NotImplementedException();

    protected Rigidbody ballRigidBody;
    protected CannonBallsPool _pool;

    [SerializeField] protected Animator animator;

    [Header("Explosion Settings")]
    [SerializeField] private float _explosionRadius = 9.0f;
    [SerializeField] private float _explosionForce = 12f;
    [SerializeField] private float _explosionUpwardsModifier = 1f;


    void Awake()
    {
        ballRigidBody = GetComponent<Rigidbody>();
    }

    public virtual void Setup(Vector3 fireForce, CannonBallsPool objectPool)
    {
        ballRigidBody.velocity = Vector3.zero;
        ballRigidBody.angularVelocity = Vector3.zero;
        ballRigidBody.isKinematic = false;

        _pool = objectPool;

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
        _pool.ReleaseCannonBall(this, BallType);
    }

    private void OnTriggerEnter(Collider other)
    {
        _pool.ReleaseCannonBall(this, BallType);
    }

    public void Activate()
    {
        throw new System.NotImplementedException();
    }

    public void Deactivate()
    {
        throw new System.NotImplementedException();
    }
}
