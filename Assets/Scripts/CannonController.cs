using CannonApp;
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

    [SerializeField] private PoolObjectId _cannonBallTypeShot;
    [SerializeField] private ObjectsPool _pool;

    [Header("Use Keyboard")]
    [SerializeField] private bool _useKeyboard;

    private bool _fireDisabled;
    private ICannonInputScheme _inputScheme;

    private void Awake()
    {
        if (_useKeyboard)
        {
            _inputScheme = new CannonKeyboardInputScheme();
        }
        else
        {
            _inputScheme = new CannonMouseInputScheme();
        }

        _pool.Setup(20);
    }

    private void Start()
    {
        GameServices.GetService<LevelController>().levelEnded += DisableFire;
    }

    void Update()
    {
        AimCannon();
        TryFireCannon();
        CannonRaycast();
    }

    private void AimCannon()
    {
        var input = _inputScheme.AimInput();

        // cannon base rotation in the X
        float newBaseRotation = _cannonBaseTransform.localRotation.eulerAngles.y + _rotationSpeed * input.x;
        newBaseRotation = Mathf.Clamp(newBaseRotation, _minYRotation, _maxYRotation);
        _cannonBaseTransform.localRotation = Quaternion.Euler(0, newBaseRotation, 0);

        // cannon barrel rotation in the Y 
        float newBarrelRotation = _cannonBarrelTransform.localRotation.eulerAngles.x - _rotationSpeed * input.y;
        newBarrelRotation = Mathf.Clamp(newBarrelRotation, _minXRotation, _maxXRotation);
        _cannonBarrelTransform.localRotation = Quaternion.Euler(newBarrelRotation, 0, 0);
    }

    private void TryFireCannon()
    {
        if (_fireDisabled || !_inputScheme.FireTriggered())
        {
            return;
        }

        CannonBall instantiatedBall = _pool.GetObject<CannonBall>(_cannonBallTypeShot);
        instantiatedBall.transform.position = _firePointTransform.position;
        instantiatedBall.Setup(_firePointTransform.forward * _projectileForce, _pool);
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

    public void DisableFire()
    {
        _fireDisabled = true;
        _inputScheme.Dispose();
    }
}
