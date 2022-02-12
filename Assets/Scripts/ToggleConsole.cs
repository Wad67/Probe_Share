using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ToggleConsole : MonoBehaviour
{
    public InputAction ToggleAction;
    private bool positioned = true;
    Vector2 initalPos;
    // Start is called before the first frame update
    void Start()
    {
        initalPos = transform.position;
        ToggleAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {


        ToggleAction.performed += ctx =>
        {
            print("<TGL>");
            if (positioned)
            {
                transform.position = initalPos + new Vector2(0, -180);
                positioned = false;
            }
            else
            {
                transform.position = initalPos;
                positioned = true;
            }
        };



    }
}
