using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Velocidad a la cual el personaje se movera
    public float speed = 3f;

    private Rigidbody2D playerRb;
    private Vector2 moveInput;
    private Animator playerAnimator;

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    // Llama los inputs cada frame
    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(moveX, moveY).normalized;

        // Si se mantiene el shift izquierdo el personaje aumetara su velocidad
        if (Input.GetKey("left shift"))
        {
            speed = 10f;
        }else
        {
            speed = 3f;
        }

        playerAnimator.SetFloat("Horizontal", moveX);
        playerAnimator.SetFloat("Vertical", moveY);
        playerAnimator.SetFloat("Speed", moveInput.sqrMagnitude);
    }

    // Genera el movimiento del personaje
    private void FixedUpdate()
    {
        playerRb.MovePosition(playerRb.position + moveInput * speed * Time.fixedDeltaTime);
    }
}
