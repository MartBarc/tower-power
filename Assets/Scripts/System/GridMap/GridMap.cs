using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum TILES
{
    NULL_TILE_ID = -1,
    WALL_TILE_ID = 10,
    WALL_TILE_PORTAL_ID = 60,
    FLOOR_ENEMY_ID = 30,
    FLOOR_WEAPONPICKUP_ID = 70,
    FLOOR_ID = 20,

    //FLOORS_CORNER
    F0 = 100,
    F3 = 103,
    F12 = 112,
    F15 = 115,

    //FLOORS_OUTER
    F1 = 101, //Top
    F2 = 102,
    F4 = 104, //Left
    F8 = 108,
    F7 = 107, // Right
    F11 = 111,
    F13 = 113,
    F14 = 114,

    //FLOORS_INNER
    F5 = 105,
    F6 = 106,
    F9 = 109,
    F10 = 110,

    //ENEMY

    E0 = 300,
    E1 = 301,
    E2 = 302
}

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

    // Grid Vectors and tiles

    public static List<int[]> innerVertexes;
    public List<GridTile> tiles;

    // -------- Othersettings
    public static List<Enemy> enemyList = new List<Enemy>();
    public static Portal portal;

    public static collectableWeapon collect;

    public static bool toBeReset = false;

    private bool gatespawned = false;
    private bool weaponspawned = false;

    public Vector3 playerSpawn;

    public int round;

    // --- FIRST LEVEL TUTORIAL

    [SerializeField] public GameObject tutObj;
    private GameObject tutObjObj;

    // CONST


    private void Awake()
    {
        Instance = this;
    }

    // ------ PUBLIC FUNCTIONS ------
    public int Init(int x, int y, float cellSize, GridTileCollection tileCollection, int newround, float enemies)
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

        GenerateInnerTileVertex();

        //if (DEBUG) grid.DrawDebugLines(Color.cyan);
        playerSpawn = new Vector3();
        round = newround;

        FillOuterTiles();

        if (newround == 0)
        {
            FillInnerTilesStart();
        }
        else
        {
            FillInnerTiles(enemies);
        }

        // --- AFTER SPAWNING TILES

        init = true;
        toBeReset = false;

        portal.SetActive(false);
        

        return 0;
    }

    public int AddTile(int id, int x, int y, out GridTile tile)
    {
        tile = null;
        if (id < -1) return -1; //null tile is -1

        GridNode gridNode = grid.GetGridObject(x, y);
        if (gridNode == null) return -1;

        Vector3 gridRealLocation = GetCellCenter(x, y);
        GameObject newGameObject = tileCollection.CreateTilePrefabFromID(id, gridRealLocation);
        if (newGameObject != null)
        {
            //gridNode.SetGameObject(newGameObject);
            gridNode.SetTile(newGameObject.GetComponent<GridTile>());

            tile = gridNode.GetTile();

           // if (DEBUG) Debug.Log("NOTE: GridMap[" + x + ", " + y + "] created at: " + gridNode.GetGameObject().transform.position.ToString());
        }

        if (newGameObject == null || tile == null)
        {
            Debug.Log("ERR: GridMap[" + x + ", " + y + "] failed to be created");
            return -1;
        }

        tiles.Add(tile);

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

    public int TriggerUpdate(Transform enemiesMoveTo) //Returns number of enemies left
    {
        //Check if Enemy Count is 0
        if (enemyList.Count == 0)
        {
            portal.SetActive(true);
            portal.isActive = true;
            if (portal.switchMap)
            {
                toBeReset = true;
            }

            if (collect != null)
                collect.triggerSummon();
        }
        else
        {
            //Check if EnemyList should remove null
            for (int i = 0; i < enemyList.Count; i++)
            {
                if (enemyList[i] == null)
                {
                    enemyList.RemoveAt(i);
                }
                else
                {
                    //debug
                    //enemyList[i].TriggerUpdate(enemiesMoveTo);
                }
            }
        }
        return enemyList.Count;
    }

    public bool isTriggerReset()
    {
        return toBeReset;
    }

    private List<int[]> GenerateInnerTileVertex()
    {
        innerVertexes = new List<int[]>();

        for (int x = 1; x < this.gridX - 1; x++)
        {
            for (int y = 1; y < this.gridY - 1; y++)
            {
                innerVertexes.Add(new int[2] { x, y });
            }
        }

        return innerVertexes;
    }

    //PRIVATE
    private int FillInnerTiles(float enemies)
    {
        while (innerVertexes.Count > 0)
        {
            int index = Random.Range(0, innerVertexes.Count - 1);
            int x = innerVertexes[index][0];
            int y = innerVertexes[index][1];

            if (round % 3 == 0)
            {
                if (!weaponspawned && x == gridX - (gridX/2))
                {
                    Debug.Log("Weapons spawned!");
                    AddTile((int)TILES.FLOOR_WEAPONPICKUP_ID, x, y, out GridTile tile);
                    tile.transform.parent = transform;
                    weaponspawned = true;
                    collect = tile.spawnedObj.GetComponent<collectableWeapon>();
                    innerVertexes.RemoveAt(index);
                    continue;
                }
            }

            if (enemyList.Count < round)//
            {
                AddTileInner(x, y, 1f);
            }
            else
            {
                AddTileInner(x, y, enemies);
            }
            

            innerVertexes.RemoveAt(index);
        }
        return 0;
    }

    private int FillInnerTilesStart()
    {
        List<int[]> innerVertexes = GenerateInnerTileVertex();

        int index = 4;
        int x = innerVertexes[index][0];
        int y = innerVertexes[index][1];

        ////TO REMOVE ENEMY
        AddTile((int)TILES.E2, x, y, out GridTile tileDummy);
        tileDummy.transform.parent = this.transform;
        Enemy dummy = tileDummy.spawnedObj.GetComponent<Enemy>();
        //dummy.isMeleEnemy = false;
        enemyList.Add(dummy);

        //tutObj init spawn here...
        tutObjObj = Instantiate(tutObj, this.transform.position, Quaternion.identity);
        tutObjObj.transform.parent = transform;

        while (innerVertexes.Count > 0)
        {
            index = Random.Range(0, innerVertexes.Count - 1);
            x = innerVertexes[index][0];
            y = innerVertexes[index][1];
            //if (x == this.gridX - 2 && y == this.gridY - 2) //If last tile
            if (x == gridX - (gridX/2) && !weaponspawned)
            {
                AddTile((int)TILES.FLOOR_WEAPONPICKUP_ID, x, y, out GridTile tile);
                tile.transform.parent = this.transform;
                collect = tile.spawnedObj.GetComponent<collectableWeapon>();
                weaponspawned = true;
            }
            else
            {
                AddTileInner(x, y, 0);
            }
            innerVertexes.RemoveAt(index);
        }
        return 0;
    }

    private int FillOuterTiles()
    {
        //Top + Bottom 
        for (int x = 0; x < this.gridX; x++)
        {
            AddTileOuter(x, 0, false);
            AddTileOuter(x, gridY - 1, false);
        }

        //Left Side + Right Side //Corners should be handled by top/bottom
        int portalIndex = Random.Range(1, this.gridY - 2);
        
        for (int y = 1; y < this.gridY - 1; y++) 
        {
            AddTileOuter(0, y, false); //Left
            AddTileOuter(gridX - 1, y, portalIndex == y); //Right
        }
        return 0;
    }

    private int AddTileInner(int x, int y, float enemies)
    {
        int TILEID = (int)TILES.NULL_TILE_ID;
        float isEnemy = Random.Range(0f, 1f);
        if (isEnemy < enemies && x > 2)
        {
            SpawnEnemy(x, y);
            return 0;
        }
        else
        {
            // ADD FLOOR TILE NUMBER MAKE IT RANDOM DUMMY
            TILEID = (int)TILES.F5;
        }

        AddTile(TILEID, x, y, out GridTile tile);
        tile.transform.parent = this.transform;
        
        return 0;
    }

    private int SpawnEnemy(int x, int y) //RANDOMIZER
    {
        int portalIndex = Random.Range(0, 3);

        GridTile tile;
        switch (portalIndex)
        {
            case 0:
                AddTile((int)TILES.E0, x, y, out tile);
                break;
            case 1:
                AddTile((int)TILES.E1, x, y, out tile);
                break;
            case 2:
                AddTile((int)TILES.E2, x, y, out tile);
                break;
            default:
                AddTile((int)TILES.E0, x, y, out tile);
                break;
        }

        tile.transform.parent = this.transform;
        Enemy newEnemy = tile.spawnedObj.GetComponent<Enemy>();
        enemyList.Add(newEnemy);
        return 0;
    }

    private int AddTileOuter(int x, int y, bool isPortal)
    {
        if (isPortal)
        {
            gatespawned = true;
            AddTile((int)TILES.WALL_TILE_PORTAL_ID, x, y, out GridTile wallGridTile);
            wallGridTile.transform.parent = this.transform;
            portal = wallGridTile.spawnedObj.GetComponent<Portal>();

            playerSpawn.y = wallGridTile.transform.position.y;
            playerSpawn.x = this.transform.position.x + cellOffset.x;
        }
        else //wall
        {
            GridTile gridTile;
            
            if (x == 0) //left
            {//Check corners
                if (y == 0)
                {
                    AddTile((int)TILES.F12, x, y, out gridTile);
                    gridTile.transform.parent = this.transform;
                }
                else if (y == gridY - 1)
                {
                    AddTile((int)TILES.F0, x, y, out gridTile);
                    gridTile.transform.parent = this.transform;
                }
                else
                {
                    AddTile((int)TILES.F4, x, y, out gridTile);
                    gridTile.transform.parent = this.transform;
                }
            }
            else if (x == gridX - 1) //right
            {
                if (y == 0) //botom right
                {
                    AddTile((int)TILES.F15, x, y, out gridTile);
                    gridTile.transform.parent = this.transform;
                }
                else if (y == gridY - 1) //top right
                {
                    AddTile((int)TILES.F3, x, y, out gridTile);
                    gridTile.transform.parent = this.transform;
                }
                else
                {
                    AddTile((int)TILES.F7, x, y, out gridTile);
                    gridTile.transform.parent = this.transform;
                }
            }
            //Bottom (do not do corners)
            else if (y == 0) //Bottom right
            {
                AddTile((int)TILES.F13, x, y, out gridTile);
                gridTile.transform.parent = this.transform;
            }
            //Top
            else if (y == gridY - 1) //Top Right
            {
                AddTile((int)TILES.F1, x, y, out gridTile);
                gridTile.transform.parent = this.transform;
            }
        }

        return 0;
    }
}
