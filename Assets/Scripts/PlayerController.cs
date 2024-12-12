using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    //Variaveis da po��o 
    public Text pocaoTxt;
    private int pocao;

    //iniciando rigidbody e animator
    private Rigidbody2D playerRigidBody;
    private Animator animator;
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

    public GameObject pocaoUI;

    void Start()
    {
        //inicializa as propriedades do Rigidbody2D e Animator
        playerRigidBody = GetComponent<Rigidbody2D>();
                
        playerAnimator = GetComponent<Animator>();

        currentHealth = maxHealth;

        ////recebe o 0 no inicio
        pocao = 0;
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



        if (SceneManager.GetActiveScene().name == "FaseBonusBunker")
        {
            //transforma pocao em texto no UI
            pocaoTxt.text = pocao.ToString();
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
        // Acessa a anima��o do Cross
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

    public void TakeDamage(int damage)
    {
        if (!isDead)
        {
            currentHealth -= damage;
            playerAnimator.SetTrigger("HitDamage");
            FindAnyObjectByType<UIManager>().UpdatePlayerHealth(currentHealth);

            if (currentHealth <= 0)
            {
                isDead = true;

                ZeroSpeed();

                animator.SetTrigger("isDead");
            }
        }
    }

    void ZeroSpeed()
    {
        currentSpeed = 0;
    }

    void ResetSpeed()
    {
        currentSpeed = playerSpeed;
    }

    public void DisablePlayer()
    {
        this.gameObject.SetActive(false);
    }

    //colisao da po��o
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // usa a tag da unity
        if (collision.gameObject.CompareTag("Pocao"))
        {
            //recebe ela mesma
            pocao = pocao + 1;
            //destroi objeto
            Destroy(collision.gameObject);

            pocaoUI.SetActive(true);
        }
    }
}
