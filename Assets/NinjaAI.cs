using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaAI : MonoBehaviour
{
    public GameObject projectilePrefab;
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

    Vector3 target;


    // Start is called before the first frame update
    void Start()
    {
        currentState = FSMStates.Run;

        player = GameObject.FindGameObjectWithTag("Player").gameObject.transform;
        projectileParent = GameObject.FindGameObjectWithTag("ProjectileParent").transform;

        stageBounds = GameObject.FindGameObjectWithTag("Stage").transform;
        enemyBounds = new Vector3(stageBounds.localScale.x / 2 - 5, 0, stageBounds.localScale.z-5);


        anim = GetComponent<Animator>();
        getRandomDestination();

    }


    // Update is called once per frame
    void Update()
    {
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
        if (Vector3.Distance(transform.position, target) < 1)
        {
            currentState = FSMStates.Punch;
        }
        else
        {
           // CHANGE STATE?
        }
        FaceTarget(target);
        //anim.SetInteger("animState", 1);

        transform.position = Vector3.MoveTowards(transform.position, target, enemySpeed * Time.deltaTime);
    }
    void UpdateDiveState()
    {

    }
    void UpdatePunchState()
    {
        //anim.SetInteger("animState", 0);

        FaceTarget(player.position);

        if (shootDelay > 2)
        {
            //anim.SetInteger("animState", 100);
            //Invoke("FireAtPlayer", 10);
            FireAtPlayer();


            getRandomDestination();
            currentState = FSMStates.Run;

        }
        shootDelay += Time.deltaTime;
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

    }
    void FaceTarget(Vector3 target)
    {
        Vector3 directonTarget = (target - transform.position).normalized;
        directonTarget.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(directonTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10 * Time.deltaTime);
    }
    private void FireAtPlayer()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized + new Vector3(0, 0.5f, 0);
        GameObject projectile = Instantiate(projectilePrefab,
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
