using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PrefabMovement : MonoBehaviour
{

    public GameObject player;

    public NavMeshAgent agent;

    private bool targeted = false;

    private float distance;

    // Start is called before the first frame update
    void Start()
    {

            
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(player.transform.position, agent.transform.position);

        if (distance > 5 && distance < 10)
        {
            agent.speed = 15;
        }

        else if (distance >= 10)
        {
            agent.speed = 1.5f;
        }

        else
        {
            agent.speed = 4.5f;
        }

        if (!targeted)
        {
            StartCoroutine(ChasePlayer());
            targeted = true;
        }
    }

    IEnumerator ChasePlayer()
    {
        agent.SetDestination(player.transform.position);
        yield return new WaitForSeconds(.5f);
        targeted = false;
    }

}
