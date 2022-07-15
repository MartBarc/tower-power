using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] public GridMapHandler mapHandler;

    // Start is called before the first frame update
    void Start()
    {
        mapHandler.Init(); //Map   
    }

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
