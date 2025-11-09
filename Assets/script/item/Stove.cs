using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Stove : Furniture
{
    public GameObject satekambing;
    public GameObject satekelinci;
    public GameObject sateayam;
    public GameObject daging;
    [SerializeField] private float timeCook = 5f;
    private float timeDefault;
    void Start()
    {
        timeDefault = timeCook;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (daging != null) 
        {
            daging.transform.SetParent(transform, true);
            daging.transform.position = transform.position;
            if (daging.TryGetComponent(out dagingmentah dagingmentah))
            {
                cooking();
            }
        }


    }

    public void cooking()
    {
        
        timeCook -= Time.deltaTime;
        if (timeCook <= 0f)
        {
            if (daging.TryGetComponent(out dagingmentah dagingmentah))
            {
                if (dagingmentah.daging == dagingmentah.tipeDaging.ayam && sateayam != null)
                {
                    Destroy(daging);
                    daging = Instantiate(sateayam, transform.position, transform.rotation);
                    cooked();
                }
                else if (dagingmentah.daging == dagingmentah.tipeDaging.kelinci && satekelinci != null)
                {
                    Destroy(daging);
                    daging = Instantiate(satekelinci, transform.position, transform.rotation);
                    cooked();
                }
                else if (dagingmentah.daging == dagingmentah.tipeDaging.kambing && satekambing != null)
                {
                    Destroy(daging);
                    daging = Instantiate(satekambing, transform.position, transform.rotation);
                    cooked();
                }

            }
            
            
        }
    }

    

    public void grabbed()
    {
        if (daging.TryGetComponent(out sate Sate) && daging != null)
        {
            daging = null;
        }
    }

    public void cooked()
    {
        timeCook = timeDefault;
    }
}
