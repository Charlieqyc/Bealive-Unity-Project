using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public float MoveSpeed;
    public float MultiplierForChasing;
    public AIBaseBehaviour[] AIBehaviours;
    public GameObject[] Avatars;
    private int currentActiveBehavioursIndex;
    // Start is called before the first frame update
    void Start()
    {
        currentActiveBehavioursIndex = -1;
        AIBehaviours = new AIBaseBehaviour[3];
        AIBehaviours[0] = this.GetComponent<AIBehaviourAttackPlayer>();
        AIBehaviours[1] = this.GetComponent<AIBehaviourChaseSound>();
        AIBehaviours[2] = this.GetComponent<AIBehaviourPatrol>();
    }


    // Update is called once per frame
    void Update()
    {
        if (currentActiveBehavioursIndex!= -1 && AIBehaviours[currentActiveBehavioursIndex].IsBehaviourEnd)
        {
            AIBehaviours[currentActiveBehavioursIndex].HandleBehaviourEnd();
            currentActiveBehavioursIndex = -1;
        }
        //if there is no current active behaviour, try find one.
        if (currentActiveBehavioursIndex == -1)
        {
            for (int i = 0; i < AIBehaviours.Length; i++)
            {
                if (AIBehaviours[i].IsBehaviourValid())
                {
                    AIBehaviours[i].HandleBehaviourStart();
                    currentActiveBehavioursIndex = i;
                    break;
                }
            }
        }
        else if(AIBehaviours[currentActiveBehavioursIndex].IsBehaviourAllowOverride())
        {
            for (int i = 0; i < currentActiveBehavioursIndex; i++)
            {
                if (AIBehaviours[i].IsBehaviourValid())
                {
                    AIBehaviours[currentActiveBehavioursIndex].HandleBehaviourEnd();
                    AIBehaviours[i].HandleBehaviourStart();
                    currentActiveBehavioursIndex = i;
                }
            }
        }

        if(currentActiveBehavioursIndex!= -1)
        {
            AIBehaviours[currentActiveBehavioursIndex].HandleUpdate();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "AIR_WALL")
        {
            this.GetComponent<CapsuleCollider>().enabled = false;
        }
    }
}
