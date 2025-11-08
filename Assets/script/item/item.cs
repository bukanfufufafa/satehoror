using UnityEngine;

public class item : MonoBehaviour
{
    public string namaItem;
    public SpriteRenderer gambar;

    private void Start()
    {
        gambar = GetComponent<SpriteRenderer>();
    }
}
