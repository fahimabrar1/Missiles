using System;
using System.Collections.Generic;
using DefaultNamespace;
using Skills;
using Skills.model;
using UnityEngine;
using Random = UnityEngine.Random;

public class SkillGenerator : MonoBehaviour
{
    public int minRespawn = 10; // Max number of objects in the scene
    public int maxRespawn = 20; // Max number of objects in the scen
    public int maxObjects = 10; // Max number of objects in the scene
    public float minRadius = 5f; // Minimum spawn radius
    public float maxRadius = 20f; // Maximum spawn radius
    public Transform playerTransform; // Reference to the player's position
    public IndicatorManager indicatorManager; // Reference to the IndicatorManage

    public List<SkillSo> skills;
    [Header("SkillSpawn Settings")] private readonly List<GameObject> spawnedObjects = new(); // List of spawned objects


    private void OnEnable()
    {
        Invoke(nameof(GenerateSkillOrCoin), Random.Range(minRespawn, maxRespawn));
    }

    private void OnDisable()
    {
        // Cancel the InvokeRepeating when the script is disabled
        CancelInvoke(nameof(GenerateSkillOrCoin));
    }

    public void GenerateSkillOrCoin()
    {
        if (spawnedObjects.Count >= maxObjects) return;

        // Randomly decide whether to spawn a coin or a skill
        var isCoin = Random.value < 0.5f; // 50% chance for each
        var skillModel = GetRandomSkillPrefab();

        // Generate a random position within the radius
        var spawnPosition = GetRandomPositionWithinRadius();

        // Spawn the object
        var spawnedObject = Instantiate(skillModel.skillModel.skillPrefab, spawnPosition, Quaternion.identity);

        spawnedObjects.Add(spawnedObject);
        var iInddicator =
            indicatorManager.CreateIndicator(skillModel.skillModel.indicatorPrefab, spawnedObject.transform);

        if (spawnedObject.TryGetComponent(out SkillPoint skillPoint)) skillPoint.Initialize(iInddicator, skillModel);
        Invoke(nameof(GenerateSkillOrCoin), Random.Range(minRespawn, maxRespawn));
    }

    private Vector3 GetRandomPositionWithinRadius()
    {
        try
        {
            var direction = Random.insideUnitCircle.normalized; // Random direction
            var distance = Random.Range(minRadius, maxRadius); // Random distance within range
            var spawnPosition = new Vector3(direction.x, 0, direction.y) * distance;

            // Offset by player's position
            return new Vector3(playerTransform.position.x + spawnPosition.x,
                playerTransform.position.y + spawnPosition.y, 0f);
        }
        catch (Exception)
        {
            return Vector3.zero;
        }
    }

    private SkillSo GetRandomSkillPrefab()
    {
        return skills[Random.Range(0, skills.Count)];
    }

    public void ConsumedSkill(SkillPoint skillPoint)
    {
        spawnedObjects.Remove(skillPoint.gameObject);
        Destroy(skillPoint.gameObject);
    }

    public void DestroyAllSkills()
    {
        for (var i = spawnedObjects.Count - 1; i >= 0; i--) Destroy(spawnedObjects[i].gameObject);
        spawnedObjects.Clear();
        enabled = false;
    }
}