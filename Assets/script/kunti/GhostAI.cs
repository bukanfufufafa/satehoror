using UnityEngine;
using System.Collections.Generic;

// ============================================
// GhostAI.cs - Sistem hantu yang minta sate
// ============================================
public class GhostAI : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private Vector2 mapMinBounds = new Vector2(-20, -20);
    [SerializeField] private Vector2 mapMaxBounds = new Vector2(20, 20);
    
    [Header("Chase Settings")]
    [SerializeField] private float chaseSpeed = 5f;
    [SerializeField] private float detectionRange = 15f;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private float attackDamage = 10f;
    
    [Header("Cooldown Settings")]
    [SerializeField] private float chaseCooldown = 3f; // Cooldown setelah menyerang
    [SerializeField] private float cooldownDuration = 5f; // Durasi istirahat
    
    private Transform player;
    private Rigidbody2D rb;
    public Animator Animator;
    
    public enum GhostState { Idle, Chasing, Cooldown, Satisfied }
    private GhostState currentState = GhostState.Idle;
    
    private int requestedAmount = 1;
    private float lastAttackTime;
    private float cooldownTimer;
    private Vector2 lastMoveDirection;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
        // Spawn di posisi random
        SpawnAtRandomPosition();
        
        // Generate permintaan sate random
        GenerateSateRequest();
        
        currentState = GhostState.Chasing;
        lastMoveDirection = Vector2.down; // Default direction
    }

    void Update()
    {
        if (player == null) return;
        
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        
        switch (currentState)
        {
            case GhostState.Chasing:
                HandleChasing(distanceToPlayer);
                break;
                
            case GhostState.Cooldown:
                HandleCooldown();
                break;
                
            case GhostState.Satisfied:
                // Hantu puas, bisa despawn atau hilang
                break;
        }
    }

    void SpawnAtRandomPosition()
    {
        float randomX = Random.Range(mapMinBounds.x, mapMaxBounds.x);
        float randomY = Random.Range(mapMinBounds.y, mapMaxBounds.y);
        transform.position = new Vector2(randomX, randomY);
        
        Debug.Log($"Hantu spawn di posisi: {transform.position}");
    }

    void GenerateSateRequest()
    {
        requestedAmount = 1; // Hanya minta 1 sate
        
        // Log permintaan
        Debug.Log("=== HANTU MUNCUL! ===");
        Debug.Log("Hantu meminta: 1x Sate");
    }

    void HandleChasing(float distanceToPlayer)
    {
        // Cek apakah masih dalam cooldown setelah attack
        if (Time.time - lastAttackTime < chaseCooldown)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }
        
        // Kejar player jika dalam jangkauan deteksi
        if (distanceToPlayer <= detectionRange)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.linearVelocity = direction * chaseSpeed;
            
            // Update animasi berdasarkan arah gerakan
            UpdateAnimation(direction);
            
            // Serang jika dalam jangkauan
            if (distanceToPlayer <= attackRange)
            {
                AttackPlayer();
            }
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }
    
    void UpdateAnimation(Vector2 moveDirection)
    {
        if (Animator == null) return;
        
        // Simpan direction untuk digunakan nanti
        if (moveDirection.magnitude > 0.1f)
        {
            lastMoveDirection = moveDirection;
        }
        
        // Tentukan arah dominan
        if (Mathf.Abs(moveDirection.x) > Mathf.Abs(moveDirection.y))
        {
            // Horizontal lebih dominan
            if (moveDirection.x > 0)
            {
                Animator.Play("Jalan Kanan");
            }
            else
            {
                Animator.Play("Jalan Kiri");
            }
        }
        else
        {
            // Vertical lebih dominan
            if (moveDirection.y > 0)
            {
                Animator.Play("Jalan Atas");
            }
            else
            {
                Animator.Play("Jalan Bawah");
            }
        }
    }

    void AttackPlayer()
    {
        if (Time.time - lastAttackTime >= chaseCooldown)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
                Debug.Log($"Hantu menyerang! Player kehilangan {attackDamage} HP");
            }
            
            lastAttackTime = Time.time;
            
            // Masuk cooldown mode
            currentState = GhostState.Cooldown;
            cooldownTimer = cooldownDuration;
            rb.linearVelocity = Vector2.zero;
            
            Debug.Log("Hantu sedang cooldown...");
        }
    }

    void HandleCooldown()
    {
        cooldownTimer -= Time.deltaTime;
        
        if (cooldownTimer <= 0)
        {
            currentState = GhostState.Chasing;
            Debug.Log("Hantu mulai mengejar lagi!");
        }
    }

    public void ReceiveSate()
    {
        if (requestedAmount > 0)
        {
            requestedAmount = 0;
            Debug.Log("Hantu menerima sate!");
            SatisfyGhost();
        }
    }
    
    public int GetRequestedAmount()
    {
        return requestedAmount;
    }

    void SatisfyGhost()
    {
        currentState = GhostState.Satisfied;
        rb.linearVelocity = Vector2.zero;
        
        Debug.Log("Hantu puas! Hantu menghilang...");
        
        // Bisa tambahkan efek visual/animasi hilang
        Destroy(gameObject, 1f);
    }

    public GhostState GetCurrentState()
    {
        return currentState;
    }

    // Visual debugging
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}