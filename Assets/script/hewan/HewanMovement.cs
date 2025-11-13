

using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;


public class HewanMovement : MonoBehaviour
{
    public enum HewanState
    {
        Idle,
        Walking,
        Fleeing
    }

    public Transform Player;

    public GameObject HealthBar;
    public GameObject HealthBarCurrent;
    // public TextMeshProUGUI DebugText;

    public Animator Animator;

    public GameObject Daging;

    public float WalkSpeed;
    public float RunSpeed;
    public float GlideSpeed;
    public int GlideTime;

    // =====================================

    // float suspicionLevel = 0f;
    HewanState state = HewanState.Idle;
    int health = 100;

    DateTime lastStateTime;

    // ------- Idle
    int idleTime;

    // ------- Walking
    int walkTime;
    Vector3 walkDirection;

    // ------ Fleeing
    DateTime lastRunDirectionTime;
    int runStep;
    Vector3 runDirection;

    bool runGlideEnabled;
    DateTime lastRunGlideTime;


    // =====================================

    public HewanMovement()
    {
    }

    void Start()
    {

    }

    void Update()
    {
        Vector3 distance = transform.position - Player.position;
        // DebugText.text = $"Distance: {distance}";

        DateTime nowTime = DateTime.Now;
        // DebugText.text = $"Distance: {distance}";

        // Debug.Log($"diff: {differenceTime.Seconds}, state: {state}");

        if (state == HewanState.Idle)
        {
            HandleIdle(nowTime);
        }
        else if (state == HewanState.Walking)
        {
            HandleWalking(nowTime);
        }
        else if (state == HewanState.Fleeing)
        {
            HandleRunning(nowTime);
        }
    }

    void HandleIdle(DateTime nowTime)
    {
        var timeDifference = nowTime - lastStateTime;

        if (timeDifference.Seconds > idleTime)
        {
            state = HewanState.Walking;
            walkTime = UnityEngine.Random.Range(1, 4);
            lastStateTime = nowTime;
        }
    }

    void HandleWalking(DateTime nowTime)
    {

        var timeDifference = nowTime - lastStateTime;

        if (timeDifference.Seconds > walkTime)
        {
            state = HewanState.Idle;
            idleTime = UnityEngine.Random.Range(1, 4);
            lastStateTime = nowTime;

            if (walkDirection.x == -1 && walkDirection.y == 0)
            {
                Animator.Play("Jalan Timur");
            }
            else if (walkDirection.x == 0 && walkDirection.y == -1)
            {
                Animator.Play("Jalan Selatan");
            }
            else if (walkDirection.x == 1 && walkDirection.y == 0)
            {
                Animator.Play("Jalan Barat");
            }
            else if (walkDirection.x == 0 && walkDirection.y == 1)
            {
                Animator.Play("Jalan Utara");
            }

            walkDirection = Vector3.zero;
            return;
        }

        if (walkDirection == Vector3.zero)
        {
            Animator.speed = 0.8f;

            float random = UnityEngine.Random.Range(0f, 10f);
            if (random < 2.5f)
            {
                walkDirection = new Vector3(-1, 0);
                Animator.Play("Lari Barat");
            }
            else if (random < 5f)
            {
                walkDirection = new Vector3(0, -1);
                Animator.Play("Lari Selatan");
            }
            else if (random < 7.5f)
            {
                walkDirection = new Vector3(1, 0);
                Animator.Play("Lari Timur");
            }
            else if (random < 10f)
            {
                walkDirection = new Vector3(0, 1);
                Animator.Play("Lari Utara");
            }
        }

        Debug.Log($"Dir: {walkDirection}");

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 movement = walkDirection * WalkSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);

        // Vector3 movement = walkDirection * WalkSpeed * Time.fixedDeltaTime;
        // transform.position = Vector3.MoveTowards(transform.position, transform.position + walkDirection, WalkSpeed * Time.deltaTime);
        // transform.position += movement;
    }

    void HandleRunning(DateTime nowTime)
    {
        var runDirectionTimeDifference = nowTime - lastRunDirectionTime;
        var runGlideTimeDifference = nowTime - lastRunGlideTime;

        if (runGlideEnabled && runGlideTimeDifference > TimeSpan.FromMilliseconds(GlideTime))
        {
            runGlideEnabled = false;
        }

        if (runDirectionTimeDifference.Seconds > 3)
        {
            if (runStep < 4)
            {
                runDirection = Vector3.zero;
                lastRunDirectionTime = nowTime;
            }
            else
            {
                runDirection = Vector3.zero;

                state = HewanState.Idle;
                idleTime = UnityEngine.Random.Range(1, 4);
                lastStateTime = nowTime;

                HealthBar.SetActive(false);
                return;
            }
        }

        if (runDirection == Vector3.zero)
        {
            float random = UnityEngine.Random.Range(0f, 10f);
            if (random < 2.5f)
            {
                runDirection = new Vector3(-1, 0);
                Animator.Play("Lari Barat");
            }
            else if (random < 5f)
            {
                runDirection = new Vector3(0, -1);
                Animator.Play("Lari Selatan");
            }
            else if (random < 7.5f)
            {
                runDirection = new Vector3(1, 0);
                Animator.Play("Lari Timur");
            }
            else if (random < 10f)
            {
                runDirection = new Vector3(0, 1);
                Animator.Play("Lari Utara");
            }
        }


        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 movement = runDirection * (RunSpeed + (runGlideEnabled ? GlideSpeed : 0f)) * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);
    }

    public void HitSlay()
    {
        DateTime nowTime = DateTime.Now;

        if (state == HewanState.Fleeing && (nowTime - lastStateTime) < TimeSpan.FromMilliseconds(300))
        {
            return;
        }

        state = HewanState.Fleeing;
        lastStateTime = DateTime.Now;
        lastRunDirectionTime = lastStateTime;
        runGlideEnabled = true;
        lastRunGlideTime = nowTime;

        HealthBar.SetActive(true);

        health -= 10;
        if (health <= 0)
        {
            Instantiate(Daging, transform.position, new Quaternion(0f, 0f, 0f, 0f));
            Destroy(gameObject);
        }
        else
        {
            // Animator.Play("Hit");
            Debug.Log($"Hit! {health}");
            HealthBarCurrent.transform.localScale = new Vector3(health / 100f, 1f, 1f);
        }
    }
}