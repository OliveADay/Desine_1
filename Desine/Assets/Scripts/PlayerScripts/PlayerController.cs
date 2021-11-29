using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    public float speed;
    public float jumpForce;
    float horizontalInput;
    Rigidbody2D rb;
    public bool facingRight = true;
    bool isGroundedJ;
    [HideInInspector]
    public bool isGroundedW;
    public Transform groundCheck;
    public float checkRadius;
    public float rDivider;
    public LayerMask whatIsGround;
    [HideInInspector]
    public int extraJumps;
    public int extraJumpsValue;
    public float distance;
    float verticalInput;
    public LayerMask whatIsLatter;
    bool isClimbing = false;
    public float climbingSpeed;
    bool isJumping;
    public AudioManager audioManager;
    public Sound[] steps;
    bool isDone = true;
    bool iswalking = false;
    [HideInInspector]
    public Vector3 velocityl;
    float jumpTimer = 1;
    bool onCeiling = false;
    public Transform CeilingPos;
    [HideInInspector]
    public bool moving;
    GameObject p;
    public ParticleSystem walkPR;
    Vector3 lastLoc;
    GameObject pL;
    public float dustStart;
    float dustTime;
    static GameObject gr;
    public GameObject land;
    public GameObject D;
    [HideInInspector]
    public bool horizMove = true;
    public Sound jump;
    private void OnEnable()
    {
        SceneManager.sceneLoaded += GetBackground;
    }
    void Start()
    {
        gr = GameObject.FindGameObjectWithTag("Ground");
        print(gr);
        extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();
        lastLoc = transform.localScale;
    }
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        if (horizontalInput < 0 || horizontalInput > 0)
        {
            if (isGroundedW)
            {
                iswalking = true;
                animator.SetTrigger("walk");
                if (isDone)
                {
                    StartCoroutine(walkSound(iswalking, steps, Random.Range(0.7f, 0.3f), audioManager));
                }
            }
            else
            {
                iswalking = false;
            }
        }
        else
        {
            iswalking = false;
        }
        if (iswalking)
        {
            if(p == null)
            {

            }
            else
            {
                if (!p.activeSelf)
                {
                    p = Instantiate(walkPR.gameObject, transform);
                }
                if (transform.localScale != lastLoc)
                {
                    if (p.activeSelf)
                    {
                        p.transform.parent = gr.transform;
                        p.transform.parent = null;
                        p.transform.localScale = new Vector2(-p.transform.localScale.x, p.transform.localScale.y);
                        if(pL != null)
                        {
                            pL.SetActive(false);
                            pL = null;
                        }
                        pL = p;
                        p = Instantiate(walkPR.gameObject, transform);
                        dustTime = dustStart;
                    }
                }
            }
        }
        else
        {
            if(p != null)
            {
                if (p.activeSelf)
                {
                    p.SetActive(false);
                }
            }
        }
        if(dustTime > 0)
        {
            dustTime -= Time.deltaTime;
        }
        else
        {
            if(pL != null)
            {
                pL.SetActive(false);
                pL = null;
            }
        }
        SideToSideMovement();
        if (isGroundedJ)
        {
            extraJumps = extraJumpsValue;
            jumpTimer = 0.3f;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isJumping = true;
            }
        }
        else
        {
            if(extraJumps > 0)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    jumpTimer = 0.3f;
                    isJumping = true;
                    if (audioManager != null)
                    {
                        if (jump != null)
                        {
                            audioManager.Play(jump);
                        }
                    }
                    LandEf();
                    extraJumps -= 1;
                }
            }
        }

        if (!isJumping)
        {
            rb.gravityScale = 110;
            velocityl.y = 0;
        }
        else
        {
            rb.gravityScale = 170;
            velocityl.y = jumpForce;
            jumpTimer -= Time.deltaTime;
        }
        if(jumpTimer <= 0 || onCeiling)
        {
            isJumping = false;
        }
        if (isClimbing)
        {
            animator.SetTrigger("Climb");
        }
        Fall(isGroundedJ);
        if (velocityl != new Vector3(0, 0, 0))
        {
            moving = true;
        }
        else
        {
            moving = false;
        }
        lastLoc = transform.localScale;
    }
    void FixedUpdate()
    {
        isGroundedJ = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        isGroundedW = Physics2D.OverlapCircle(groundCheck.position, checkRadius / rDivider, whatIsGround);
        onCeiling = Physics2D.OverlapCircle(CeilingPos.position, checkRadius, whatIsGround);
        if (facingRight == false && horizontalInput > 0)
        {
            Flip();
        }
        else if (facingRight == true && horizontalInput < 0)
        {
            Flip();
        }
        if (horizMove)
        {
            rb.MovePosition(transform.position + velocityl);
        }
        else
        {
            rb.MovePosition(new Vector2(transform.position.x, transform.position.y + velocityl.y));
        }
    }
    void Fall(bool isfalling)
    {
        if (!isfalling)
        {
            if (!animator.GetBool("Falling"))
            {
                animator.SetBool("Falling", true);
            }                             
        }
        else
        {
            if (animator.GetBool("Falling"))
            {
                animator.SetBool("Falling", false);
            }                                   
        }
    }
    IEnumerator walkSound(bool value, Sound[] ss, float time, AudioManager audio)
    {
        isDone = false;
        yield return new WaitForSeconds(time);
        Sound s = audio.Random(ss);
        if (value)
        {
            audio.Play(s);
        }
        isDone = true;
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
    void SideToSideMovement()
    {
        if (horizMove)
        {
            velocityl.x = horizontalInput * speed;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius/rDivider);
        Gizmos.DrawWireSphere(CeilingPos.position, checkRadius);
    }
    public IEnumerator SpawnParticleOrig(GameObject g, Vector2 origP)
    {
        g.SetActive(true);
        g.transform.parent = null;
        ParticleSystem p = g.GetComponent<ParticleSystem>();
        if(p != null)
        {
            yield return new WaitForSeconds(p.main.duration + 0.2f);
        }
        g.SetActive(false);
        g.transform.parent = transform;
        g.transform.localPosition = origP;
    }
    public void LandEf()
    {
        StartCoroutine(SpawnParticleOrig(land, new Vector2(0, -1)));
    }
    public void setActive(GameObject g, bool value)
    {
        g.SetActive(value);
    }
    public void DeathP()
    {
        setActive(D, true);
    }
    void GetBackground(Scene scene, LoadSceneMode scenemode)
    {
        gr = GameObject.FindGameObjectWithTag("Ground");
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= GetBackground;
    }
}
