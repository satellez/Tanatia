using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy : MonoBehaviour
{
    NavMeshAgent agent;
    GameObject target;
    [SerializeField]
    float currentDistance, distanceToFollow, distanceToFight;
    // Start is called before the first frame update

    Animator anim;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        target = GameObject.Find("Character");
    }

    // Update is called once per frame
    void Update()
    {
        currentDistance = Vector3.Distance(this.transform.position, target.transform.position);
        if (currentDistance <= distanceToFight)
        {
            agent.SetDestination(this.transform.position);
            anim.SetInteger("state", 2);
        }
        else if (currentDistance <= distanceToFollow)
        {
            agent.SetDestination(target.transform.position);
            anim.SetInteger("state",1);
        }
        else
        {
            agent.SetDestination(this.transform.position);
            anim.SetInteger("state",0);
        }
    }
}
