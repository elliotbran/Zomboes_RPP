using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class RoundManager : MonoBehaviour
{
    public static RoundManager instance;
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public int round = 1;
    public int enemiesToSpawn = 1;
    public int enemiesAlive = 0;

    public TextMeshProUGUI roundText;
    public TextMeshProUGUI enemiesLeftText;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartRound();    
    }

    void StartRound()
    {
        enemiesAlive = 0;
        foreach (Transform spawn in spawnPoints)
        {
            for (int i = 0; i < round; i++)
            {
                SpawnEnemy(spawn.position);
            }
        }

        UpdateUI();
    }

    void SpawnEnemy(Vector3 position)
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(position, out hit, 2.0f, NavMesh.AllAreas))
        {
            Debug.Log("Spawned enemy at: " + hit.position);
            GameObject enemy = Instantiate(enemyPrefab, hit.position, Quaternion.identity);
            enemiesAlive++;
        }
    }

    public void EnemyKilled()
    {
        enemiesAlive--;
        UpdateUI();

        if (enemiesAlive <= 0)
        {
            Invoke(nameof(NextRound), 2f);
        }
    }

    void NextRound()
    {
        round++;
        enemiesToSpawn++;
        StartRound();
    }

    void UpdateUI()
    {
        if (roundText != null)
        {
            roundText.text = "Ronda: " + round;
        }
        if (enemiesLeftText != null)
        {
            enemiesLeftText.text = "Enemigos: " + enemiesAlive;
        }
    }
}
