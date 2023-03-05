using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Speeds { Slow = 0, Normal = 1, Fast = 2, Faster = 3, Insane = 4 };
public enum GameModes { Cube = 0, Ship = 1, FlappyBird = 2 , BackWards};
//public enum Gravity { Normal = 1, Invertido = -1 };

public class Movement : MonoBehaviour
{
    public Speeds CurrentSpeed;
    public GameModes CurrentGamemode;

    public float[] MovementSpeed = { 4.5f, 10.5f, 12.5f, 15.5f, 21.5f };

    public Transform GroundCheck;
    public float GroundCheckRad;
    public LayerMask GroundMask;
    public Transform Sprite;
    bool isMouseButtonPressed;
    public GameObject mySprite;
    public Sprite Cube_Sprite;
    public Sprite Ship_Sprite;
    public Sprite Flappy_Sprite;
    public int Gravity = 1;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        transform.position += Vector3.right * MovementSpeed[(int)CurrentSpeed] * Time.deltaTime;

        if (rb.velocity.y < -24.2f)
            rb.velocity = new Vector2(rb.velocity.x, -24.2f);
        Invoke(CurrentGamemode.ToString(),0);
    }
    public void Jump()
    {   
        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.up * 26.6581f * Gravity, ForceMode2D.Impulse);
        
    }
    void Cube()
    {
        
        if (OnGround())
        {
            Vector3 Rotation = Sprite.rotation.eulerAngles;
            Rotation.z = Mathf.Round(Rotation.z / 90) * 90;
            Sprite.rotation = Quaternion.Euler(Rotation);

            if (Input.GetMouseButton(0))
            {
                Jump();
            }
        }
        else
        {
            Sprite.Rotate(Vector3.back, 452.4152186f * Time.deltaTime * Gravity);
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
        UnityEditor.EditorApplication.isPlaying = false;
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
                mySprite.GetComponent<SpriteRenderer>().sprite = Ship_Sprite;
                break;
            case GameModes.FlappyBird:
                mySprite.GetComponent<SpriteRenderer>().sprite = Flappy_Sprite;
                break;
        }
    }
    public void ChangeSpeed(Speeds Speed)
    {
        CurrentSpeed = Speed;       
    }
    public void ChangeGravity(int gravity)
    {
       Gravity = gravity;
       rb.gravityScale = Mathf.Abs(rb.gravityScale);
    }


}
