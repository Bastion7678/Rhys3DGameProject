using UnityEngine;
using UnityEngine.InputSystem;

public class BowPhysics : MonoBehaviour
{

    [SerializeField] private float maxDrawDistance = 1.0f;
    [SerializeField] private float arrowSpeed = 20.0f;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform arrowSpawnPoint;
    private Vector3 initialMousePosition;
    private bool isDrawing = false;
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            StartDrawing();
        }
        else if (Mouse.current.leftButton.isPressed)
        {
            ContinueDrawing();
        }
        else if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            ReleaseArrow();
        }
    }
    private void StartDrawing()
    {
        initialMousePosition = Mouse.current.position.ReadValue();
        isDrawing = true;
    }
    private void ContinueDrawing()
    {
        if (!isDrawing) return;
        Vector3 currentMousePosition = Mouse.current.position.ReadValue();
        float drawDistance = Vector3.Distance(initialMousePosition, currentMousePosition);
        drawDistance = Mathf.Clamp(drawDistance, 0, maxDrawDistance);
        // 
    }
    private void ReleaseArrow()
    {
        if (!isDrawing) return;
        Vector3 currentMousePosition = Mouse.current.position.ReadValue();
        float drawDistance = Vector3.Distance(initialMousePosition, currentMousePosition);
        drawDistance = Mathf.Clamp(drawDistance, 0, maxDrawDistance);
       
        // 
        Vector3 direction = (currentMousePosition - initialMousePosition).normalized;
        float speed = (drawDistance / maxDrawDistance) * arrowSpeed;
        
        //
        GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, Quaternion.identity);
        Rigidbody rb = arrow.GetComponent<Rigidbody>();
        rb.linearVelocity = direction * speed;
        isDrawing = false;
    }
}
