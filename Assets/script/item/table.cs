using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class table : Furniture
{
    public GameObject taro;

    void Update()
    {

        if (taro != null)
        {
            taro.transform.SetParent(transform, true);
            taro.transform.position = transform.position;
        }


    }

    public void grabbed()
    {
        taro = null;
    }
}
