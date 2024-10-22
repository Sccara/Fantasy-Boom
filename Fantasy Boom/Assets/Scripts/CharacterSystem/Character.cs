using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private ProgressBar healthBar;

    [SerializeField] private float health;
    [SerializeField] private float mana;

    private float maxHealth;

    private void Awake()
    {
        maxHealth = health;
    }

    public void OnTakeDamage(float damage)
    {
        health -= damage;

        healthBar.SetProgress(health / maxHealth, 3);

        if (health <= 0)
        {
            OnDied();
        }
    }

    public virtual void Attack()
    {

    }

    private void OnDied()
    {
        Destroy(gameObject);
    }

    public void SetupHealthBar(Camera camera)
    {
        // Setup face camera logic
    }
}
