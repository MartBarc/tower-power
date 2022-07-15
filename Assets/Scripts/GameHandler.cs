using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] public GridHandler gridHandler;
    [SerializeField] public GridSidebar gridSidebar;

    // Start is called before the first frame update
    void Start()
    {
        gridHandler.Init();
        gridSidebar.Init();
        gridHandler.gridObject.AddSnapPoints(gridSidebar.gridObject.snapPoints);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
