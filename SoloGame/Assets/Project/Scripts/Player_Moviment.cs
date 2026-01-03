using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Moviment : MonoBehaviour
{
    [Header("Configuracao do Movimento")]
    [SerializeField] float moveSpeed = 5.0f;
    [SerializeField] float jumpForce = 3.0f;
    private float horizontalMoviment;


    [Header("Variaveis do Player")]
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private bool isOnFloor;

    void Awake()
    {
        // pega as duas componentes do objeto ao ser instanciado
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        // altera o valor da velocidade linear do objeto usando new Vector2 e o horizontalMoviment
        rb.linearVelocity = new Vector2(horizontalMoviment * moveSpeed, rb.linearVelocityY);
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        horizontalMoviment = context.ReadValue<Vector2>().x;

        // flip da direcao do sprite
        if (horizontalMoviment < 0)
            spriteRenderer.flipX = true;
        else if (horizontalMoviment > 0)
            spriteRenderer.flipX = false;
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && isOnFloor)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isOnFloor = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MainGround")
            isOnFloor = true;
    }
}