using UnityEngine;
using TMPro;
using UnityEngine.Audio;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    float inputHorizontal;
    float jumpAmount = 6.6f;
    bool faceRight = true;
    
    Rigidbody2D rb;
    public Animator animator;

    public TextMeshProUGUI coinsCollected;
    public static int coins;

    gameState game_s;

    bool isSlippery;
    bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    public LayerMask whatIsSlippery;

    // Start is called before the first frame update
    void Start()
    {
        game_s = GameObject.FindGameObjectWithTag("Player").GetComponent<gameState>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator.SetBool("isRunning", false);
    }

    // Update is called once per frame
    void Update()
    {
        inputHorizontal = Input.GetAxis("Horizontal");
        //transform.position += new Vector3(inputHorizontal, 0, 0) * Time.deltaTime * speed;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpAmount, ForceMode2D.Impulse);
        }
        if (!isGrounded)
        {
            animator.SetBool("isJumping", true);

            if (isSlippery)
            {
                Debug.Log("Slope!");
                animator.SetBool("isSlidingOrHurt", true);
                rb.AddForce(Vector2.down, ForceMode2D.Impulse);
            }
        }
        else
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isSlidingOrHurt", false);
        }
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        isSlippery = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsSlippery);

        rb.velocity = new Vector2(inputHorizontal * speed, rb.velocity.y);

        if (inputHorizontal > 0)
        {
            animator.SetBool("isRunning", true);
            if (!faceRight)
            {
                Flip();
            }
        }
        if (inputHorizontal < 0)
        {
            animator.SetBool("isRunning", true);
            if (faceRight)
            {
                Flip();
            }
        }
        if (inputHorizontal == 0)
        {
            animator.SetBool("isRunning", false);
        }

    }



    // Flips the character by accessing transform component and
    // resetting scale's x-axis
    private void Flip()
    {
        faceRight = !faceRight;

        Vector2 theScale = transform.localScale;
        theScale.x = theScale.x * -1;
        transform.localScale = theScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            Destroy(collision.gameObject);
            SoundManager.instance.Audio.PlayOneShot(SoundManager.instance.clink);
            Debug.Log("Collided!");
            AddCoins();
        }

        if (collision.gameObject.tag == "Door")
        {
            game_s.levelCompleted();
            Debug.Log("Collided!");
        }
    }

    public void AddCoins()
    {
        coins++;
        coinsCollected.text = coins.ToString();
    }

    public int resetCoins()
    {
        coins = 0;
        //coinsCollected.text = coins.ToString();
        return coins;
    }
}
