using UnityEngine;

public class BossEnemyStaticController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    // VAriavel que indica se o inimigo esta vivo
    public bool isDead;

    // Variaveis para controlar o lado que o inimigo esta virado
    private bool facingRight;
    public bool previousDirectionRight;

    // Variavel para armazenar posição do Player
    // target - alvo
    private Transform target;


    // Variaveis para mecânica de ataque
    private float attackRate = 1f;
    private float nextAttack;

    // Variaveis para mecânica de dano
    public int maxHealth;
    public int currentHealth;

    public float staggerTime = 0.5f;
    private float damageTimer;
    private bool isTakingDamage;



    void Start()
    {
        // Inicializa os componentes do Rigibody e Animator
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Buscar o Player e armazenar sua posição
        target = FindAnyObjectByType<PlayerController>().transform;

    }

    // Update is called once per frame
    void Update()
    {
        // Verificar se o Player esta para a Direita ou para a Esquerda
        // E determinar o lado que o inimigo ficara virado
        if (target.position.x < this.transform.position.x)
        {
            facingRight = false;
        }
        else
        {
            facingRight = true;
        }

        //rotação do inimigo
        if (facingRight && !previousDirectionRight)
        {
            this.transform.Rotate(0, 180, 0);
            previousDirectionRight = true;
        }

        
        if (!facingRight && previousDirectionRight)
        {
            this.transform.Rotate(0, -180, 0);
            previousDirectionRight = false;
        }


        // Gerenciar o tempo de stagger 
        if (isTakingDamage && !isDead)
        {
            damageTimer += Time.deltaTime;
                       
        }

        

    }


    private void FixedUpdate()
    {
        
        // Variavel para armazenar a distância entre o Inimigo e o Player
        Vector3 targetDistance = target.position - this.transform.position;

        // ATAQUE
        // Se estiver perto do Player e o timer do jogo for maior que o valor de nextAttack 
        if (Mathf.Abs(targetDistance.x) < 0.2f && Mathf.Abs(targetDistance.y) < 0.05f && Time.time > nextAttack)
        {
            // Esse comando executa a naimação de ataque do inimigo
            animator.SetTrigger("Attack");

            // Pega o tempo atual e soma o attackRate, para definir a partir de quando o inimigo poderá atacar novamente
            nextAttack = Time.time + attackRate;
        }
    }


    public void TakeDamage(int damage)
    {
        if (!isDead)
        {
            isTakingDamage = true;

            currentHealth -= damage;

            animator.SetTrigger("hitDamage");
        }
    }

    



}
