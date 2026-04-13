using UnityEngine;

public class playerController : MonoBehaviour
{
    [SerializeField] private float basePlayerSpeed;
    [SerializeField] private float currentPlayerSpeed;
    [SerializeField] private float rotationRate; //horizontal sens
    [SerializeField] private float vertRotateRate; //vert sens
    private Transform playerCamera;
    private float vertConstraints; //constraints for vert camera looking
    private Rigidbody rb;
    private float sprintMod = 2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerCamera = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //player movement
        Vector3 playerVelocity = gameObject.transform.rotation * new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")) * currentPlayerSpeed;
        rb.linearVelocity = playerVelocity;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            currentPlayerSpeed = currentPlayerSpeed * sprintMod;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            currentPlayerSpeed = basePlayerSpeed;
        }


        //camera
        gameObject.transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * rotationRate);//horizontal
        vertConstraints = Mathf.Clamp(Input.GetAxis("Mouse Y") * -vertRotateRate + vertConstraints, -60f, 60f);
        playerCamera.localRotation = Quaternion.Euler(new Vector3(vertConstraints, 0f, 0f)); //probably vert
    }
}