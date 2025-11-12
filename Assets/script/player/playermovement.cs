using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove2D : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float sprintSpeed = 1f;
    public float stamina = 4f;
    [SerializeField] private float defaultMovespeed;
    [SerializeField] private float defaultStamina;
    [SerializeField] private bool sprinting = false;
    private bool staminaEmpty = false;
    private Rigidbody2D rb;
    private float moveInputY;
    private float moveInputX;
    public int direction = 1;
    public Transform detector;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        defaultStamina = stamina;
        defaultMovespeed = moveSpeed;
    }

    void Update()
    {
        moveInputX = Input.GetAxisRaw("Horizontal");
        moveInputY = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.J))
        {
            if (stamina > 0 && !staminaEmpty)
            {
                sprinting = true;
                sprint();
            }
            else
            {
                sprinting = false;
                unsprint();
            }
        }
        else if (Input.GetKeyUp(KeyCode.J))
        {
            sprinting = false;
            unsprint();
        }

        staminadrain();

        if (Mathf.Abs(moveInputX) > 0.1f)
        {
            if (moveInputX > 0)
                direction = 4;
            else if (moveInputX < 0)
                direction = 3;
        }
        else if (Mathf.Abs(moveInputY) > 0.1f)
        {
            if (moveInputY > 0)
                direction = 2;
            else if (moveInputY < 0)
                direction = 1;
        }

        if (detector != null)
        {
            if (direction == 1)
                detector.transform.localPosition = new Vector2(0, -3.2f);
            else if (direction == 2)
                detector.transform.localPosition = new Vector2(0, 3.2f);
            else if (direction == 3)
                detector.transform.localPosition = new Vector2(-2.74f, -0.35f);
            else if (direction == 4)
                detector.transform.localPosition = new Vector2(2.74f, 0.35f);
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInputX * moveSpeed, moveInputY * moveSpeed);
    }

    private void sprint()
    {
        moveSpeed = defaultMovespeed + sprintSpeed;
    }

    private void unsprint()
    {
        moveSpeed = defaultMovespeed;
    }

    private void staminadrain()
    {
        if (sprinting)
        {
            if (stamina > 0)
            {
                stamina -= Time.deltaTime;
                if (stamina <= 0)
                {
                    stamina = 0;
                    staminaEmpty = true;
                    sprinting = false;
                    unsprint();
                }
            }
        }
        else
        {
            if (stamina < defaultStamina)
            {
                stamina += Time.deltaTime;
                if (stamina >= defaultStamina)
                {
                    stamina = defaultStamina;
                    staminaEmpty = false;
                }
            }
        }
    }
}
