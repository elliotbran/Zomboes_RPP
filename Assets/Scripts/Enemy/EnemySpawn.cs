using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnsPoints;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform spawn in spawnsPoints)
        {
            Debug.Log("Trying to spawn at: " + spawn.position);
            SpawnEnemyAt(spawn.position);
        }
    }

    void SpawnEnemyAt(Vector3 position)
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(position, out hit, 2.0f, NavMesh.AllAreas))
        {
            Debug.Log("Spawned enemy at: " + hit.position);
            GameObject enemy = Instantiate(enemyPrefab, hit.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("No valid NavMesh position near: " + position);
        }
    }
}