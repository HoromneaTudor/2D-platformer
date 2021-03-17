using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
//using UnityEngine.Rendering.PostProcessing;
//using UnityEngine.Rendering.Universal;
// sa folosesc post processingu ca sa folosesc ChromaticAberration

public class Movement : MonoBehaviour
{
    private Collision coll;
    [HideInInspector]
    public Rigidbody2D rb;
    //private GameMaster gm;
    public float death_height;
    private AnimationScript anim;

    [Space]
    [Header("Stats")]
    public float speed = 10;
    public float jumpForce = 50;
    public float slideSpeed = 5;
    public float wallJumpLerp = 10;
    public float dashSpeed = 20;
    public float angle;
    public float MaxDash;
    public float MinDash;
    private float dashTime;
    public float startDashTime;
    //public float linearDrag = 4;
    public float maxSpeed = 7;
    public float accelerationAtTheMoment=0;
    float AccelRatePerSec;
    float brakePerSec;
    float forwardVelocity;
    public float timeZeroToMax = 2.5f;
    public float timeBraketoZero = 1f;
    public float jumpDelay = 0.25f;
    private float jumpTimer;
    //public bool landsqueeze;

    public float acceleration;

    [Space]
    [Header("Booleans")]
    public bool canMove;
    public bool wallGrab;
    public bool wallJumped;
    public bool wallSlide;
    public bool isDashing;
    public bool isJumping = false;
    public bool isDoubleJumping = false;
    public bool inWatter;
    //public bool isDasing = false;

    [Space]

    private bool groundTouch;
    private bool hasDashed;

    public int side = 1;

    public GameObject characterHolder;

    [Space]
    [Header("Polish")]
    public ParticleSystem dashParticle;
    public ParticleSystem jumpParticle;
    public ParticleSystem wallJumpParticle;
    public ParticleSystem slideParticle;

    public Vector2 grabRightLedge;
    public Vector2 grabLeftLedge;

    private bool exitCourtime = true;

    float x;
    float y;
    float xRaw;
    float yRaw;
    Vector2 dir;
    Vector2 dirRaw;

    // Start is called before the first frame update
    void Awake()
    {
        AccelRatePerSec = speed / timeZeroToMax;
        forwardVelocity = 0f;
        brakePerSec = -speed / timeBraketoZero;

    }

    void Start()
    {
        dashParticle.Stop();
        coll = GetComponent<Collision>();
        anim = GetComponentInChildren<AnimationScript>();
        rb = GetComponent<Rigidbody2D>();
        //gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        //dashTime = startDashTime;
        //transform.position = GameMaster.lastCheckPos;
        //GameMaster.timeInGame = 0;
        //Debug.Log(GameMaster.timeInGame);
    }

    // Update is called once per frame
    void Update()
    {
        //bool wasOnGround = onGround;
        //onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, groundLayer);
        //if (!wasOnGround && onGround)
        //{
        //    landsqueeze = true;
        //    StartCoroutine(JumpSqueeze(1.25f, 0.8f, 0.05f));    //<-- merge daca am onGround in Update
        //}

        GameMaster.timeInGame += Time.deltaTime;
        //Debug.Log(GameMaster.timeInGame);
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        xRaw = Input.GetAxisRaw("Horizontal");
        yRaw = Input.GetAxisRaw("Vertical");
        dir = new Vector2(x, y);
        dirRaw = new Vector2(xRaw, yRaw);

        //Walk(dirRaw);
        
        accelerationAtTheMoment = rb.velocity.x;
        anim.SetHorizontalMovement(xRaw, yRaw, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.R) || transform.position.y < death_height || coll.onSpykeWall)
        {
            if (exitCourtime == true)
            { StartCoroutine(Death()); }
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //mari probleme la dash (nu imi mai gaseste rigid bodiul din cauza ca a fost distrus)
        }
        if (coll.inWater)
        {
            //if (dir.y >= 0.3 || dir.y <= -0.3)
            //{
            //    rb.velocity = new Vector2(rb.velocity.x, y*10);
            //}
            //rb.gravityScale = 0;
            rb.gravityScale = 0f;
        }
        else
        {
            inWatter = false;
        }

        //if (Input.GetButton("Fire3") && coll.onWall  && canMove)
        if(Input.GetAxis("Fire3")!=0 && coll.onWall && canMove)
        {
            if (side != coll.wallSide)
                anim.Flip(side * -1);
            wallGrab = true;
            wallSlide = false;
            CheckLedgeClimb();
        }

        //if (Input.GetButtonUp("Fire3") || !coll.onWall || !canMove)
        if(Input.GetAxis("Fire3") == 0 || !coll.onWall || !canMove)
        {
            wallGrab = false;
            wallSlide = false;
            coll.ledgeDetected = false;
        }

        //reset dash if touch resetDashBloc
        if (GameMaster.resetDash)
        {
            hasDashed = false;
            GameMaster.DashChangeColor = false;
            GameMaster.resetDash = false;
        }

        if (coll.onGround && !isDashing)
        {
            wallJumped = false;
            GetComponent<BetterJumping>().enabled = true;
            isDoubleJumping = false;
            GameMaster.DashChangeColor = false;
            //StartCoroutine(JumpSqueeze(1.25f, 0.8f, 0.05f));
        }

        if (wallGrab && !isDashing)
        {
            rb.velocity = new Vector2(0, 0);//this fixes a buf in witch at the end of a wall i get pushed away
            rb.gravityScale = 0;
            if (x > .2f || x < -.2f)
                rb.velocity = new Vector2(rb.velocity.x, 0);

            float speedModifier = y > 0 ? .5f : 1;

            rb.velocity = new Vector2(rb.velocity.x, y * (maxSpeed * speedModifier)); //inainte era speed
            //CheckLedgeClimb();
        }
        else if(!coll.inWater)
        {
            rb.gravityScale = 3;
        }
        if (isDashing)
        {
            rb.gravityScale = 0; //better dash (while dashing gravity=0)
        }
        else if (!isDashing && !coll.inWater && !wallGrab)
        {
            rb.gravityScale = 3; //reset gravity
        }

        if (coll.onWall && !coll.onGround)
        {
            if (x != 0 && !wallGrab)
            {
                wallSlide = true;
                WallSlide();
            }
        }

        if (!coll.onWall || coll.onGround)
            wallSlide = false;

        if (Input.GetButtonDown("Jump"))
        {

            jumpTimer = Time.time + jumpDelay;
            anim.SetTrigger("jump");

            if (coll.onGround && jumpTimer > Time.time && !coll.onWall)
            {
                Jump(Vector2.up, false);
                isJumping = true;
                jumpTimer = 0;
            }
            if (!coll.onGround && isJumping && !coll.onWall) //doubleJumping
            {
                Jump(Vector2.up, false);
                isJumping = false;
                isDoubleJumping = true;

            }
            //if (!coll.onGround && isJumping && !coll.onWall) //doubleJumping care merge si daca am atins un wall (gen daca nu am facut un double jump cand am sarit de pe wall pot dupa)
            //{                                                 // permite recuparare daca incarc sa sar pe acelasi wall
            //    Jump(Vector2.up, false);
            //    isJumping = false;
            //    isDoubleJumping = true;

            //}
            if (coll.onWall && !coll.onGround)
            {
                WallJump(dirRaw);
                //Debug.Log("wallJumped");
            }
            if (coll.inWater && !coll.onWall)
            {
                Jump(Vector2.up, false);
            }
        }

        if (Input.GetButtonDown("Fire1") && !hasDashed)
        {
            //if (xRaw != 0 || yRaw != 0) //a trebuit sa scot pentru a putea sa fac sa mearga dash fara directie in functie de directia unde se uita
                Dash(xRaw, yRaw);
        }

        if (coll.onGround && !groundTouch)
        {
            GroundTouch();
            groundTouch = true;
            isDashing = false;
        }

        if (!coll.onGround && groundTouch)
        {
            groundTouch = false;
        }

        WallParticle(y);

        if (wallGrab || wallSlide || !canMove)
            return;

        if (x > 0.3)    //am crescut valorile inainte dashul fara imput era un pic jank
        {
            side = 1;
            anim.Flip(side);
        }
        if (x < -0.3)
        {
            side = -1;
            anim.Flip(side);
        }

        //CheckLedgeClimb();


    }

    void FixedUpdate()
    {
        //aici ar trebui puse toate functiile ce au legatura cu physics
        //o sa pun... cred

        Walk(dirRaw);
    }

    void GroundTouch()
    {
        hasDashed = false;
        isDashing = false;

        side = anim.sr.flipX ? -1 : 1;

        jumpParticle.Play();
        //landsqueeze = true;
        StartCoroutine(JumpSqueeze(1.25f, 0.8f, 0.05f));
    }

    private void Dash(float x, float y)
    {
        //Camera.main.transform.DOComplete();
        //Camera.main.transform.DOShakePosition(.2f, .5f, 14, 90, false, true);
        //FindObjectOfType<RippleEffect>().Emit(Camera.main.WorldToViewportPoint(transform.position));

        //hasDashed = true;

        //anim.SetTrigger("dash");

        //rb.velocity = Vector2.zero;
        //Vector2 dir = new Vector2(x, y);

        //rb.velocity += dir.normalized * dashSpeed;
        //StartCoroutine(DashWait());

        //v2 aceeasi directie de dash si pentru controller si pentru keybord

        Camera.main.transform.DOComplete();
        Camera.main.transform.DOShakePosition(.2f, .5f, 14, 90, false, true);
        //FindObjectOfType<RippleEffect>().Emit(Camera.main.WorldToViewportPoint(transform.position));

 
        hasDashed = true;

        GameMaster.isDashing = true;
        GameMaster.shake = true;
        GameMaster.DashChangeColor = true;

        StartCoroutine(JumpSqueeze(1.2f, 0.7f, 0.1f));
        anim.SetTrigger("dash");
        isDashing = true;
        //CinemachineShake.Instance.ShakeCamera(5f, 0.1f); //metoda cu instanta

        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2(x, y);
        Vector2 buf;
        if (x != 0.0f || y != 0.0f)
        {
            angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;


            //rb.velocity += dir.normalized * dashSpeed;
            if (((angle >= 0 && angle < 22.5) || (angle > -22.5 && angle < 0)) || (angle >= -67.5 && angle <= -22.5 && coll.onGround)) //dash dreapta
            {
                //rb.velocity += dir.normalized * dashSpeed;
                rb.velocity = Vector2.right * dashSpeed;
            }
            else if (angle > 67.5 && angle < 112.5) //dash sus
            {
                //rb.velocity = new Vector2(rb.velocity.x * (-1)*dashSpeed, 0);
                rb.velocity = Vector2.up * dashSpeed;
            }
            else if (((angle > 159.5 && angle <= 180) || (angle > -180 && angle < -159.7)) || (angle >= -159.5 && angle <= -112.5 && coll.onGround)) //dash stanga
            {
                //rb.velocity = new Vector2(0, rb.velocity.y * dashSpeed);
                rb.velocity = Vector2.left * dashSpeed;
            }
            else if (angle > -112.5 && angle < -67.5) //dash jos
            {
                //rb.velocity = new Vector2(0, rb.velocity.y * (-1) * dashSpeed);
                rb.velocity = Vector2.down * dashSpeed;
            }
            else if (angle >= 22.5 && angle <= 67.5) //dash dreapta sus
            {
                //rb.velocity = Vector2.left * dashSpeed;
                //rb.velocity = Vector2.up * dashSpeed;

                //rb.velocity += dir.normalized * dashSpeed;
                buf = new Vector2(1, 1);
                rb.velocity += buf.normalized * dashSpeed;


            }
            else if (angle >= 112.5 && angle <= 159.5) //dash stanga sus
            {
                //rb.velocity = Vector2.right * dashSpeed;
                //rb.velocity = Vector2.up * dashSpeed;

                //rb.velocity += dir.normalized * dashSpeed;
                buf = new Vector2(-1, 1);
                rb.velocity += buf.normalized * dashSpeed;
            }
            else if (angle >= -159.5 && angle <= -112.5 && !coll.onGround) //dash stanga jos
            {
                //rb.velocity = Vector2.left * dashSpeed;
                //rb.velocity = Vector2.down * dashSpeed;

                //rb.velocity += dir.normalized * dashSpeed;
                buf = new Vector2(-1, -1);
                rb.velocity += buf.normalized * dashSpeed;
            }
            else if (angle >= -67.5 && angle <= -22.5 && !coll.onGround) //dash dreapta jos
            {
                //rb.velocity = Vector2.right * dashSpeed;
                //rb.velocity = Vector2.down * dashSpeed;

                //rb.velocity += dir.normalized * dashSpeed;
                buf = new Vector2(1, -1);
                rb.velocity += buf.normalized * dashSpeed;
            }
        }
        else if(x == 0f && y == 0f)
        {
            //Debug.Log("bla");
            buf = new Vector2(side, 0);
            rb.velocity += buf.normalized * dashSpeed;
        }
        
        if (coll.inWater)
        {
            StartCoroutine(DashWait(MinDash));
        }
        else
        {
            StartCoroutine(DashWait(MaxDash));
        }

    }

    IEnumerator DashWait(float con)
    {
        FindObjectOfType<GhostTrail>().ShowGhost();
        StartCoroutine(GroundDash());
        //DOTween.RestartAll(); //pentru a reintializa ghosturile dar nu ii nevoie sincer
        DOVirtual.Float(con, 0, .8f, RigidbodyDrag);

        dashParticle.Play();
        rb.gravityScale = 0;
        GetComponent<BetterJumping>().enabled = false;
        wallJumped = true;
        isDashing = true;

        yield return new WaitForSeconds(.3f);

        dashParticle.Stop();
        rb.gravityScale = 3;
        GetComponent<BetterJumping>().enabled = true;
        wallJumped = false;
        isDashing = false;
    }

    IEnumerator GroundDash()
    {
        yield return new WaitForSeconds(.15f);
        if (coll.onGround)
            hasDashed = false;
    }

    private void WallJump(Vector2 dir)
    {
        if ((side == 1 && coll.onRightWall) || (side == -1 && !coll.onRightWall))
        {
            side *= -1;
            anim.Flip(side);
        }

        StopCoroutine(DisableMovement(0));
        StartCoroutine(DisableMovement(.1f));

        Vector2 wallDir = coll.onRightWall ? Vector2.left : Vector2.right;
        //v1 daca vreau sa folosesc din nou pot sa scor dir
        //Jump((Vector2.up / 1 + wallDir / 1), true);
        if ((dir.x >= 0.1 || dir.x <= -0.1) && (dir.y < 0.8 && dir.y >= 0))//original 0.3 //schimbat oleaca
        {
            Jump((Vector2.up / 1 + wallDir / 1), true); //original /1.5,/1.5 wallJumpLerp=5 (sa ma mai joc daca am timp) gasit pt moment ok 1,1,walljumplearp=4,jumpForce=14
                                                        //update 1,1,walljumpLearp=3.7,jumpForce=13 (speed=20,speedmax=10)
                                                        // 1,0.5f merge foarte bn tre sa vad si un learp value dar las originalul pt moment
            //Debug.Log("nu e bn");
        }
        else if(dir.y>=0.8f)    //jump in sus de pe wall
        {
            Jump(Vector2.up, false);
        }
        else
        {
            Jump((Vector2.up / 1 + wallDir / 2), true);
            //Debug.Log("totnu e bn");
        }

        wallJumped = true;
        if(!isDoubleJumping)
            isJumping = true;
    }

    private void WallSlide()
    {
        if (coll.wallSide != side)
            anim.Flip(side * -1);

        if (!canMove)
            return;

        bool pushingWall = false;
        if ((rb.velocity.x > 0 && coll.onRightWall) || (rb.velocity.x < 0 && coll.onLeftWall))
        {
            pushingWall = true;
        }
        float push = pushingWall ? 0 : rb.velocity.x;

        rb.velocity = new Vector2(push, -slideSpeed);
        //wallJumpParticle.Play(); // ar trebui slideParticle
    }

    private void Walk(Vector2 dir)
    {
        if (!canMove)
            return;

        if (wallGrab)
            return;

        //original
        //if (!wallJumped)
        //{
        //    rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);
        //}
        //else
        //{
        //    rb.velocity = Vector2.Lerp(rb.velocity, (new Vector2(dir.x * speed, rb.velocity.y)), wallJumpLerp * Time.deltaTime);
        //}

        //v2
        //incercarea mea (las pe mai tarziu)
        //update acum merge si am implementat si ace;asi imput si pentru tastatura si controller)
        //v2 sau v3 (v2 nu are acceleratie dar este destul de rapida oricum)

        //if (!wallJumped)
        //{
        //    if (dir.x >= 0.3)
        //    {
        //        //DOVirtual.Float(accelerationAtTheMoment, 20, .8f, rigidBodyAcceleration);
        //        //if(dir.x>0.4||dir.x<-0.4) // some give on horizontal
        //        rb.velocity = new Vector2(speed, rb.velocity.y);
        //        //rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);

        //        //acceleratie more or less
        //        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        //        {
        //            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        //        }
        //    }
        //    else if (dir.x <= -0.3)
        //    {
        //        rb.velocity = new Vector2(-1 * speed, rb.velocity.y);
        //        //rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);

        //        //acceleratie more or less
        //        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        //        {
        //            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        //        }

        //    }
        //    else if(dir.x>-0.3 && dir.x<0.3)
        //    {
        //        rb.velocity = new Vector2(0, rb.velocity.y);
        //    }

        //}

        //v3 merge dar nu stiu
        //update merge chiar ok trebuie hotarat care din v2 sau v3 il folosesc
        //hotarat v3 ramane

        if (!wallJumped)
        {
            //if (GameMaster.onMovingPlatform)
            //{
            //    if (dir.x >= 0.3 || dir.x <= -0.3)
            //    {//rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);
            //        forwardVelocity += AccelRatePerSec * Time.deltaTime;
            //        forwardVelocity = Mathf.Min(forwardVelocity, maxSpeed);
            //        rb.velocity = new Vector2(Mathf.Sign(dir.x) * (forwardVelocity + MovingPlatforms.rb.transform.position.x), rb.velocity.y);
            //    }
            //    else if (dir.x > -0.3 && dir.x < 0.3)
            //    {
            //        rb.velocity = new Vector2(0, rb.velocity.y);
            //        forwardVelocity = 0;
            //    }
            //    Debug.Log(GameMaster.onMovingPlatform);
            //}
            //else
            //{
            if (dir.x >= 0.3 || dir.x <= -0.3)
            {//rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);
                forwardVelocity += AccelRatePerSec * Time.deltaTime;
                forwardVelocity = Mathf.Min(forwardVelocity, maxSpeed);
                rb.velocity = new Vector2(Mathf.Sign(dir.x) * forwardVelocity, rb.velocity.y);
                //Debug.Log("aicias");
            }
            //v1 la v3 merge ok dar mai are niste problemute
            //else if (dir.x < 0.3 && dir.x > 0)
            //{
            //    forwardVelocity += brakePerSec * Time.deltaTime;
            //    forwardVelocity = Mathf.Max(forwardVelocity, 0);
            //    rb.velocity = new Vector2(forwardVelocity, rb.velocity.y);
            //}
            //else if (dir.x > -0.3 && dir.x < 0)
            //{
            //    forwardVelocity += brakePerSec * Time.deltaTime;
            //    forwardVelocity = Mathf.Max(forwardVelocity, 0);
            //    rb.velocity = new Vector2((-1) * forwardVelocity, rb.velocity.y);
            //}
            //else if (dir.x == 0)
            //{
            //    forwardVelocity = 0;
            //}

            //v2 la v3 more snappy ca in celest (instant)
            //poate ar trebui sa ii fac totusi o decelerare
            //i mean celest nu are una sau este foarte agresiva dar nust overall ar trebui sa fie ca si cel de miscare numai ca cu -= si max la
            //mathf.max(brake,0) sau ceva de genu
            else if (dir.x > -0.3 && dir.x < 0.3)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
                forwardVelocity = 0;
                //Debug.Log("aicias2");
            }
            //}

        }

        else
        {
            rb.velocity = Vector2.Lerp(rb.velocity, (new Vector2(dir.x * maxSpeed, rb.velocity.y)), wallJumpLerp * Time.deltaTime);
            //Debug.Log("asa da");
            //Debug.Log(GameMaster.onMovingPlatform);
        }


    }

    private void Jump(Vector2 dir, bool wall)
    {
        //Debug.Log("Jump");
        slideParticle.transform.parent.localScale = new Vector3(ParticleSide(), 1, 1);
        ParticleSystem particle = wall ? wallJumpParticle : jumpParticle;

        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += dir * jumpForce;
        //rb.AddForce(Vector2.up*jumpForce);    //cu add force tho este problematic la wallJump

        particle.Play();
        StartCoroutine(JumpSqueeze(0.5f, 1.2f, 0.08f));
        //isLanding = true;
    }

    IEnumerator DisableMovement(float time)
    {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }

    IEnumerator JumpSqueeze(float xSqueeze, float ySqueeze, float seconds)
    {
        Vector3 originalSize = Vector3.one;
        Vector3 newSize = new Vector3(xSqueeze, ySqueeze, originalSize.z);
        float t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            characterHolder.transform.localScale = Vector3.Lerp(originalSize, newSize, t);
            yield return null;
        }
        t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            characterHolder.transform.localScale = Vector3.Lerp(newSize, originalSize, t);
            yield return null;
        }

    }

    private void CheckLedgeClimb()
    {
        if (coll.ledgeDetected && !coll.canclimbLedge && !coll.onGround)
        {
            coll.canclimbLedge = true;

            //if (coll.wallSide == -1)
            //{
            //    //coll.ledgePos1 = new Vector2(Mathf.Floor(coll.ledgePosBot.x + coll.rightOffset.x) - coll.ledgeClimbXOffset1, Mathf.Floor(coll.ledgePosBot.y) + coll.ledgeClimbXOffset1);
            //    //coll.ledgePos2 = new Vector2(Mathf.Floor(coll.ledgePosBot.x + coll.rightOffset.x) + coll.ledgeClimbXOffset2, Mathf.Floor(coll.ledgePosBot.y) + coll.ledgeClimbXOffset2);
            //    grabRightLedge = new Vector2(0, 0);
            //    grabRightLedge = new Vector2(transform.position.x + 1, transform.position.y + 1);
            //    grabLeftLedge = new Vector2(0, 0);

            //}
            //else
            //{
            //    //coll.ledgePos1 = new Vector2(Mathf.Ceil(coll.ledgePosBot.x + coll.leftOffset.x) + coll.ledgeClimbXOffset1, Mathf.Floor(coll.ledgePosBot.y) + coll.ledgeClimbXOffset1);
            //    //coll.ledgePos2 = new Vector2(Mathf.Ceil(coll.ledgePosBot.x + coll.leftOffset.x) - coll.ledgeClimbXOffset2, Mathf.Floor(coll.ledgePosBot.y) + coll.ledgeClimbXOffset2);
            //    grabLeftLedge = new Vector2(0, 0);
            //    grabLeftLedge = new Vector2(transform.position.x - 1, transform.position.y + 1);
            //    grabRightLedge = new Vector2(0, 0);

            //}
            //coll.canclimbLedge = true;
            //StartCoroutine(waitLedgeClimb());
        }
        if (coll.canclimbLedge)
        {
            if (coll.wallSide==-1)//dreapta
            {
                //transform.position = grabLeftLedge;
                //Jump(Vector2.up + Vector2.left, false);
                transform.DOMove(new Vector2(transform.position.x + 0.35f, transform.position.y + 1), 0.1f, false);
            }
            else if (coll.wallSide==1)
            {

                //transform.position = grabRightLedge;
                //Jump(Vector2.up + Vector2.right, false);
                transform.DOMove(new Vector2(transform.position.x - 0.35f, transform.position.y + 1), 0.1f, false);
            }
            //coll.ledgeDetected = false;
            //coll.canclimbLedge = false;
        }
        //else
        //{
        //    coll.ledgeDetected = false;
        //    coll.canclimbLedge = false;
        //}
        coll.canclimbLedge = false;
        coll.canclimbLedge = false;
        //if (coll.canclimbLedge)
        //{
        //    transform.position = coll.ledgePos1;
        //}
    }
    //IEnumerator waitLedgeClimb()
    //{
    //    canMove = false;
    //    yield return new WaitForSeconds(0.5f);
    //    coll.canclimbLedge = false;
    //    //transform.position = coll.ledgePos2;
    //    coll.ledgeDetected = false;
    //    canMove = true;

    //}



    void RigidbodyDrag(float x)
    {
        rb.drag = x;
    }

    void WallParticle(float vertical)
    {
        var main = slideParticle.main;

        if (wallSlide || (wallGrab && vertical < 0))
        {
            slideParticle.transform.parent.localScale = new Vector3(ParticleSide(), 1, 1);
            main.startColor = Color.white;
        }
        else
        {
            main.startColor = Color.clear;
        }
    }

    int ParticleSide()
    {
        int particleSide = coll.onRightWall ? 1 : -1;
        return particleSide;
    }

    IEnumerator Death()
    {
        GameMaster.playDeathTransition = true;
        exitCourtime = false;
        canMove = false;
        if (rb.velocity.x > 0)
            rb.velocity = new Vector2(-10, 20);
        else if (rb.velocity.x < 0)
            rb.velocity = new Vector2(10, 20);
        else
            rb.velocity = new Vector2(0, 20);
        anim.SetTrigger("Dead");
        //Vector3 originalSize = Vector3.one;
        //Vector3 newSize = new Vector3(0, 0, originalSize.z);
        //float t = 0f;
        //while (t <= 1.0)
        //{
        //    t += Time.deltaTime / 0.2f;
        //    characterHolder.transform.localScale = Vector3.Lerp(originalSize, newSize, t);
        //    yield return null;
        //}
        StartCoroutine(JumpSqueeze(0, 0, 0.2f));//easier
        yield return new WaitForSeconds(0.1f);
        gameObject.transform.position = GameMaster.lastCheckPos;
        anim.SetTrigger("Dead");
        canMove = true;
        GameMaster.nr_death += 1;
        //Debug.Log(GameMaster.nr_death);
        exitCourtime = true;
        //yield return new WaitForSeconds(4f);
    }
    


}
