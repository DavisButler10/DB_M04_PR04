using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavAnimation : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();        
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("BlendAstra", agent.velocity.sqrMagnitude);
        Debug.Log(agent.velocity.sqrMagnitude);
    }
}
