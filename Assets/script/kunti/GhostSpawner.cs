using System.Collections.Generic;
using UnityEngine;

public class GhostSpawner : MonoBehaviour
{
    [SerializeField] private GameObject ghostPrefab;
    [SerializeField] private float spawnInterval = 30f; // Spawn setiap 30 detik
    [SerializeField] private int maxGhosts = 3; // Max hantu di map
    
    private float spawnTimer;
    private List<GameObject> activeGhosts = new List<GameObject>();
    
    void Update()
    {
        // Bersihkan list dari hantu yang sudah hilang
        activeGhosts.RemoveAll(g => g == null);
        
        spawnTimer += Time.deltaTime;
        
        // Spawn hantu baru jika waktunya tiba dan belum max
        if (spawnTimer >= spawnInterval && activeGhosts.Count < maxGhosts)
        {
            SpawnGhost();
            spawnTimer = 0f;
        }
    }
    
    void SpawnGhost()
    {
        GameObject ghost = Instantiate(ghostPrefab);
        activeGhosts.Add(ghost);
        Debug.Log($"Hantu baru spawn! Total hantu aktif: {activeGhosts.Count}");
    }
}