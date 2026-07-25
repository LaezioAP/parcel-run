using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Rigidbody2D _rig;

    [SerializeField] private float lifeTime = 3f;

    private void Awake()
    {
        _rig = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _rig.linearVelocity = Vector2.up * speed;
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {

    }
}
