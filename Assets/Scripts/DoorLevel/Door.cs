using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator _animator;
    private AudioSource _audioSource;


    [SerializeField] private AudioClip _OpenDoor;
    [SerializeField] private AudioClip _CloseDoor;


    void Start()
    {
        _animator= GetComponent<Animator>();
        _audioSource= GetComponent<AudioSource>();
    }

    public void OpenDoor()
    {
        _animator.Play("OpenDoorLevel");
    }
    public void OpenDoorSound()
    {
        _audioSource.clip= _OpenDoor;
        _audioSource.Play();
    }
    public void CloseDoor()
    {
        _animator.Play("ExitDoorLevel");
    }
    public void CloseDoorSound()
    {
        _audioSource.clip= _CloseDoor;
        _audioSource.Play();
    }

}
