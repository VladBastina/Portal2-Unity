using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    public GameObject door;
    public GameObject buttonBase;

    private Animator animator;
    private Animator buttonAnimator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (door == null)
        {
            Debug.LogError("Door GameObject is not assigned.");
            return;
        }

        animator = door.GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found on the assigned door GameObject.");
        }

        buttonAnimator = buttonBase.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Cube") || collision.collider.CompareTag("Player"))
        {
            animator.Play("OpenDoorLevel");
            buttonAnimator.Play("PressButton");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Cube") || collision.collider.CompareTag("Player"))
        {
            animator.Play("ExitDoorLevel");
            buttonAnimator.Play("UnpressButton");
        }
    }
}
