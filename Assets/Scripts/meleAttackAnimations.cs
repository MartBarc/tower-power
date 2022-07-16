using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleAttackAnimations : MonoBehaviour
{
    public Animator AttackAnimationController;

    public void playSwingAnim()
    {
        AttackAnimationController.SetTrigger("meleSwingTrig");
    }

    public void playSpearStabAnim()
    {
        AttackAnimationController.SetTrigger("spearStabTrig");
    }
}
