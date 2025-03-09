using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Variables related to player character movement
    public InputAction MoveAction;
    Rigidbody2D rigidbody2d;
    Vector2 move;
    public float speed = 3.0f;

    // Variables related to the health system
    public int maxHealth = 5;
    int currentHealth;
    public int health { get { return currentHealth; }}

    // Variables related to temporary invincibility
    public float timeInvincible = 2.0f;
    bool isInvincible;
    float damageCooldown;

    // Start is called before the first frame update
    void Start()
    {
        MoveAction.Enable();
        rigidbody2d = GetComponent<Rigidbody2D>();
        if (rigidbody2d == null)
        {
            Debug.LogError("Rigidbody2D component missing on " + gameObject.name);
        }

        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        move = MoveAction.ReadValue<Vector2>();

        if (isInvincible)
        {
            damageCooldown -= Time.deltaTime;
            if (damageCooldown < 0)
                isInvincible = false;
        }
    }

    // FixedUpdate has the same call rate as the physics system
    void FixedUpdate()
    {
        if (rigidbody2d != null)
        {
            Vector2 position = (Vector2)rigidbody2d.position + move * speed * Time.deltaTime;
            rigidbody2d.MovePosition(position);
        }
    }

    // Method to change the player's health
    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
                return;

            isInvincible = true;
            damageCooldown = timeInvincible;
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log("Health changed by " + amount + ". Current health: " + currentHealth);

        if (UIhandle.instance != null)
        {
            UIhandle.instance.SetHealthValue(currentHealth / (float)maxHealth);
        }
        else
        {
            Debug.LogError("UIhandle instance is null. Ensure UIhandle is set up in the scene.");
        }
    }

    // Disable the MoveAction when the object is destroyed or disabled
    void OnDestroy()
    {
        MoveAction.Disable();
    }

    void OnDisable()
    {
        MoveAction.Disable();
    }
}