using UnityEngine;

public class EnemyFazendeiro : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    // VAriavel que indica se o inimigo esta vivo
    public bool isDeath;

    // Variaveis para controlar o lado que o inimigo esta virado
    private bool facingRight;
    public bool previousDirectionRight;

    // Variavel para armazenar posi��o do Player
    // target - alvo
    private Transform target;

    // Variaveis para movimenta��o do inimigo
    private float enemySpeed = 0.3f;
    private float currentSpeed;
    private bool isWalking;
    private float horizontalForce;
    public float verticalForce;

    private float walktimer;

    // Variaveis para mecanica de dano
    public int maxHealth;
    public int currentHealth;
    public Sprite enemyImage;

    public float staggerTime = 0.5f;
    private float damageTime;
    public bool isTalkingDamage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Inicializa os componentes do Rigibody e Animator
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Buscar o Player e armazenar sua posi��o
        target = FindAnyObjectByType<PlayerController>().transform;

        // Inicializar a velocidade do inimigo
        currentSpeed = enemySpeed;

        currentHealth = maxHealth;
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

        // Se facingRight for TRUE, vamos virar o inimigo em 180 graus no eixo Y,
        // Sen�o vamos virar o inimigo para a esquerda
        // Se o Player est� � direita e a posi��o anterior N�O era direita (estava olhando para a esquerda)
        if (facingRight && !previousDirectionRight)
        {
            this.transform.Rotate(0, 180, 0);
            previousDirectionRight = true;
        }

        // Se o player n�o est� a direita e a posi��o anterior ERA direita
        if (!facingRight && previousDirectionRight)
        {
            this.transform.Rotate(0, -180, 0);
            previousDirectionRight = false;
        }

        walktimer += Time.deltaTime;

        // Gerenciar  a anima��o do inimigo
        if (horizontalForce == 0 && verticalForce == 0)
        {
            isWalking = false;
        }
        else
        {

            isWalking = true;

        }

        // Atualiza o animator
        UpdateAnimator();
    }

    private void FixedUpdate()
    {
        if (!isDeath)
        {
            
        // MOVIMENTA��O

        // Variavel para armazenar a dist�ncia entre o Inimigo e o Player
        Vector3 targetDistance = target.position - this.transform.position;

        // Determina se a for�a horizontal deve ser negativa ou positiva
        // 5 / 5 = 1
        // -5 / 5 = -1
        horizontalForce = targetDistance.x / Mathf.Abs(targetDistance.x);

            // Entre 1 e 2 segundos, ser� feita uma defini��o de dire��o vertical
            if (walktimer >= Random.Range(1f, 2f))
            {
                verticalForce = Random.Range(-1, 2);

                // Zerar o timer de movimenta��o patra andar verticalmente novamente daqui a +- 1 seg
                walktimer = 0;

            }

            // Caso estaeja perto do Player, parar  a movimenta��o
            if (Mathf.Abs(targetDistance.x) < 0.2f)
        {
            horizontalForce = 0;
        }

        // Aplica a velocidade no inimigo fazendo o movimentar
        rb.linearVelocity = new Vector2(horizontalForce * currentSpeed, verticalForce * currentSpeed);
        
        }
    }

    void UpdateAnimator()
    {
        animator.SetBool("isWalking", isWalking);
    }

    void ZeroSpeed()
    {
        currentSpeed = 0;

    }


    void ResetSpeed()
    {
        currentSpeed = enemySpeed;

    }

    public void TakeDamage(int damage)
    {
        if (!isDeath)
        {
            isTalkingDamage = true;

            currentHealth -= damage;

            animator.SetTrigger("HitDamage");

            if (currentHealth <= 0)
            {
                isDeath = true;

                ZeroSpeed();
            }
        }
    }
}
