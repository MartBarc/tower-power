using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class collectableWeapon : MonoBehaviour
{
    public GameObject weapon;
    public bool rerollweapon = false;
    [SerializeField]
    public HashSet<int> playerWeaponIds = new HashSet<int>();
    public AudioSource pickupSound;

    private void Start()
    {


        pickupSound = GameObject.Find("Sounds/enemyAttackNoise").GetComponent<AudioSource>();//change this to a different noise

        foreach (GameObject weaponIds in GameObject.Find("player").GetComponent<weaponController>().CurrentWeaponList)
        {
            playerWeaponIds.Add(weaponIds.GetComponent<WeaponData>().weaponId);
        }
        var range = Enumerable.Range(0, GameObject.Find("GameController").GetComponent<GameController>().TotalWeaponList.Count).Where(i => !playerWeaponIds.Contains(i));
        var rand = new System.Random();
        int index = rand.Next(0, GameObject.Find("GameController").GetComponent<GameController>().TotalWeaponList.Count - playerWeaponIds.Count);

        int randomNumber = range.ElementAt(index);
        //Debug.Log("i rolled a " + randomNumber + ". count = " + GameObject.Find("GameController").GetComponent<GameController>().TotalWeaponList.Count);

        weapon = GameObject.Find("GameController").GetComponent<GameController>().TotalWeaponList[randomNumber];
        playerWeaponIds.Clear();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<weaponController>().CurrentWeaponList.Count == 6)
            {
                //if player already has 6 weapons, ask if they want to swap a weapon out
                //add ui for this
                //Debug.Log("too many weapons, do you want to swap a weapon out?");

                ////replace random weapon with new one
                //int randomNumber = Random.Range(0, collision.gameObject.GetComponent<weaponController>().CurrentWeaponList.Count);
                //collision.gameObject.GetComponent<weaponController>().CurrentWeaponList.RemoveAt(randomNumber);
                //collision.gameObject.GetComponent<weaponController>().CurrentWeaponList.Add(weapon);


                HashSet<int> playerWeaponIds2 = new HashSet<int>();
                //replace random 2 weapons with new one
                int randomNumber1 = Random.Range(0, collision.gameObject.GetComponent<weaponController>().CurrentWeaponList.Count);
                playerWeaponIds2.Add(randomNumber1);
                var range = Enumerable.Range(0, collision.gameObject.GetComponent<weaponController>().CurrentWeaponList.Count).Where(i => !playerWeaponIds2.Contains(i));
                var rand = new System.Random();
                int index = rand.Next(0, collision.gameObject.GetComponent<weaponController>().CurrentWeaponList.Count - playerWeaponIds2.Count);

                int randomNumber2 = range.ElementAt(index);
                //weapon = GameObject.Find("GameController").GetComponent<GameController>().TotalWeaponList[randomNumber];
                collision.gameObject.GetComponent<weaponController>().weaponRemoveUI1.SetActive(true);
                collision.gameObject.GetComponent<weaponController>().weaponRemoveUI2.SetActive(true);
                collision.gameObject.GetComponent<weaponController>().weaponRemoveUI3.SetActive(true);


                Debug.Log("too many weapons, remove " + randomNumber1 + " or " + randomNumber2);
                collision.gameObject.GetComponent<weaponController>().slotToRemove1 = randomNumber1;
                collision.gameObject.GetComponent<weaponController>().slotToRemove2 = randomNumber2;

                collision.gameObject.GetComponent<weaponController>().weaponRemoveUI1.GetComponentInChildren<Image>().sprite = 
                    collision.gameObject.GetComponent<weaponController>().CurrentWeaponList[randomNumber1].GetComponent<WeaponData>().UISprite;
                collision.gameObject.GetComponent<weaponController>().weaponRemoveUI2.GetComponentInChildren<Image>().sprite =
                    collision.gameObject.GetComponent<weaponController>().CurrentWeaponList[randomNumber2].GetComponent<WeaponData>().UISprite;
                collision.gameObject.GetComponent<weaponController>().weaponRemoveUI3.GetComponentInChildren<Image>().sprite =
                    weapon.GetComponent<WeaponData>().UISprite;

                collision.gameObject.GetComponent<weaponController>().newWeapon = weapon.GetComponent<WeaponData>();


                //add buttons to click what weapon we replace

                //add rolling animation here

            }
            else
            {
                collision.gameObject.GetComponent<weaponController>().CurrentWeaponList.Add(weapon);
                Debug.Log("new count =  " + GameObject.Find("player").GetComponent<weaponController>().CurrentWeaponList.Count);
            }

            Destroy(gameObject);
        }
    }
}
