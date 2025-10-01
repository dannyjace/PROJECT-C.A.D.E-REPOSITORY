using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnGroup
{
    [Tooltip("Label for this group (used for debugging or UI)")]
    public string groupName;

    [Tooltip("Prefabs to spawn from this group")]
    public GameObject[] objects;

    [Tooltip("Number of objects to spawn")]
    public int count = 1;

    [Tooltip("Time Interval to spawn in seconds")]
    [Range(0, 60)]
    public int spawnRate = 5;

    [Tooltip("Number of objects to spawn at a time")]
    public int spawnsPerInterval = 1;

    [Tooltip("Spawn positions for this group")]
    public Transform[] spawnPositions;
    [HideInInspector] public int spawnedCount;
}


public class Spawner : MonoBehaviour
{

    [Header("Spawner Mode")]
    [Tooltip("Enable automatic spawning at runtime")]
    [SerializeField] private bool autoSpawn = false;
    [SerializeField] private GameObject[] autoSpawnObjects;
    [SerializeField] private int autoSpawnCount;
    [SerializeField] private int autoSpawnObjAtATime;
    [SerializeField] private int autoSpawnRate;
    [SerializeField] private float autoSpawnRadius;

    //[Header("Manual Spawn Groups")]

    [SerializeField] private SpawnGroup mainGroup;
    [SerializeField] private SpawnGroup secondaryGroup;
    [SerializeField] private SpawnGroup tertiaryGroup;
    [SerializeField] private SpawnGroup quaternaryGroup;


    //[Header("Boss Settings")]
    [SerializeField] private bool isBossSpawner = false;
    [SerializeField] private GameObject[] bossObjects;
    [SerializeField] private Transform[] bossSpawnPositions;
    [SerializeField][Range(0, 120f)] private float bossSpawnRate;
    [SerializeField] private int bossesAtATime;
    [Tooltip("Total spawns completed is normalized from (0–100%)")]
    [SerializeField][Range(0, 1f)] private float spawnAtCompletionProgess;



    private Dictionary<SpawnGroup, float> groupTimers = new();

    private float autoSpawnTimer;
    private int autoSpawned;


    private float bossTimer;
    private int bossesSpawned;

    private bool bossSpawned;
    private bool startSpawning;
    private void Start()
    {
        groupTimers[mainGroup] = 0;
        groupTimers[secondaryGroup] = 0;
        groupTimers[tertiaryGroup] = 0;
        groupTimers[quaternaryGroup] = 0;


        if (autoSpawn)
        {
            autoSpawnTimer = 0;
            autoSpawned = 0;
        }

        if (isBossSpawner)
        {
            bossSpawned = false;
            bossesSpawned = 0;
        }

        startSpawning = false;
    }

    private void Update()
    {
        if (startSpawning)
        {

            SpawnNow();
        }
    }

    private void TryAutoSpawn(float delta)
    {
        if (autoSpawnObjects == null || autoSpawnObjects.Length == 0 || autoSpawnCount <= 0)
        {
            Debug.LogWarning("AutoSpawn-Error. No objects to spawn");
            return;
        }

        autoSpawnTimer += delta;
        if (autoSpawnTimer >= autoSpawnRate && autoSpawnCount > autoSpawned)
        {
            autoSpawnTimer = 0f;

            SpawnAutoObjects();
        }
    }
    private void TrySpawnGroup(SpawnGroup group, float delta)
    {
        if (group == null || group.objects == null || group.spawnPositions == null ||
            group.objects.Length == 0 || group.spawnPositions.Length == 0 || group.count <= 0)
            return;

        groupTimers[group] += delta;
        if (groupTimers[group] >= group.spawnRate && group.count > group.spawnedCount) // Check every second
        {
            groupTimers[group] = 0f;
            SpawnGroupObjects(group);


        }
    }


    private void TryBoss(float delta)
    {
        if (bossObjects == null || bossObjects.Length == 0 || bossSpawnPositions == null || bossSpawnPositions.Length == 0)
        {
            Debug.LogWarning("No Boss objects to spawn.");
            return;
        }

        bossTimer += delta;
        if (bossTimer >= bossSpawnRate && bossObjects.Length > bossesSpawned) // Check every second
        {
            bossTimer = 0f;
            SpawnBoss();
        }

    }

    private void SpawnAutoObjects()
    {


        int arrayPos = Random.Range(0, autoSpawnObjects.Length);
        Vector3 randomPos = transform.position + Random.insideUnitSphere * autoSpawnRadius;
        randomPos.y = transform.position.y; // Keep the same height as the spawner
        Instantiate(autoSpawnObjects[arrayPos], randomPos, Quaternion.identity);

        autoSpawned++;
        HUDManager.instance.updateGameGoal(1);

    }

    private void SpawnGroupObjects(SpawnGroup group)
    {
        int arrayPos = Random.Range(0, group.spawnPositions.Length);
        int arrayObjPos = Random.Range(0, group.objects.Length);

        Instantiate(group.objects[arrayObjPos], group.spawnPositions[arrayPos].position, group.spawnPositions[arrayPos].rotation);
        group.spawnedCount++;
        HUDManager.instance.updateGameGoal(1);

    }

    public void SpawnNow()
    {
        float delta = Time.deltaTime;

        if (autoSpawn)
        {
            TryAutoSpawn(delta);
        }
        else if (!autoSpawn)
        {
            TrySpawnGroup(mainGroup, delta);
            TrySpawnGroup(secondaryGroup, delta);
            TrySpawnGroup(tertiaryGroup, delta);
            TrySpawnGroup(quaternaryGroup, delta);
        }

        if (isBossSpawner && !bossSpawned && GetOverallSpawnProgress() >= spawnAtCompletionProgess)
        {
            TryBoss(delta);
        }
    }
    private void SpawnBoss()
    {


        if (bossObjects.Length > 1)
        {
            int arrayPos = Random.Range(0, bossSpawnPositions.Length);
            int arrayObjPos = Random.Range(0, bossObjects.Length);

            Instantiate(bossObjects[arrayObjPos], bossSpawnPositions[arrayPos].position, bossSpawnPositions[arrayPos].rotation);

            bossesSpawned++;
            HUDManager.instance.updateGameGoal(1);
        }
        else if (bossObjects.Length == 1)
        {
            Instantiate(bossObjects[0], bossSpawnPositions[0].position, bossSpawnPositions[0].rotation);
            bossesSpawned++;
            bossSpawned = true;
            HUDManager.instance.updateGameGoal(1);
        }


    }

    private float GetGroupSpawnProgress(SpawnGroup group)
    {
        if (group == null || group.count == 0) return 0;
        return (float)group.spawnedCount / group.count;
    }

    private float GetOverallSpawnProgress()
    {
        int totalToSpawn = 0;
        int totalSpawned = 0;
        SpawnGroup[] groups = { mainGroup, secondaryGroup, tertiaryGroup, quaternaryGroup };
        foreach (var group in groups)
        {
            if (group != null)
            {
                totalToSpawn += group.count;
                totalSpawned += group.spawnedCount;
            }
        }
        if (totalToSpawn == 0) return 0;

        return (float)totalSpawned / totalToSpawn;
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered spawner trigger.");
            startSpawning = true;
        }
    }
}
