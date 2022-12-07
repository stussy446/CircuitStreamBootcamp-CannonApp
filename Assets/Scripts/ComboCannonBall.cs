using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboCannonBall : CannonBall
{
    public CannonBall ComboCannonBallPrefab;

    private float ball1Speed;
    private float ball2Speed;


    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        var position = transform.position + new Vector3(0, 2, 0);
        ball1Speed = Random.Range(25f, 40f);
        ball2Speed = Random.Range(25f, 40f);

        // calculate ball 1 forward at the split angle 
        var ball1Forward = Quaternion.AngleAxis(-45, Vector3.up) * Vector3.left * ball1Speed;

        // calculate ball 2 forward at the inverse split angle
        var ball2Forward = Quaternion.AngleAxis(45, Vector3.up) * Vector3.right * ball2Speed;


        var ball1 = Instantiate(ComboCannonBallPrefab, position, Quaternion.identity);
        var ball2 = Instantiate(ComboCannonBallPrefab, position, Quaternion.identity);

        ball1.Setup(ball1Forward);
        ball2.Setup(ball2Forward);
    }
}
