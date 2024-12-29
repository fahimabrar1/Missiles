using Indicator;
using Interfaces;
using UnityEngine;

public class MissileGenerator : MonoBehaviour
{
    public HomingMissile homingMissilePrefab; // Prefab for the missile
    public GameObject indicatorPrefab; // Prefab for the missile indicator
    public Transform player; // Player reference
    public float spawnRange = 10f; // Distance outside the camera for spawning missiles
    public float spawnInterval = 2f; // Time interval between spawns
    private Camera _mainCamera; // Reference to the main camera

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void Start()
    {
        InvokeRepeating(nameof(SpawnMissile), spawnInterval, spawnInterval);
    }

    private void SpawnMissile()
    {
        // Get a random spawn position outside the camera view
        var spawnPosition = GetRandomSpawnPosition();

        // Instantiate the missile
        var missile = Instantiate(homingMissilePrefab, spawnPosition, Quaternion.identity);
        missile.Initialize(player);
        missile.OnMissileDestroyed += HandleMissileDestroyed;

        if(missile==null) return;
 
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Vector3[] edges = {
            _mainCamera.ViewportToWorldPoint(new Vector3(-0.1f, Random.value, 10f)), // Left
            _mainCamera.ViewportToWorldPoint(new Vector3(1.1f, Random.value, 10f)),  // Right
            _mainCamera.ViewportToWorldPoint(new Vector3(Random.value, -0.1f, 10f)), // Bottom
            _mainCamera.ViewportToWorldPoint(new Vector3(Random.value, 1.1f, 10f))   // Top
        };
        return edges[Random.Range(0, edges.Length)];
    }
    
    private void HandleMissileDestroyed(HomingMissile missile)
    {
        // Find and destroy the related indicator if needed
        // var indicator = FindIndicatorForMissile(missile);
        // if (indicator != null) Destroy(indicator.gameObject);
    }

}