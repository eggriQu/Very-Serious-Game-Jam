using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private bool canJump;
    [SerializeField] private Transform grabPoint;
    [SerializeField] private float launchForce;

    private InputAction jump;

    private void OnEnable()
    {
        jump = InputSystem.actions.FindAction("Jump");

        jump.Enable();
        jump.performed += Jump;
    }

    void Jump(InputAction.CallbackContext context)
    {
        if (canJump && grabPoint != null)
        {
            Vector2 launchVelocity = grabPoint.right.normalized;
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(launchVelocity * launchForce, ForceMode2D.Impulse);
            canJump = false;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Grab Blade"))
        {
            canJump = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Grab Blade") && canJump)
        {
            rb.linearVelocityY = 0;
            grabPoint = collision.transform.GetChild(0);
            transform.position = grabPoint.position;
            Vector3 grabRotation = grabPoint.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(grabRotation.x, grabRotation.y, grabRotation.z - 90);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Grab Blade") && canJump)
        {
            canJump = false;
        }
    }
}
