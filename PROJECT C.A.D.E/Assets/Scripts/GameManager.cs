using RevolutionStudios.GameManagement;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private InputManager inputManager;

    private GameObject playerCharacter;
    private PlayerController playerController;

    private float initialTimeScale;
    private bool gamePaused;

    public GameObject PlayerCharacter { get { return playerCharacter; } }
    public PlayerController PlayerController { get { return playerController; } }
    public InputManager InputManager { get { return inputManager; } }


    private void InitializeGameManager()
    {
        if (instance != null) { return; }

        instance = this;

<<<<<<< Updated upstream
        //Application.targetFrameRate = 60;
=======
    public GameObject player;
    public PlayerController playerScript;
    public GameObject playerSpawnPos;
    public GameObject checkpointPopup;
>>>>>>> Stashed changes

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        instance = this;
        initialTimeScale = Time.timeScale;

        playerCharacter = GameObject.FindWithTag("Player");
        playerController = playerCharacter.GetComponent<PlayerController>();
    }


    void Awake()
    {
        InitializeGameManager();
        inputManager.InitializeInputManager();
    }

    private void OnDisable()
    {
        inputManager.UninitializeInputManager();
    }
}
