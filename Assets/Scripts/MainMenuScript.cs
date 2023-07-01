using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManger.LoadScene(SceneManger.GetActiveScene().buildIndex + 1);
    }
}
