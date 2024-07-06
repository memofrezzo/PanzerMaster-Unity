using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Base : MonoBehaviour
{
    public float health;
    public string faction;
    public GameObject explosionPrefab;

    public void TakeDamage(float damageAmount)
    {
        // L�gica existente de da�o
        health -= damageAmount;
        if (health <= 0)
        {
            DestroyBase();
        }
    }

    public void DestroyBase()
    {
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}


