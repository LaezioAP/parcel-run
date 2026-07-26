using UnityEngine;

public class ScrollController : MonoBehaviour
{
    [SerializeField] private float speed = 2f;

    public float Speed => speed;

    public void SetSpeed(float newSpeed)
    {
        speed = Mathf.Max(0f, newSpeed);
    }
}