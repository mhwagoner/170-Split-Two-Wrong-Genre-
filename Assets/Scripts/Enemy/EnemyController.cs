using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float patrolDistance = 3f;
    [SerializeField] int damage = 1;
    [SerializeField] float damageCooldown = 1f;
    
    private Vector2 startPos;
    private int direction = 1;
    private float cooldownTimer = 0f;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        Patrol();
        if (cooldownTimer > 0f)
            cooldownTimer -= Time.deltaTime;
    }

    void Patrol()
    {
        transform.Translate(Vector2.right * direction * moveSpeed * Time.deltaTime);

        if (Mathf.Abs(transform.position.x - startPos.x) >= patrolDistance)
        {
            direction *= -1;
            transform.localScale = new Vector3(
                -transform.localScale.x,
                transform.localScale.y,
                transform.localScale.z
            );
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Collided with: " + col.gameObject.name);
        
        if (cooldownTimer > 0f) return;

        IDamageable damageable = col.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage);
            cooldownTimer = damageCooldown;
        }
    }
}