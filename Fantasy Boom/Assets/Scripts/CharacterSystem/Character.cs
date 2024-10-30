using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Character : NetworkBehaviour
{
    [SerializeField] private CharacterSO characterConfig;
    [SerializeField] private ProgressBar healthBar;
    
    public List<AbilitySO> abilities;

    public float Health { get; private set; }
    public float Mana {  get; private set; }

    private float maxHealth;

    private void Start()
    {
        InitializeCharacter();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            abilities[0].Use(this);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            abilities[1].Use(this);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            abilities[2].Use(this);
        }
    }

    private void InitializeCharacter()
    {
        Health = characterConfig.health;
        Mana = characterConfig.mana;

        maxHealth = Health;

        abilities = characterConfig.abilities;
    }

    public void OnTakeDamage(float damage)
    {
        Health -= damage;

        UpdateHealthBar();

        if (Health <= 0)
        {
            OnDied();
        }
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.SetProgress(Health / maxHealth, 3);
        }
    }

    private void OnDied()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position + transform.forward * 2 * 1.25f, 2);
    }
}
