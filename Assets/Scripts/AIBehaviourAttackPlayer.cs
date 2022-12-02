using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehaviourAttackPlayer : AIBaseBehaviour
{
    public float DistanceCatchPlayer = 5;
    public override bool IsBehaviourValid()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return false;

        if (Vector3.Distance(this.transform.position, player.transform.position) < DistanceCatchPlayer)
        {
            return true;
        }
        return false;
    }
    public override void HandleBehaviourStart()
    {
        base.HandleBehaviourStart();
        //kill player;
        print("Player Die");
        GameManager.Instance.HandleGameFailed();
        this.GetComponent<AudioSource>().Play();
    }

    public override void HandleUpdate()
    {
        base.HandleUpdate();
        this.transform.position = Vector3.MoveTowards(this.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position, this.GetComponent<AIController>().MoveSpeed * 2 * Time.deltaTime);
    }
}
