using System.Collections;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    int attackParam = Animator.StringToHash("attack");
    int isAttackParam = Animator.StringToHash("isAttack");
    int isGroundedParam = Animator.StringToHash("isGrounded");
    int speedYParam = Animator.StringToHash("speedY");
    int WalkingParam = Animator.StringToHash("Walking");
    int JumpingParam = Animator.StringToHash("Jumping");

    private GameController _GameController;
    private Rigidbody2D playerRb;
    private Animator playerAnimator;
    private SpriteRenderer playerSr;
    private bool isGrounded;
    private bool isWalking;
    private bool isJumping;
    private bool isAttack;
    public bool isLookLeft;
    //public Transform groundCheck;
    public Transform hitBox;
    public float jumpPower;
    public float velocidadeMovimento;
    public GameObject hitBoxPrefab;
    public Color hitColor;
    public Color noColor;
    public Vector3 offset = Vector2.zero;
    public float radious = 12.0f;
    public LayerMask mask;

    // Despertado é chamado quando a instância do script for carregada
    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerSr = GetComponent<SpriteRenderer>();
        playerAnimator = GetComponent<Animator>();
        _GameController = FindObjectOfType(typeof(GameController)) as GameController;
        _GameController.playerTransform = this.transform;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        isJumping = false;
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        float horizontal = Input.GetAxis("Horizontal");

        if (horizontal != 0)
        {
            isWalking = true;
            if (horizontal > 0 && isLookLeft == true)
            {
                flip();
            }
            else if (horizontal < 0 && isLookLeft == false)
            {
                flip();
            }
        }
        else
        {
            isWalking = false;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
            _GameController.playSFX(_GameController.sfxJump, 1);
            playerRb.AddForce(new Vector2(0, jumpPower));
            //Debug.Log("Pulo pressionado: " + isGrounded);
            //isGrounded = false;
            //Debug.Log("Pulo pressionado depois: " + isGrounded);
        }

        //if (Input.GetKeyDown("space") && isGrounded == true)
        //{
        //    _GameController.playSFX(_GameController.sfxJump, 1);
        //    playerRb.AddForce(new Vector2(0, jumpPower));
        //}

        if (Input.GetButtonDown("Fire1") && isAttack == false)
        {
            isAttack = true;
            if (isGrounded)
            {
                playerRb.velocity = new Vector2(0, 0);
            }
            _GameController.playSFX(_GameController.sfxAttack, 1);
            playerAnimator.SetTrigger(attackParam);
        }

        if (isAttack == false)
        {
            playerRb.velocity = new Vector2(horizontal * velocidadeMovimento, playerRb.velocity.y);
        }

        playerAnimator.SetBool(isGroundedParam, isGrounded);
        playerAnimator.SetFloat(speedYParam, playerRb.velocity.y);
        playerAnimator.SetBool(WalkingParam, isWalking);
        playerAnimator.SetBool(JumpingParam, isJumping);
        playerAnimator.SetBool(isAttackParam, isAttack);
    }

    // Essa função será chamada a cada quadro da taxa de quadro fixada, se MonoBehaviour estiver habilitado
    private void FixedUpdate()
    {
        //isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.02f);

        isGrounded = Physics2D.OverlapCircle(this.transform.position + offset,
            radious, mask);
    }

    void flip()
    {
        isLookLeft = !isLookLeft;
        float x = transform.localScale.x;
        x *= -1;
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }

    void onAttackEnded()
    {
        isAttack = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Enemy":
                _GameController.getHit();
                if (_GameController.life > 0)
                {
                    StartCoroutine("hitController");
                }
                else
                {
                    GameManager.GetInstance().SetDeath(true);
                }
                break;
            case "Coins":
                _GameController.getCoin();
                Destroy(collision.gameObject);
                break;
        }
    }

    public void onHitBox()
    {
        GameObject hit = Instantiate(hitBoxPrefab, hitBox.position, hitBox.localRotation);
        Destroy(hit.gameObject, 0.03f);
    }

    void footStep()
    {
        _GameController.playSFX(_GameController.sfxStep[Random.Range(0, _GameController.sfxStep.Length)], 1f);
    }

    void onHited()
    {
        _GameController.playSFX(_GameController.sfxHited, 1f);
    }

    IEnumerator hitController()
    {
        playerAnimator.SetTrigger("Hit");
        isAttack = false;
        this.gameObject.layer = LayerMask.NameToLayer("Invencible");
        playerSr.color = hitColor;
        yield return new WaitForSeconds(0.4f);
        playerSr.color = noColor;
        for (int i = 0; i < 5; i++)
        {
            playerSr.enabled = false;
            yield return new WaitForSeconds(0.2f);
            playerSr.enabled = true;
            yield return new WaitForSeconds(0.2f);
        }
        this.gameObject.layer = LayerMask.NameToLayer("Player");
        playerSr.color = Color.white;
    }
}
