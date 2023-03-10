using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
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

    private Pistol pistol;
    private Inventory inventory;
    public int ammo;
    private bool isReloading;
    public AudioManager audioManager;
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
        inventory = GetComponent<Inventory>();

        //if (instance != null)
        //{
        //    Destroy(this.gameObject);
        //    return;
        //}
        //instance = this;
        //GameObject.DontDestroyOnLoad(this.gameObject);
    }

    private void OnEnable()
    {
        shootAction.performed += _ => ShootGun();
    }

    public void Interact()
    {
        RaycastHit hit;
        if (Physics.Raycast(cameraTransform.position, cameraTransform.transform.forward, out hit, Mathf.Infinity))
        {
            Debug.Log("Interact: " + hit.collider.gameObject.name);
            if (hit.collider.gameObject.name == "Next Level")
            {
                hit.collider.gameObject.GetComponent<Interact>().Trigger();
            }
        } 
    }

    public Weapon EquipWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            var equippedWeapon = pistol;
            Debug.Log("Equipped pistol");

            return equippedWeapon;

        }
        else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            // something else
            Debug.Log("Equipped something else");

        } 
        return pistol;

    }

    IEnumerator Reload()
    {
        Debug.Log("reloading");
        isReloading = true;
        audioManager.PlaySound("Reload");
        yield return new WaitForSeconds(3);
        Debug.Log("reload finished");
        var leftoverAmmo = inventory.ammo;
        inventory.ammo = 24;
        inventory.ammoInInventory = inventory.ammoInInventory - 24 + leftoverAmmo;
        isReloading = false;
    }

    public GameObject CreateBullet()
    {
        GameObject bullet = GameObject.Instantiate(bulletPrefab, barrelTransform.position, Quaternion.identity, bulletParent);
        bullet.GetComponent<MeshRenderer>().enabled = false;
        return bullet;
    }

    public void ShootGun()
    {
        if (!PauseMenu.isPaused)
        {
            if (!isReloading && inventory.ammo <= 0)
            {
                StartCoroutine(Reload());
            }

            if (!isReloading && inventory.ammo > 0)
            {
                inventory.ammo -= 1;
                Debug.Log("ammo: " + ammo);
                RaycastHit hit;
                GameObject bullet = CreateBullet();
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
            } else
            {
                Debug.Log("Wait i am reloading!");
            }
        }
    }

    void Update()
    {
        if (!PauseMenu.isPaused)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Debug.Log("Equipped pistol");


            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                // something else
                Debug.Log("Equipped something else");

            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                Interact();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                StartCoroutine(Reload());
            }

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
        } 
        else
        {
            crosshair.SetActive(false);
        }
    }
}
