using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicHorizontalChase : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform playerTransform;
    public Vector3 originalPosition;
    public NavMeshAgent agent;
    void Start()
    {
        originalPosition = transform.position;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        ChaseHorizontally(playerTransform);
    }
        public void ChaseHorizontally(Transform toChase)
    {
        Vector3 toChaseH  = new Vector3(toChase.position.x, originalPosition.y, 0);
        //Debug.DrawLine(transform.position, toChaseH, Color.red);
        //animController.Play(AnimName.WalkAnim, 1);
        agent.SetDestination(toChaseH);
    }
}
