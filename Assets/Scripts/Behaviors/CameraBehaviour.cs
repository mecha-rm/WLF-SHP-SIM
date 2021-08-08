using UnityEngine;

// controls the camera
public class CameraBehaviour : MonoBehaviour
{
    // enables or disables the camera controls.
    public bool cameraLock = false; // locks the camera if 'true'

    [Header("Control Speed")]
    // vectors for movement and rotation.
    public Vector3 movementSpeed = new Vector3(30.0F, 30.0F, 30.0F);
    public Vector3 rotationSpeed = new Vector3(45.0F, 45.0F, 45.0F);

    // reset position and orientation.
    private Vector3 defaultPosition;
    private Quaternion defaultRotation;

    // Start is called before the first frame update
    void Start()
    {
        // gets the default position and rotation
        defaultPosition = transform.position;
        defaultRotation = transform.rotation;
    }

    // translates the camera
    public void TranslateCamera(Vector3 translate)
    {
        gameObject.transform.Translate(translate);
    }

    // translates the camera
    public void TranslateCamera(float x, float y, float z)
    {
        gameObject.transform.Translate(x, y, z);
    }

    // translates the camera along the x-axis
    public void TranslateCameraX(float x)
    {
        // translate along x
        gameObject.transform.Translate(x, 0, 0);
    }

    // translates the camera along the y-axis
    public void TranslateCameraY(float y)
    {
        // translate along y
        gameObject.transform.Translate(0, y, 0);
    }

    // translates the camera along the z-axis
    public void TranslateCameraZ(float z)
    {
        // translate along z
        gameObject.transform.Translate(0, 0, z);
    }

    // rotates the camera
    public void RotateCamera(Vector3 euler)
    {
        gameObject.transform.Rotate(euler);
    }

    // rotates the camera on the x-axis
    public void RotateCameraX(float x)
    {
        gameObject.transform.Rotate(x, 0, 0);
    }

    // rotates the camera on the y-axis
    public void RotateCameraY(float y)
    {
        gameObject.transform.Rotate(0, y, 0);
    }

    // rotates the camera on the z-axis
    public void RotateCameraZ(float z)
    {
        gameObject.transform.Rotate(0, 0, z);
    }

    // Update is called once per frame
    void Update()
    {
        // checks to see if the camera is locked. If it isn't, it will move.
        if (!cameraLock)
        {
            // Movement of the Camera //
            // forward movement and backward movement
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(new Vector3(0, 0, movementSpeed.z * Time.deltaTime));
            }
            else if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(new Vector3(0, 0, -movementSpeed.z * Time.deltaTime));
            }

            // leftward and rightward movement
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(new Vector3(-movementSpeed.x * Time.deltaTime, 0, 0));
            }
            else if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(new Vector3(movementSpeed.x * Time.deltaTime, 0, 0));
            }

            // upward movmenet and downward movement
            if (Input.GetKey(KeyCode.Q))
            {
                transform.Translate(new Vector3(0, movementSpeed.y * Time.deltaTime, 0));
            }
            else if (Input.GetKey(KeyCode.E))
            {
                transform.Translate(new Vector3(0, -movementSpeed.y * Time.deltaTime, 0));
            }


            // Rotation of the Camera //
            // x-axis rotation
            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.Rotate(Vector3.right, -rotationSpeed.x * Time.deltaTime);
                // transform.Rotate(-rotationSpeed.x * Time.deltaTime, 0, 0);
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.Rotate(Vector3.right, +rotationSpeed.x * Time.deltaTime);
                // transform.Rotate(+rotationSpeed.x * Time.deltaTime, 0, 0);
            }

            // y-axis rotation
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Rotate(Vector3.up, -rotationSpeed.y * Time.deltaTime);
                // transform.Rotate(0, -rotationSpeed.y * Time.deltaTime, 0);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Rotate(Vector3.up, +rotationSpeed.y * Time.deltaTime);
                // transform.Rotate(0, +rotationSpeed.y * Time.deltaTime, 0);
            }

            // z-axis rotation
            if (Input.GetKey(KeyCode.PageUp))
            {
                transform.Rotate(Vector3.forward, -rotationSpeed.z * Time.deltaTime);
                // transform.Rotate(0, 0, -rotationSpeed.z * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.PageDown))
            {
                transform.Rotate(Vector3.forward, +rotationSpeed.z * Time.deltaTime);
                // transform.Rotate(0, 0, +rotationSpeed.z * Time.deltaTime);
            }
        }

        // resets values
        // resets the camera's position to what it was when the project was first ran.
        if (Input.GetKey(KeyCode.T))
        {
            transform.position = defaultPosition;
        }

        // resets the camera's orientation to what it was when the project was first ran.
        if (Input.GetKey(KeyCode.R))
        {
            transform.rotation = defaultRotation;
        }
    }
}
