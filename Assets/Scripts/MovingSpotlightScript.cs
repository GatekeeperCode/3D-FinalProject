using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovingSpotlightScript : MonoBehaviour
{
    [SerializeField]
    private List<Transform> _waypoints;
    private int _target;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(_waypoints[0].position);
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "waypoint")
        {
            _target++;
            if (_target == _waypoints.Count)
            {
                _target = 0;
            }
            agent.SetDestination(_waypoints[_target].position);
        }
    }

}




