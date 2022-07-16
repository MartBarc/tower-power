using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] public GridMapHandler mapHandler;
    [SerializeField] public Player player;

    public int levelCounter = 0;

    private Transform targetTransform;

    // Start is called before the first frame update
    void Start()
    {
        mapHandler.InitMap(levelCounter, 0.0f); //Map
        //mapHandler.InitTiles();
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}

    private void FixedUpdate()
    {
        
        if (Input.GetKey(KeyCode.F))
        {
            targetTransform = this.transform;
        }
        else
        {
            targetTransform = player.transform;
        }
        if (mapHandler.map.TriggerUpdate(targetTransform) == 0)
        {
            if (mapHandler.map.isTriggerReset())
            {
                levelCounter++;
                player.transform.position = mapHandler.map.playerSpawn;
                float enemySpawnRate = levelCounter / 100f;
                //Debug.Log(enemySpawnRate);
                mapHandler.ReInitMap(levelCounter, enemySpawnRate);
            }
        }
        if (!player.GetComponent<Player>().isAlive)
        {
            //Debug.Log("player dead ahahahahaha");
        }
    }
}
