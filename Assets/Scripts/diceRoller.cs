using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class diceRoller : MonoBehaviour
{
    public Animator diceAnimator;
    public GameObject text;
    public AudioSource diceSound;
    public GameObject weaponSprite;

    //private void Start()
    //{
    //    diceSound = GameObject.Find("Sounds/diceSound").GetComponent<AudioSource>();
    //}

    public void triggerDiceRoll()
    {
        diceAnimator.SetTrigger("RollDiceTrig");
        GameObject.Find("Sounds/diceNoise").GetComponent<AudioSource>().Play();
        if (text != null || weaponSprite != null)
        {
            if (text != null)
            {
                text.SetActive(false);
            }
            if (weaponSprite != null)
            {
                weaponSprite.SetActive(false);
            }
            StartCoroutine(dicerollwait());
        }
    }

    IEnumerator dicerollwait()
    {
        yield return new WaitForSecondsRealtime(1.6f);
        if (text != null)
        {
            text.SetActive(true);
        }
        if (weaponSprite != null)
        {
            weaponSprite.SetActive(true);
        }
    }
}
