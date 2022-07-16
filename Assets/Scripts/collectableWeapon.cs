using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class collectableWeapon : MonoBehaviour
{
    public GameObject weapon;
    public bool rerollweapon = false;
    [SerializeField]
    public HashSet<int> playerWeaponIds = new HashSet<int>();

    private void Start()
    {


        
        //weapon = GameObject.Find("GameController").GetComponent<GameController>().TotalWeaponList[randomNumber];

        foreach (GameObject weaponIds in GameObject.Find("player").GetComponent<weaponController>().CurrentWeaponList)
        {
            playerWeaponIds.Add(weaponIds.GetComponent<WeaponData>().weaponId);
        }
        var range = Enumerable.Range(0, GameObject.Find("GameController").GetComponent<GameController>().TotalWeaponList.Count).Where(i => !playerWeaponIds.Contains(i));
        var rand = new System.Random();
        int index = rand.Next(0, GameObject.Find("GameController").GetComponent<GameController>().TotalWeaponList.Count - playerWeaponIds.Count);
        //return range.ElementAt(index);



        int randomNumber = range.ElementAt(index);
        //Debug.Log("i rolled a " + randomNumber + ". count = " + GameObject.Find("GameController").GetComponent<GameController>().TotalWeaponList.Count);

        weapon = GameObject.Find("GameController").GetComponent<GameController>().TotalWeaponList[randomNumber];


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<weaponController>().CurrentWeaponList.Add(weapon);
            Debug.Log("new count =  " + GameObject.Find("player").GetComponent<weaponController>().CurrentWeaponList.Count);
        }
    }
}
