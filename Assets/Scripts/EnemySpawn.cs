using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnsPoints;
    public float spawnInterval = 7;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform spawn in spawnsPoints)
        {
            SpawnEnemyAt(spawn.position);
        }
    }

    void SpawnEnemyAt(Vector3 position)
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(position, out hit, 2.0f, NavMesh.AllAreas))
        {
            GameObject enemy = Instantiate(enemyPrefab, hit.position, Quaternion.identity);
        }
    }
}
