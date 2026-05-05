using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLane : MonoBehaviour
{
    public float laneDistance = 3f;
    public float laneChangeSpeed = 10f;

    private int currentLane = 0;
    private Vector3 targetPosition;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && currentLane > -1)
        {
            currentLane--;
            anim.SetTrigger("Left");
        }

        if (Input.GetKeyDown(KeyCode.D) && currentLane < 1)
        {
            currentLane++;
            anim.SetTrigger("Right");
        }

        targetPosition = new Vector3(currentLane * laneDistance, transform.position.y, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, targetPosition, laneChangeSpeed * Time.deltaTime);
    }
}
