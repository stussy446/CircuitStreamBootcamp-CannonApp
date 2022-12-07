using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    [SerializeField] protected float moveSpeed;

    // Update is called once per frame
    protected virtual void Update()
    {
        Move();
    }

    private void Move()
    {
        float inputValue = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(0, 0, inputValue) * moveSpeed * Time.deltaTime);
    }
}
