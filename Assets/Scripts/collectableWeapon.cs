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
    public bool makeNewWeaponCurrent = false;
    //public GameObject newWeaponDiceRoll;

    public Vector3 oLoc;
    public bool trg = false;


    [SerializeField] public GameObject landAnim;

    private void Start()
    {
        //newWeaponDiceRoll = GameObject.Find("Canvas/NewWeaponDiceRoller");

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

        oLoc = this.transform.position;
        transform.position = new Vector3(transform.position.x, 1000f);

    }

    //public void FlyIn()
    //{
    //    float speed = 20f;

    //    if (assemble == new Vector3(0f, 0f, 0f))
    //    {
    //        assemble = transform.position; //SAVE CURENT POSITION
    //        Vector3 away = new Vector3(Random.Range(-100f, 100f), Random.Range(-100f, 100f));
    //        transform.position = away;
    //    }
    //    else
    //    {
    //        transform.position = Vector2.MoveTowards(transform.position, oLoc, speed * Time.deltaTime);
    //    }
    //}
    public void triggerSummon()
    {
        if (trg)
        {
            return;
        }
        trg = true;
        transform.position = new Vector3(transform.position.x, 10f);
        StartCoroutine(Summon());
    }

    IEnumerator Summon()
    {
        float speed = 30f;
        float rot = 0f;
        while (transform.position != oLoc)
        {
            transform.eulerAngles = new Vector3(0, 0, rot);
            rot += 10f;
            transform.position = Vector2.MoveTowards(transform.position, oLoc, speed * Time.deltaTime);
            yield return new WaitForSeconds(0.01f);
        }
        GameObject effect = Instantiate(landAnim, transform.position, Quaternion.identity);
        if (effect != null)
        {
            Destroy(effect, 1f);
        }
        transform.eulerAngles = new Vector3(0, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //newWeaponDiceRoll.SetActive(true);
            //newWeaponDiceRoll.GetComponent<diceRoller>().triggerDiceRoll();
            //StartCoroutine(rollAnimationDelay());

            if (collision.gameObject.GetComponent<weaponController>().CurrentWeaponList.Count == 6)
            {
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
                collision.gameObject.GetComponent<weaponController>().ImageRemoveUI1.gameObject.SetActive(true);
                collision.gameObject.GetComponent<weaponController>().ImageRemoveUI2.gameObject.SetActive(true);
                collision.gameObject.GetComponent<weaponController>().ImageRemoveUI3.gameObject.SetActive(true);
                collision.gameObject.GetComponent<weaponController>().BtnRemoveUI1.gameObject.SetActive(true);
                collision.gameObject.GetComponent<weaponController>().BtnRemoveUI2.gameObject.SetActive(true);
                collision.gameObject.GetComponent<weaponController>().BtnRemoveUI3.gameObject.SetActive(true);
                collision.gameObject.GetComponent<weaponController>().ImageRemovearrow1.gameObject.SetActive(true);
                collision.gameObject.GetComponent<weaponController>().ImageRemovearrow2.gameObject.SetActive(true);

                collision.gameObject.GetComponent<shooting>().ShootingEnabled = false;
                Debug.Log("too many weapons, remove " + randomNumber1 + " or " + randomNumber2);
                collision.gameObject.GetComponent<weaponController>().slotToRemove1 = randomNumber1;
                collision.gameObject.GetComponent<weaponController>().slotToRemove2 = randomNumber2;

                
                
                collision.gameObject.GetComponent<weaponController>().ImageRemoveUI1.sprite =
                    collision.gameObject.GetComponent<weaponController>().CurrentWeaponList[randomNumber1].GetComponent<WeaponData>().UISprite;

                collision.gameObject.GetComponent<weaponController>().ImageRemoveUI2.sprite =
                    collision.gameObject.GetComponent<weaponController>().CurrentWeaponList[randomNumber2].GetComponent<WeaponData>().UISprite;

                collision.gameObject.GetComponent<weaponController>().ImageRemoveUI3.sprite =
                    weapon.GetComponent<WeaponData>().UISprite;

                collision.gameObject.GetComponent<weaponController>().newWeapon = weapon.GetComponent<WeaponData>();

            }
            else
            {
                if (collision.gameObject.GetComponent<weaponController>().CurrentWeaponList.Count == 0)
                {
                    makeNewWeaponCurrent = true;
                }
                collision.gameObject.GetComponent<weaponController>().CurrentWeaponList.Add(weapon);
                //Debug.Log("new count =  " + GameObject.Find("player").GetComponent<weaponController>().CurrentWeaponList.Count);
                collision.gameObject.GetComponent<weaponController>().updateUISprites();
                if (makeNewWeaponCurrent)
                {
                    collision.gameObject.GetComponent<weaponController>().currentWeapon = collision.gameObject.GetComponent<weaponController>().CurrentWeaponList[0];
                    collision.gameObject.GetComponent<weaponController>().updateAmmo();
                    makeNewWeaponCurrent = false;
                }
            }
            collision.gameObject.GetComponent<weaponController>().newWeaponTrackerImage.gameObject.GetComponent<Image>().sprite = weapon.GetComponent<WeaponData>().UISprite;
            collision.gameObject.GetComponent<weaponController>().newWeaponDiceAnimation();
            Destroy(gameObject);
        }
    }



}
