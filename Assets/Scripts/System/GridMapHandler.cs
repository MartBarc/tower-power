using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMapHandler : MonoBehaviour
{
    public static GridMapHandler Instance { private set; get; }
    private bool DEBUG = true;
    public bool IsDebug() { return this.DEBUG; }

    // ------ SETTINGS ------
    private bool init = false;
    public bool IsInit() { return init; }

    // ------ PUBLIC PROPERTIES ------
    [SerializeField] public GridMap gridMap;
    [SerializeField] public GridTileCollection gridTileCollection;

    private GridMapHandlerProperties PROP;
    public GridMapHandlerProperties GetGridProperties() { return PROP; }

    private GridMap map;
    private GridTileCollection tiles;
    public GridMap GetMap() { return this.map; }

    // ------ MonoBehavior Functions ------
    private void Awake()
    {
        Instance = this;
    }

    public int Init()
    {
        map = (GridMap)Instantiate(gridMap, this.transform.position, Quaternion.identity);
        tiles = (GridTileCollection)Instantiate(gridTileCollection, this.transform.position, Quaternion.identity);
        if (map != null && tiles != null)
        {
            PROP = new GridMapHandlerProperties();
            int status = map.Init(PROP.GRIDSIZE_X, PROP.GRIDSIZE_Y, PROP.GetCellSize(), tiles);
            if (status == 0)
            {
                map.transform.parent = this.transform;
                tiles.transform.parent = this.transform;
            }
            return status;
        }

        return -1;
    }
}
