using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class PopUpMassage : MonoBehaviour
{
    public grab Grab;
    public TextMeshProUGUI textHeld;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Grab.Objectinrange != null && Grab.ObjectGrabbed == null)
        {
            ObjectInRange();
        }
        else if (Grab.furnitureInRange != null && Grab.ObjectGrabbed != null)
        {
            FurnitureInRange();
        }
        else { textHeld.text = ""; }
    }

    private void ObjectInRange()
    {
        if (Grab.Objectinrange.TryGetComponent(out Item item) && item.grabbable == true)
        {
            textHeld.text = "Ambil " + item.namaItem;
        }
        
        
    }

    private void FurnitureInRange()
    {
        if (Grab.furnitureInRange.TryGetComponent(out Furniture furniture) && Grab.ObjectGrabbed.TryGetComponent(out Item item))
        {
            if (item is dagingmentah && furniture is Stove && Grab.furnitureInRange.TryGetComponent(out Stove kompor))
            {
                if (kompor.daging == null)
                {
                    textHeld.text = "Bakar " + item.namaItem;
                }
                
                
            }
            if (furniture is Garbage)
            {
                textHeld.text = "Buang " + item.namaItem;
            }
            if (furniture is table && Grab.furnitureInRange.TryGetComponent(out table meja))
            {
                if (meja.taro == null)
                {
                    textHeld.text = "Simpan " + item.namaItem;
                }
                
            }
        }
        
    }
}
