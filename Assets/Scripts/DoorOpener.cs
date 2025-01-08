using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    public GameObject door;
    public GameObject buttonBase;

    private Animator animator;
    private AudioSource doorAudioSource;

    public AudioClip doorOpen;
    public AudioClip doorClose;

    private Animator buttonAnimator;
    private AudioSource buttonAudioSource;

    public AudioClip buttonPress;
    public AudioClip buttonRelease;

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
        doorAudioSource = door.GetComponent<AudioSource>();

        buttonAnimator = buttonBase.GetComponent<Animator>();
        buttonAudioSource = buttonBase.GetComponent<AudioSource>();
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
            buttonAudioSource.clip = buttonPress;
            buttonAudioSource.Play();
            doorAudioSource.clip = doorOpen;
            doorAudioSource.Play();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Cube") || collision.collider.CompareTag("Player"))
        {
            animator.Play("ExitDoorLevel");
            buttonAnimator.Play("UnpressButton");
            buttonAudioSource.clip = buttonRelease;
            buttonAudioSource.Play();
            doorAudioSource.clip = doorClose;
            doorAudioSource.Play();
        }
    }
}
