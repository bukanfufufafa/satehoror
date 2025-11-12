

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HewanMovement : MonoBehaviour
{
    public Transform Player;

    public TextMeshProUGUI DebugText;

    // =====================================

    float suspicionLevel = 0f;

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
    }
}