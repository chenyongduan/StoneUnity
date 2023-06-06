using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  public float runSpeed;
  public float jumpSpeed;
  public float doubleJumpSpeed;
  private Rigidbody2D myRigidbody;
  private BoxCollider2D myFeet;
  private Animator myAnim;
  private bool isGround;
  private bool canDoubleJump;

  // Start is called before the first frame update
  void Start()
  {
    myRigidbody = GetComponent<Rigidbody2D>();
    myFeet = GetComponent<BoxCollider2D>();
    myAnim = GetComponent<Animator>();
  }

  // Update is called once per frame
  void Update()
  {
    checkGround();
    Run();
    Flip();
    Jump();
    SwitchAnimation();
  }

  void checkGround()
  {
    isGround = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));
  }

  void Flip()
  {
    bool playerHasXSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
    if (playerHasXSpeed)
    {
      if (myRigidbody.velocity.x > 0.1f)
      {
        transform.localRotation = Quaternion.Euler(0, 0, 0);
      }
      if (myRigidbody.velocity.x < -0.1f)
      {
        transform.localRotation = Quaternion.Euler(0, 180, 0);
      }
    }
  }

  void Run()
  {
    float moveDir = Input.GetAxis("Horizontal");
    Vector2 playerVel = new Vector2(moveDir * runSpeed, myRigidbody.velocity.y);
    myRigidbody.velocity = playerVel;
    bool playerHasXSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
    ChangeAnimation("Run", playerHasXSpeed);
  }

  void Jump()
  {
    if (Input.GetButtonDown("Jump"))
    {
      if (isGround)
      {
        ChangeAnimation("Jump", true);
        Vector2 jumpVel = new Vector2(0, jumpSpeed);
        myRigidbody.velocity = Vector2.up * jumpVel;
        canDoubleJump = true;
      }
      else
      {
        if (canDoubleJump)
        {
          ChangeAnimation("DoubleJump", true);
          Vector2 doubleJumpVel = new Vector2(0, doubleJumpSpeed);
          myRigidbody.velocity = Vector2.up * doubleJumpVel;
          canDoubleJump = false;
        }
      }
    }
  }

  void SwitchAnimation()
  {
    ChangeAnimation("Idle", true);
    if (myAnim.GetBool("Jump"))
    {
      if (myRigidbody.velocity.y < 0)
      {
        ChangeAnimation("Jump", false);
        ChangeAnimation("Fall", true);
      }
    }
    else if (isGround)
    {
      ChangeAnimation("Fall", false);
      ChangeAnimation("Idle", true);
    }

    if (myAnim.GetBool("DoubleJump"))
    {
      if (myRigidbody.velocity.y < 0)
      {
        ChangeAnimation("DoubleJump", false);
        ChangeAnimation("DoubleFall", true);
      }
    }
    else if (isGround)
    {
      ChangeAnimation("DoubleFall", false);
      ChangeAnimation("Idle", true);
    }
  }

  void ChangeAnimation(string name, bool animated)
  {
    myAnim.SetBool(name, animated);
  }
}
