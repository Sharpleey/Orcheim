using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdlePlayerState : PlayerState
{
    public IdlePlayerState(Player player) : base(player)
    {
    }

    public override void Enter()
    {
        //if (player.UsedWeapon != null)
        //    player.UsedWeapon.SetIdleState();
    }

    public override void Exit()
    {
       
    }

    public override void Update()
    {
        
    }
}
