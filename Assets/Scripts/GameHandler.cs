using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameHandler : MonoBehaviour
{
    [SerializeField] public GridMapHandler mapHandler;
    [SerializeField] public Player player;
    [SerializeField] public GameObject transition;


    public TextMeshProUGUI levelText;

    public int levelCounter = 0;

    private Transform targetTransform;

    private int tOut = 100; //For transition
    private bool isTIn = false;
    private bool isTOut = false;
    private int tIn = 100;

    private Color trans;

    // Start is called before the first frame update
    void Start()
    {
        mapHandler.InitMap(levelCounter, 0.0f); //Map
        //mapHandler.InitTiles();
        trans = transition.GetComponent<SpriteRenderer>().color;
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}

    private void FixedUpdate()
    {
        if (player != null)
            return;
        //if (Input.GetKey(KeyCode.F))
        //{
        //    targetTransform = this.transform;
        //}
        //else
        //{
        targetTransform = player.transform;
        //}

        if (mapHandler.map.TriggerUpdate(targetTransform) == 0)
        {
            if (mapHandler.map.isTriggerReset())
            {
                //Start Transition
                TransitionOut();
            }
        }

        if (isTOut)
        {
            StartCoroutine(FadeIn());
            StartCoroutine(PlayerFadeOut());
            isTOut = false;
        }

        if (isTIn)
        {
            StartCoroutine(FadeOut());
            StartCoroutine(PlayerFadeIn());
            isTIn = false;
        }

        if (!player.GetComponent<Player>().isAlive)
        {
            //Debug.Log("player dead ahahahahaha");
        }
    }

    public int TransitionOut()
    {
        if (tOut == 100)
            isTOut = true;

        if (tOut > 0)
        {
            //Debug.Log(tOut);
            foreach (GridTile tile in mapHandler.map.tiles)
            {
                Vector3 away = new Vector3(Random.Range(-100f, 100f), Random.Range(-100f, 100f));
                tile.FlyAway(away);
            }
            tOut--;

            trans.a = tOut / 100f;
            return 0;
        }

        levelCounter++;
        player.transform.position = mapHandler.map.playerSpawn;
        float enemySpawnRate = levelCounter / 200f;
        mapHandler.ReInitMap(levelCounter, enemySpawnRate);
        tOut = 100;

        isTIn = true;
        //StartCoroutine(FadeIn());
        //StartCoroutine(FadeOut());

        levelText.text = "Level: " + levelCounter;

        return 0;
    }

    //public int TransitionIn() // DOONE
    //{
    //    if (tIn > 0)
    //    {
    //        Debug.Log(tOut);
    //        trans.a = tIn / 100f;
    //    }
    //    else
    //    {
    //        isTIn = false;
    //    }
    //    return 0;
    //}


    private IEnumerator FadeOut() // go to zero
    {
        yield return new WaitForSeconds(0.5f);

        float fadeSpeed = 45f;
        while (transition.GetComponent<SpriteRenderer>().color.a > 0)

        {
            Color ob = transition.GetComponent<SpriteRenderer>().color;
            float fadeAm = ob.a - (fadeSpeed * Time.deltaTime);

            ob = new Color(ob.r, ob.g, ob.b, fadeAm);
            transition.GetComponent<SpriteRenderer>().color = ob;

            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator FadeIn() // make it appear
    {
        float fadeSpeed = 25f;
        while (transition.GetComponent<SpriteRenderer>().color.a < 1)

        {
            Color ob = transition.GetComponent<SpriteRenderer>().color;
            float fadeAm = ob.a + (fadeSpeed * Time.deltaTime);

            ob = new Color(ob.r, ob.g, ob.b, fadeAm);
            transition.GetComponent<SpriteRenderer>().color = ob;
   
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator PlayerFadeOut() // make it disappear
    {
        float fadeSpeed = 40f;
        while (player.GetComponent<SpriteRenderer>().color.a > 0)

        {
            Color ob = player.GetComponent<SpriteRenderer>().color;
            float fadeAm = ob.a - (fadeSpeed * Time.deltaTime);

            ob = new Color(ob.r, ob.g, ob.b, fadeAm);
            player.GetComponent<SpriteRenderer>().color = ob;

            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator PlayerFadeIn()
    {
        float fadeSpeed = 40f;
        while (player.GetComponent<SpriteRenderer>().color.a < 1)

        {
            Color ob = player.GetComponent<SpriteRenderer>().color;
            float fadeAm = ob.a + (fadeSpeed * Time.deltaTime);

            ob = new Color(ob.r, ob.g, ob.b, fadeAm);
            player.GetComponent<SpriteRenderer>().color = ob;

            yield return new WaitForSeconds(0.1f);
        }
    }
}
