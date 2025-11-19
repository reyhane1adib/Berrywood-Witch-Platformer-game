using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovment : MonoBehaviour
{

    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Color hitFlashColor = Color.red;

    [SerializeField] float flashDuration = 0.08f;
    [SerializeField] int flashCount = 6;

    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;


    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    //BoxCollider2D myFeetCollider;
    float gravityScaleAtStart;
    bool isAlive=true;
     
    SpriteRenderer mySpriteRenderer;
    Color originalColor;
    bool isInvincible = false;

  

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        //myFeetCollider = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidbody.gravityScale;

        mySpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        originalColor = mySpriteRenderer.color;

    }

    void Update()
    {
        if(!isAlive) { return; }
        Run();
        FlipSprite();
        ClimbLadder(); 
        Die();
    

    }
    
    void OnFire(InputValue value)
    {
        if(!isAlive) { return; }
        Instantiate(bullet, gun.position, transform.rotation);
    }
    void OnMove(InputValue value)
    {
        if(!isAlive) { return; }
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);

    }

    void Run()
    {
        if(!isAlive) { return; }
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);

    }

    void ClimbLadder()
    {
          if (!myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
           {
              myRigidbody.gravityScale = gravityScaleAtStart;
              myAnimator.SetBool("isClimbing", false);
            
            return;
            }
    
        Vector2 climbVelocity = new Vector2(myRigidbody.velocity.x, moveInput.y * climbSpeed);
        myRigidbody.velocity = climbVelocity;
        myRigidbody.gravityScale =0f;
        //bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        //myAnimator.SetBool("isClimbing", playerHasHorizontalSpeed);
        bool playerHasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("isClimbing", playerHasVerticalSpeed);
        myAnimator.SetBool("isRunning", false);



    }
    

    void OnJump(InputValue value)
    {
        if(!isAlive) { return; }
        if (!myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))){ return;}

        if (value.isPressed)

        {
            myRigidbody.velocity += new Vector2(0f, jumpSpeed);
        }

        
    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
           transform.localScale = new Vector2 (Mathf.Sign(myRigidbody.velocity.x), 1f);
    } 
        }
     void Die()

    {
        //if(myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies")))
       // {
            //isAlive= false;
            
        //}
     
    
       if (isInvincible)  { return; }

       if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies","Hazards","Water")))
    {
        Debug.Log("Hit enemy! Starting flash.");
        StartCoroutine(FlashWhiteRoutine());
        FindObjectOfType<GameSession>().ProcessPlayerDeath();

    }
 }

    IEnumerator FlashWhiteRoutine()
    {
        isInvincible = true;

        for (int i = 0; i < flashCount; i++)
        {
            mySpriteRenderer.color = hitFlashColor;      // flash white
            yield return new WaitForSeconds(flashDuration);

            mySpriteRenderer.color = originalColor;      // back to normal
            yield return new WaitForSeconds(flashDuration);
        }

        mySpriteRenderer.color = originalColor;
        isInvincible = false;
    }


}    

