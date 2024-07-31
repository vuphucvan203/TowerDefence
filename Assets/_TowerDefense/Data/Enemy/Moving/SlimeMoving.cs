using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlimeMoving : MovingFollowCheckpoint
{
    protected override void CheckDirectionMove()
    {
        base.CheckDirectionMove();
        if (distance.x > 0)
        {
            this.ctrl.Animator.SetBool("isUp", false);
            this.ctrl.Animator.SetBool("isDown", false);
            this.ctrl.Animator.SetBool("isSide", true);
            this.ctrl.Sprite.flipX = false;
        }
        if (distance.x < 0)
        {
            this.ctrl.Animator.SetBool("isUp", false);
            this.ctrl.Animator.SetBool("isDown", false);
            this.ctrl.Animator.SetBool("isSide", true);
            this.ctrl.Sprite.flipX = true;
        }
    }
}
