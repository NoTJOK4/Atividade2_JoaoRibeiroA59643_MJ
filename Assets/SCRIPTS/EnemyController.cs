using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Public variables
    public float speed;
    public bool vertical;
    public float changeTime = 3.0f;

    // Private variables
    private Rigidbody2D rigidbody2d;
    private Animator animator;
    private float timer;
    private int direction = 1;
    private bool broken = true;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        timer = changeTime;
    }

    // Update is called every frame
    void Update()
    {
        if (!broken)
        {
            return; // Se o inimigo estiver "quebrado", não faça nada
        }

        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction; // Inverte a direção
            timer = changeTime;
        }
    }

    // FixedUpdate has the same call rate as the physics system
    void FixedUpdate()
    {
        if (!broken)
        {
            return; // Se o inimigo estiver "quebrado", não se mova
        }

        Vector2 position = rigidbody2d.position;

        if (vertical)
        {
            position.y = position.y + speed * direction * Time.deltaTime;
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        }
        else
        {
            position.x = position.x + speed * direction * Time.deltaTime;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }

        rigidbody2d.MovePosition(position);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            player.ChangeHealth(-1); // Causa dano ao jogador
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Destruir o inimigo ao colidir com algo (opcional)
        // Destroy(gameObject);
    }

    // Método para "consertar" o inimigo (destruí-lo)
    public void Fix()
    {
        broken = false;
        rigidbody2d.simulated = false; // Desativa a simulação de física
        animator.SetTrigger("Fixed"); // Reproduz a animação de "consertado"

        // Destruir o inimigo após um pequeno atraso (para permitir que a animação seja reproduzida)
        StartCoroutine(DestroyAfterDelay(0.5f)); // Ajuste o atraso conforme necessário
    }

    // Corrotina para destruir o inimigo após um atraso
    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Aguarda o tempo especificado
        Destroy(gameObject); // Destroi o inimigo
    }
}