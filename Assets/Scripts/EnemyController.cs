using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform objective;
    public float velocity;
    public NavMeshAgent IA;

    // Update is called once per frame
    void Update()
    {
        IA.speed = velocity;
        IA.SetDestination(objective.position);
    }
}
