using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    #region Core GameObjects
    [SerializeField] private GameObject _mapBaseObject;
    [SerializeField] private GameObject _nodeManagerObject;
    [SerializeField] private GameObject _mapGeneratorObject;
    #endregion

    #region Components
    private CreateMapBase _createMapBase;
    private NodeManager _nodeManager;
    private PrototypeRoomGenerator _prototypeRoomGenerator;
    #endregion


    #region Awake
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            GetMapComponents();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void GetMapComponents()
    {
        _createMapBase = _mapBaseObject.GetComponent<CreateMapBase>();
        _nodeManager = _nodeManagerObject.GetComponent<NodeManager>();
        _prototypeRoomGenerator = _mapGeneratorObject.GetComponent<PrototypeRoomGenerator>();
    }
    #endregion

    #region Game Initialization
    void Start()
    {
        _prototypeRoomGenerator.StartMapGeneration();
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
