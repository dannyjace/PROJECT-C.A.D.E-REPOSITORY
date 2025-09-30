using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("CAMERA CONTROLLER SETTINGS")]
    [Space(10)]
    [SerializeField] [Range(0, 250)] private int sensitivity;
    [Space(10)]
    [SerializeField] [Range(-90, 0)] private int xRotationMin;
    [SerializeField] [Range(0, 90)] private int xRotationMax;
    [Space(10)]
    [SerializeField] private bool invertY;


    private float targetFieldOfView;
    private float fieldOfViewSpeed;
    private float targetCameraHeight;
    private float targetCameraHeightSpeed;
    private float xRotation;
    private float zRotation;


    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        UpdateCameraLook();
        UpdateCameraFOV();
        UpdateCameraHeight();
    }

    private void Initialize()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void UpdateCameraLook()
    {
        // calculate and store look input //
        float lookX = Input.GetAxisRaw("Mouse X") * sensitivity * Time.deltaTime;
        float lookY = Input.GetAxisRaw("Mouse Y") * sensitivity * Time.deltaTime;

        //storing horizontal movement input for zRotation processing //
        float moveX = Input.GetAxis("Horizontal");

        // use invertY to give options to look up/down //
        if (invertY)
        {
            xRotation += lookY;
        }
        else
        {
            xRotation -= lookY;
        }

        // clamp camera rotation on X axis //
        xRotation = Mathf.Clamp(xRotation, xRotationMin, xRotationMax);

        // modifying zRotation based on input //
        zRotation = Mathf.Lerp(zRotation, 0f + -moveX + lookX, Time.deltaTime * 10f);

        // clamping zRotation to prevent effect from being too intense //
        zRotation = Mathf.Clamp(zRotation, -1f, 1f);

        // rotate the camera to look up and down //
        transform.localRotation = Quaternion.Euler(xRotation, 0, zRotation);

        // rotate the player to look left and right //
        transform.parent.Rotate(Vector3.up * lookX);
    }

    private void UpdateCameraFOV()
    {
        // lerping the current camera FOV to the target camera FOV //
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, targetFieldOfView, Time.deltaTime * fieldOfViewSpeed);
    }

    private void UpdateCameraHeight()
    {
        // lerping the current camera height to the target camera height //
        transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(0, targetCameraHeight, 0), Time.deltaTime * targetCameraHeightSpeed);
    }

    public void SetFieldOfView(float fov, float speed)
    {
        // used to set target FOV values //
        targetFieldOfView = fov;
        fieldOfViewSpeed = speed;
    }

    public void SetCameraHeight(float height, float speed)
    {
        // used to set target height values //
        targetCameraHeight = height;
        targetCameraHeightSpeed = speed;
    }
}
