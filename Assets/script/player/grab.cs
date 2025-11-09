using UnityEngine;

public class grab : MonoBehaviour
{
    public GameObject Objectinrange;
    public GameObject ObjectGrabbed;
    public Transform held;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E)) 
        {
            grabbing();
        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Item") && other.gameObject.CompareTag("Item") != ObjectGrabbed)
        {
            Objectinrange = other.gameObject;
            Debug.Log("barang di deteksi");
        }

    }
    
    public void OnTriggerExit2D(Collider2D other)
    {
        Objectinrange = null;
        Debug.Log("barang keluar");
    }

    public void grabbing()
    {
        if (Objectinrange != null)
        {
            ObjectGrabbed = Objectinrange;
            ObjectGrabbed.transform.position = held.transform.position;
            ObjectGrabbed.transform.SetParent(held.transform, true);
        }
        
        

    }




}
