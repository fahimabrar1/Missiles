using System.Collections.Generic;
using UnityEngine;

public class SkillGenerator : MonoBehaviour
{
    [Header("Spawn Settings")] public GameObject coinPrefab;

    public List<GameObject> skillPrefabs; // List of skill prefabs (e.g., shield, speed boost)
    public int maxObjects = 10; // Max number of objects in the scene
    public float minRadius = 5f; // Minimum spawn radius
    public float maxRadius = 20f; // Maximum spawn radius
    public Transform playerTransform; // Reference to the player's position

    [Header("Indicators")] public GameObject indicatorPrefab; // Prefab for indicators

    private readonly List<GameObject> indicators = new(); // List of indicators

    private readonly List<GameObject> spawnedObjects = new(); // List of spawned objects

    private void Update()
    {
        UpdateIndicators();
    }

    public void GenerateSkillOrCoin()
    {
        if (spawnedObjects.Count >= maxObjects) return;

        // Randomly decide whether to spawn a coin or a skill
        var isCoin = Random.value < 0.5f; // 50% chance for each
        var prefab = isCoin ? coinPrefab : GetRandomSkillPrefab();

        // Generate a random position within the radius
        var spawnPosition = GetRandomPositionWithinRadius();

        // Spawn the object
        var spawnedObject = Instantiate(prefab, spawnPosition, Quaternion.identity);
        spawnedObjects.Add(spawnedObject);

        // Spawn an indicator for the object
        var indicator = Instantiate(indicatorPrefab, playerTransform.position, Quaternion.identity, playerTransform);
        indicators.Add(indicator);
    }

    private Vector3 GetRandomPositionWithinRadius()
    {
        var direction = Random.insideUnitCircle.normalized; // Random direction
        var distance = Random.Range(minRadius, maxRadius); // Random distance within range
        var spawnPosition = new Vector3(direction.x, 0, direction.y) * distance;

        // Offset by player's position
        return playerTransform.position + spawnPosition;
    }

    private GameObject GetRandomSkillPrefab()
    {
        return skillPrefabs[Random.Range(0, skillPrefabs.Count)];
    }

    private void UpdateIndicators()
    {
        for (var i = 0; i < spawnedObjects.Count; i++)
            if (spawnedObjects[i] == null)
            {
                // Clean up destroyed objects
                Destroy(indicators[i]);
                indicators.RemoveAt(i);
                spawnedObjects.RemoveAt(i);
                i--;
            }
            else
            {
                // Update indicator position to point to the object
                var direction = (spawnedObjects[i].transform.position - playerTransform.position).normalized;
                indicators[i].transform.position = playerTransform.position + direction * 2f; // Offset from player
                indicators[i].transform.LookAt(spawnedObjects[i].transform.position);
            }
    }
}