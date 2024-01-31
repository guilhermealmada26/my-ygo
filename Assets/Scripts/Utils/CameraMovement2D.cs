using UnityEngine;

public class CameraMovement2D : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rect borderRect;
    public float zoomSpeed = 3f;
    public Vector2 zoomMinMax;

    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void OnDrawGizmos()
    {
        var color = Gizmos.color;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(new Vector3(borderRect.center.x, borderRect.center.y), new Vector3(borderRect.width, borderRect.height));
        Gizmos.color = color;
    }

    private void LateUpdate()
    {
        HandleMovement();
        HandleZoom();
    }

    private void HandleMovement()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");
        var movement = new Vector2(horizontalInput, verticalInput) * moveSpeed * Time.deltaTime;
        var newPosition = new Vector2(transform.position.x + movement.x, transform.position.y + movement.y);
        // Clamp the camera position within the border rectangle
        newPosition.x = Mathf.Clamp(newPosition.x, borderRect.xMin, borderRect.xMax);
        newPosition.y = Mathf.Clamp(newPosition.y, borderRect.yMin, borderRect.yMax);
        transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
    }

    private void HandleZoom()
    {
        var view = cam.orthographicSize;
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
            view += zoomSpeed * Time.deltaTime;
        else if (Input.GetAxis("Mouse ScrollWheel") > 0)
            view -= zoomSpeed * Time.deltaTime;
        cam.orthographicSize = Mathf.Clamp(view, zoomMinMax.x, zoomMinMax.y);
    }
}