using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 5f;
    [SerializeField]
    private float jumpHeight = 9.0f;
    [SerializeField]
    private float gravityValue = -9.81f;
    [SerializeField]
    private float rotationSpeed = 5f;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Transform bulletParent;
    [SerializeField]
    private Transform barrelTransform;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction shootAction;
    private Transform cameraTransform;
    [SerializeField]
    private float bulletHitMissDistance = 25f;
    public GameObject crosshair;

    private Animator animator;
    int moveXAnimatiorParamId;
    int moveZAnimatiorParamId;
    int jumpAnimation;
    int recoilAnimation;

    public bool canShoot;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        cameraTransform = Camera.main.transform;
        shootAction = playerInput.actions["Shoot"];
        crosshair.SetActive(true);
        animator = GetComponent <Animator>();
        moveXAnimatiorParamId = Animator.StringToHash("MoveX");
        moveZAnimatiorParamId = Animator.StringToHash("MoveZ");
        jumpAnimation= Animator.StringToHash("Jump");
        recoilAnimation= Animator.StringToHash("Recoil");
        canShoot = true;
    }

    private void OnEnable()
    {
        shootAction.performed += _ => ShootGun();
    }

    public void ShootGun()
    {
        if (!PauseMenu.isPaused)
        {

                RaycastHit hit;
                GameObject bullet = GameObject.Instantiate(bulletPrefab, barrelTransform.position, Quaternion.identity, bulletParent);
                bullet.GetComponent<MeshRenderer>().enabled = false;
                AudioManager.instance.PlaySound("Shoot");
                BulletController bulletController = bullet.GetComponent<BulletController>();
                if (Physics.Raycast(cameraTransform.position, cameraTransform.transform.forward, out hit, Mathf.Infinity))
                {
                    bulletController.target = hit.point;
                    bulletController.hit = true;
                }
                else
                {
                    bulletController.target = cameraTransform.position + cameraTransform.forward * bulletHitMissDistance;
                    bulletController.hit = false;
                }
                animator.CrossFade(recoilAnimation, 0.2f);
            
        }
    }

    void Update()
    {
        if (!PauseMenu.isPaused)
        {
            crosshair.SetActive(true);
            groundedPlayer = controller.isGrounded;
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

            Vector2 input = moveAction.ReadValue<Vector2>();
            Vector3 move = new Vector3(input.x, 0, input.y);
            // Camera direction when moving
            move = move.x * cameraTransform.right.normalized + move.z * cameraTransform.forward.normalized;
            move.y = 0f;
            controller.Move(move * Time.deltaTime * playerSpeed);

            // 
            animator.SetFloat(moveXAnimatiorParamId, input.x);
            animator.SetFloat(moveZAnimatiorParamId, input.y);

            // Changes the height position of the player..
            if (jumpAction.triggered && groundedPlayer)
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
                animator.SetTrigger(jumpAnimation);
            }

            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);

            // Rotate towards camera
            float targetAngle = cameraTransform.eulerAngles.y;
            Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        } else
        {
            crosshair.SetActive(false);
        }
    }
}
