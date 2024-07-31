using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeMoving : MovingFollowCheckpoint
{
    protected override void CheckDirectionMove()
    {
        base.CheckDirectionMove();
        if (distance.x > 0)
        {
            this.ctrl.Animator.SetBool("isUp", false);
            this.ctrl.Animator.SetBool("isDown", false);
            this.ctrl.Animator.SetBool("isSide", true);
            this.ctrl.Sprite.flipX = true;
        }
        if (distance.x < 0)
        {
            this.ctrl.Animator.SetBool("isUp", false);
            this.ctrl.Animator.SetBool("isDown", false);
            this.ctrl.Animator.SetBool("isSide", true);
            this.ctrl.Sprite.flipX = false;

        }
    }
}
