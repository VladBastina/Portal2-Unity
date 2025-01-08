using UnityEngine;

public class ReverseElevator : MonoBehaviour
{
    [SerializeField] private Animator animator; // Animatorul obiectului
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private string rightDoor_parameterName = "IsNear"; // Numele parametrului în Animator
    [SerializeField] private string leftDoor_parameterName = "IsLNear"; // Numele parametrului în Animator
    [SerializeField] private bool left_or_right = false;

    private void Start()
    {
        animator.SetBool(leftDoor_parameterName, true);
        audioSource.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verificăm dacă playerul intră în trigger
        if (other.CompareTag("Player"))
        {
            if (!left_or_right)
            {
                animator.SetBool(rightDoor_parameterName, true);
                audioSource.Play();
            }
            else
            {
                animator.SetBool(leftDoor_parameterName, true);
                audioSource.Play();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Verificăm dacă playerul iese din trigger
        if (other.CompareTag("Player"))
        {
            if (!left_or_right)
            {
                animator.SetBool(rightDoor_parameterName, false);
                audioSource.Play();
            }
            else
            {
                animator.SetBool(leftDoor_parameterName, false);
                audioSource.Play();
            }
        }
    }
}
