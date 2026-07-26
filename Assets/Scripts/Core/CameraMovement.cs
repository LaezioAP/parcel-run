using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private ScrollController scrollController;

    private void FixedUpdate()
    {
        float movement = scrollController.Speed * Time.fixedDeltaTime;

        transform.position += Vector3.up * movement;
    }
}