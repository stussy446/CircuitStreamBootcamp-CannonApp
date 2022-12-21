using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CannonApp;

public class TargetAmountLevelController : LevelController
{
    [SerializeField] private int _targetDestructionCount = 30;

    public override void RegisterTarget()
    {

    }

    protected override void Awake()
    {
        base.Awake();
        remainingTargets = _targetDestructionCount;
        UpdateRemainingTargets();
    }
}
