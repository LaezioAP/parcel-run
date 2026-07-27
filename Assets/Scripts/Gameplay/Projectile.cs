using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime = 3f;
    [SerializeField] private int damage = 1;
    private Rigidbody2D _rig;

    private void Awake()
    {
        _rig = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _rig.linearVelocity = Vector2.up * speed;
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent(out EnemyHealth enemyHealth)) return;

        enemyHealth.TakeDamage(damage);
        Destroy(gameObject);
    }
}
