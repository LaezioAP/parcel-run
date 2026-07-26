using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private ScrollController scrollController;

    [Header("Shooting")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 0.2f;

    private Rigidbody2D _rigidbody;
    private Vector2 _movementInput;

    private bool isFiring;
    private float nextFireTime;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputValue value)
    {
        _movementInput = value.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        Vector2 playerMovement = _movementInput * movementSpeed;
        Vector2 scrollMovement = Vector2.up * scrollController.Speed;

        _rigidbody.linearVelocity = playerMovement + scrollMovement;
    }

    private void Update()
    {
        if (!isFiring)
            return;

        if (Time.time >= nextFireTime)
        {
            Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            nextFireTime = Time.time + fireRate;
        }
    }

    public void OnFire(InputValue value)
    {
        isFiring = value.isPressed;
    }
}