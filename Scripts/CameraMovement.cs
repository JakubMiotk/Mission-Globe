using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Vector3 direction;
    Vector3 touchStart;
    public float zoomOutMin = 1;
    public float zoomOutMax = 8;
    public float maxY;
    public float minY;
    public float minX;
    public float maxX;
    private float difference;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            difference = currentMagnitude - prevMagnitude;
            zoom(difference * 0.003f);
        }
        else if (Input.GetMouseButton(0))
        {
            direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Camera.main.transform.position = new Vector3
            (Mathf.Clamp(Camera.main.transform.position.x + direction.x, minX, maxX), 
            Mathf.Clamp(Camera.main.transform.position.y + direction.y, minY, maxY),
            Camera.main.transform.position.z + direction.z);
        }
        zoom(Input.GetAxis("Mouse ScrollWheel"));
    }

    void zoom(float increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax);
    }
}