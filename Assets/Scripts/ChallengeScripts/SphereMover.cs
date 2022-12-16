using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    void Update()
    {
        MoveSphere();
    }

    /// <summary>
    /// Moves the sphere horizontally and vertically based on player input 
    /// </summary>
    private void MoveSphere()
    {
        float xMovement = Input.GetAxis("Horizontal") * _moveSpeed;
        float yMovement = Input.GetAxis("Vertical") * _moveSpeed;

        transform.Translate(new Vector3(xMovement, yMovement) * Time.deltaTime);
    }
}
