using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public enum Speeds { Slow = 0, Normal = 1, Fast = 2, Faster = 3, Insane = 4 };
public enum GameModes { Cube = 0, Ship = 1, FlappyBird = 2 , BackWards=3, Cube_Invert=4, Arrow=5};
//public enum Gravity { Normal = 1, Invertido = -1 };

public class Movement : MonoBehaviour
{
    public Speeds StartingSpeed;
    public GameModes StartingGameMode;
    public int StartingGravity;

    float[] MovementSpeed = { 4.5f, 10.5f, 12.5f, 15.5f, 21.5f };

    public bool checkpointMode;
    public Sprite Checkpoint;
    Speeds CurrentSpeed;
    GameModes CurrentGamemode;
    public int Gravity;
    Transform player;
    int attempts;
    bool movible;
    public Transform GroundCheck;
    public float GroundCheckRad;
    public LayerMask GroundMask;
    public Transform Sprite;
    bool isMouseButtonPressed;
    public GameObject mySprite;
    public Sprite Cube_Sprite;
    public Sprite Ship_Sprite;
    public Sprite Flappy_Sprite;
    public Sprite Ship_Rev_Sprite;
    public Sprite Flappy_Rev_Sprite;
    public TMP_Text AttempsText;
    public Sprite imgsBack1;
    public Image imageComponent;

    [SerializeField] public AudioSource BackgroundMusic;
    [SerializeField] public AudioSource jumpEffect;
    [SerializeField] public AudioSource dieEffect;
    [SerializeField] public AudioSource winEffect;
    [SerializeField] public AudioSource ChangeToFlappyEffect;
    [SerializeField] public AudioSource ChangeToShipEffect;
    [SerializeField] public AudioSource invertEffect;
    public GameObject textRemover;
    public GameObject PausaPanel;

    int idElemt;
    ArrayList spawnPosis;
    ArrayList spawnPosisGameMode;
    ArrayList spawnPosisSpeed;
    
    bool ballCheckOnce;
    bool pauseState;

    float gScale = 0;
    Rigidbody2D rb;
    Vector3 pausePosition;
    void Start()
    {
        Time.timeScale = 1f;
        checkpointMode = OptionMenu.godMode;
        PausaPanel.SetActive(false);
        pauseState = false;
        ballCheckOnce = true;
        spawnPosis = new ArrayList();
        spawnPosisGameMode= new ArrayList();
        spawnPosisSpeed = new ArrayList();
        movible = true;
        attempts = 0;        
        CurrentSpeed = StartingSpeed;
        CurrentGamemode = StartingGameMode;
        Gravity = StartingGravity;
        player = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        gScale = rb.gravityScale;
        AttempsText.text = "Attempt " + attempts;
        pausePosition = player.position;
        Vector3 aPosis = player.position;
        spawnPosis.Add(aPosis);
        spawnPosisGameMode.Add(CurrentGamemode);
        spawnPosisSpeed.Add(CurrentSpeed);
        idElemt=spawnPosis.Count-1;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
            Pause();

        if (movible)
        {
            transform.position += Vector3.right * MovementSpeed[(int)CurrentSpeed] * Time.deltaTime;
            if (rb.velocity.y < -24.2f)
                rb.velocity = new Vector2(rb.velocity.x, -24.2f);        
            
            Invoke(CurrentGamemode.ToString(),0);
            if (checkpointMode)
            {
                if (Input.GetKeyDown(KeyCode.A))
                    addCheckpoint();
                if (Input.GetKeyDown(KeyCode.D))
                    delCheckpoint();
            }
        }
       
    }
    public void RestartAudio()
    {
        BackgroundMusic.Stop();
        BackgroundMusic.Play();
    }
    public void Pause()
    {
        if (pauseState)
        {
            PausaPanel.SetActive(false);
            pauseState = false; 
            Time.timeScale = 1f;
            BackgroundMusic.Play();
        }
        else
        {
            BackgroundMusic.Stop();
            PausaPanel.SetActive(true);
            pauseState = true;
            Time.timeScale = 0f;
        }
    }
    public void Jump(float boost)
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.up * 26.6581f * boost, ForceMode2D.Impulse);
        jumpEffect.Play();  
    }
    void Cube()
    {
        rb.gravityScale = gScale;

        if (OnGround())
        {
            Vector3 Rotation = Sprite.rotation.eulerAngles;
            Rotation.z = Mathf.Round(Rotation.z / 90) * 90;
            Sprite.rotation = Quaternion.Euler(Rotation);
            if (Input.GetMouseButton(0))
                Jump(1f);
        }
        else
        {
            Sprite.Rotate(Vector3.back, 452.4152186f * Time.deltaTime * Gravity);
        }
    }
    public void Jump_Invert(float boost)
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.down * 26.6581f * boost, ForceMode2D.Impulse);
    }
    void Cube_Invert()
    {
        rb.gravityScale = -gScale; // cambiar gScale a -gScale para invertir la gravedad

        if (OnGround())
        {
            Vector3 Rotation = Sprite.rotation.eulerAngles;
            Rotation.z = Mathf.Round(Rotation.z / (-90)) * 90;
            Sprite.rotation = Quaternion.Euler(Rotation);
            if (Input.GetMouseButton(0))
                Jump_Invert(1f);
        }
        else
        {
            Sprite.Rotate(Vector3.back, 452.4152186f * Time.deltaTime ); // cambiar Gravity a -Gravity
        }
    }
    void FlappyBird()
    {
        
        Vector3 Rotation = Sprite.rotation.eulerAngles;
        Rotation.z = Mathf.Round(Rotation.z / 90) * 90;
        Sprite.rotation = Quaternion.Euler(Rotation);
        if (Input.GetMouseButtonDown(0))
        {
            isMouseButtonPressed = true;
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * 12.6581f * Gravity, ForceMode2D.Impulse);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isMouseButtonPressed = false;
        }

    }
    void BackWards()
    {
        transform.position += Vector3.left * MovementSpeed[(int)CurrentSpeed] * Time.deltaTime;

        if (rb.velocity.y < -24.2f)
            rb.velocity = new Vector2(rb.velocity.x, -24.2f);
        if (OnGround())
        {
            Vector3 Rotation = Sprite.rotation.eulerAngles;
            Rotation.z = Mathf.Round(Rotation.z / -90) * 90;
            Sprite.rotation = Quaternion.Euler(Rotation);

            if (Input.GetMouseButton(0))
            {
                rb.velocity = Vector2.zero;
                rb.AddForce(Vector2.up * 26.6581f * Gravity, ForceMode2D.Impulse);
            }
        }
        else
        {
            Sprite.Rotate(Vector3.back, 452.4152186f * Time.deltaTime * Gravity);
        }
    }
    void Ship()
    {
        Sprite.rotation = Quaternion.Euler(0, 0, rb.velocity.y * 2);
        if (Input.GetMouseButton(0))
            rb.gravityScale = -4.314969f;
        else
            rb.gravityScale = 4.314969f;

        rb.gravityScale = rb.gravityScale * Gravity;

    }
    void Arrow()
    {
        Sprite.rotation = Quaternion.Euler(0, 0, rb.velocity.y * 2);
        if (Input.GetMouseButton(0))
            Gravity = -1;
        else
            Gravity = 1;

        rb.gravityScale = rb.gravityScale * Gravity;

    }
    bool OnGround()
    {
        return Physics2D.OverlapBox(GroundCheck.position + Vector3.up - Vector3.up * (Gravity - 1 / -2), Vector2.right * 1.1f + Vector2.up * GroundCheckRad, 0, GroundMask);
    }
    bool TouchingWall()
    {
        return Physics2D.OverlapBox((Vector2)transform.position + (Vector2.right * 0.55f), Vector2.up * 0.8f + (Vector2.right * GroundCheckRad), 0, GroundMask);
    }
    public void Die()
    {
        dieEffect.Play();

        //movible = false;
        Time.timeScale = 0.01f;
        attempts++;
        StartCoroutine(Timeout_BackToStart());  
    }
    IEnumerator Timeout_BackToStart()
    {
        yield return new WaitForSeconds(0.005f);
        player.position = (Vector3)spawnPosis[spawnPosis.Count-1];
        //movible = true;
        Time.timeScale = 1f;
        if (!   checkpointMode)
        {
            CurrentGamemode = StartingGameMode;
            CurrentSpeed = StartingSpeed;
            ChangeGameMode(CurrentGamemode);
            ChangeSpeed(CurrentSpeed);
            ChangeGravity(5);
        }
        else
        {
            CurrentGamemode = (GameModes)spawnPosisGameMode[spawnPosisGameMode.Count-1];
            CurrentSpeed = (Speeds)spawnPosisSpeed[spawnPosisSpeed.Count-1];
            ChangeGameMode(CurrentGamemode);
            ChangeSpeed(CurrentSpeed);
            ChangeGravity(5);
        }
       if(player.position.x < 240.5 || player.position.x > 288.9)
        {
            imageComponent.sprite = imgsBack1;
            imageComponent.type = Image.Type.Simple;
            imageComponent.preserveAspect = false;
        }

        AttempsText.text = "Attempt " + attempts;
        textRemover.transform.position = new Vector3(player.position.x + 20f, textRemover.transform.position.y, textRemover.transform.position.z);
        RestartAudio();
        
    }
    public void ChangeGameMode(GameModes Gamemode)
    {
        CurrentGamemode = Gamemode;
        switch (CurrentGamemode)
        {
            case GameModes.Cube:
                mySprite.GetComponent<SpriteRenderer>().sprite = Cube_Sprite;
                break;
            case GameModes.Ship:
                if (Gravity == 1)
                {
                    ChangeToShipEffect.Play();
                    mySprite.GetComponent<SpriteRenderer>().sprite = Ship_Sprite;
                }
                else
                {
                    invertEffect.Play();
                    mySprite.GetComponent<SpriteRenderer>().sprite = Ship_Rev_Sprite;
                }
                break;
            case GameModes.FlappyBird:
                if (Gravity == 1)
                {
                    ChangeToFlappyEffect.Play();
                    mySprite.GetComponent<SpriteRenderer>().sprite = Flappy_Sprite;
                }
                else
                {
                    invertEffect.Play();
                    mySprite.GetComponent<SpriteRenderer>().sprite = Flappy_Rev_Sprite;
                }
                break;
            case GameModes.Cube_Invert:
                invertEffect.Play();
                mySprite.GetComponent<SpriteRenderer>().sprite = Cube_Sprite;
                break;
            case GameModes.Arrow:
                mySprite.GetComponent<SpriteRenderer>().sprite = Cube_Sprite;
                break;
        }
    }
    public void ChangeSpeed(Speeds Speed)
    {
        CurrentSpeed = Speed;       
    }
    public void ChangeGravity(int gravity)
    {
        if (gravity == 5)
            Gravity = StartingGravity;
        else
            Gravity = gravity;
        rb.gravityScale = Mathf.Abs(rb.gravityScale);


    }
    void noGravityDash()
    {
        
        Debug.Log("No Grav");
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "JumpBall":
                if (Input.GetMouseButton(0) && ballCheckOnce)
                {
                    ballCheckOnce = false;
                    Jump(1.3f);
                }
                break;
            case "GravityBall":
                if (Input.GetMouseButton(0) && ballCheckOnce)
                {
                    ballCheckOnce = false;
                    changeCubeCubeInvert();
                }
                break;
            case "JumpBallInvert":
                if (Input.GetMouseButton(0) && ballCheckOnce)
                {
                    ballCheckOnce = false;
                    Jump_Invert(1.2f);
                }
                break;
                //case "0GravityDash":
                //    Debug.Log("No Gravity dash");
                //    ChangeGravity(-1);
                //    //if (Input.GetMouseButton(0))
                //    //    noGravityDash();

                //    break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "GravityBall":
                ballCheckOnce = true;
                break;
            case "JumpBall":
                ballCheckOnce = true;
                break;
        }
    }
    public void changeCubeCubeInvert()
    {
        if (CurrentGamemode < (GameModes)4)
        {
            Jump(0.5f);
            GameModes gm = (GameModes)4;
            ChangeGameMode(gm);
        }
        else if (CurrentGamemode == (GameModes)4)
        {
            Jump_Invert(0.5f);
            GameModes gm = (GameModes)0;
            ChangeGameMode(gm);
        }
    }
    private void addCheckpoint()
    {
        spawnPosis.Add(player.position);
        spawnPosisGameMode.Add(CurrentGamemode);
        spawnPosisSpeed.Add(CurrentSpeed);
        idElemt = spawnPosis.Count - 1;

        Transform playerTransform;
        playerTransform = transform; // Assuming this script is attached to the player object
        GameObject newSpriteObject = new GameObject("CheckPoint_"+idElemt+""); // Create a new game object with a name
        newSpriteObject.transform.position = playerTransform.position + Vector3.up * 0.5f; // Set the position to the player position with an offset of 1.5 units on the Y axis
        SpriteRenderer spriteRenderer = newSpriteObject.AddComponent<SpriteRenderer>(); // Add a sprite renderer component
        spriteRenderer.sprite = Checkpoint; // Set the sprite of the sprite renderer component
    }
    private void delCheckpoint()
    {
        if (spawnPosis.Count > 1)
        {
            spawnPosis.RemoveAt(spawnPosis.Count - 1);
            spawnPosisGameMode.RemoveAt(spawnPosis.Count - 1);
            spawnPosisSpeed.RemoveAt(spawnPosis.Count - 1);
            idElemt = spawnPosis.Count - 1;

            GameObject delete = GameObject.Find("CheckPoint_"+idElemt+"");
            Destroy(delete);
        }
    }
    public void End()
    {
        winEffect.Play();
        movible = false;
        StartCoroutine(returnMenu());
    }
    IEnumerator returnMenu()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(0);


    }
}
