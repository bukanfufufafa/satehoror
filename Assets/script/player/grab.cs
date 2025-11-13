using TMPro;
using UnityEngine;
using static sate;

public class grab : MonoBehaviour
{
    public GameObject Objectinrange;
    public GameObject ObjectGrabbed;
    public GameObject furnitureInRange;
    public Transform held;

    [SerializeField] private float deliveryRange = 2f;



    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyUp(KeyCode.K) && ObjectGrabbed == null)
        {
            grabbing();
        }
        else if (Input.GetKeyUp(KeyCode.K) && ObjectGrabbed != null && furnitureInRange != null)
        {
            if (furnitureInRange.TryGetComponent(out Garbage garbage))
            {
                dump();
            }
            else if (furnitureInRange.TryGetComponent(out Stove stove))
            {
                cook();
            }
            else if (furnitureInRange.TryGetComponent(out table Table))
            {
                if (Table.taro == null) { PutIntable(); }

            }
        } else if (Input.GetKeyUp(KeyCode.K) && ObjectGrabbed != null) {
            if (ObjectGrabbed.TryGetComponent(out sate sate))
            {
                Debug.Log("Sate!");
                TryDeliverSate(sate.dagingSate);
            }
        }

    }

    public void OnTriggerStay2D(Collider2D other)
    {
        GameObject detectedObject = other.gameObject;
        Debug.Log("Triggered by: " + detectedObject.name);


        if (other.TryGetComponent(out Item item) && detectedObject != ObjectGrabbed)
        {

            Objectinrange = detectedObject;
            Debug.Log("barang di deteksi");

        }
        if (other.TryGetComponent(out Furniture furniture))
        {

            furnitureInRange = detectedObject;
            Debug.Log("barang di deteksi");
        }


        if (detectedObject.CompareTag("Hewan"))
        {
            Debug.Log("Hewan entered the trigger!");

            if (Input.GetMouseButton(0))
            {
                detectedObject.transform.parent.SendMessage("HitSlay");
            }
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {

        Objectinrange = null;
        furnitureInRange = null;
        Debug.Log("barang keluar");
    }

    public void grabbing()
    {
        if (Objectinrange == null) return;

        if (Objectinrange.TryGetComponent(out dagingmentah Dagingmentah) && Dagingmentah.grabbable == false)
        {
            return;
        }
        if (Objectinrange != null && ObjectGrabbed == null)
        {
            ObjectGrabbed = Objectinrange;
            ObjectGrabbed.transform.position = held.transform.position;
            ObjectGrabbed.transform.SetParent(held.transform, true);
        }
        if (furnitureInRange != null && furnitureInRange.TryGetComponent(out Stove stove))
        {
            stove.grabbed();
        }
        if (furnitureInRange != null && furnitureInRange.TryGetComponent(out table Table))
        {
            Table.grabbed();
        }

    }

    public void dump()
    {

        if (ObjectGrabbed != null)
        {
            Destroy(ObjectGrabbed);
        }
    }

    public void cook()
    {
        if (ObjectGrabbed != null && furnitureInRange.TryGetComponent(out Stove stove) && ObjectGrabbed.TryGetComponent(out dagingmentah dagingmentah))
        {
            if (stove.daging == null)
            {
                dagingmentah.grabbable = false;
                stove.daging = ObjectGrabbed;
                ObjectGrabbed = null;

            }


        }
    }

    public void PutIntable()
    {
        if (ObjectGrabbed != null && furnitureInRange.TryGetComponent(out table Table))
        {
            Table.taro = ObjectGrabbed;
            ObjectGrabbed = null;


        }
    }

    public void message()
    {

    }

    void TryDeliverSate(SateType sateType)
    {
        // Cari hantu terdekat
        GhostAI[] ghosts = FindObjectsOfType<GhostAI>();
        GhostAI nearestGhost = null;
        float nearestDistance = deliveryRange;

        foreach (GhostAI ghost in ghosts)
        {
            float distance = Vector2.Distance(transform.position, ghost.transform.position);
            if (distance < nearestDistance)
            {
                nearestGhost = ghost;
                nearestDistance = distance;
            }
        }

        if (nearestGhost != null)
        {
            nearestGhost.ReceiveSate();
            dump();
        }
        else
        {
            Debug.Log("Tidak ada hantu di dekat sini!");
        }
    }
}
