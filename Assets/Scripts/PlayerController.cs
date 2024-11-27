using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
