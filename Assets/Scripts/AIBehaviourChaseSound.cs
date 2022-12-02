using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehaviourChaseSound : AIBaseBehaviour
{
    public float MinDistanceGetAttracted;
    private Vector3 currentTargetPos;
    public override void HandleUpdate()
    {
        base.HandleUpdate();
        GetCloestAudioSource();
        HandleStartMove(currentTargetPos);
        this.transform.position = Vector3.MoveTowards(this.transform.position, currentTargetPos,this.GetComponent<AIController>().MoveSpeed* this.GetComponent<AIController>().MultiplierForChasing * Time.deltaTime);
        if(Vector3.Distance(this.transform.position,currentTargetPos)<0.5f)
        {
            HandleStopMove();
            if (GameManager.Instance.ActiveAudioList.Count == 0)
            {
                IsBehaviourEnd = true;
            }
        }
    }

    public override bool IsBehaviourValid()
    {
        if ((GameManager.Instance.ActiveAudioList.Count != 0  && GetCloestAudioSource()) || !IsBehaviourEnd)
        {
            return true;
        }
        return false;
    }

    public override void HandleBehaviourStart()
    {
        base.HandleBehaviourStart();
        GetCloestAudioSource();
    }
    public override bool IsBehaviourAllowOverride()
    {
        return true;
    }

    private bool GetCloestAudioSource()
    {
        if (GameManager.Instance.ActiveAudioList.Count == 0) return false;

        float shortestDis = float.MaxValue;
        for(int i = 0; i<GameManager.Instance.ActiveAudioList.Count;i++)
        {
            float dis = Vector3.Distance(this.transform.position, ((GameObject)GameManager.Instance.ActiveAudioList[i]).transform.position);
            if (dis<shortestDis && dis<= MinDistanceGetAttracted)
            {
                shortestDis = dis;
                currentTargetPos = ((GameObject)GameManager.Instance.ActiveAudioList[i]).transform.position;
            }
        }
        return shortestDis != float.MaxValue && shortestDis <= MinDistanceGetAttracted;
    }

}
