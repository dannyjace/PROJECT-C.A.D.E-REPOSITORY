using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoManager : MonoBehaviour
{
    public static InfoManager instance;

    [SerializeField] GameObject infoLabelWindow;
    [SerializeField] TMP_Text infoLabelText;
    [SerializeField] GameObject infoMessageWindow;
    [SerializeField] TMP_Text infoMessageText;
    [SerializeField] Image infoMessageBKG;
    [SerializeField] Image line;
    [SerializeField] Image[] icons;
    private Color iconColor;

    [SerializeField] bool isShowing;
    [SerializeField] float lerpDuration = 0.5f;
    private float elapsed;
    private float waitTime;
    private Vector3 labelOrigPos;
    private Vector3 labelHidPos;
    private Vector3 textBoxOrigPos;
    private Vector3 textBoxHidPos;
    private Coroutine currentCoroutine;
    private bool isCoroutineRunning = false;
    private bool isInfoShowing = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        instance = this;
        infoLabelWindow.SetActive(false);
        infoMessageWindow.SetActive(false);
        labelOrigPos = infoLabelWindow.transform.position;
        labelHidPos = new Vector3(labelOrigPos.x, labelOrigPos.y + 38, labelOrigPos.z);
        textBoxOrigPos = infoMessageWindow.transform.position;
        textBoxHidPos = new Vector3(textBoxOrigPos.x, textBoxOrigPos.y + 130, textBoxOrigPos.z);
        infoLabelWindow.transform.position = labelHidPos;
        infoMessageWindow.transform.position = textBoxHidPos;
    }

    // Update is called once per frame
    private void Update()
    {



    }

    // Make an Enumerator to show a warning message titled with a label for a set duration
    public void ShowMessage(string label, string text, Color colorScheme, int duration = 5)
    {
        if (isCoroutineRunning)
        {
            StopCoroutine(currentCoroutine);
            isCoroutineRunning = false;
        }
        currentCoroutine = StartCoroutine(showMessage(label, text, duration, colorScheme));

    }
    public void HideMessage()
    {
        if (isCoroutineRunning)
        {
            StopCoroutine(currentCoroutine);
            isCoroutineRunning = false;
        }
        currentCoroutine = StartCoroutine(hideMessage());

    }

    public IEnumerator hideMessage()
    {
        isCoroutineRunning = true;
        // Lerp out the text box back to the original position
        elapsed = 0f;

        while (elapsed < lerpDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / lerpDuration);
            infoMessageWindow.transform.position = Vector3.Lerp(textBoxOrigPos, textBoxHidPos, t);
            yield return null;
        }
        infoMessageWindow.SetActive(false);
        // Lerp out the warning box back to the original position
        elapsed = 0f;

        while (elapsed < lerpDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / lerpDuration);
            infoLabelWindow.transform.position = Vector3.Lerp(labelOrigPos, labelHidPos, t);
            yield return null;

        }
        infoLabelWindow.SetActive(false);
        isCoroutineRunning = false;
        isInfoShowing = false;
    }

    public IEnumerator showMessage(string label, string text, int duration, Color colorScheme)
    {
        isCoroutineRunning = true;
        isInfoShowing = true;
        infoLabelText.text = label;
        infoLabelText.color = colorScheme;
        infoMessageText.text = text;
        icons[0].color = colorScheme;
        line.color = colorScheme;
        infoMessageBKG.color = new Color(colorScheme.r, colorScheme.g, colorScheme.b, 0.1f);

        infoLabelWindow.SetActive(true);
        // Lerp in the warning box from the original y position to a difference of -39 on the Y axis
        elapsed = 0f;

        while (elapsed < lerpDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / lerpDuration);
            infoLabelWindow.transform.position = Vector3.Lerp(labelHidPos, labelOrigPos, t);
            yield return null;
        }
        // Lerp the text box in from the original x position to a difference of -39 on the Y axis
        elapsed = 0f;

        infoMessageWindow.SetActive(true);
        while (elapsed < lerpDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / lerpDuration);
            infoMessageWindow.transform.position = Vector3.Lerp(textBoxHidPos, textBoxOrigPos, t);
            yield return null;
        }
        yield return new WaitForSeconds(duration);
        isCoroutineRunning = false;
        StartCoroutine(hideMessage());

    }


    public bool IsInfoShowing() { return isInfoShowing; }
}
