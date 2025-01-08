using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelReturn : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene("MenuScene");
    }
}
