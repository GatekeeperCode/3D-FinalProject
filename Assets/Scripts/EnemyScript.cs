using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    [SerializeField]
    private List<Transform> _waypoints;
    private int _target;
    private NavMeshAgent agent;

    public GameObject player;
    public LayerMask _player;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(_waypoints[0].position);

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetDir = player.transform.position - transform.position;
        float angle = Vector3.Angle(targetDir, transform.forward);

        if (seePlayer() && angle < 45.0f)
        {
            agent.SetDestination(player.transform.position);
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "waypoint")
        {
            _target++;
            if (_target == _waypoints.Count)
            {
                _waypoints.Reverse();
                _target = 1;
            }
            agent.SetDestination(_waypoints[_target].position);
        }
    }

    public bool seePlayer()
    {
        Vector3 rayDirection = player.transform.position - transform.position;
        if (Physics.Raycast(transform.position, rayDirection, 20f, _player))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

