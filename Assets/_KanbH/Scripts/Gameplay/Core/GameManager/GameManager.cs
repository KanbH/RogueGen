using UnityEngine;
using NUnit.Framework;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    #region Components
    [SerializeField] private CreateMapBase _createMapBase;
    [SerializeField] private NodeManager _nodeManager;
    [SerializeField] private MapGen _mapGen;
    #endregion

    #region Awake
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            CheckComponentsNotNull();
        }
        else if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void CheckComponentsNotNull()
    {
        Assert.IsNotNull(_createMapBase);
        Assert.IsNotNull(_nodeManager);
        Assert.IsNotNull(_mapGen);
    }
    #endregion

    #region Game Initialization
    void Start()
    {
        _createMapBase.InitMapBase();
        _mapGen.StartMapGeneration();
        _nodeManager.DestroyAllNodeGameObjects();
    }
    #endregion

    public void PauseGame()
    {
        Time.timeScale = 0f;  // Stops all movement in the game
        Debug.Log("Game Paused");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;  // Resumes normal time
        Debug.Log("Game Resumed");
    }

    public void EndGame()
    {
        Debug.Log("Game Over");
        // Logic for ending the game, such as displaying game over screen
    }
}
