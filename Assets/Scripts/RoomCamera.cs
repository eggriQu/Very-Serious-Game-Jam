using UnityEngine;

public class RoomCamera : MonoBehaviour
{
    [SerializeField] private GameObject cameraObj;
    [SerializeField] private Vector2 respawnPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cameraObj.gameObject.SetActive(true);
            PlayerController player = other.GetComponent<PlayerController>();
            player.SetRespawnPos(respawnPos);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cameraObj.gameObject.SetActive(false);
        }
    }
}
