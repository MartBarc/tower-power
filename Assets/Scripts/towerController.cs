using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class towerController : MonoBehaviour
{

    [SerializeField]
    public static List<GameObject> enemyList = new List<GameObject>();

    public float attackSpeed = 1f;
    public float attackDamage = 1f;
    public bool canAttack = true;
    public AudioSource laserSound;
    public GameObject TowerUICanvas;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //Debug.Log("enemy entered");
            enemyList.Add(collision.gameObject);
        }
        if (collision.gameObject.tag == "Player")
        {
            //open tower menus
            //Debug.Log("player enter, open canvas");
            TowerUICanvas.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //Debug.Log("enemy exit");
            enemyList.Remove(collision.gameObject);
        }
        if (collision.gameObject.tag == "Player")
        {
            //close tower menus
            //Debug.Log("player leave, close canvas");
            TowerUICanvas.SetActive(false);
        }
    }

    private void Update()
    {
        //int listnumber = 0;
        //foreach (GameObject item in enemyList)
        //{
        //    Debug.Log("enemy " + listnumber + ": " + item.name);
        //    listnumber++;
        //}
    }

    private void FixedUpdate()
    {
        if (canAttack && enemyList.Count > 0)
        {
            laserSound.Play();
            canAttack = false;
            enemyList[0].gameObject.GetComponent<enemyMove>().TakeHit(attackDamage);
            StartCoroutine(attackCooldown());
            return;
        }
    }

    IEnumerator attackCooldown()
    {
        yield return new WaitForSecondsRealtime(1);
        canAttack = true;
    }

    public void PressButton1()
    {
        Debug.Log("press button 1");
    }
    public void PressButton2()
    {
        Debug.Log("press button 2");
    }
    public void PressButton3()
    {
        Debug.Log("press button 3");
    }
    public void PressButton4()
    {
        Debug.Log("press button 4");
    }
}
