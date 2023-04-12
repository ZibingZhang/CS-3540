using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TutorialNinjaAI : MonoBehaviour
{
    private enum FSMState
    {
        Idle,
        Walk,
        Attack,
        Dead,
    }

    private GameObject player;
    private Transform projectileParent;

    public GameObject attackProjectilePrefab;
    public GameObject specialProjectilePrefab;
    public float percentAttack = 0.5f;

    private FSMState currentState;
    private NavMeshAgent navMeshAgent;
    private GameObject[] wanderPoints;
    private Animator animator;
    private Vector3 destination;

    private const float shootTime = 1.5f;
    private float shootDelay = 0;
    private bool hasAttacked = false;

    // for tutorial only set to false
    public bool shouldAttack = false;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        projectileParent = GameObject.FindGameObjectWithTag("ProjectileParent").transform;
        wanderPoints = GameObject.FindGameObjectsWithTag("WanderPoint");

        currentState = FSMState.Walk;
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        ChooseNextDestination();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case FSMState.Idle:
                UpdateIdleState();
                break;

            case FSMState.Walk:
                UpdateWalkState();
                break;

            case FSMState.Attack:
                UpdateAttackState();
                break;

            case FSMState.Dead:
                UpdateDeadState();
                break;
        }
    }

    private void UpdateIdleState()
    {

    }

    private void UpdateWalkState()
    {
        animator.SetInteger("animState", 1);

        if (Vector3.Distance(destination, gameObject.transform.position) < 2)
        {
            if (shouldAttack)
            {
                hasAttacked = false;
                currentState = FSMState.Attack;
            }
            else
            {
                ChooseNextDestination();
            }
        }
    }

    private void UpdateAttackState()
    {
        if (!hasAttacked)
        {
            shootDelay = 0;
            animator.SetInteger("animState", 2);
            Invoke("ShootProjectile", shootTime);
            hasAttacked = true;
            gameObject.transform.LookAt(player.transform);
        }
        else
        {
            shootDelay += Time.deltaTime;
            if (shootDelay > shootTime * 2)
            {
                ChooseNextDestination();
                currentState = FSMState.Walk;
            }
        }
    }

    private void UpdateDeadState()
    {

    }

    private void ChooseNextDestination()
    {
        while (true)
        {
            int nextDestinationIndex = Random.Range(0, wanderPoints.Length);
            Vector3 nextDestination = wanderPoints[nextDestinationIndex].transform.position;
            if (destination != nextDestination)
            {
                destination = nextDestination;
                navMeshAgent.SetDestination(destination);
                break;
            }
        }
    }

    private void ShootProjectile()
    {
        GameObject currentPrefab;
        float randomValue = Random.value;

        if (randomValue <= percentAttack)
        {
            currentPrefab = attackProjectilePrefab;
        }
        else
        {
            currentPrefab = specialProjectilePrefab;
        }
        gameObject.transform.LookAt(player.transform);

        Vector3 direction = (player.transform.position - transform.position).normalized + new Vector3(0, 0.5f, 0);
        GameObject projectile = Instantiate(currentPrefab,
                transform.position + direction, transform.rotation);
        Rigidbody rigidBody = projectile.GetComponent<Rigidbody>();
        rigidBody.AddForce(direction * 50, ForceMode.VelocityChange);
        projectile.transform.SetParent(projectileParent);
    }
}
