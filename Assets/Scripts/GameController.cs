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
    public GameObject chickenPrefab;
    public int scytheKills = 0;
    //public Animator AttackAnimationController;

    private void Start()
    {
        //gate = GameObject.Find("gate");
        player = GameObject.Find("player");
        score = 0;
        enemiesKilled = 0;
        scoreText.text = "Score: " + score;
        scytheKills = 0;
        TotalWeaponList[6].GetComponent<WeaponData>().bulletPrefab = TotalWeaponList[6].GetComponent<scytheScript>().scythePrefab1;
        TotalWeaponList[6].GetComponent<WeaponData>().UISprite = TotalWeaponList[6].GetComponent<scytheScript>().UISprite1;
        TotalWeaponList[6].GetComponent<WeaponData>().inHandSprite = TotalWeaponList[6].GetComponent<scytheScript>().inHandSprite1;

    }

    public void addEnemyScore()
    {
        if (player != null)
            return;

        score += 10;
        enemiesKilled++;
        scoreText.text = "Score: " + score;
        if (player.gameObject.GetComponent<weaponController>().currentWeapon.name == "scytheGun")
        {
            scytheKills++;
        }
    }

}
