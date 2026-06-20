using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private bool canJump;
    [SerializeField] private Transform grabPoint;

    private InputAction jump;

    private void OnEnable()
    {
        jump = InputSystem.actions.FindAction("Jump");

        jump.Enable();
        jump.performed += Jump;
    }

    void Jump(InputAction.CallbackContext context)
    {
        if (canJump)
        {
            rb.AddForce(rb.linearVelocity.normalized * 10, ForceMode2D.Impulse);
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
            grabPoint = collision.gameObject.GetComponentInChildren<Transform>();
            //transform.position = grabPoint.localToWorldMatrix.GetPosition();
            //transform.position = grabPoint.position;
            rb.position = grabPoint.localToWorldMatrix.GetPosition();
        }
    }
}
