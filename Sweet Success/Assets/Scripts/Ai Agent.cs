using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiAgent : MonoBehaviour
{
    private NavMeshAgent agent; // Reference to the NavMeshAgent component
    [SerializeField] private Transform movePos; // The target position the agent will move to
    public Animator npcAnimator; // Reference to the Animator component

    private void Awake()
    {
        // Get the NavMeshAgent component attached to this GameObject
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        // Set the initial destination
        SetDestination(movePos.position);
    }

    void Update()
    {
        // Check if the agent has reached the destination
        if (Vector3.Distance(agent.transform.position, movePos.position) > 1f)
        {
            // If not reached, set the destination and play walking animation
            if (agent.destination != movePos.position)
            {
                SetDestination(movePos.position);
            }

            // Check if the agent is moving
            if (agent.velocity.magnitude > 0.1f)
            {
                npcAnimator.SetBool("isWalking", true); // Set walking animation
            }
            else
            {
                npcAnimator.SetBool("isWalking", false); // Set idle animation if not moving
            }
        }
        else
        {
            // Stop the agent when close to the destination
            agent.isStopped = true;
            npcAnimator.SetBool("isWalking", false); // Set idle animation
        }
    }

    private void SetDestination(Vector3 destination)
    {
        agent.SetDestination(destination);
        agent.isStopped = false; // Ensure the agent is not stopped when setting a new destination
    }

    private void OnTriggerEnter(Collider other)
    {
        // Implement interaction logic here
        if (other.CompareTag("Player"))
        {
            // Example: Stop moving and play a greeting animation
            agent.isStopped = true;
            npcAnimator.SetTrigger("Greet");
        }
    }
}
