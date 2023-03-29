using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{

    public GameObject projectilePrefab;
    public float frequency = 5;
    public float erraticness = 10;
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


    private Transform player;
    private Transform projectileParent;
    private Transform stageBounds;
    private Vector3 enemyBounds;
    private bool attacking = false;

    Vector3 target;

    // Start is called before the first frame update
    void Start()
    {

        stageBounds = GameObject.FindGameObjectWithTag("Stage").transform;
        enemyBounds = new Vector3(stageBounds.localScale.x / 2, 0, stageBounds.localScale.z);

        anim = GetComponent<Animator>();
        currentState = FSMStates.Run;
        getRandomDestination();

    }

    // Update is called once per frame
    void Update()
    {
        print(target);
        if (Vector3.Distance(transform.position, target) < 2)
        {
            getRandomDestination();
        }
        if (!attacking)
        {
            FaceTarget(target);
            anim.SetInteger("animState", 1);
        }
        /*
        switch (currentState)
        {
            case FSMStates.Run:
                UpdateRunState();
                break;

        }*/
    }
    void UpdateRunState()
    {
        //print(Vector3.Distance(target, transform.position));
        if (Vector3.Distance(transform.position, target) < 2)
        {
            getRandomDestination();
        }
        else
        {
            // CHANGE STATE?
        }
        FaceTarget(target);
        anim.SetInteger("animState", 1);

        transform.position = Vector3.MoveTowards(transform.position, target, enemySpeed * Time.deltaTime);
    }
    void FaceTarget(Vector3 target)
    {
        Vector3 directonTarget = (target - transform.position).normalized;
        directonTarget.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(directonTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10 * Time.deltaTime);
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
    private void OnDrawGizmos()
    {
        {
            Vector3 origin = stageBounds.position;
            origin.x = origin.x - stageBounds.localScale.x / 4;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(target, 3);
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(origin, enemyBounds);
        }
    }
}
