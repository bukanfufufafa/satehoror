using UnityEngine;

public class grab : MonoBehaviour
{
    public GameObject Objectinrange;
    public GameObject ObjectGrabbed;
    public GameObject furnitureInRange;
    public Transform held;
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E) && ObjectGrabbed == null) 
        {
            grabbing();
        }
        else if (Input.GetKeyUp(KeyCode.E) && ObjectGrabbed != null && furnitureInRange != null)
        {
            if (furnitureInRange.TryGetComponent(out Garbage garbage))
            {
                dump();
            }
            else if (furnitureInRange.TryGetComponent(out Stove stove))
            {
                cook();
            }
        }

    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.TryGetComponent(out Item item) && other.gameObject != ObjectGrabbed)
        {
            Objectinrange = other.gameObject;
            Debug.Log("barang di deteksi");
        }
        if (other.TryGetComponent(out Furniture furniture))
        {
            furnitureInRange = other.gameObject;
            Debug.Log("barang di deteksi");
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
            dagingmentah.grabbable = false;
            stove.daging = ObjectGrabbed;
            ObjectGrabbed = null;   


        }
    }


}
