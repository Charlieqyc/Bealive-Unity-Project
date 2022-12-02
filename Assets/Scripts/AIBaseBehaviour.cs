using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBaseBehaviour: MonoBehaviour
{
    public bool IsBehaviourActive
    {
        get { return isActive; }
        set { isActive = value; } 
    }
    private bool isActive;

    public bool IsBehaviourEnd
    {
        get { return isEnd; }
        set { isEnd = value; }
    }
    private bool isEnd;

    public virtual void HandleStartMove(Vector3 targetPos)
    {
        Animator[] animators = this.GetComponentsInChildren<Animator>();
        for(int i = 0;i<animators.Length;i++)
        {
            animators[i].SetBool("Move",true);
        }
        GameObject[] avatars = this.GetComponent<AIController>().Avatars;
        for (int i = 0; i < avatars.Length; i++)
        {
            Vector3 currentpos = avatars[i].transform.position;
            currentpos.x = targetPos.x;
            currentpos.z = targetPos.z;
            avatars[i].transform.LookAt(currentpos);
        }
    }

    public virtual void HandleStopMove()
    {
        Animator[] animators = this.GetComponentsInChildren<Animator>();
        for (int i = 0; i < animators.Length; i++)
        {
            animators[i].SetBool("Move", false);
        }
    }

    public virtual bool IsBehaviourValid()
    {
        return false;
    }

    public virtual void HandleBehaviourStart() 
    {
        IsBehaviourActive = true;
        IsBehaviourEnd = false;
    }
    public virtual void HandleBehaviourEnd() 
    {
        IsBehaviourActive = false;
        IsBehaviourEnd = true;
    }
    public virtual void HandleUpdate()
    {

    }
    private void Start()
    {
        IsBehaviourEnd = true;
    }
    public virtual bool IsBehaviourAllowOverride()
    {
        return false;
    }
}
