using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading;
using System.Threading.Tasks;
using Mirror;

public class player : NetworkBehaviour
{
    //Config
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(25f, 25f);
    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip deathSound;

     

    //State
    bool isAlive = true;


    //Cached component references
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider2D;
    BoxCollider2D myFeetCollider2D;
    float gravityScaleAtStart;
    float runSpeedAtStart;
    public Button jumpButton;
    public Joystick joystick;



    //Messege then methods
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider2D = GetComponent<CapsuleCollider2D>();
        myFeetCollider2D = GetComponent<BoxCollider2D>();
        jumpButton = GetComponent<Button>();
        gravityScaleAtStart = myRigidBody.gravityScale;
        runSpeedAtStart = runSpeed;
        joystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<Joystick>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer) 
        {
            OnStartLocalPlayer();
            if (!isAlive) 
            {
                { return; }
            }
        
            Run();
            ClimbLadder();
            Jump();
            FlipSprite();
            BackToMenu();
            StartCoroutine("Die");
        }
    }
    private void Run()
    {
        float controlThrow = joystick.Horizontal;

        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;

        bool playerHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Running", playerHorizontalSpeed);
    }
    private void ClimbLadder()
    {
        if (!myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myAnimator.SetBool("Climbing", false);
            myRigidBody.gravityScale = gravityScaleAtStart;
            runSpeed = runSpeedAtStart;
            return;
        }


        float controlThrow = joystick.Vertical;
        
        Vector2 climbVelocity = new Vector2(myRigidBody.velocity.x, controlThrow * climbSpeed);
        myRigidBody.velocity = climbVelocity;
        myRigidBody.gravityScale = 0f;
        runSpeed = 3f;

        bool playerVericalSpeed = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("Climbing", playerVericalSpeed);
        
    }
    private void Jump()
    {
        if (!myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground", "Player"))) /*!myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Climbing"))*/
        { return; }
        if (CrossPlatformInputManager.GetButtonDown("JumpButton"))
        {
            AudioSource.PlayClipAtPoint(jumpSound, Camera.main.transform.position);
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            myRigidBody.velocity += jumpVelocityToAdd;
        }
    }

    IEnumerator Die()
    {
        if (myBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position);
            GetComponent<Rigidbody2D>().velocity = deathKick;

            yield return new WaitForSeconds(2f);
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
            isAlive = true;

        }

    }

    private void FlipSprite()
    {
        bool playerHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if (playerHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
        }
    }
    public void BackToMenu()
    {
        if (CrossPlatformInputManager.GetButtonDown("Cancel"))
        {
            SceneManager.LoadScene(0);
        }
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        gameObject.name = "Local";
    }
}
