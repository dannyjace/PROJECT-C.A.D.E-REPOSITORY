using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnStack
{
    [Tooltip("Label for this group (used for debugging or UI)")]
    public string spawnerName;

    [Tooltip("Prefabs to spawn from this group")]
    public GameObject[] spawner;

    [Tooltip("Number of objects to attempt spawning")]
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

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    [SerializeField] private List<Spawner> spawnerStack;
    public List<Spawner> SpawnerStack => spawnerStack;
    [SerializeField] private AdvancedPlayerController playerController;


    public bool isSpawning = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ActivateSpawner(Spawner spawner)
    {
        spawner.GetComponentInChildren<Spawner>().enabled = true;

    }

    public void DeactivateSpawner(Spawner spawner)
    {
        spawner.GetComponentInChildren<Spawner>().enabled = false;
    }

    public void ClearSpawners()
    {
        foreach (Spawner spawner in spawnerStack)
        {
            spawner.GetComponentInChildren<Spawner>().enabled = false;
        }
        // remove current spawners from the stack
        spawnerStack[0] = null;
    }
}
