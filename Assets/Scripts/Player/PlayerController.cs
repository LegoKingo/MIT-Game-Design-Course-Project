using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, PlayerControls.IPlayerActions
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private Vector3 moveDirection;
    public float moveSpeed = 5f;
    private Vector2 aim;
    private Vector2 mouseAim;
    private Vector3 playerDirection;
    private float rotateSpeed = 1000f;
    public GameObject projPrefab;
    public bool gamepadConnection;
    private PlayerControls playerControls;
    private PlayerController controller;
    private PlayerInput playerInput;
    public AmmoCounter AmmoCounter;

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
        playerControls = new PlayerControls();
        playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        playerControls.Disable();
    }

    private void OnDisable()
    {
        playerControls.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
       moveDirection = transform.InverseTransformDirection(context.ReadValue<Vector3>());
       moveDirection.Normalize();


    }
    public void OnGamepadRotate(InputAction.CallbackContext context)
    {
        aim = context.ReadValue<Vector2>();
        playerDirection = Vector3.right * aim.x + Vector3.forward * aim.y;
        if (playerDirection.sqrMagnitude > 0.0f)
        {
            Quaternion newrotation = Quaternion.LookRotation(playerDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, newrotation, rotateSpeed * Time.deltaTime);
        }
        
    }
    public void OnMouseRotate(InputAction.CallbackContext context)
    {
        if (gamepadConnection == false)
        {
            mouseAim = context.ReadValue<Vector2>();
            Ray ray = Camera.main.ScreenPointToRay(mouseAim);
            Plane groundPlane = new Plane(Vector3.up, Vector3.up);
            float rayDistance;

            if (groundPlane.Raycast(ray, out rayDistance))
            {
                Vector3 point = ray.GetPoint(rayDistance);
                LookAt(point);
            }
        }
    }

    private void LookAt(Vector3 lookPoint)
    {
        Vector3 heightCorrectedPoint = new Vector3(lookPoint.x, transform.position.y, lookPoint.z);
        transform.LookAt(heightCorrectedPoint);
    }

    public void OnFire(InputAction.CallbackContext context) 
    {
        if (AmmoCounter.counter > 0)
        {
            Instantiate(projPrefab, transform.position, transform.rotation);
            AmmoCounter.AmmoCount();
        }
        else
        {
            AmmoCounter.OutOfAmmo();
        }
        
    }
    public void OnReload(InputAction.CallbackContext context)
    {
        AmmoCounter.InitializeAmmo();
    }

    public void OnDeviceChange(PlayerInput pi)
    {
        gamepadConnection = pi.currentControlScheme.Equals("Gamepad") ? true : false;

    }
 
}
