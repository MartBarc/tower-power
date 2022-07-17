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

    public void playSwingBigAnim()
    {
        AttackAnimationController.SetTrigger("meleSwingBigTrig");
    }

    public void playSpearStabAnim()
    {
        AttackAnimationController.SetTrigger("spearStabTrig");
    }

    public void playDaggerStabAnim()
    {
        AttackAnimationController.SetTrigger("daggerStabTrig");
    }

    public void playBigHammerAnim()
    {
        AttackAnimationController.SetTrigger("bigHammerAttackTrig");
    }
}
