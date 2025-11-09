using Unity.VisualScripting;
using UnityEngine;


public class PlayerMove2D : MonoBehaviour
{
    public float moveSpeed = 5f; 
    
    private Rigidbody2D rb;
    private float moveInputY;
    private float moveInputX;
    public int direction = 1; //1 = bawah, 2 = atas, 3 = kiri, 4 = kanan
    public Transform detector;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        moveInputX = Input.GetAxisRaw("Horizontal");
        moveInputY = Input.GetAxisRaw("Vertical");
        
        //buat detect value posisi terakhir
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
            { detector.transform.localPosition = new Vector2(0, -3.2f); }
            else if (direction == 2)
            {
                detector.transform.localPosition = new Vector2(0, 3.2f);
            }
            else if (direction == 3)
            {
                detector.transform.localPosition = new Vector2(-2.74f, -0.35f);
            }
            else if (direction == 4)
            {
                detector.transform.localPosition = new Vector2(2.74f, 0.35f);
            }
        }
        


    }

    void FixedUpdate()
    {

        rb.linearVelocity = new Vector2(moveInputX * moveSpeed, moveInputY * moveSpeed);
    }

    


}
