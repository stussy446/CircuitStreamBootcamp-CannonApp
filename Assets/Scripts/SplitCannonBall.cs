using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class SplitCannonBall : CannonBall
{
    private static readonly int SpecialAvailableHash = Animator.StringToHash("SpecialAvailable");
    private static readonly int SpecialUsedHash = Animator.StringToHash("SpecialUsed");

    public float splitTime = 0.7f;
    public float splitAngle = 20.0f;
    public CannonBall splitCannonBallPrefab;

    public override void Setup(Vector3 fireForce)
    {
        base.Setup(fireForce);

        animator.SetTrigger(SpecialAvailableHash);
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        enabled = false;
    }

    private void SpawnSplitCannonBalls()
    {
        // need the position and forward direction of the cannonBall 
        var position = transform.position;
        var forward = ballRigidBody.velocity;

        // calculate ball 1 forward at the split angle 
        var ball1Forward = Quaternion.AngleAxis(-splitAngle, Vector3.up) * forward; ;

        // calculate ball 2 forward at the inverse split angle
        var ball2Forward = Quaternion.AngleAxis(splitAngle, Vector3.up) * forward;

        // instantiate both balls at their angled positions
        var ball1 = Instantiate(splitCannonBallPrefab, position, Quaternion.identity);
        ball1.Setup(ball1Forward);

        var ball2 = Instantiate(splitCannonBallPrefab, position, Quaternion.identity);
        ball2.Setup(ball2Forward);

        // trigger the SpecialUsedHash
        animator.SetTrigger(SpecialUsedHash);
        enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        splitTime -= Time.deltaTime;

        if (splitTime <= 0)
        {
            SpawnSplitCannonBalls();
        }
    }
}
