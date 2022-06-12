<<<<<<< HEAD
using System.Collections;
=======
>>>>>>> dc2ab68383fe2c67c2180513288696cac2b4031d
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Velocidad a la cual el personaje se movera
    public float speed = 3f;

    private Rigidbody2D playerRb;
    private Vector2 moveInput;
<<<<<<< HEAD
    private Animator playerAnimator;
=======
>>>>>>> dc2ab68383fe2c67c2180513288696cac2b4031d

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
<<<<<<< HEAD
        playerAnimator = GetComponent<Animator>();
=======
>>>>>>> dc2ab68383fe2c67c2180513288696cac2b4031d
    }

    // Llama los inputs cada frame
    void Update()
<<<<<<< HEAD
    {
=======
    {   
>>>>>>> dc2ab68383fe2c67c2180513288696cac2b4031d
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(moveX, moveY).normalized;

        // Si se mantiene el shift izquierdo el personaje aumetara su velocidad
        if (Input.GetKey("left shift"))
<<<<<<< HEAD
        {
            speed = 10f;
        }else
        {
=======
        {
            speed = 10f;
        }else
        {
>>>>>>> dc2ab68383fe2c67c2180513288696cac2b4031d
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

    // Genera el movimiento del personaje
    private void FixedUpdate()
    {
        playerRb.MovePosition(playerRb.position + moveInput * speed * Time.fixedDeltaTime);
    }
}
