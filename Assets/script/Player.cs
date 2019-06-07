using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;//velocidade do personagem
    public int jumpForce;//foça do pulo
    public int health;//vidas do player
    public Transform groundCheck; //verificar toque no chão
    public float attackRate;//taxa de ataque
    public Transform spawnAttack;
    public GameObject attackPreFab;//atacar
    public GameObject crown;//morte

    private bool invunarable = false; //não está vulneravel
    private bool grounded = false;//verificaar se estar tocando o chão
    private bool jumping = false;//verificar se está pulando
    private bool facingRight = true;//verificar o lado que o personagem está olhando
    private int qPulo = 2;
    private float nextAttack = 0f;//calcular o tempo par ao proximo ataque

    private SpriteRenderer sprite; //criar variaval para controlar o Sprite Renderer
    private Rigidbody2D rb2d; //criar variaval para controlar o Rigid Body 2D
    private Animator anim; //criar variaval para controlar a aAnimação
    

    private camera cameraScript;

    public AudioClip fxHurt;
    public AudioClip fxJump;
    public AudioClip fxAttack;
    public AudioClip fxCoin;

    private int direction = 0;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>(); // definir qual o sprite que será modificado, nesse caso é o do proprio personagem
        //sprite = GameObject.Find("Snake").GetComponent<SpriteRenderer>(); esse seria o caso de usar o sprite de outro personagem
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        cameraScript = GameObject.Find("Main Camera").GetComponent<camera>();

    }

    void Update()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1<< LayerMask.NameToLayer("Ground"));
        if (Input.GetButtonDown("Jump") && grounded)
        {
            jumping = true;
        }
        SetAnimations();

        if (Input.GetButton("Fire1") && grounded && Time.time > nextAttack)
        {
            Attack();
        }
    }

   void FixedUpdate()
    {
        float move = Input.GetAxis("Horizontal");
        rb2d.velocity = new Vector2(move * speed, rb2d.velocity.y);
        if((move < 0f && facingRight) || (move > 0f && !facingRight)){
            Flip();
        }
        if (jumping)
        {
            rb2d.AddForce(new Vector2(0f, jumpForce));
            jumping = false;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    void SetAnimations()
    {
        anim.SetFloat("VelY", rb2d.velocity.y);
        anim.SetBool("JumpFall", !grounded);
        anim.SetBool("Walk", rb2d.velocity.x != 0f && grounded);
    }

    void Attack()
    {
        anim.SetTrigger("Attack");
        nextAttack = Time.time + attackRate;
        GameObject cloneAttack = Instantiate(attackPreFab, spawnAttack.position, spawnAttack.rotation);
        if (!facingRight)
        {
            cloneAttack.transform.eulerAngles = new Vector3(180, 0, 180);
        }
    }
}

