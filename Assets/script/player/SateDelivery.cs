// using UnityEngine;

// public class SateDelivery : MonoBehaviour
// {
//     [Header("Inventory (contoh)")]
//     [SerializeField] private int sateKambing = 0;
//     [SerializeField] private int sateAyam = 0;
//     [SerializeField] private int sateKelinci = 0;
    
//     [Header("Delivery Settings")]
//     [SerializeField] private float deliveryRange = 2f;

//     void Update()
//     {
//         // Contoh: tekan tombol untuk kasih sate ke hantu terdekat
//         if (Input.GetKeyDown(KeyCode.G))
//         {
//             TryDeliverSate();
//         }
//     }
    
//     void TryDeliverSate()
//     {
//         // Cari hantu terdekat
//         GhostAI[] ghosts = FindObjectsOfType<GhostAI>();
//         GhostAI nearestGhost = null;
//         float nearestDistance = deliveryRange;
        
//         foreach (GhostAI ghost in ghosts)
//         {
//             float distance = Vector2.Distance(transform.position, ghost.transform.position);
//             if (distance < nearestDistance)
//             {
//                 nearestGhost = ghost;
//                 nearestDistance = distance;
//             }
//         }
        
//         if (nearestGhost != null)
//         {
//             // Lihat apa yang diminta hantu
//             var requests = nearestGhost.GetSateRequest();
            
//             // Coba kasih sate yang diminta
//             foreach (var request in requests)
//             {
//                 SateType type = request.Key;
//                 int needed = request.Value;
                
//                 int available = GetSateCount(type);
                
//                 if (available > 0)
//                 {
//                     int toGive = Mathf.Min(available, needed);
//                     RemoveSate(type, toGive);
//                     nearestGhost.ReceiveSate(type, toGive);
//                     return;
//                 }
//             }
            
//             Debug.Log("Tidak punya sate yang diminta hantu!");
//         }
//         else
//         {
//             Debug.Log("Tidak ada hantu di dekat sini!");
//         }
//     }
    
//     int GetSateCount(SateType type)
//     {
//         return type switch
//         {
//             SateType.Kambing => sateKambing,
//             SateType.Ayam => sateAyam,
//             SateType.Kelinci => sateKelinci,
//             _ => 0
//         };
//     }
    
//     void RemoveSate(SateType type, int amount)
//     {
//         switch (type)
//         {
//             case SateType.Kambing:
//                 sateKambing -= amount;
//                 break;
//             case SateType.Ayam:
//                 sateAyam -= amount;
//                 break;
//             case SateType.Kelinci:
//                 sateKelinci -= amount;
//                 break;
//         }
//     }
    
//     public void AddSate(SateType type, int amount)
//     {
//         switch (type)
//         {
//             case SateType.Kambing:
//                 sateKambing += amount;
//                 break;
//             case SateType.Ayam:
//                 sateAyam += amount;
//                 break;
//             case SateType.Kelinci:
//                 sateKelinci += amount;
//                 break;
//         }
        
//         Debug.Log($"Mendapat {amount}x Sate {type}!");
//     }
// }