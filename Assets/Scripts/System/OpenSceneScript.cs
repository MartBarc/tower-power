using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OpenSceneScript : MonoBehaviour
{
    [SerializeField] Animator player;
    [SerializeField] Animator witch;
    [SerializeField] Animator bubble;
    [SerializeField] public TextMeshProUGUI titleText;
    [SerializeField] 

    public void TriggerScene(int num)
    {
        //sceneNumber
        switch (num)
        {
            case 0:
                witch.SetBool("talking", true);
                player.SetBool("talking", false);
                bubble.SetBool("talkingAlan", false);
                titleText.text = "Alan, you managed to lose all of your money... again.";
                break;
            case 1:
                player.SetBool("talking", true);
                witch.SetBool("talking", false);
                bubble.SetBool("talkingAlan", true);
                titleText.text = "This can't be! Tails never fails!";
                break;
            case 2:
                witch.SetBool("talking", true);
                player.SetBool("talking", false);
                bubble.SetBool("talkingAlan", false);
                titleText.text = "Yes, yes, of course. 'Tails never fails', 'Bet it all on black', 'You're thinking of the number 69'...";
                break;
            case 3:
                player.SetBool("talking", true);
                witch.SetBool("talking", false);
                bubble.SetBool("talkingAlan", true);
                titleText.text = "You had to have been lying on the last one!";
                break;
            case 4:
                witch.SetBool("talking", true);
                player.SetBool("talking", false);
                bubble.SetBool("talkingAlan", false);
                titleText.text = "I said I was thinking of a number from 1 to 10 Alan.";
                break;
            case 5:
                player.SetBool("talking", true);
                witch.SetBool("talking", false);
                bubble.SetBool("talkingAlan", true);
                titleText.text = "Please! One more game! This time I'll win with my lucky die! I bet my life on it!";
                break;
            case 6:
                witch.SetBool("talking", true);
                player.SetBool("talking", false);
                bubble.SetBool("talkingAlan", false);
                titleText.text = "Bet... your life?";
                break;
            case 7:
                witch.SetBool("talking", true);
                player.SetBool("talking", false);
                bubble.SetBool("talkingAlan", false);
                titleText.text = ". . .";
                break;
            case 8:
                witch.SetBool("talking", true);
                player.SetBool("talking", false);
                bubble.SetBool("talkingAlan", false);
                titleText.text = "Deal.";
                break;
            //case 9:
                //StartCoroutine(FadeIn());
                //break;
        }
        return;
    }

    public void TITLE()
    {
        return;// witch.SetTrigger("EnemyDieTrig");
    }
}
