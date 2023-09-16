using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Jump : StateMachineBehaviour
{
    [SerializeField] BossPeacock bossPeacock;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossPeacock = GameObject.FindGameObjectWithTag("Bosspeacock").GetComponent<BossPeacock>();
        bossPeacock.JumpAttack();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossPeacock.FlipTowardsPlayer();
    }
    



}
