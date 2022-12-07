using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : Vehicle
{
    [SerializeField] private float _rotationSpeed;

    protected override void Update()
    {
        base.Update();

        Steer();
    }

    private void Steer()
    {
        float rotateValue = Input.GetAxis("Horizontal");
        transform.Rotate(new Vector3(0, rotateValue, 0) * _rotationSpeed * Time.deltaTime);
    }
}
