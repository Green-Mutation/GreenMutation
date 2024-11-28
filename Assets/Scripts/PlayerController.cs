using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //iniciando rigidbody e animator
    private Rigidbody2D playerRigidBody;
    public float playerSpeed = 1f;

    public Vector2 playerDirection;

    private bool isWalk;
    private Animator playerAnimator;

    private bool playerFaceRight = true;





    void Start()
    {
        // obtem e inicializa as propriedades do Rigidbody2D
        playerRigidBody = GetComponent<Rigidbody2D>();

        // obtem e inicializa as propriedades do Animator
        playerAnimator = GetComponent<Animator>();
    }

    
    void Update()
    {
        PlayerMove();
        UpdadeAnimator();
        playerRigidBody.MovePosition(playerRigidBody.position + playerSpeed * Time.fixedDeltaTime * playerDirection);

    }


    private void FixedUpdate()
    {
        // verificar se o player esta em movimento
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
        // Pega a entrada do jogador, e cria um Vector2 para usar no playerDirection
        playerDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));


        //se o player vai para a Esquerda e esta olhando para a DIREITA
        if (playerDirection.x < 0 && playerFaceRight)
        {
            Flip();
        }

        //se o player vai pra a Direita e esta olhando para a ESQUERDA
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
        // definir o valor do parametro do animator, igual a propriedade isWalking
        // parametro entre aspas "IsWalking" é um parametro do Animator - Unity
        // isWalking sem aspas é a variavel criada no codigo acima
        playerAnimator.SetBool("IsWalk", isWalk);
    }

}
