using UnityEngine;
using System.Collections;

public class checkpoint : MonoBehaviour
{
    [SerializeField] Renderer checkModel;

    //Color colorOrg;
    private void Start()
    {
        //colorOrg = checkModel.material.color;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && GameManager.instance.playerSpawnPos.transform.position != transform.position)
        {
            GameManager.instance.playerSpawnPos.transform.position = GameManager.instance.player.transform.position;
            StartCoroutine(checkpFeedback());
        }
    }

    IEnumerator checkpFeedback()
    {
        GameManager.instance.checkpointPopup.SetActive(true);
        //checkModel.material.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        //checkModel.material.color = colorOrg;
        GameManager.instance.checkpointPopup.SetActive(false);
    }

}
