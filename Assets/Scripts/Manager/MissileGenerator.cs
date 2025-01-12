using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

public class MissileGenerator : MonoBehaviour
{
    public HomingMissile homingMissilePrefab; // Prefab for the missile
    public GameObject homingMissileIndicatorPrefab; // Prefab for the missile
    public Transform player; // Player reference
    public float spawnRange = 10f; // Distance outside the camera for spawning missiles
    public float spawnAfterDelay = 6f; // Delay before spawning missiles
    public float spawnInterval = 2f; // Time interval between spawns
    public IndicatorManager indicatorManager; // Reference to the IndicatorManager
    public List<HomingMissile> activeMissiles = new();
    private Camera _mainCamera;
    private ComponentPoolManager<HomingMissile> _objectPoolManager;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        _objectPoolManager = new ComponentPoolManager<HomingMissile>(homingMissilePrefab, 10);
        InvokeRepeating(nameof(SpawnMissile), spawnAfterDelay, spawnInterval);
    }

    private void OnDisable()
    {
        // Cancel the InvokeRepeating when the script is disabled
        CancelInvoke(nameof(SpawnMissile));
    }

    private void SpawnMissile()
    {
        var spawnPosition = GetRandomSpawnPosition();

        // Instantiate the missile
        var missile = _objectPoolManager.Get();
        missile.transform.SetPositionAndRotation(spawnPosition, Quaternion.identity);
        // missile.OnMissileDestroyed += HandleMissileDestroyed;
        missile.Initialize(player, this);
        activeMissiles.Add(missile);
        if (missile == null) return;

        // Create the indicator using IndicatorManager
        var indicatorScript = indicatorManager.CreateIndicator(homingMissileIndicatorPrefab, missile.transform);
        missile.Indicator = indicatorScript;
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Vector3[] edges =
        {
            _mainCamera.ViewportToWorldPoint(new Vector3(-0.1f, Random.value, 10f)), // Left
            _mainCamera.ViewportToWorldPoint(new Vector3(1.1f, Random.value, 10f)), // Right
            _mainCamera.ViewportToWorldPoint(new Vector3(Random.value, -0.1f, 10f)), // Bottom
            _mainCamera.ViewportToWorldPoint(new Vector3(Random.value, 1.1f, 10f)) // Top
        };
        return edges[Random.Range(0, edges.Length)];
    }


    public void DestroyAllMissilesAndDeactivate()
    {
        foreach (var t in activeMissiles)
            _objectPoolManager.ReturnToPool(t);
        indicatorManager.DestroyAllIndicators();
        activeMissiles.Clear();
        enabled = false;
    }

    public void ReturnToPool(HomingMissile homingMissile)
    {
        activeMissiles.Remove(homingMissile);
        _objectPoolManager.ReturnToPool(homingMissile);
    }
}