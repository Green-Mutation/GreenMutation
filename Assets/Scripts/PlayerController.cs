using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //iniciando rigidbody e animator
    private Rigidbody2D playerRigidBody;
    public float playerSpeed = 1f;
    public float currentSpeed;

    public Vector2 playerDirection;

    private bool isWalk;
    private Animator playerAnimator;

    private bool playerFaceRight = true;
    private int punchCount;
    private float timeCross = 0.75f;
    private bool comboControl;
    private bool isDead;
    public int maxHealth = 10;
    public int currentHealth;
    public Sprite playerImage;

    void Start()
    {
        //inicializa as propriedades do Rigidbody2D e Animator
        playerRigidBody = GetComponent<Rigidbody2D>();
                
        playerAnimator = GetComponent<Animator>();

        currentHealth = maxHealth;
    }

    
    void Update()
    {
        PlayerMove();
        UpdadeAnimator();
        playerRigidBody.MovePosition(playerRigidBody.position + playerSpeed * Time.fixedDeltaTime * playerDirection);


        // jab ataque
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isWalk == false)
            {
                if (punchCount < 2)
                {
                    PlayerJab();
                    punchCount++;
                    if (!comboControl)
                    {
                        StartCoroutine(CrossController());
                    }
                }
                else if (punchCount >= 2)
                {
                    PlayerCross();
                    punchCount = 0;
                }
            }
        }
    }


    private void FixedUpdate()
    {
        //movimento player
        if (playerDirection.x != 0 || playerDirection.y != 0)
        {
            isWalk = true;
        }
        else
        {
            isWalk = false;
        }

        playerRigidBody.MovePosition(playerRigidBody.position + playerSpeed * Time.fixedDeltaTime * playerDirection);

    }



    void PlayerMove()
    {        
        playerDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));


        //flip
        if (playerDirection.x < 0 && playerFaceRight)
        {
            Flip();
        }        
        else if (playerDirection.x > 0 && !playerFaceRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        //inverter o valor da varialver playerFaceRight
        playerFaceRight = !playerFaceRight;

        //girar o sprite do player em 180g
        transform.Rotate(0, 180, 0);
    }

    void UpdadeAnimator()
    {
        // definir o valor do parametro do animator, igual a propriedade isWalk        
        playerAnimator.SetBool("IsWalk", isWalk);
    }

    void PlayerJab ()
    {
        playerAnimator.SetTrigger("IsJab");
    }

    void PlayerCross()
    {
        // Acessa a animação do Cross
        // Ativa o gatilho de ataque Cross
        playerAnimator.SetTrigger("isCross");
    }

    IEnumerator CrossController()
    {
        comboControl = true;
        yield return new WaitForSeconds(timeCross);
        punchCount = 0;
        comboControl = false;
    }

    void ZeroSpeed()
    {
        currentSpeed = 0;
    }

    void ResetSpeed()
    {
        currentSpeed = playerSpeed;
    }

    public void TakeDamage(int damage)
    {
        if (!isDead)
        {
            currentHealth -= damage;
            playerAnimator.SetTrigger("HitDamage");
            FindAnyObjectByType<UIManager>().UpdatePlayerHealth(currentHealth);
        }
    }
}
