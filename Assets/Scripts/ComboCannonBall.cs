using CannonApp;
using UnityEngine;

public class ComboCannonBall : CannonBall
{
    public CannonBall ComboCannonBallPrefab;

    private float _ball1Speed;
    private float _ball2Speed;

    //public override CannonBallType BallType => CannonBallType.Combo;
    public override PoolObjectId PoolId => PoolObjectId.SplitCannonBall;


    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        var position = transform.position + new Vector3(0, 2, 0);
        _ball1Speed = Random.Range(25f, 40f);
        _ball2Speed = Random.Range(25f, 40f);

        // calculate ball 1 forward at the split angle 
        var ball1Forward = Quaternion.AngleAxis(-45, Vector3.up) * Vector3.left * _ball1Speed;

        // calculate ball 2 forward at the inverse split angle
        var ball2Forward = Quaternion.AngleAxis(45, Vector3.up) * Vector3.right * _ball2Speed;


        var ball1 = Instantiate(ComboCannonBallPrefab, position, Quaternion.identity);
        var ball2 = Instantiate(ComboCannonBallPrefab, position, Quaternion.identity);

        ball1.Setup(ball1Forward, _pool);
        ball2.Setup(ball2Forward, _pool);
    }
}
