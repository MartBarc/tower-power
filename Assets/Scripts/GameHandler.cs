using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] public GridMapHandler mapHandler;
    [SerializeField] public playerMovement player;

    public int levelCounter = 0;

    private Transform targetTransform;

    // Start is called before the first frame update
    void Start()
    {
        mapHandler.InitMap(false); //Map
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
            if (mapHandler.map.resetMyself())
            {
                player.transform.position = mapHandler.map.playerSpawn;
                mapHandler.ReInitMap();
                levelCounter++;
            }
        }
        if (!player.GetComponent<playerMovement>().isAlive)
        {
            Debug.Log("player dead ahahahahaha");
        }
    }
}
