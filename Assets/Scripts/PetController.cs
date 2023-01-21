using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PetController : MonoBehaviour
{
    private float gravity = -9.81f;
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        playerVelocity.y += gravity * Time.deltaTime;

    }
}
