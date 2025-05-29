using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(iA.transform.position, range);
    }
}
