using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float forwardSpeed = 10f;
    public float laneDistance = 3f;
    public float laneSpeed = 12f;

    [Header("Jump Settings")]
    public float jumpForce = 7f;
    public float gravity = -20f;

    [Header("Animation Settings")]
    public float animationSmoothTime = 8f;

    private CharacterController controller;
    private Animator animator;

    private int targetLane = 0;
    private float velocityY;
    private float currentSideAnimValue = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleInput();
        UpdateAnimations();
        MovePlayer();

    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.A) && targetLane > -1)
            targetLane--;

        if (Input.GetKeyDown(KeyCode.D) && targetLane < 1)
            targetLane++;

        if (controller.isGrounded)
        {
            if (velocityY < 0) velocityY = -2f;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                velocityY = jumpForce;
                animator.SetTrigger("Jump");
            }
        }
    }

    void UpdateAnimations()
    {
        float targetX = targetLane * laneDistance;
        float distanceToTarget = targetX - transform.position.x;

        float targetSideAnim = Mathf.Clamp(distanceToTarget / laneDistance, -1f, 1f);

        currentSideAnimValue = Mathf.Lerp(currentSideAnimValue, targetSideAnim, Time.deltaTime * animationSmoothTime);

        animator.SetFloat("Side", currentSideAnimValue);
        animator.SetBool("Grounded", controller.isGrounded);
    }

    void MovePlayer()
    {
        float targetX = targetLane * laneDistance;

        float nextX = Mathf.MoveTowards(transform.position.x, targetX, laneSpeed * Time.deltaTime);
        float deltaX = nextX - transform.position.x;

        velocityY += gravity * Time.deltaTime;

        Vector3 moveVector = new Vector3(
            deltaX,
            velocityY * Time.deltaTime,
            forwardSpeed * Time.deltaTime
        );

        controller.Move(moveVector);
    }
}
