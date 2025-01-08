using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private bool left_or_right;

    private void OnTriggerEnter(Collider other)
    {
        if (left_or_right)
        {
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            SceneManager.LoadScene("Level1");
        }
    }
}
