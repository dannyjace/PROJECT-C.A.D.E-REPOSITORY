using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour
{
    public void resume()
    {
        GameManager.instance.stateUnpause();
    }

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameManager.instance.stateUnpause();
    }

    public void quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
    }

    public void respawn()
    {
        GameManager.instance.playerScript.SpawnPlayer();
        GameManager.instance.stateUnpause();
    }
    public void back()
    {
        GameManager.instance.closeSettings();
    }

    public void openSettings()
    {
        GameManager.instance.openSettings();
    }

    public void openInventory()
    {
        GameManager.instance.openInventory();
    }

    public void openSkillTree()
    {
        GameManager.instance.openSkillTree();
    }

    public void openControllerSettings()
    {
        GameManager.instance.openControllerSettings();
    }
    public void openAudioSettings()
    {
        GameManager.instance.openAudioSettings();
    }
    public void selectItem(int index)
    {
        //InventoryManager.instance.SelectItem(index);
    }
    public void removeItem()
    {
        // Drop selected item
        // InventoryManager.instance.RemoveSelectedItem();
    }
    public void useItem()
    {
        // Use selected item
        //InventoryManager.instance.UseItem();
    }

    public void addItem()
    {
        // Add selected item to inventory
        //InventoryManager.instance.AddSelectedItem();
    }

    public void resetAll()
    {
        //InventoryManager.instance.ResetAll();
    }
}
