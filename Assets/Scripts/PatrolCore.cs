using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class PatrolCore : MonoBehaviour
{
    public Transform target;
    public float speed;
    private float fixed_speed;
    public Transform[] targets;
    private bool is_chasing;
    public Transform player;
    public GameObject key;
    public bool can_move;
    private float player_distance;
    private float distance;
    private NavMeshAgent _navMeshAgent;

    private void Start()
    {
        fixed_speed = speed;
        can_move = true;
        _navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        player_distance = Vector3.Distance(transform.position, player.position);
        if (can_move)
        {
            float step = speed * Time.deltaTime;
            //transform.position = Vector3.MoveTowards(transform.position, target.position, step);
            //var targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
            //_navMeshAgent.SetDestination(target.position);
            //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
            distance = Vector3.Distance(transform.position, target.position);
            
            if (distance < 5 && !is_chasing)
            {
                changeTarget();
            }

            if (Vector3.Distance(target.transform.position, transform.position) < _navMeshAgent.stoppingDistance)
            {
                _navMeshAgent.SetDestination(transform.position);
            }
            else
            {
                _navMeshAgent.SetDestination(target.transform.position);
            }

            if (player_distance < 3f)
            {
                key.GetComponent<Dancefloor>().startDancing();
                key.GetComponent<Dancefloor>().is_dancing = true;
                target = null;
                _navMeshAgent.speed = 0;
                transform.LookAt(player.transform);
                can_move = false;
            }

            Debug.Log((player_distance - player.GetComponent<CharacterCore>().sound_making));
            if ((player_distance - player.GetComponent<CharacterCore>().sound_making) < 4 && !is_chasing)
            {
                is_chasing = true;
                _navMeshAgent.speed = fixed_speed * 4;
                target = player;
            }
        } else
        {
            
        }
    }

    void changeTarget()
    {
        target = targets[Random.Range(0, targets.Length)];
    }

    public void relocate()
    {
        changeTarget();
        is_chasing = false;
        _navMeshAgent.speed = fixed_speed;
        can_move = true;
    }

}
