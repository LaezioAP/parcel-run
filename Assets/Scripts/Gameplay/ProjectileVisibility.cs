using UnityEngine;

public class ProjectileVisibility : MonoBehaviour
{
    [SerializeField] private float margin = 0.05f;

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        Vector3 viewportPosition =
            mainCamera.WorldToViewportPoint(transform.position);

        bool isOutsideCamera =
            viewportPosition.z < 0f ||
            viewportPosition.x < -margin ||
            viewportPosition.x > 1f + margin ||
            viewportPosition.y < -margin ||
            viewportPosition.y > 1f;

        if (isOutsideCamera)
        {
            Destroy(gameObject);
        }
    }
}