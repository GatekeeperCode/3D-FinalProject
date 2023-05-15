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
    Animator animator;

    public AudioSource _source;
    public AudioClip _hey;
    bool playOnce = true;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(_waypoints[0].position);
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 targetDir = player.transform.position - transform.position;
        float yDifference = player.transform.position.y - transform.position.y;
        float angle = Vector3.Angle(targetDir, transform.forward);

        if ((seePlayer() && angle < 45.0f && yDifference < 1) || alert)
        {
            animator.SetBool("animAlert", true);
            agent.SetDestination(player.transform.position);
            agent.speed = 10;
            if (!alert && playOnce) {
                // if only one guard sees the player
                _source.PlayOneShot(_hey);
                playOnce = false;
            }
        }
    }

    private void OnEnable()
    {
        Messenger.AddListener(Messages.LIGHT_ALERT, Alert);
    }

    public void Alert()
    {
        alert = true;
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




