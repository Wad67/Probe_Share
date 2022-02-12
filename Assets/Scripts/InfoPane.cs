using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPane : MonoBehaviour
{
     bool IsEnabled;

    // Start is called before the first frame update
    void Start()
    {
        IsEnabled = true;
    }
    public void Toggle()
    {
        print("ass");
        if (IsEnabled)
        {
            GetComponent<Renderer>().enabled = false;
            IsEnabled = false;
        }
        else
        {
            GetComponent<Renderer>().enabled = true;
            IsEnabled = true;
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
