using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehaviourPatrol : AIBaseBehaviour
{
    public Transform[] PatrolPaths;
    private int currentPathIndex;
    public override void HandleUpdate()
    {
        base.HandleUpdate();
        HandleStartMove(PatrolPaths[currentPathIndex].position);
        
        this.transform.position = Vector3.MoveTowards(this.transform.position, PatrolPaths[currentPathIndex].position, this.GetComponent<AIController>().MoveSpeed * Time.deltaTime);
        if(Vector3.Distance(this.transform.position, PatrolPaths[currentPathIndex].position)<=1)
        {
            currentPathIndex++;
            if (currentPathIndex >= PatrolPaths.Length) currentPathIndex = 0;
        }
    }
    private void Start()
    {
        currentPathIndex = 0;
    }

    public override bool IsBehaviourValid()
    {
        return true;
    }

    public override void HandleBehaviourStart()
    {
        base.HandleBehaviourStart();
    }

    public override void HandleBehaviourEnd()
    {
        base.HandleBehaviourEnd();
        HandleStopMove();
    }
    public override bool IsBehaviourAllowOverride()
    {
        return true;
    }
}
