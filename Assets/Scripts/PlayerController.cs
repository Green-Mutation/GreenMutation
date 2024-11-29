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
        //inicializa as propriedades do Rigidbody2D e Animator
        playerRigidBody = GetComponent<Rigidbody2D>();
                
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

    }
}
