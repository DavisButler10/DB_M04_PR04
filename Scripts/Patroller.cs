using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patroller : MonoBehaviour
{
	NavMeshAgent agent;
	bool patrolling;
	public Transform[] patrolTargets;
	private int destPoint;
	bool arrived;

	//get the animator controller from the patrolling unit
	Animator anim;
	//give a place in the inspector to place a target to look for, the character
	public Transform attackTarget;
	//last known position to get it a reference to get back on track
	Vector3 lastKnownPosition;
	//place to put a game object to shoot a ray
	public Transform eye;
	//attack range to decide when to do the attack animation
	public float attackRange = 4;

	public bool canSee;


	void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		anim = GetComponentInChildren<Animator>();
		lastKnownPosition = transform.position;
	}

	//boolean to test whether it can see the target
	//assigning a function to the variable to decide whether true or false
	bool CanSeeTarget()
	{
		//initially set to false, since we most likeley wont be seeing anything
		canSee = false;
		//create a ray based on the eye position and point it to the target
		Ray ray = new Ray(eye.position, attackTarget.transform.position - eye.position);
		//create the hit variable for the ray that is cast
		RaycastHit hit;

		//check if it is hitting anything, check what it is
		if (Physics.Raycast(ray, out hit))
		{
			//if it isnt the target, set to false again
			if (hit.transform != attackTarget)
			{
				canSee = false;
			}
			//otherwise it hit the target, lets chase
			else
			{
				//get the position of the target
				lastKnownPosition = attackTarget.transform.position;
				canSee = true;
			}
		}

		//must return the variable
		return canSee;
	}

	void Update()
	{
		if (agent.pathPending)
		{
			return;
		}
		if (patrolling)
		{
			if (agent.remainingDistance < agent.stoppingDistance)
			{
				if (!arrived)
				{
					arrived = true;
					StartCoroutine("GoToNextPoint");
				}
			}
		}

		//get to target if it can be seen
		if (CanSeeTarget())
		{
			//get the target position
			agent.SetDestination(attackTarget.transform.position);
			//stop the patrolling
			patrolling = false;
			//if it is closer than the attack range, start the attack animation
			if (agent.remainingDistance < attackRange)
			{
				anim.SetBool("Attack", true);
				agent.stoppingDistance = attackRange;
			}
			else
			{
				anim.SetBool("Attack", false);
			}
		}
		//if it cant see the target, then stop attacking
		else
		{
			anim.SetBool("Attack", false);
			//if not patrolling, get back to patrolling
			if (!patrolling)
			{
				agent.SetDestination(lastKnownPosition);
				if(agent.remainingDistance < agent.stoppingDistance)
				{
					patrolling = true;
					StartCoroutine("GoToNextPoint");
				}
			}
		}
		anim.SetFloat("Blend", agent.velocity.sqrMagnitude);
	}
	
	IEnumerator GoToNextPoint()
	{
		if(patrolTargets.Length == 0)
		{
			yield break;
		}
		patrolling = true;

		yield return new WaitForSeconds(2f);
		arrived = false;
		agent.destination = patrolTargets[destPoint].position;
		destPoint = (destPoint + 1) % patrolTargets.Length;
	}
}
