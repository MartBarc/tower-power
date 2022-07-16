using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] public GridMapHandler mapHandler;
    [SerializeField] public playerMovement player;

    // Start is called before the first frame update
    void Start()
    {
        mapHandler.InitMap(); //Map
        //mapHandler.InitTiles();
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}

    private void FixedUpdate()
    {
        if (mapHandler.gridMap.numberOfEnemy() == 0)
        {
            if (mapHandler.gridMap.resetMyself())
            {
                 mapHandler.ReInitMap();
            }
        }
        if (!player.GetComponent<playerMovement>().isAlive)
        {
            Debug.Log("player dead ahahahahaha");
        }
    }
}
