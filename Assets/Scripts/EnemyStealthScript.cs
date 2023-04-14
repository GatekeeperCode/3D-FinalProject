using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStealthScript : MonoBehaviour
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

    }

    // Update is called once per frame
    void Update()
    {

        Vector3 targetDir = player.transform.position - transform.position;
        float angle = Vector3.Angle(targetDir, transform.forward);

        if ((seePlayer() && angle < 45.0f) || alert)
        {
            agent.SetDestination(player.transform.position);
        }
    }

    private void OnEnable()
    {
        Messenger.AddListener(Messages.LIGHT_ALERT, Alert);
    }

    public void Alert()
    {
        alert = true;
        //print("hi");

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




