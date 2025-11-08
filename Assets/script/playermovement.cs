using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove2D : MonoBehaviour
{
    public float moveSpeed = 5f; // Kecepatan gerak
    
    private Rigidbody2D rb;
    private float moveInputY;
    private float moveInputX;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Ambil input horizontal (A/D atau panah kiri/kanan)
        moveInputX = Input.GetAxisRaw("Horizontal");
        moveInputY = Input.GetAxisRaw("Vertical");
        // Balik arah sprite jika perlu

    }

    void FixedUpdate()
    {
        // Gerakkan karakter ke kiri/kanan
        rb.linearVelocity = new Vector2(moveInputX * moveSpeed, moveInputY * moveSpeed);
    }

    


}
