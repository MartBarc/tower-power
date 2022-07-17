using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diceRoller : MonoBehaviour
{
    public Animator diceAnimator;
    public GameObject text;


    public void triggerDiceRoll()
    {
        diceAnimator.SetTrigger("RollDiceTrig");
        if (text != null)
        {
            text.SetActive(false);
            StartCoroutine(dicerollwait());
        }
        
        
    }

    IEnumerator dicerollwait()
    {
        yield return new WaitForSecondsRealtime(1.6f);
        text.SetActive(true);
    }
}
