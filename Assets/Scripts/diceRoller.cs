using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diceRoller : MonoBehaviour
{
    public Animator diceAnimator;
    public GameObject text;
    public AudioSource diceSound;

    //private void Start()
    //{
    //    diceSound = GameObject.Find("Sounds/diceSound").GetComponent<AudioSource>();
    //}

    public void triggerDiceRoll()
    {
        diceAnimator.SetTrigger("RollDiceTrig");
        GameObject.Find("Sounds/diceNoise").GetComponent<AudioSource>().Play();
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
