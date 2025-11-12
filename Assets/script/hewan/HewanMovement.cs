

using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
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

    public TextMeshProUGUI DebugText;

    public Animator Animator;

    public float Speed;

    // =====================================

    // float suspicionLevel = 0f;
    HewanState state = HewanState.Idle;

    DateTime lastStateTime;

    Vector3 movementDirection;

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
        DebugText.text = $"Distance: {distance}";

        DateTime nowTime = DateTime.Now;
        var differenceTime = nowTime - lastStateTime;
        // DebugText.text = $"Distance: {distance}";

        Debug.Log($"diff: {differenceTime.Seconds}, state: {state}");

        if (state == HewanState.Idle)
        {
            HandleIdle(nowTime, differenceTime);
        }
        else if (state == HewanState.Walking)
        {
            HandleWalking(nowTime, differenceTime);
        }
    }

    void HandleIdle(DateTime nowTime, TimeSpan timeDifference)
    {
        if (timeDifference.Seconds > 2)
        {
            state = HewanState.Walking;
            lastStateTime = nowTime;
        }
    }

    void HandleWalking(DateTime nowTime, TimeSpan timeDifference)
    {
        if (timeDifference.Seconds > 3)
        {
            state = HewanState.Idle;
            lastStateTime = nowTime;
            movementDirection = Vector3.zero;
            Animator.SetFloat("MoveX", 0f);
            Animator.SetFloat("MoveY", 0f);
            return;
        }

        if (movementDirection == Vector3.zero)
        {
            float random = UnityEngine.Random.Range(0f, 10f);
            if (random < 2.5f)
            {
                movementDirection = new Vector3(-1, 0);
                // Animator.SetFloat("MoveX", -1f);
            }
            else if (random < 5f)
            {
                movementDirection = new Vector3(0, -1);
                // Animator.SetFloat("MoveY", -1f);
            }
            else if (random < 7.5f)
            {
                movementDirection = new Vector3(1, 0);
                // Animator.SetFloat("MoveX", 1f);
            }
            else if (random < 10f)
            {
                movementDirection = new Vector3(0, 1);
                // Animator.SetFloat("MoveY", 1f);
            }
        }

        Debug.Log($"Dir: {movementDirection}");

        Rigidbody rb = GetComponent<Rigidbody>();
        Vector3 movement = movementDirection * Speed * Time.fixedDeltaTime;

                Animator.SetFloat("MoveX", movement.x);
                Animator.SetFloat("MoveY", movement.y);


        rb.MovePosition(rb.position + movement);
    }
}