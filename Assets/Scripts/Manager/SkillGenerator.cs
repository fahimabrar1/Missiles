using System.Collections.Generic;
using DefaultNamespace;
using Skills;
using Skills.model;
using UnityEngine;

public class SkillGenerator : MonoBehaviour
{
    [Header("SkillSpawn Settings")] public SkillModel coinPrefab;

    public List<SkillModel> skillPrefabs; // List of skill prefabs (e.g., shield, speed boost)
    public int minRespawn = 10; // Max number of objects in the scene
    public int maxRespawn = 30; // Max number of objects in the scene


    public int maxObjects = 10; // Max number of objects in the scene
    public float minRadius = 5f; // Minimum spawn radius
    public float maxRadius = 20f; // Maximum spawn radius
    public Transform playerTransform; // Reference to the player's position
    public IndicatorManager indicatorManager; // Reference to the IndicatorManage
    private readonly List<GameObject> spawnedObjects = new(); // List of spawned objects

    private void Start()
    {
        Invoke(nameof(GenerateSkillOrCoin), Random.Range(minRespawn, maxRespawn));
    }

    public void GenerateSkillOrCoin()
    {
        if (spawnedObjects.Count >= maxObjects) return;

        // Randomly decide whether to spawn a coin or a skill
        var isCoin = Random.value < 0.5f; // 50% chance for each
        var skillModel = isCoin ? coinPrefab : GetRandomSkillPrefab();

        // Generate a random position within the radius
        var spawnPosition = GetRandomPositionWithinRadius();

        // Spawn the object
        var spawnedObject = Instantiate(skillModel.skillPrefab, spawnPosition, Quaternion.identity);

        spawnedObjects.Add(spawnedObject);
        var iInddicator = indicatorManager.CreateIndicator(skillModel.indicatorPrefab, spawnedObject.transform);

        if (spawnedObject.TryGetComponent(out SkillPoint skillPoint)) skillPoint.Initialize(iInddicator);
        Invoke(nameof(GenerateSkillOrCoin), Random.Range(minRespawn, maxRespawn));
    }

    private Vector3 GetRandomPositionWithinRadius()
    {
        var direction = Random.insideUnitCircle.normalized; // Random direction
        var distance = Random.Range(minRadius, maxRadius); // Random distance within range
        var spawnPosition = new Vector3(direction.x, 0, direction.y) * distance;

        // Offset by player's position
        return new Vector3(playerTransform.position.x+spawnPosition.x, playerTransform.position.y+spawnPosition.y, 0f);
    }

    private SkillModel GetRandomSkillPrefab()
    {
        return skillPrefabs[Random.Range(0, skillPrefabs.Count)];
    }
}