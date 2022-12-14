using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CannonBallsPool 
{
    [SerializeField] private List<CannonBall> _possibleCannonBallPrefabs;

    private Dictionary<CannonBallType, List<CannonBall>> _pools;

    public void Setup(int prewarmCount)
    {
        _pools = new Dictionary<CannonBallType, List<CannonBall>>(_possibleCannonBallPrefabs.Count);

        foreach (var ballPrefab in _possibleCannonBallPrefabs)
        {
            var pool = new List<CannonBall>(prewarmCount);

            for(int i = 0; i < prewarmCount; i++)
            {
                pool.Add(Instantiate(ballPrefab));
            }

            _pools[ballPrefab.BallType] = pool;
        }
    }

    private CannonBall Instantiate(CannonBall prefab)
    {
        var newBall = UnityEngine.Object.Instantiate(prefab);
        newBall.gameObject.SetActive(false);

        return newBall;
    }

    public CannonBall GetCannonBall(CannonBallType ballType)
    {
        var pool = _pools[ballType];
        CannonBall ball;

        if (pool.Count == 0)
        {
            ball = Instantiate(_possibleCannonBallPrefabs.Find(prefab => prefab.BallType == ballType));
        }
        else
        {
            ball = pool[0];
            pool.RemoveAt(0);
        }

        ball.gameObject.SetActive(true);
        return ball;
    }

    public void ReleaseCannonBall(CannonBall ball, CannonBallType ballType)
    {
        _pools[ballType].Add(ball);
        ball.gameObject.SetActive(false);
    }
}
