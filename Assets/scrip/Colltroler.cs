using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Colltroler : MonoBehaviour
{
    public float movespeed;
    public float jumpheight;
    public float GroudedcheckRadius;

    [Header("Dash info")]
    [SerializeField] private float Dashduration;
    [SerializeField] private float DashTime;    



    Rigidbody2D rb;
    Animator anim;
  
    private bool FacingRight;
 


    bool  doubleJump;
    bool isGround;

    //[Header("Attack info")]
    //private  bool isAttacking=false;////////////
    // int ComboCounter;

    public float attackRange = 0.5f;
    public LayerMask enemylayer;
    public Transform attackpoint;
    public int  AttackDame = 40;


    public Transform Pointcheckground;
    public LayerMask WhatisGround;
    public LayerMask Whatiswall;

    private CapsuleCollider2D capsulecollide22d;
    private float move;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        FacingRight = true;
        capsulecollide22d = GetComponent<CapsuleCollider2D>();
        //amountofjumpleft = amountofjump;
    }


    void Update()
    {       
        checkInput();
        CheckGround();
        Checkwall();
        checkMovemenDirection();
        CheckIfcanjump();
        setupAnimatorK();
    }
  
    private void FixedUpdate()
    {
        ApplyMovement();
        CheckGround();
    



    }
   
    private void checkInput()// check nut bam 
    {
        move = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.R))
        {
            AAttack();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            dame();

        }

    }

    public void dame()
    {
        anim.SetTrigger("dame");
        Collider2D[] hitenemy = Physics2D.OverlapCircleAll(attackpoint.position, attackRange, enemylayer);
        foreach (Collider2D enemy in hitenemy)
        {
            enemy.GetComponent<enemy>().Takedame(AttackDame);
        }

    }

    public void AAttack()
    {
        anim.SetTrigger("attack");
    }
    private void setupAnimatorK()
    {
        anim.SetBool("isGrounded", isGround);
        anim.SetFloat("yVelocity",rb.velocity.y);      
        //anim.SetBool("isAttacking", isAttacking);   
        //anim.SetInteger("ComboCounter", ComboCounter);
      
    }
    private void ApplyMovement()//di chuyen 
    {
      
        rb.velocity = new Vector2(move * movespeed, rb.velocity.y);
        anim.SetFloat("speed", Mathf.Abs(move));

    }
   
    void checkMovemenDirection()//kiem tra huong di chuyen
    {
        if (move > 0 && !FacingRight)
        {
            Flip();
        }
        else if (move < 0 && FacingRight)
        {
            Flip();
        }


    }
    void CheckIfcanjump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGround)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpheight);
                doubleJump = true;

            }
            else if (doubleJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpheight * 1.2f);
                doubleJump = false;
            }
        }
    }
    void Flip()
    {
        FacingRight = !FacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void CheckGround()// kiem tra san dat
    {

        isGround = Physics2D.OverlapCircle(Pointcheckground.position, GroudedcheckRadius, WhatisGround);

        //float heighttext = 0.11f; cach Check ground su dung raycast
        //RaycastHit2D raycasthit = Physics2D.BoxCast(capsulecollide22d.bounds.center, capsulecollide22d.bounds.size, 0, Vector2.down, heighttext, WhatisGround);
        //return raycasthit.collider != null;
    }
    private bool Checkwall()// kiem tra  tuong
    {
        float heighttext = 1f;
        RaycastHit2D raycasthit = Physics2D.BoxCast(capsulecollide22d.bounds.center, capsulecollide22d.bounds.size, 0, new Vector2(transform.localScale.x, 0), heighttext, Whatiswall);
        return raycasthit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(Pointcheckground.position, GroudedcheckRadius);
    }
    private void OnDrawGizmosSelected()
    {
      
        Gizmos.DrawWireSphere(attackpoint.position, attackRange);

    }
}

    

