using System;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private Camera m_Camera;
    // Viteza de mișcare a jucătorului
    public float speed = 5f;

    // Referință la Animator pentru a controla animațiile
    private Animator animator;
    private Rigidbody rb;

    private float lastVertical = 0;
    private float lastHorizontal = 0;

    private Vector2 turn;
    private float sensitivity = 50.0f;

    [SerializeField] private float jumpForce = 5f;

    private bool isGrounded = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        // Obținem componenta Animator și Rigidbody de pe obiectul atașat
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.LogError("Rigidbody component is missing from the player!");
        }

        animator.SetBool("IsRunning", false);
    }

    void Update()
    {
        // Captăm intrările de la tastatură pentru deplasare
        float horizontal = Input.GetAxisRaw("Horizontal"); // -1 pentru stânga, 1 pentru dreapta
        float vertical = Input.GetAxisRaw("Vertical"); // -1 pentru spate, 1 pentru față

        // Actualizăm animațiile bazate pe input
        UpdateAnimations(horizontal, vertical);

        turn.x += Input.GetAxisRaw("Mouse X") * sensitivity * Time.deltaTime;
        turn.y += Input.GetAxisRaw("Mouse Y") * sensitivity * Time.deltaTime;

        turn.y = Math.Clamp(turn.y, -90.0f, 90.0f);

        transform.localRotation = Quaternion.Euler(0, turn.x, 0);
        m_Camera.transform.localRotation = Quaternion.Euler(-turn.y, 0, 0);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Create a movement vector based on input
        Vector3 movement = new Vector3(horizontal, 0f, vertical).normalized;

        // Transform movement direction to align with the player's current rotation
        Vector3 moveDirection = transform.TransformDirection(movement);

        // Apply movement to Rigidbody
        rb.MovePosition(rb.position + moveDirection * speed * Time.fixedDeltaTime);
    }

    void UpdateAnimations(float horizontal, float vertical)
    {
        if (vertical == 1 && lastVertical != vertical)
        {
            animator.SetInteger("VerticalWalk", (int)vertical);
            lastVertical = vertical;
        }
        else if (vertical == 0 && lastVertical != vertical)
        {
            animator.SetInteger("VerticalWalk", (int)vertical);
            lastVertical = vertical;
        }
        else if (vertical == -1 && lastVertical != vertical)
        {
            animator.SetInteger("VerticalWalk", (int)vertical);
            lastVertical = vertical;
        }

        if (horizontal == 0 && lastHorizontal != horizontal)
        {
            animator.SetInteger("HorizontalWalk", (int)horizontal);
            lastHorizontal = horizontal;
        }
        else if (horizontal == 1 && lastHorizontal != horizontal)
        {
            animator.SetInteger("HorizontalWalk", (int)horizontal);
            lastHorizontal = horizontal;
        }
        else if (horizontal == -1 && lastHorizontal != horizontal)
        {
            animator.SetInteger("HorizontalWalk", (int)horizontal);
            lastHorizontal = horizontal;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = false;
        }
    }
}
