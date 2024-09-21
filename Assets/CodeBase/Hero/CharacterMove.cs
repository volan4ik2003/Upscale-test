using System;
using CodeBase.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class CharacterMove : MonoBehaviour
{
    public float playerSpeed = 5f;
    public float runSpeed = 10f;
    public float mouseSensitivity = 2f;
    public Transform playerCamera;
    public AudioClip stepSound;
    private AudioSource audioSource;

    private UIController _uiController;
    private KeyCollector _keyCollector;

    private Vector3 velocity;
    private float speed;
    private float xRotation = 0f;
    private IInputService _inputService;
    private CharacterController _controller;
    private float gravity = -9.8f;
    private float stepRate = 0.5f;
    private float stepTimer = 0f;

    [Inject]
    private void Construct(IInputService inputService)
    {
        _inputService = inputService;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Block cursor
        _controller = GetComponent<CharacterController>();
        _controller.slopeLimit = 180f;

        // Camera start position
        xRotation = 0f;
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        _keyCollector = FindAnyObjectByType<KeyCollector>();
        _uiController = FindAnyObjectByType<UIController>();

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        HandleMouseLook();  // Camera move
        HandleMovement();    // Character movement
    }

    private void HandleMouseLook()
    {
        Vector3 mouse = GetMouseVector();
        transform.Rotate(Vector3.up * mouse.x * mouseSensitivity);
        xRotation -= mouse.y * mouseSensitivity;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    private void HandleMovement()
    {
        Vector3 moveVector = GetMoveVector();
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = runSpeed;
            stepRate = 0.3f;
        }
        else
        {
            speed = playerSpeed;
            stepRate = 0.5f;
        }

        // Character vector move
        Vector3 move = transform.right * moveVector.x + transform.forward * moveVector.z;

        // Normalize the move vector to keep speed constant
        if (move.magnitude > 1f)
            move.Normalize();

        // Move the character
        _controller.Move(speed * Time.deltaTime * move);

        // Check if the character is grounded
        if (_controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;  // Character stay on the ground
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        _controller.Move(velocity * Time.deltaTime);  // Move under gravity

        // Footstep SFX
        if (move.magnitude > 0)
        {
            stepTimer += Time.deltaTime;
            if (stepTimer >= stepRate)
            {
                PlayStepSound();
                stepTimer = 0f;
            }
        }
    }

    private void PlayStepSound()
    {
        audioSource.PlayOneShot(stepSound);
    }

    private Vector3 GetMoveVector()
    {
        return new Vector3(_inputService.Axis.x, 0, _inputService.Axis.y);
    }

    private Vector3 GetMouseVector()
    {
        return new Vector3(_inputService.MouseAxis.x, _inputService.MouseAxis.y, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            _keyCollector.OnKeyCollected();
            _uiController.UpdateScore();
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Exit"))
        {
            if (other.gameObject.GetComponent<Exit>().IsAllKeysCollected)
            {
                _uiController.ShowWinPanel();
                enabled = false;
            }
        }

        if (other.CompareTag("Obstacle"))
        {
            _uiController.ShowLosePanel();
            enabled = false;
        }
    }
}