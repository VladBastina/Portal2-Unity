using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] private GameObject plm;

    private Animator _animator;
   void Start()
   {
    _animator=plm.GetComponent<Animator>();     
   }


    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Atins"))
        {
            _animator.SetBool("isOpen",true);
        }
    }

}
