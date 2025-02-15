﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CameraMove : MonoBehaviour
{
    [SerializeField] private Camera m_Camera;

    private const float moveSpeed = 7.5f;
    private const float cameraSpeed = 3.0f;
    private const float jumpForce = 5.0f;

    public Quaternion BodyRotation { private set; get; }
    public Quaternion CameraRotation { private set; get; }

    private Vector3 moveVector = Vector3.zero;
    private bool isGrounded = true; // Track whether the player is grounded.
    private new Rigidbody rigidbody;

    private AudioSource audioSource;

    [SerializeField]
    private AudioClip walksound;
    [SerializeField]
    private AudioClip jumpSound;
    [SerializeField]
    private AudioClip landSound;

    private float targetVolume = 0.0f;
    private float fadeSpeed = 5.0f;

    private Animator animator;
    private float lastVertical = 0;
    private float lastHorizontal = 0;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;

        BodyRotation = transform.rotation;
        CameraRotation = m_Camera.transform.localRotation;
    }

    private void Update()
    {
        if (Time.timeScale == 0f) return;

        // Rotate the body around the Y-axis.
        float bodyRotationY = Input.GetAxis("Mouse X") * cameraSpeed;
        BodyRotation *= Quaternion.Euler(0.0f, bodyRotationY, 0.0f);
        transform.rotation = BodyRotation;

        // Rotate the camera around the X-axis.
        float cameraRotationX = -Input.GetAxis("Mouse Y") * cameraSpeed;
        CameraRotation *= Quaternion.Euler(cameraRotationX, 0.0f, 0.0f);

        // Clamp the camera's rotation to avoid flipping.
        Vector3 cameraEuler = CameraRotation.eulerAngles;
        if (cameraEuler.x > 180.0f) cameraEuler.x -= 360.0f;
        cameraEuler.x = Mathf.Clamp(cameraEuler.x, -75.0f, 75.0f);
        CameraRotation = Quaternion.Euler(cameraEuler);

        m_Camera.transform.localRotation = CameraRotation;

        // Move the body.
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        moveVector = new Vector3(x, 0.0f, z).normalized * moveSpeed;

        HandleWalkingSound();

        UpdateAnimations(x, z);

        // Check for jump.
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false; // Prevent double jumping.
            
            PlaySound(jumpSound,false);
        }
    }

    private void FixedUpdate()
    {
        // Apply movement.
        Vector3 moveDirection = transform.TransformDirection(moveVector);
        rigidbody.linearVelocity = new Vector3(moveDirection.x, rigidbody.linearVelocity.y, moveDirection.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if grounded.
        if (collision.contacts[0].normal.y > 0.5f)
        {
            if (!isGrounded)
            {
                PlaySound(landSound, false);
            }
            isGrounded = true;
        }
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

    private void HandleWalkingSound()
    {
        if (moveVector != Vector3.zero && isGrounded)
        {
            targetVolume = 1.0f; // Setează volumul țintă pentru sunetul de mers.

            if (!audioSource.isPlaying || audioSource.clip != walksound)
            {
                PlaySound(walksound, true); // Redă sunetul în buclă.
            }
        }
        else
        {
            targetVolume = 0.0f; // Reduce volumul la 0 dacă jucătorul nu se mișcă.
            audioSource.Stop();
        }
    }

    public void PlaySound(AudioClip clip, bool loop)
    {
        if (audioSource.isPlaying && audioSource.clip == clip)
            return; // Dacă sunetul curent este deja redat, nu-l relua.

        audioSource.clip = clip;
        audioSource.loop = loop;
        audioSource.Play();
    }

    public void ResetBodyRotation()
    {
        BodyRotation = Quaternion.LookRotation(transform.forward, Vector3.up);
    }

    public Vector3 GetForward()
    {
        // Combine the body's Y-axis rotation with the camera's X-axis rotation.
        Quaternion combinedRotation = Quaternion.Euler(CameraRotation.eulerAngles.x, BodyRotation.eulerAngles.y, 0.0f);
        return combinedRotation * Vector3.forward;
    }
}
