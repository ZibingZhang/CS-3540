using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NinjaAI : MonoBehaviour
{
    public GameObject attackProjectilePrefab;
    public GameObject specialProjectilePrefab;
    public float percentAttack = 0.5f;
    public float stillTime = 3;
    public float enemySpeed = 7;
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

    private float shootDelay = 0;
    private Transform player;
    private Transform projectileParent;
    private Transform stageBounds;
    private Vector3 enemyBounds;

    //private NavMeshAgent agent;
    Vector3 target;


    // Start is called before the first frame update
    void Start()
    {
        currentState = FSMStates.Run;
        anim = GetComponent<Animator>();
        anim.SetInteger("animState", 1);

        player = GameObject.FindGameObjectWithTag("Player").gameObject.transform;
        projectileParent = GameObject.FindGameObjectWithTag("ProjectileParent").transform;

        stageBounds = GameObject.FindGameObjectWithTag("Stage").transform;
        enemyBounds = new Vector3(stageBounds.localScale.x / 2 - 5, 0, stageBounds.localScale.z-5);


        anim = GetComponent<Animator>();
        //agent = GetComponent<NavMeshAgent>();

        getRandomDestination();
    }


    // Update is called once per frame
    void Update()
    {
        Vector3 eulerAngles = transform.rotation.eulerAngles;
        eulerAngles.x = 0;
        eulerAngles.z = 0;
        transform.rotation = Quaternion.Euler(eulerAngles);
        transform.position = new Vector3(transform.position.x, LevelManager.EnemyHeight(), transform.position.z);

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
    }
    void UpdateRunState()
    {
        // print(target);
        //print(target);
        //print(Vector3.Distance(transform.position, target));
        if (Vector3.Distance(transform.position, target) < 3)
        {
            hasShot = false;
            currentState = FSMStates.Punch;
            anim.SetInteger("animState", 0);
        }
        else
        {
           // CHANGE STATE?
        }
        FaceTarget(target);
        //agent.SetDestination(target);
        //agent.Move(target);
        MoveTowardsTarget(target);
        //transform.position = Vector3.MoveTowards(transform.position, target, enemySpeed * Time.deltaTime);
    }
    void UpdateDiveState()
    {

    }

    bool hasShot = false;

    void UpdatePunchState()
    {
        FaceTarget(player.position);

        if (shootDelay > 2 && !hasShot)
        {
            hasShot = true;
            anim.SetInteger("animState", 101);
            //Invoke("FireAtPlayer", 10);
            //FireAtPlayer();


            getRandomDestination();
            Invoke("GoToRunState", 2);
            Invoke("FireAtPlayer", 1);
            
        }
        shootDelay += Time.deltaTime;
    }


    void GoToRunState()
    {
        currentState = FSMStates.Run;
        anim.SetInteger("animState", 1);
    }

    void UpdateKickState()
    {

    }

    void UpdateDeadState()
    {

    }

    private void getRandomDestination()
    {
        Vector3 origin = stageBounds.position;
        origin.x = origin.x - stageBounds.localScale.x / 4;
        Vector3 range = enemyBounds / 2.0f;
        Vector3 randomPosition = new Vector3(Random.Range(-range.x, range.x),
                                          0,
                                          Random.Range(-range.z, range.z));
        target = origin + randomPosition;
        target.y = transform.position.y;
        //target.y = gameObject.transform.position.y;
        //print(target);
        //agent.SetDestination(target);
    }

    void FaceTarget(Vector3 target)
    {
        Vector3 directionToTarget = (target - transform.position).normalized;
        directionToTarget.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10 * Time.deltaTime);
    }

    void MoveTowardsTarget(Vector3 target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target, enemySpeed * Time.deltaTime);
    }

    private void FireAtPlayer()
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

        Vector3 direction = (player.transform.position - transform.position).normalized + new Vector3(0, 0.5f, 0);
        GameObject projectile = Instantiate(currentPrefab,
                transform.position + direction, transform.rotation);
        Rigidbody rigidBody = projectile.GetComponent<Rigidbody>();
        rigidBody.AddForce(direction * 50, ForceMode.VelocityChange);
        projectile.transform.SetParent(projectileParent);
        //Debug.Log(Time.time + " fired at player");
        shootDelay = 0;
        //anim.SetInteger("animState", 100);

    }
    private void OnDrawGizmos()
    {
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(target, 3);
        }
    }
}
