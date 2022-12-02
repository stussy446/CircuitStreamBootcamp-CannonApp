using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    [Header("Cannon Rotation")]
    [SerializeField] private float _maxYRotation = 150f;
    [SerializeField] private float _minYRotation = 50f;
    [SerializeField] private float _maxXRotation = 75f;
    [SerializeField] private float _minXRotation = 15f;
    [SerializeField] private float _rotationSpeed = 10f;

    [Header("Cannon transform setup")]
    [SerializeField] private Transform _cannonBarrelTransform;
    [SerializeField] private Transform _cannonBaseTransform;

    [Header("Cannon ball")]
    [SerializeField] private float _projectileForce;
    [SerializeField] private CannonBall _projectilePrefab;
    [SerializeField] private Transform _firePointTransform;


    void Start()
    {
        
    }

    void Update()
    {
        AimCannon();
        TryFireCannon();
        CannonRaycast();
    }

    private void AimCannon()
    {
        // cannon base rotation in the X
        float newBaseRotation = _cannonBaseTransform.localRotation.eulerAngles.y + _rotationSpeed * Input.GetAxis("Mouse X");
        newBaseRotation = Mathf.Clamp(newBaseRotation, _minYRotation, _maxYRotation);
        _cannonBaseTransform.localRotation = Quaternion.Euler(0, newBaseRotation, 0);

        // cannon barrel rotation in the Y 
        float newBarrelRotation = _cannonBarrelTransform.localRotation.eulerAngles.x - _rotationSpeed * Input.GetAxis("Mouse Y");
        newBarrelRotation = Mathf.Clamp(newBarrelRotation, _minXRotation, _maxXRotation);
        _cannonBarrelTransform.localRotation = Quaternion.Euler(newBarrelRotation, 0, 0);
    }

    private void TryFireCannon()
    {
        if (!Input.GetButtonDown("Fire1"))
        {
            return;
        }

        CannonBall instantiatedBall = Instantiate(_projectilePrefab, _firePointTransform.position, Quaternion.identity);
        instantiatedBall.Setup(_firePointTransform.forward * _projectileForce);
    }

    private void CannonRaycast()
    {
        // shoots the ray from the cannons firepoint forward and stores results into m_results
        RaycastHit[] m_results = new RaycastHit[1];
        int hits = Physics.RaycastNonAlloc(_firePointTransform.position, _firePointTransform.forward, m_results, 100, LayerMask.GetMask("Targets"));

        if (hits == 0) { return; }
        
        // goes through the results and sets the hit gameobjects color to red
        for (int i = 0; i < hits; i++)
        {
            Material hitMaterial = m_results[i].collider.GetComponent<Renderer>().material;
            hitMaterial.color = Color.red;
        }
    }
}
