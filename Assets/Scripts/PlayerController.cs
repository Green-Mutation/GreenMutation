using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //iniciando rigidbody e animator
    private Rigidbody2D playerRigidBody;
    public float playerSpeed = 1f;


    private bool isWalking;
    private Animator playerAnimator;



    
    void Start()
    {
        // obtem e inicializa as propriedades do Rigidbody2D
        playerRigidBody = GetComponent<Rigidbody2D>();

        // obtem e inicializa as propriedades do Animator
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
