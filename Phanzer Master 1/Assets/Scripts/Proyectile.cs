using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float damage = 10f;
    public float fireRate = 1f; // Tiempo en segundos entre cada disparo
    public float lifeTime = 0.3f;
    private Transform target;
    private float nextFireTime; // Tiempo en el que se podrá disparar nuevamente

    void Start()
    {
        nextFireTime = Time.time; // Inicialmente, puede disparar de inmediato
    }
    public void Initialize(Transform target, float speed, float damage)
    {
        this.target = target;
        this.speed = speed;
        this.damage = damage;
    }

    void Update()
    {
        // Movimiento hacia el objetivo si existe
        if (target != null)
        {
            // Mueve el disparo hacia el objetivo
            Vector3 direction = (target.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime);
            Destroy(gameObject, lifeTime);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        // Solo procesar colisión si es con un BoxCollider2D
        if (other is BoxCollider2D)
        {
            Tank tank = other.GetComponent<Tank>();
            Base baseComponent = other.GetComponent<Base>();

            if (tank != null && tank.transform == target)
            {
                // Aplicar daño al tanque y destruir el disparo
                tank.TakeDamage(damage);
                Destroy(gameObject);
            }
            else if (baseComponent != null && baseComponent.transform == target)
            {
                // Aplicar daño a la base y destruir el disparo
                baseComponent.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
    public bool CanFire()
    {
        return Time.time >= nextFireTime;
    }

    public void Fire()
    {
        nextFireTime = Time.time + 1f / fireRate;
    }
}
