using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float spinSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        rb.angularVelocity = spinSpeed;
    }
}
