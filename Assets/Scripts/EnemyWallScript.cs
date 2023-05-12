using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWallScript : MonoBehaviour
{
    [SerializeField]
    private List<Transform> _waypoints;
    private int _target;
    private NavMeshAgent agent;


    public GameObject player;
    public LayerMask _player;
    public bool alert = false;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(_waypoints[0].position);
        player = GameObject.FindGameObjectWithTag("Player");
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
                _waypoints.Reverse();
                _target = 1;
            }
            agent.SetDestination(_waypoints[_target].position);
        }
    }

}




