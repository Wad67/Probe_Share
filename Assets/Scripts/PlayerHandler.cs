using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;
using UnityEngine.InputSystem.Controls;

public class PlayerHandler : NetworkBehaviour
{
    public InputAction Movement;
    public InputAction CameraMovementButtons;
    public InputAction CameraMovementMouse;
    public InputAction StabilizerToggle;
    public InputAction MouseActions;
    private bool StabilizerState;
    private Vector3 rotationalOffset;

    public float CameraMoveSpeed = 1.0f;
    public float MoveSpeedMultiplier = 1.0f;
    Vector3 Input;
    Vector3 CamInputButtons;
    Vector2 CamInputMouse;
    Rigidbody rb;

    public Camera FrontCamera;
    public Camera ThirdPCamera;
    public Camera Cockpit;


    public override void OnStartAuthority()
    {
        base.OnStartAuthority();

        Movement.Enable();
        CameraMovementButtons.Enable();
        CameraMovementMouse.Enable();
        StabilizerToggle.Enable();
       //FrontCamera.enabled = true;
       //ThirdPCamera.enabled = true;
        Cockpit.enabled = true;
        MouseActions.Enable();



    }
    void Start()
    {
        Movement.Enable();
        CameraMovementButtons.Enable();
        CameraMovementMouse.Enable();
        StabilizerToggle.Enable();
        MouseActions.Enable();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {


        if (!isLocalPlayer)
        {
            Movement.Disable();
            CameraMovementButtons.Disable();
            CameraMovementMouse.Disable();
            StabilizerToggle.Disable();
        }
        else
        {
            Movement.Enable();
            CameraMovementButtons.Enable();
            CameraMovementMouse.Enable();
            StabilizerToggle.Enable();
        }

        if (Movement.inProgress)
            {

                Input = Movement.ReadValue<Vector3>();
                rb.AddRelativeForce(Input * Time.deltaTime * MoveSpeedMultiplier);

            }
        if (Mouse.current.leftButton.isPressed)
        {
            CamInputButtons = CameraMovementButtons.ReadValue<Vector3>();
            CamInputMouse = CameraMovementMouse.ReadValue<Vector2>();

            CamInputButtons.y -= CamInputMouse.x;
            CamInputButtons.x += CamInputMouse.y;
            CamInputButtons = CamInputButtons * CameraMoveSpeed;

            CamInputButtons = CamInputButtons * Time.deltaTime;

            Cockpit.transform.Rotate(CamInputButtons);
        }
        else
        {
            Cockpit.transform.rotation = Quaternion.Lerp(Cockpit.transform.rotation, gameObject.transform.rotation, Time.deltaTime * 2);

            Vector2Control v2c = Mouse.current.scroll;
            if (v2c.ReadValue() != new Vector2(0, 0) && Cockpit.transform.rotation == transform.rotation)
            {
                Vector2 val = v2c.ReadValue();
                Cockpit.transform.Translate(new Vector3(0, 0, val[1]) / 4 * Time.deltaTime, Space.Self);
            }

        }
            if (Mouse.current.rightButton.isPressed)
        {

            if (CameraMovementButtons.inProgress || CameraMovementMouse.inProgress)
            {
                CamInputButtons = CameraMovementButtons.ReadValue<Vector3>();
                CamInputMouse = CameraMovementMouse.ReadValue<Vector2>();

                CamInputButtons.x += CamInputMouse.x;
                CamInputButtons.y += CamInputMouse.y;
                CamInputButtons = CamInputButtons * CameraMoveSpeed;
                rb.AddRelativeTorque(CamInputButtons.y * -1, CamInputButtons.x, CamInputButtons.z * 20);

            }
        }
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {   

            Ray ray = Cockpit.ScreenPointToRay(new Vector3(Mouse.current.position.x.ReadValue(),Mouse.current.position.y.ReadValue(),0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                print(hit);
                hit.collider.SendMessage("ProcessActivation");
            }
        }

        StabilizerToggle.performed += ctx =>
            {

                if (StabilizerState)
                {
                    print("STABILIZER 0");
                    StabilizerState = false;
                }
                else
                {
                    print("STABILIZER 1");
                    StabilizerState = true;
                }
            };

            if (StabilizerState)
            {
                Quaternion rotation = rb.rotation;

                if (rotation.x > 0)
                {
                    rotationalOffset.x = 1;
                }
                else if (rotation.x < 0)
                {
                    rotationalOffset.x = -1;
                }

                if (rotation.y > 0)
                {
                    rotationalOffset.y = 1;
                }
                else if (rotation.y < 0)
                {
                    rotationalOffset.y = -1;
                }

                if (rotation.z > 0)
                {
                    rotationalOffset.z = 1;
                }
                else if (rotation.z < 0)
                {
                    rotationalOffset.z = -1;
                }
                print("STABILIZER SYSTEM ACTIVE");
                print("|");
                print("ROTATION:");
                print(rotation);
                print("|");
                print("CORRECTION:");
                print(rotationalOffset);
                print("STABILIZER SYSTEM ACTIVE");

            }
        

        }

    }




    


    

