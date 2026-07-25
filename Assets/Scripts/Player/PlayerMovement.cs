using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    private Rigidbody2D _rigidbody;
    private Vector2 _movementInput;


    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;

    [SerializeField] private float fireRate = 0.2f;
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

    void FixedUpdate()
    {
        _rigidbody.linearVelocity = _movementInput * movementSpeed;
    }

    public void Update()
    {
        if (!isFiring) return;

        if (Time.time >= nextFireTime)
        {
            Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            nextFireTime = Time.time + fireRate;
        }
    }

    private void OnFire(InputValue value)
    {
        isFiring = value.isPressed;
    }
}
