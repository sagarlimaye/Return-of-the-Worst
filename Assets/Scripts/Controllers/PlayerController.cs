using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public float speed = 3f;             //Floating point variable to store the player's movement speed.
    public float clampXL = -5;
    public float clampXR = 15;
    public float ClampYT = 1;
    public float ClampYB = -3;

    public float ClampXDist = 5; // assumes evenly in the middle
    
    public JointController joint;

    public Animator screenWipe;
    public GameObject screenWipeObj;
    public float superRange = 9f;
    private PlayerSuper super;
    

    public Animator anim;
    public Animator swing;


    private bool facingRight = true;
    private float prevX = 0;
    private Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.

    public Camera cam;
    public bool isLocked;
    public GameObject following;

    public GameObject playerSprite;

    private AudioSource superSound;

    public Sprite jaySuper;
    public Sprite richSuper;
    public Sprite mikeSuper;

    public SpriteRenderer ScreenWiper;

    // Use this for initialization
    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();
        super = GetComponent<PlayerSuper>();
        isLocked = false;
        GameObject playerClone = null;
        switch(PlayerAttributes.instance.characterName)
        {
            case PlayerAttributes.Character.Mike:
                playerClone = Instantiate(PlayerAttributes.instance.mikeModel, playerSprite.transform);
                ScreenWiper.sprite = mikeSuper;
                break;
            case PlayerAttributes.Character.Jay:
                playerClone = Instantiate(PlayerAttributes.instance.jayModel, playerSprite.transform);
                ScreenWiper.sprite = jaySuper;
                break;
            case PlayerAttributes.Character.Rich:
                playerClone = Instantiate(PlayerAttributes.instance.richModel, playerSprite.transform);
                ScreenWiper.sprite = richSuper;
                break;
        }
        if(playerClone != null)
        {
            playerClone.transform.position = new Vector3(-0.016f, -2.36f, 0f);
            playerClone.transform.localScale = new Vector3(0.1f, 0.1f, 1);
            playerSprite.GetComponent<SpriteRenderer>().enabled = false;
            anim = playerClone.GetComponent<Animator>();
            PlayerHealth playerHealth = playerSprite.GetComponent<PlayerHealth>();
            playerHealth.anim = anim;
            anim.SetInteger("Health", (int)playerHealth.StartingHealth);
        }

        GameObject pa = GameObject.Find("PlayerAttributes");
        if (pa != null)
        {
            PlayerAttributes pas = pa.GetComponent<PlayerAttributes>();
            if (pas != null)
            {
                speed = pas.speed;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("jump");
            Debug.Log("space pressed");
        }
        else if (Input.GetButtonDown("Fire1"))
        {
            //Debug.Log("CTRL PRESSED");
            if (joint != null)
            {
                //joint.doSwing();
                anim.SetTrigger("Swing");
            }

        }
        else if (Input.GetButtonDown("Fire2") && super.canSuper())
        {
            doSuper();
            if (superSound == null) superSound = GameObject.Find("SuperSound").GetComponent<AudioSource>();
            superSound.Play();
        }
        if (Input.GetKeyDown("left") || Input.GetKeyDown("right"))
        {
            anim.SetTrigger("Walk");
        }
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        //Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxis("Horizontal");

        //Store the current vertical input in the float moveVertical.
        float moveVertical = Input.GetAxis("Vertical");

        //Use the two store floats to create a new Vector2 variable movement.
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        //rb2d.position += movement * speed;


        float x = rb2d.position.x + moveHorizontal * speed * Time.deltaTime;
        float y = rb2d.position.y + moveVertical * speed * Time.deltaTime;
        //Debug.Log("Player speed = " + moveHorizontal * speed);

        float moveSpeed = Mathf.Abs(moveHorizontal) + Mathf.Abs(moveVertical);
        anim.SetFloat("speed", moveSpeed);
        //Debug.Log("movespeed = " + moveSpeed);

        // Don't move if attacking or super
        var animInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (animInfo.IsName("Attack") || animInfo.IsName("Super"))
            return;
  
        Vector2 pos;

        if (!isLocked)
        {
            pos = new Vector2
            (
            Mathf.Clamp(x, clampXL, clampXR),
            Mathf.Clamp(y, ClampYB, ClampYT)
            );
        }
        else
        {
            pos = new Vector2
            (
            Mathf.Clamp(x, following.transform.position.x - ClampXDist, following.transform.position.x + ClampXDist),
            Mathf.Clamp(y, ClampYB, ClampYT)
            );
        }


        //if (pos.x < 0 && rb2d.position.x > 0 || pos.x > 0 && rb2d.position.x < 0)
        //if (pos.x - rb2d.position.x > 0) // went right

        if (pos.x - rb2d.position.x > 0 && !facingRight)
        {
            transform.Rotate(new Vector3(0, 180, 0));
            facingRight = true;
        }
        if (pos.x - rb2d.position.x < 0 && facingRight)
        {
            transform.Rotate(new Vector3(0, 180, 0));
            facingRight = false;
        }
        
        prevX = rb2d.position.x;
        rb2d.position = pos;
        
    }

    public void cameraEventDone()
    {
        cam.GetComponent<CameraController>().setToFollow(this.gameObject);
        isLocked = false;
        Debug.Log("Did unlock");
    }

    private void doSuper()
    {
        //GetComponent<AudioSource>().Play();
        //need to do super here
        Debug.Log("Did Super");
        
        //screenWipeObj.SetActive(true);
        screenWipe.SetTrigger("Wipe");
        anim.SetTrigger("Super");
        super.changeSuper(-100);
    }
}