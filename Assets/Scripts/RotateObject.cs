using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class RotateObject : MonoBehaviour
{
    public float rotationSpeed = 2.0f; // Adjust this value to control rotation speed
    public bool isRotating = false;
    public Vector2 rotationTouchStartPos;
    private Camera arRaycastManager;
    private void Start()
    {
        arRaycastManager = FindObjectOfType<ARInteraction>().GetComponentInChildren<Camera>();
    }
    private void Update()
    {
        if (Input.touchCount == 1)
        {

            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Moved:

                    if (IsTouchingObject(touch))
                    {
                        RotateObjectWithTouch(touch);
                    }

                    break;

              
                case TouchPhase.Ended:
                    isRotating = false;

                    break;
            }
        }
    }
    private void RotateObjectWithTouch(Touch touch)
    {
        Vector2 deltaPos = touch.deltaPosition;
        float rotationAmount = deltaPos.x * rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up, -rotationAmount);
    }


    private bool IsTouchingObject(Touch touch)
    {
      
        Ray ray = arRaycastManager.ScreenPointToRay(touch.position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            return hit.transform == transform;
        }
        return false;
    }
}