using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diceRoller : MonoBehaviour
{
    public Animator diceAnimator;

    public void triggerDiceRoll()
    {
        diceAnimator.SetTrigger("RollDiceTrig");
    }
}
