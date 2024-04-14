using System.Collections;
using UnityEngine;

public class PlayerKeyboardMovement : MonoBehaviour
{
    [Header("References")]
    /* Cameras */
    [SerializeField] Camera cam;


    [Header("Values")]
    /* Floats and Ints */
    [SerializeField] float rotationSpeed = 5.0f;
    [SerializeField] float rotateLimit = 45.0f;
    [SerializeField] float normalizedSpeed = 2f;
    [SerializeField] float minX = 0.25f;
    [SerializeField] float maxX = 0.75f;
    private float xMovement;
    private float yMovement;
    public float delayInitial = 1f;
    /* Booleans */
    bool delayComplete = false;
    /* Vectors */
    Vector2 moveDir;


    [Header("Scripts")]
    private PlayOption playStatus;

    void Start()
    {
        GameObject playButton = GameObject.Find("Play Button");
        if (playButton != null )
        {
            playStatus = playButton.GetComponent<PlayOption>();
        }
        else
        {
            Debug.LogError("Play Button not found");
        }
    }

    void Update()
    {
        if (playStatus.started)
        {
            if (!delayComplete)
            {
                StartCoroutine(delayMovement());
            }
            else
            {
                Movement();
            }
        }
    }

    void Movement()
    {
        xMovement = Input.GetAxisRaw("Horizontal"); // Takes in input such as "A, D, left arrow, right arrow" [customizable] to move LEFT or RIGHT.
        yMovement = Input.GetAxisRaw("Vertical"); // Takes in input such as "W, S, up arrow, down arrow" [customizable] to move UP or DOWN.
        moveDir = new Vector2(xMovement, yMovement).normalized;

        Vector2 newPosition = (Vector2)transform.position + new Vector2(moveDir.x * normalizedSpeed * Time.deltaTime, moveDir.y * normalizedSpeed * Time.deltaTime); // Calculate the new position of players.
        Vector2 viewportPos = cam.WorldToViewportPoint(newPosition); // Convert the player's position in the game world to viewport coordinates (viewport can be understood as camera).

        viewportPos.x = Mathf.Clamp(viewportPos.x, minX, maxX); // Clamping the player within the X Axis  // The reason I use "Mathf.Clamp01" is because it return value between 0 and 1, and ultimately,
        viewportPos.y = Mathf.Clamp01(viewportPos.y); // Clamping the player within the Y Axis //  viewport returns coordinates between (0,0) and (1,1), which is also 0 and 1.

        Vector2 worldPos = cam.ViewportToWorldPoint(viewportPos); // After the clamping process, the game needs to know the player's gameobject position within the game world, thus using ViewportToWorldPoint.
        transform.position = worldPos; // Updating the player's new position.

        //////// Rotation of the player's plane ////////
        float desiredRotation = rotateLimit * xMovement;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, -desiredRotation);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        //////////////////////////////////////////////////////////////////////////////////////////////////////////
    }

    IEnumerator delayMovement()
    {
        yield return new WaitForSeconds(delayInitial);
        delayComplete = true;
    }
}




