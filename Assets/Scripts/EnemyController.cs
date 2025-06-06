using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform objective;
    public float velocity;
    public NavMeshAgent iA;
    public float range;
    public float distance;
    public bool persiguiendo;

    public int life = 100;

    private void Start()
    {
        iA = GetComponent<NavMeshAgent>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            objective = player.transform;
        }

        velocity = Random.Range(2f, 8f);
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(iA.transform.position, objective.position);
        if (distance < range)
        {
            persiguiendo = true;
        }
        else if (distance > range + 3)
        {
            persiguiendo = false;
        }

        if (persiguiendo == false)
        {
            iA.speed = 0;
        }
        else if (persiguiendo == true)
        {
            iA.speed = velocity;
            iA.SetDestination(objective.position);
        }

        if (life <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(int amount)
    {
        life -= amount;
        Debug.Log("Enemigo dañado, Vida restante: " + life);
    }

    void Die()
    {
        if (RoundManager.instance != null)
        {
            RoundManager.instance.EnemyKilled();
        }

        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player_Health player_Health = collision.gameObject.GetComponent<Player_Health>();
            if (player_Health != null)
            {
                player_Health.TakeDamage(20);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(iA.transform.position, range);
    }

}