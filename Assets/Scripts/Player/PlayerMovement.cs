using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private ScrollController scrollController;

    [Header("Shooting")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 0.2f;

    [Header("Camera")]
    [SerializeField] private Camera mainCamera;

    [Header("Input")]
    private Collider2D _collider;

    private Rigidbody2D _rigidbody;
    private Vector2 _movementInput;

    private bool isFiring;
    private float nextFireTime;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
    }

    public void OnMove(InputValue value)
    {
        _movementInput = value.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        Vector2 nextPosition =
            _rigidbody.position + GetMovementDelta();

        nextPosition = ClampToCamera(nextPosition);

        _rigidbody.MovePosition(nextPosition);
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

    private Vector2 GetMovementDelta()
    {
        Vector2 playerMovement = _movementInput * movementSpeed;
        Vector2 scrollMovement = Vector2.up * scrollController.Speed;

        Vector2 velocity = playerMovement + scrollMovement;

        return velocity * Time.fixedDeltaTime;
    }

    private Vector2 ClampToCamera(Vector2 desiredPosition)
    {
        float distanceFromCamera =
            Mathf.Abs(mainCamera.transform.position.z - transform.position.z);

        Vector3 cameraBottomLeft =
            mainCamera.ViewportToWorldPoint(
                new Vector3(0f, 0f, distanceFromCamera)
            );

        Vector3 cameraTopRight =
            mainCamera.ViewportToWorldPoint(
                new Vector3(1f, 1f, distanceFromCamera)
            );

        Bounds bounds = _collider.bounds;

        Vector2 colliderOffset =
            (Vector2)bounds.center - _rigidbody.position;

        Vector2 colliderExtents = bounds.extents;

        float minimumX =
            cameraBottomLeft.x + colliderExtents.x - colliderOffset.x;

        float maximumX =
            cameraTopRight.x - colliderExtents.x - colliderOffset.x;

        float minimumY =
            cameraBottomLeft.y + colliderExtents.y - colliderOffset.y;

        float maximumY =
            cameraTopRight.y - colliderExtents.y - colliderOffset.y;

        desiredPosition.x =
            Mathf.Clamp(desiredPosition.x, minimumX, maximumX);

        desiredPosition.y =
            Mathf.Clamp(desiredPosition.y, minimumY, maximumY);

        return desiredPosition;
    }
}