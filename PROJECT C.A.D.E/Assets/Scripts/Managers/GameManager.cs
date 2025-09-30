using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    [SerializeField] GameObject menuActive;
    [SerializeField] GameObject menuPrev;
    [SerializeField] GameObject subMenuActive;
    [SerializeField] GameObject menuPause;
    [SerializeField] GameObject menuWin;
    [SerializeField] GameObject menuLose;
    [SerializeField] GameObject menuMain;
    [SerializeField] GameObject menuSettings;
    [SerializeField] GameObject menuInventory;
    [SerializeField] GameObject menuSkillTree;

    [SerializeField] GameObject subMenuGameplay;
    [SerializeField] GameObject subMenuControls;
    [SerializeField] GameObject subMenuAudio;
    [SerializeField] GameObject subMenuInventory;
    [SerializeField] GameObject subMenuSkillTree;


    // public GameObject playerRageScreen;


    public GameObject player;
    public AdvancedPlayerController playerScript;
    //public PlayerController playerScript;
    public GameObject playerSpawnPos;
    public GameObject checkpointPopup;



    public bool isPaused;

    float timeScaleOrig;

    void Awake()
    {
        instance = this;
        timeScaleOrig = Time.timeScale;

        player = GameObject.FindWithTag("Player");

        playerScript = player.GetComponent<AdvancedPlayerController>();
        //playerScript = player.GetComponent<PlayerController>();
        playerSpawnPos = GameObject.FindWithTag("Player Spawn Pos");

        if (isPaused)
        {
            stateUnpause();
        }


    }


    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (menuActive == null)
            {
                statePause();
                menuActive = menuPause;
                menuActive.SetActive(true);
            }

            else if (menuActive == menuPause)
            {
                stateUnpause();
            }
        }

    }

    public void statePause()
    {
        isPaused = !isPaused;
        Time.timeScale = 0;
        //transform.GetComponent<AudioSource>().Pause();

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void stateUnpause()
    {
        isPaused = !isPaused;
        Time.timeScale = timeScaleOrig;
        transform.GetComponent<AudioSource>().Play();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        if (subMenuActive != null)
            subMenuActive.SetActive(false);
        if (menuMain != null)
            menuMain.SetActive(false);

        menuActive.SetActive(false);
        menuActive = null;
    }


    public void youLose()
    {
        if (menuActive == null)
        {

            //deathStats.text = gameDeathCount.ToString("F0");
            statePause();
            //soundManager.instance.playDeathSound();
            menuActive = menuLose;
            menuActive.SetActive(true);
            //soundManager.instance.playLoseSound();

        }
    }

    public void youWin()
    {
        if (menuActive == null)
        {
            statePause();
            menuActive = menuWin;
            menuActive.SetActive(true);
        }
    }



    public void openSettings()
    {
        if (subMenuActive != null && subMenuActive != subMenuGameplay)
            subMenuActive.SetActive(false);

        if (menuActive != null && menuActive != menuSettings)
            menuActive.SetActive(false);

        if (menuActive == menuPause || menuActive == menuWin || menuActive == menuLose)
        {
            menuPrev = menuActive;
        }
        menuActive = menuSettings;
        subMenuActive = subMenuGameplay;
        menuMain.SetActive(true);
        subMenuGameplay.SetActive(true);
        menuActive.SetActive(true);

    }
    public void closeSettings()
    {
        menuActive = menuPrev;
        menuMain.SetActive(false);
        subMenuActive.SetActive(false);
        menuActive.SetActive(true);
    }
    public void openInventory()
    {
        if (menuActive != null && menuActive != menuInventory)
            menuActive.SetActive(false);
        if (subMenuActive != null && subMenuActive != subMenuInventory)
            subMenuActive.SetActive(false);
        subMenuActive.SetActive(false);
        menuActive.SetActive(false);
        menuActive = menuMain;
        subMenuActive = menuInventory;
        menuMain.SetActive(true);
        subMenuActive.SetActive(true);
        menuActive.SetActive(true);
    }

    public void openSkillTree()
    {
        if (menuActive != null && menuActive != menuSkillTree)
        {
            menuActive.SetActive(false);
        }
        if (subMenuActive != null && subMenuActive != subMenuSkillTree)
        {
            subMenuActive.SetActive(false);
        }

        menuActive = menuMain;
        subMenuActive = menuSkillTree;

        menuMain.SetActive(true);
        subMenuActive.SetActive(true);
        menuActive.SetActive(true);
    }

    public void openControllerSettings()
    {
        subMenuActive.SetActive(false);
        subMenuActive = subMenuControls;
        subMenuActive.SetActive(true);
    }

    public void openAudioSettings()
    {
        subMenuActive.SetActive(false);
        subMenuActive = subMenuAudio;
        subMenuActive.SetActive(true);
    }

}
