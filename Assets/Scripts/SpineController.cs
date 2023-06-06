using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;


struct MyState
{
  public StateEnum state;
  public bool loop;

  public MyState(StateEnum curState, bool curLoop)
  {
    state = curState;
    loop = curLoop;
  }
}

enum StateEnum
{
  Idle,
  Run,
  Jump,
  Fall,
  Attack,
}


public class SpineController : MonoBehaviour
{
  public float runSpeed;
  public float jumpSpeed;
  public float doubleJumpSpeed;
  private Rigidbody2D myRigidbody;
  private BoxCollider2D myFeet;
  private bool isGround;
  private bool canDoubleJump;
  public SkeletonAnimation skeletonAnimation;
  private StateEnum lastAnimationState;
  private MyState myAnimationState;

  void Start()
  {
    myAnimationState = new MyState(StateEnum.Idle, true);
    myRigidbody = GetComponent<Rigidbody2D>();
    myFeet = GetComponent<BoxCollider2D>();
  }

  void Update()
  {
    checkGround();
    Run();
    Flip();
    Jump();
    Attack();
    checkAnimationState();
  }

  void checkAnimationState()
  {
    if (myRigidbody.velocity.y < 0)
    {
      SetAnimationState(StateEnum.Fall);
    }

    if (myAnimationState.state != lastAnimationState)
    {
      SetAnimationByName(myAnimationState.state, myAnimationState.loop);
      lastAnimationState = myAnimationState.state;
    }
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
    if (lastAnimationState != StateEnum.Jump && lastAnimationState != StateEnum.Attack)
    {
      if (playerHasXSpeed)
      {
        SetAnimationState(StateEnum.Run);
      }
      else if (isGround)
      {
        SetAnimationState(StateEnum.Idle);
      }
    }
  }

  void Jump()
  {
    if (Input.GetButtonDown("Jump"))
    {
      if (isGround)
      {
        SetAnimationState(StateEnum.Jump);
        Vector2 jumpVel = new Vector2(0, jumpSpeed);
        myRigidbody.velocity = Vector2.up * jumpVel;
        canDoubleJump = true;
      }
      else
      {
        if (canDoubleJump)
        {
          SetAnimationState(StateEnum.Jump);
          Vector2 doubleJumpVel = new Vector2(0, doubleJumpSpeed);
          myRigidbody.velocity = Vector2.up * doubleJumpVel;
          canDoubleJump = false;
        }
      }
    }
  }

  void Attack()
  {
    if (Input.GetButtonDown("Attack"))
    {
      SetAnimationState(StateEnum.Attack, false);
    }
  }

  void SetAnimationState(StateEnum state, bool loop = true)
  {
    myAnimationState.state = state;
    myAnimationState.loop = loop;
  }

  void SetAnimationByName(StateEnum state, bool loop = true)
  {
    string aimName = "idle";
    switch (state)
    {
      case StateEnum.Jump:
        aimName = "jump";
        break;
      case StateEnum.Fall:
        aimName = "fall";
        break;
      case StateEnum.Run:
        aimName = "run";
        break;
      case StateEnum.Attack:
        aimName = "attack";
        break;
    }
    var trackEntry = skeletonAnimation.AnimationState.SetAnimation(0, aimName, loop);
    trackEntry.Complete += OnWalkAnimationComplete;
  }

  void OnWalkAnimationComplete(Spine.TrackEntry trackEntry)
  {
    if (trackEntry.Animation.Name == "attack")
    {
      SetAnimationState(StateEnum.Idle);
    }
  }
}
