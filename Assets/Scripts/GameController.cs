using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> enemyList = new List<GameObject>();
    //public GameObject gate;
    public GameObject player;
    [SerializeField]
    public List<GameObject> TotalWeaponList = new List<GameObject>();
    public int enemiesKilled;
    public int score;
    public TextMeshProUGUI scoreText;
    //public Animator AttackAnimationController;

    private void Start()
    {
        //gate = GameObject.Find("gate");
        player = GameObject.Find("player");
        score = 0;
        enemiesKilled = 0;
        scoreText.text = "Score: " + score;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //Debug.Log("enemy entered");
            enemyList.Add(collision.gameObject);
        }
        if (collision.gameObject.tag == "Player")
        {

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //Debug.Log("enemy exit");
            enemyList.Remove(collision.gameObject);
            score += 10;
            enemiesKilled++;
            scoreText.text = "Score: " + score;
        }
        if (collision.gameObject.tag == "Player")
        {

        }
    }

    private void FixedUpdate()
    {
        if (enemyList.Count == 0)
        {
            //Debug.Log("all enemies dead, open gate/portal");
            //gate.SetActive(false);
        }
        else
        {
            //gate.SetActive(true);
        }
        if (!player.GetComponent<Player>().isAlive)
        {
            //Debug.Log("player dead ahahahahaha");
        }
    }

    //public void playSwingAnim()
    //{
    //    AttackAnimationController.SetTrigger("meleSwingTrig");
    //}

}
