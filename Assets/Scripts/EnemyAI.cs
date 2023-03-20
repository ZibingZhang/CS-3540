using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float frequency = 5;
    public float erraticness = 10;
    public float stillTime = 3;
    public float enemySpeed = 5;

    public enum FSMStates
    {
        Idle,
        Run,
        Dive,
        Punch,
        Kick,
        SpecialAttack,
        Damage,
        Dead
    }
    public FSMStates currentState;


    Animator anim;

    private float timePassedSinceAttack = 0;
    private float timePassedSinceDirectionChange = 0;
    private float timeTilDirectionChange;
    private Transform player;
    private Transform projectileParent;
    private Transform stageBounds;
    private Vector3 enemyBounds;
    private Vector3 destination;
    private Vector3 target;
    private bool standStill = true;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").gameObject.transform;
        projectileParent = GameObject.FindGameObjectWithTag("ProjectileParent").transform;

        stageBounds = GameObject.FindGameObjectWithTag("Stage").transform;
        enemyBounds = new Vector3(stageBounds.localScale.x / 2, 0, stageBounds.localScale.z);

        timeTilDirectionChange = getRandomInterval();

        anim = GetComponent<Animator>();
        currentState = FSMStates.Run;
        target = getRandomDestination();

    }

    // Update is called once per frame
    void Update()
    {
        print(timeTilDirectionChange - timePassedSinceDirectionChange);
        timePassedSinceAttack += Time.deltaTime;
        timePassedSinceDirectionChange += Time.deltaTime;

        switch (currentState)
        {
            case FSMStates.Run:
                UpdateRunState();
                break;

            case FSMStates.Dive:
                UpdateDiveState();
                break;

            case FSMStates.Punch:
                UpdatePunchState();
                break;

            case FSMStates.Kick:
                UpdateKickState();
                break;

            case FSMStates.Dead:
                UpdateDeadState();
                break;

        }

        if (timePassedSinceAttack > frequency)
        {
            UpdatePunchState();
        }
    }

    void UpdateRunState()
    {
        if (timePassedSinceDirectionChange < timeTilDirectionChange)
        {
            currentState = FSMStates.Run;
            if (Vector3.Distance(transform.position, target) < 2)
            {
                target = getRandomDestination();
                timeTilDirectionChange = getRandomInterval();
                timePassedSinceDirectionChange = 0;
            }
        }
        else
        {
            target = getRandomDestination();
            timeTilDirectionChange = getRandomInterval();
            timePassedSinceDirectionChange = 0;
        }
        FaceDirection(target);
        anim.SetInteger("animState", 1);
        transform.position = Vector3.MoveTowards(transform.position, target, enemySpeed * Time.deltaTime);


    }
    void UpdateDiveState()
    {

    }
    void UpdatePunchState()
    {
        FireAtPlayer();
        timePassedSinceAttack = 0;
        anim.SetInteger("animState", 100);

    }
    void UpdateKickState()
    {

    }
    void UpdateDeadState()
    {

    }

    void FaceDirection(Vector3 target)
    {
        Vector3 directionToTarget = (target - transform.position).normalized;
        directionToTarget.y =  0;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10 * Time.deltaTime);
    }

    private void FireAtPlayer()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized + new Vector3(0, 0.05f, 0);
        GameObject projectile = Instantiate(projectilePrefab,
                transform.position + direction, transform.rotation);
        Rigidbody rigidBody = projectile.GetComponent<Rigidbody>();
        rigidBody.AddForce(direction * 50, ForceMode.VelocityChange);
        projectile.transform.SetParent(projectileParent);
        Debug.Log(Time.time + " fired at player");
    }
    private Vector3 getRandomDestination()
    {
        Vector3 origin = stageBounds.position;
        origin.x = origin.x - stageBounds.localScale.x / 4;
        Vector3 range = enemyBounds / 2.0f;
        Vector3 randomPosition = new Vector3(Random.Range(-range.x, range.x),
                                          0,
                                          Random.Range(-range.z, range.z));
        randomPosition = origin + randomPosition;

        print(randomPosition);
        return randomPosition;
    }
    float getRandomInterval()
    {
        return Random.Range(erraticness, erraticness * 3);
    }
    private void OnDrawGizmos()
    {
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(target, 3);
        }
    }
}
