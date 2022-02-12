using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSwitch : MonoBehaviour
{
    public bool toggle;
    public Vector3 initialRot;
    // Start is called before the first frame update
    void Start()
    {
        initialRot = transform.rotation.eulerAngles;
        
    }


    public void ToggleThis()
    {
        if (toggle)
        {
            toggle = false;
        }
        else
        {
            toggle = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (toggle)
        {
            transform.rotation.eulerAngles.Set(0, 0, 90);
        }
        else
        {
            transform.rotation.eulerAngles.Set(initialRot.x,initialRot.y,initialRot.z);
        }
    }
}
