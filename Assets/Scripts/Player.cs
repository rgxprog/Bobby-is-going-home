using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //-------------------------------------------

    public static Player instance;
    public LayerMask groundLayer;

    private readonly float speed = 2f;
    private int startXLeft, startXRight;
    private Rigidbody2D rb;
    private float jumpForce;
    private AudioManager audioManager;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool inHurt;

    //-------------------------------------------

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        rb = GetComponent<Rigidbody2D>();
        jumpForce = 5.5f;
        inHurt = false;

        audioManager = FindObjectOfType<AudioManager>();
        animator = transform.Find("Bobby").GetComponent<Animator>();
        spriteRenderer = transform.Find("Bobby").GetComponent<SpriteRenderer>();
    }

    //-------------------------------------------

    private void Start()
    {
        startXLeft = -7;
        startXRight = 7;
    }

    //-------------------------------------------

    private void Update()
    {
        if (GameManager.instance.state == GameManager.GameState.InGame && !inHurt)
            Move();
        animator.SetBool("OnGround", OnGround());
    }

    //-------------------------------------------

    private void Move()
    {
        float currSpeed = 0;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += speed * Time.deltaTime * Vector3.right;
            spriteRenderer.flipX = false;
            currSpeed = speed;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += -speed * Time.deltaTime * Vector3.right;
            spriteRenderer.flipX = true;
            currSpeed = -speed;
        }

        animator.SetBool("Walk", currSpeed != 0);

        if (OnGround() && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            audioManager.Play("Jump");
        }
    }

    //-------------------------------------------

    public void ResetPosX(char side)
    {
        float newX = side == 'L' ? startXLeft : startXRight;
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    //-------------------------------------------

    private bool OnGround()
    {
        RaycastHit2D hit2D = Physics2D.BoxCast(
            transform.position,
            Vector2.one * 0.55f,
            0f,
            Vector2.down,
            0.25f,
            groundLayer
            );

        return hit2D.collider != null;
    }

    //-------------------------------------------

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (inHurt)
            return;

        if (collision.gameObject.layer == LayerMask.NameToLayer("DeathZone") || collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            StartCoroutine(ResetByHurt());
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Home"))
        {
            animator.SetBool("Walk", false);
            StartCoroutine(GameManager.instance.SetInHomeState());
        }
    }

    //-------------------------------------------

    private IEnumerator ResetByHurt()
    {
        audioManager.Play("Lose");
        inHurt = true;
        animator.SetBool("Hurt", inHurt);

        yield return new WaitForSeconds(audioManager.GetClipLength("Lose") + 0.5f);
        inHurt = false;
        animator.SetBool("Hurt", inHurt);
        ResetPosX('L');

        ResetGansitosIfExists();
    }

    //-------------------------------------------

    // Si existen gansitos en la escena, resetear su posición
    private void ResetGansitosIfExists()
    {
        GansitosLogic gansitosLogic = FindObjectOfType<GansitosLogic>();
        if (gansitosLogic != null)
            gansitosLogic.ResetPositions();
    }

    //-------------------------------------------
}
