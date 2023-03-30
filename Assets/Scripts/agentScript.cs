using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class agentScript : MonoBehaviour
{
    NavMeshAgent _agent;
    public GameObject _target; 


    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.SetDestination(_target.GetComponent<Transform>().position);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
