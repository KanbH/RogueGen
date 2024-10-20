using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;


    public static List<GameObject> CurrentLevelObjects = new List<GameObject>();

    public static GameManager GetInstance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    GameObject GameManagerObject = new GameObject("GameManager");
                    _instance = GameManagerObject.AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }

    private void Start()
    {
        FreezeGame();
        GeneratedLevel();
        UnfreezeGame();
    }
    private void GeneratedLevel()
    {

    }

    private void UnloadCurrentLevel()
    {
        foreach (GameObject obj in CurrentLevelObjects)
        {
            Destroy(obj);
        }
    }

    private void FreezeGame()
    {
        Time.timeScale = 0f;
    }

    private void UnfreezeGame()
    {
        Time.timeScale = 1f;
    }
}
