using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMap : MonoBehaviour
{
    public static GridMap Instance { private set; get; }
    private bool DEBUG = false;

    private Grid<GridNode> grid;
    public Grid<GridNode> GetGrid() { return this.grid; }

    private GridTileCollection tileCollection;

    private bool init = false;

    // ------ GRID SETTINGS ------
    //Ex. 475 = (480, 480):(middle of a 96x96)
    //          so (-5, -5):(middle tile size 10fx10f)
    private int gridX;
    private int gridY;
    private Vector3 gridOffset;

    private float cellSize;
    private Vector3 cellOffset;

    // -------- Othersettings
    public static List<Enemy> enemyList = new List<Enemy>();
    public static Portal portal;

    public static bool toBeReset = false;

    // CONST
    const int NULL_TILE_ID = -1;
    const int WALL_TILE_ID = 10;
    const int FLOOR_ENEMY_ID = 30;
    const int FLOOR_GATE_ID = 60; 
    const int FLOOR = 20;

    private void Awake()
    {
        Instance = this;
    }

    // ------ PUBLIC FUNCTIONS ------
    public int Init(int x, int y, float cellSize, GridTileCollection tileCollection)
    {
        this.gridX = x;
        this.gridY = y;
        this.cellSize = cellSize;

        this.gridOffset = new Vector3(x * cellSize / 2.0f, y * cellSize / 2.0f);
        this.cellOffset = new Vector3(cellSize / 2.0f, cellSize / 2.0f);

        this.transform.position -= gridOffset; //move to offset location
        this.grid = new Grid<GridNode>(x, y, cellSize, this.transform.position, (Grid<GridNode> grid, int x, int y) => new GridNode(grid, x, y));

        this.tileCollection = tileCollection;
        if (this.tileCollection == null) { return -1; }

        //if (DEBUG) grid.DrawDebugLines(Color.cyan);

        FillNullTiles();

        init = true;
        toBeReset = false;

        return 0;
    }

    public int AddTile(int id, int x, int y, out GridTile tile)
    {
        tile = null;
        if (id < -1) return -1; //null tile is -1

        GridNode gridNode = grid.GetGridObject(x, y);
        if (gridNode == null) return -1;

        if (gridNode.GetGameObject() != null)
        {
            if (DEBUG) Debug.Log("NOTE: Tile[" + x + ", " + y + "] exists! Deleting!");
            Destroy(gridNode.GetGameObject());
        }

        Vector3 gridRealLocation = GetCellCenter(x, y);
        GameObject newGameObject = tileCollection.CreateTilePrefabFromID(id, gridRealLocation);
        if (newGameObject != null)
        {
            gridNode.SetGameObject(newGameObject);
            gridNode.SetTile(gridNode.GetGameObject().GetComponent<GridTile>());

            tile = gridNode.GetTile();

            if (DEBUG) Debug.Log("NOTE: GridMap[" + x + ", " + y + "] created at: " + gridNode.GetGameObject().transform.position.ToString());
        }

        if (newGameObject == null || tile == null)
        {
            if (DEBUG) Debug.Log("ERR: GridMap[" + x + ", " + y + "] failed to be created");
            return -1;
        }

        return 0;
    }

    public void GetMapSettings(out int x, out int y, out float size)
    {
        x = this.gridX;
        y = this.gridY;
        size = this.cellSize;
    }

    public Vector3 GetCellCenter(int x, int y)
    {
        float x_axis = (x * grid.GetCellSize() - cellOffset.x) + this.transform.position.x;
        float y_axis = (y * grid.GetCellSize() - cellOffset.y) + this.transform.position.y;
        return new Vector3(x_axis + 1.0f , y_axis + 1.0f); //may have to remove 1.0f
    }

    public int numberOfEnemy()
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            if (enemyList[0] == null)
            {
                enemyList.RemoveAt(0);
            }
        }
        if (enemyList.Count == 0)
        {
            portal.isActive = true;
            if (portal.switchMap)
            {
                toBeReset = true;
            }
        }
        return enemyList.Count;
    }

    public bool resetMyself()
    {
        return toBeReset;
    }

    public void destroySelf()
    {
        Destroy(this);
        DestroyImmediate(this);
    }

    //PRIVATE
    private int FillNullTiles()
    {
        bool gatespawned = false;
        for (int x = 0; x < this.gridX; x++)
        {

            for (int y = 0; y < this.gridY; y++)
            {
                GridNode gridNodeObj = grid.GetGridObject(x, y);
                if (gridNodeObj == null) return -1;

                if (gridNodeObj.GetGameObject() == null)
                {
                    if (x == 0 || x == gridX - 1 || y == 0 || y == gridY - 1)
                    {
                        if (DEBUG) Debug.Log("NOTE: GridTile[" + x + ", " + y + "] is NULL! Adding Wall!");
                        AddTile(WALL_TILE_ID, x, y, out GridTile wallGridTile);
                        wallGridTile.transform.parent = this.transform;
                    }
                    else
                    {
                        int floorType = Random.Range(0, 30);
                        int TILEID = NULL_TILE_ID;
                        switch(floorType)
                        {
                            case 0:
                                TILEID = FLOOR_ENEMY_ID;
                                break;
                            case 9:
                                if (!gatespawned)
                                {
                                    TILEID = FLOOR_GATE_ID;
                                    gatespawned = true;
                                }
                                else
                                {
                                    TILEID = FLOOR;
                                }
                                break;
                            default:
                                TILEID = FLOOR;
                                break;
                        }

                        AddTile(TILEID, x, y, out GridTile tile);
                        tile.transform.parent = this.transform;

                        switch(TILEID)
                        {
                            case FLOOR_ENEMY_ID:
                                enemyList.Add(tile.spawnedObj.GetComponent<Enemy>());
                                break;
                            case FLOOR_GATE_ID:
                                portal = tile.spawnedObj.GetComponent<Portal>();
                                break;
                        }

                        if (DEBUG) Debug.Log("NOTE: GridTile[" + x + ", " + y + "] is NULL! Filling TileID: " + TILEID);
                    }
                }
            }
        }
        return 0;
    }


    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Enemy")
    //    {
    //        Debug.Log("enemy entered");
    //        enemyList.Add(collision.gameObject.GetComponent<Enemy>());
    //    }
    //    if (collision.gameObject.tag == "Player")
    //    {

    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Enemy")
    //    {
    //        Debug.Log("enemy exit");
    //        enemyList.Remove(collision.gameObject.GetComponent<Enemy>());
    //    }
    //    if (collision.gameObject.tag == "Player")
    //    {

    //    }
    //}
}
