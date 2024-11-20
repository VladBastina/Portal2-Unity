using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // Viteza de mișcare a jucătorului
    public float speed = 5f;

    // Referință la Animator pentru a controla animațiile
    private Animator animator;
    private float lastVertical = 0;
    private float lastHorizontal = 0;

    void Start()
    {
        // Obținem componenta Animator de pe obiectul atașat
        animator = GetComponent<Animator>();
        animator.SetBool("IsRunning", false);
    }

    void Update()
    {
        // Captăm intrările de la tastatură pentru deplasare
        float horizontal = Input.GetAxisRaw("Horizontal"); // -1 pentru stânga, 1 pentru dreapta
        float vertical = Input.GetAxisRaw("Vertical"); // -1 pentru spate, 1 pentru față

        // Creăm un vector de mișcare
        Vector3 movement = new Vector3(horizontal, 0f, vertical).normalized;

        // Deplasăm jucătorul
        transform.Translate(movement * speed * Time.deltaTime, Space.World);

        if (vertical == 1 && lastVertical != vertical)
        {
            animator.SetInteger("VerticalWalk", (int)vertical);
            lastVertical = vertical;
        }else if (vertical == 0 && lastVertical != vertical)
        {
            animator.SetInteger("VerticalWalk", (int)vertical);
            lastVertical = vertical;
        }else if (vertical == -1 && lastVertical != vertical)
        {
            animator.SetInteger("VerticalWalk", (int)vertical);
            lastVertical = vertical;
        }

        if(horizontal == 0 && lastHorizontal != horizontal)
        {
            animator.SetInteger("HorizontalWalk", (int)horizontal);
            lastHorizontal = horizontal;
        }else if(horizontal == 1 && lastHorizontal != horizontal)
        {
            animator.SetInteger("HorizontalWalk", (int)horizontal);
            lastHorizontal = horizontal;
        }
        else if(horizontal == -1 && lastHorizontal != horizontal)
        {
            animator.SetInteger("HorizontalWalk", (int)horizontal);
            lastHorizontal = horizontal;
        }
        
    }
}
