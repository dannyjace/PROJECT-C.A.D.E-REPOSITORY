
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public static HUDManager instance;

    [SerializeField] private GameObject hudCanvas;
    [SerializeField] private GameObject reticle;

    [SerializeField] private GameObject playerUIBars;


    public Image playerHPBar;
    // public Image playerRageBar;
    public Image playerStaminaBar;
    public Image playerEXPBar;
    public GameObject playerAmmoCanvas;
    public Image playerAmmoBar;
    public Image playerBulletImage;
    public GameObject bossHPBar;
    public Image bossHPBarFill;
    public TMP_Text bossNameText;




    public int playerLevel = 0;
    [SerializeField] TMP_Text levelHUDText;
    [SerializeField] GameObject levelHUDFeedback;
    [SerializeField] TMP_Text levelHUDFeedbackText;


    [SerializeField] TMP_Text playerAmmo;
    [SerializeField] TMP_Text gameGoalCountText;
    [SerializeField] TMP_Text keyCountText;
    [SerializeField] TMP_Text playerLevelEXP;
    [SerializeField] TMP_Text playerHealthValue;
    [SerializeField] TMP_Text playerStaminaValue;

    public TMP_Text interactPromptText;

    int gameGoalCount;
    int gameKillCount;
    int gameDeathCount;
    int gameScore;
    int keyCount;

    public GameObject playerDamageScreen;
    public GameObject playerHealthScreen;

    public TMP_Text gameGoalMenuStat;
    public TMP_Text scoreMenuStat; // Score stat in the main/settings menu
    public TMP_Text killStats;
    public TMP_Text deathStats;

    public GameObject Reticle => reticle;


    // List of XRE Modules collected



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this;
        bossHPBar.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        // Check if collected modules is empty and hide minimap if so

    }
    public void updateGameGoal(int amount)
    {
        gameGoalCount += amount;
        gameGoalCountText.text = gameGoalCount.ToString("F0");
        //gameGoalMenuStat.text = gameGoalCount.ToString("F0"); //this line was making the win menu not show up

        // Debug.Log("Game Goal Count: " + gameGoalCount);
        //if (gameGoalCount <= 0 && menuActive == null)
        //{
        //    statePause();
        //    menuActive = menuWin;
        //    // gameScore += 500;
        //    menuActive.SetActive(true);
        //    // soundManager.instance.playVictorySound();
        //}
    }

    public void updateGameKillStat(int amount)
    {
        gameKillCount += amount;
        killStats.text = gameKillCount.ToString("F0");
        Debug.Log("Game Kill Count: " + gameKillCount);
    }

    public void updatePlayerScore(int amount)
    {
        gameScore += amount;
        levelHUDText.text = gameScore.ToString("F0");
        scoreMenuStat.text = gameScore.ToString("F0");
        StartCoroutine(addScoreHUD(amount));
    }
    public void updatePlayerAmmo(int curr, int max)
    {
        playerAmmo.text = curr.ToString("F0") + " / " + max.ToString("F0");

        // Tie amount to Ammo Bar fill amount
        playerAmmoBar.fillAmount = (float)curr / (float)max;
        playerBulletImage.fillAmount = (float)curr / (float)max;
    }
    IEnumerator addScoreHUD(int amount)
    {
        levelHUDFeedbackText.text = amount.ToString("F0");
        Color colorOrig = levelHUDText.color;
        Color colorChange = Color.green;

        levelHUDFeedback.SetActive(true);

        float duration = 0.7f;
        float elapsed = 0f;
        Vector3 startScale = Vector3.one * 1.5f;
        Vector3 endScale = Vector3.zero;

        CanvasGroup cg = levelHUDFeedback.GetComponent<CanvasGroup>();
        cg.alpha = 1f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            levelHUDText.color = Color.Lerp(colorOrig, colorChange, t);
            levelHUDFeedback.transform.localScale = Vector3.Lerp(startScale, endScale, t);

            cg.alpha = Mathf.Lerp(1f, 0f, t);
            yield return null;
        }
        levelHUDText.color = colorOrig;
        levelHUDFeedback.transform.localScale = endScale;
        cg.alpha = 0f;
        levelHUDFeedback.SetActive(false);
        cg.alpha = 1f; // reset alpha for next time
        levelHUDFeedback.transform.localScale = startScale;

    }

    public void UpdateInteractPrompt(string prompt)
    {
        interactPromptText.text = prompt;
    }

    public void UpdateKeyCountText()
    {
        keyCount++;
        keyCountText.text = keyCount.ToString() + "/4";

    }
    public void ActivateAmmoUI()
    {
        playerAmmoCanvas.SetActive(true);
        reticle.SetActive(true);
    }

    public void DeactivateAmmoUI()
    {
        playerAmmoCanvas.SetActive(false);
        reticle.SetActive(false);

    }

    public void LevelUp()
    {
        //levelHUDFeedbackText.text = "Level Up!";
        //levelHUDFeedback.SetActive(true);
        //StartCoroutine(LevelUpRoutine());
        //playerLevel++;
        //levelHUDText.text = playerLevel.ToString();
    }

    IEnumerator LevelUpRoutine()
    {
        float duration = 1.5f;
        float elapsed = 0f;
        Vector3 startScale = Vector3.one * 1.5f;
        Vector3 endScale = Vector3.zero;
        CanvasGroup cg = levelHUDFeedback.GetComponent<CanvasGroup>();
        cg.alpha = 1f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            levelHUDFeedback.transform.localScale = Vector3.Lerp(startScale, endScale, t);
            cg.alpha = Mathf.Lerp(1f, 0f, t);
            yield return null;
        }
        levelHUDFeedback.transform.localScale = endScale;
        cg.alpha = 0f;
        levelHUDFeedback.SetActive(false);
        cg.alpha = 1f; // reset alpha for next time
        levelHUDFeedback.transform.localScale = startScale;
    }
    public void ShowPromptTemporary(string prompt, int duration)
    {
        UpdateInteractPrompt(prompt);
        StartCoroutine(ClearPrompt(duration));
    }
    private IEnumerator ClearPrompt(int waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        interactPromptText.text = "";
    }

    public void updatePlayerEXP(int curr, int max)
    {
        playerLevelEXP.text = curr.ToString("F0") + "/" + max.ToString("F0");
    }

    public void updateHealthValue(int current)
    {
        playerHealthValue.text = current.ToString("F0");
    }

    public void updateStaminaValue(int current)
    {
        playerStaminaValue.text = current.ToString("F0");
    }
}