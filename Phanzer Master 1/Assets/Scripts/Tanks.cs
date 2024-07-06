using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tank : MonoBehaviour
{
    public string tankName;
    public HealthBarCanvas HealthBar;
    public float health;
    public float range;
    public float damage;
    public float speed;
    public string faction;
    public float fireRate = 1f;
    public GameObject projectilePrefab; // Prefab del proyectil
    public float projectileSpeed = 10f; // Velocidad del proyectil
    public int goldCost; // Costo en oro
    public int metalCost; // Costo en metal
    public GameObject explosionPrefab; // Prefab de la explosión
    private SpriteRenderer spriteRenderer;
    private float maxHealth;
    private float nextFireTime;
    private Transform enemyBase;
    private Rigidbody2D rb;
    private bool isShootingBase = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        string enemyBaseTag = faction == "Base1" ? "Base2" : "Base1";
        GameObject enemyBaseObject = GameObject.FindGameObjectWithTag(enemyBaseTag);
        if (enemyBaseObject != null)
        {
            enemyBase = enemyBaseObject.transform;
        }

        nextFireTime = Time.time;
        maxHealth = health;
        HealthBar.UpdateHealthBar(maxHealth, health);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (enemyBase == null) return;

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, range);
        Transform closestEnemy = null;
        float closestDistance = float.MaxValue;

        foreach (Collider2D collider in hitColliders)
        {
            Tank enemyTank = collider.GetComponent<Tank>();
            if (enemyTank != null && enemyTank.faction != faction)
            {
                float distance = Vector2.Distance(transform.position, collider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = collider.transform;
                }
            }
        }

        if (closestEnemy != null)
        {
            rb.velocity = Vector2.zero;
            Shoot(closestEnemy);
        }
        else if (!isShootingBase)
        {
            MoveTowards(enemyBase);
        }
    }

    void MoveTowards(Transform target)
    {
        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * speed;
    }

    void Shoot(Transform target)
    {
        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + 1f / fireRate;

            if (projectilePrefab != null)
            {
                GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                Projectile projectileComponent = projectile.GetComponent<Projectile>();
                if (projectileComponent != null)
                {
                    projectileComponent.Initialize(target, projectileSpeed, damage);
                }
                Destroy(projectile, 0.5f); // Destruir el proyectil después de 0.5 segundos
            }
            else
            {
                Debug.LogError("Projectile prefab is not assigned.");
            }
        }
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        HealthBar.UpdateHealthBar(maxHealth, health);

        if (health <= 0)
        {
            DestroyTank();
        }
    }

    private void DestroyTank()
    {
        if (explosionPrefab != null)
        {
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(explosion, 1f); // Destruir la explosión después de 1 segundo
        }

        if (tankName == "Tank998")
        {
            Base base1 = GameObject.FindGameObjectWithTag("Base1").GetComponent<Base>();
            Destroy(base1.gameObject);

            SceneManager.LoadScene("P2Win");
        }
        else if (tankName == "Tank999")
        {
            Base base2 = GameObject.FindGameObjectWithTag("Base2").GetComponent<Base>();
            Destroy(base2.gameObject);
            SceneManager.LoadScene("P1Win");
        }

        Destroy(HealthBar.gameObject); // Asegúrate de destruir el gameObject de la barra de salud
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Base baseComponent = other.GetComponent<Base>();
        if (baseComponent != null && baseComponent.faction != faction)
        {
            rb.velocity = Vector2.zero;
            isShootingBase = true;
            StartCoroutine(AttackBase(baseComponent));
        }
    }

    IEnumerator AttackBase(Base enemyBase)
    {
        while (enemyBase != null && health > 0)
        {
            if (Time.time >= nextFireTime)
            {
                nextFireTime = Time.time + 1f / fireRate;
                enemyBase.TakeDamage(damage);
            }
            yield return null;
        }
    }

    public void Initialize(string tankName, float health, float range, float damage, float speed, string faction, int goldCost, int metalCost)
    {
        this.tankName = tankName;
        this.health = health;
        this.maxHealth = health;
        this.range = range;
        this.damage = damage;
        this.speed = speed;
        this.faction = faction;
        this.goldCost = goldCost;
        this.metalCost = metalCost;
    }
}
