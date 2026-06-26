using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private bool canJump;
    [SerializeField] private Transform grabPoint;
    [SerializeField] private float launchForce;
    [SerializeField] private Animator spriteAnim;

    private bool justJumped;

    private Vector2 spawnPos;
    [SerializeField] private Vector2 currentVelocity;

    private InputAction jump;
    private InputAction pause;

    private void OnEnable()
    {
        jump = InputSystem.actions.FindAction("Jump");
        pause = InputSystem.actions.FindAction("Pause");

        jump.Enable();
        jump.performed += Jump;

        pause.Enable();
        pause.performed += PauseResume;
    }

    void Jump(InputAction.CallbackContext context)
    {
        if (!GameManager.instance.levelStarted && canJump && grabPoint == null)
        {
            rb.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
            canJump = false;
            GameManager.instance.levelStarted = true;
        }
        else if (GameManager.instance.levelStarted && canJump && grabPoint != null)
        {
            Vector2 launchVelocity = grabPoint.right.normalized;
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0;
            rb.AddForce(launchVelocity * launchForce, ForceMode2D.Impulse);
            canJump = false;
            StartCoroutine(KeepRotation());
        }
        spriteAnim.Play("Jump");
    }

    void PauseResume(InputAction.CallbackContext context)
    {
        if (!GameManager.instance.isPaused)
        {
            currentVelocity = rb.linearVelocity;
            GameManager.instance.PauseGame();
        }
        else
        {
            rb.linearVelocity = currentVelocity;
            GameManager.instance.ResumeGame();
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteAnim.Play("Idle");
    }

    // Update is called once per frame
    void Update()
    {
        if (justJumped)
        {
            rb.angularVelocity = 0;
        }
        else
        {
            
        }

        if (GameManager.instance.isPaused)
        {
            rb.linearVelocity = Vector2.zero;
            rb.gravityScale = 0;
        }
        else
        {
            rb.gravityScale = 1.4f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Grab Blade"))
        {
            canJump = true;
        }

        if (collision.CompareTag("Death"))
        {
            Die();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Grab Blade") && canJump && !GameManager.instance.isPaused)
        {
            rb.linearVelocityX = 0;
            rb.linearVelocityY = 0;
            grabPoint = collision.transform.GetChild(0);
            transform.position = grabPoint.position;
            Vector3 grabRotation = grabPoint.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(grabRotation.x, grabRotation.y, grabRotation.z - 90);
        }
        else
        {
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Grab Blade") && canJump)
        {
            canJump = false;
            rb.angularVelocity = 0;
            StartCoroutine(KeepRotation());
        }
    }

    public void SetRespawnPos(Vector2 pos)
    {
        spawnPos = pos;
    }

    void Die()
    {
        spriteAnim.Play("Idle");
        transform.position = spawnPos;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0;
        rb.rotation = 0;
        grabPoint = null;
        GameManager.instance.levelStarted = false;
        canJump = true;
    }

    public void Restart()
    {
        GameManager.instance.ResumeGame();
        Die();
    }

    private IEnumerator KeepRotation()
    {
        justJumped = true;
        yield return new WaitForSeconds(0.5f);
        justJumped = false;
    }
}
