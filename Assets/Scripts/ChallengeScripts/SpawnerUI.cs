using UnityEngine;
using TMPro;

public class SpawnerUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _countText;
    [SerializeField] private SphereSpawner _spawner;

    private int sphereCount = 0;

    private void OnEnable()
    {
        _spawner.onSphereSpawned += SphereSpawned;
    }

    private void OnDisable()
    {
        _spawner.onSphereSpawned -= SphereSpawned;

    }

    /// <summary>
    /// Increases sphereCount by 1 each time a sphere is spawned by the SphereSpawner and updats UI to reflect the count increase
    /// </summary>
    private void SphereSpawned()
    {
        sphereCount++;
        _countText.text = $"Count: {sphereCount}";
    }
}
