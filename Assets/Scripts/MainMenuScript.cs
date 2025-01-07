using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("MenuScene"); // Replace "GameScene" with the name of your game scene
    }

    // Function to open options
    public void OpenOptions()
    {
        Debug.Log("Options button clicked!"); // Placeholder - Add options logic here
    }

    // Function to exit the game
    public void ExitGame()
    {
        Debug.Log("Exit button clicked! Exiting game...");
        Application.Quit();
    }
}
