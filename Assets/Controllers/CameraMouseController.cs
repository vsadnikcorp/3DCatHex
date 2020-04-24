using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMouseController : MonoBehaviour
{
    Vector3 lastFramePosition;
    private float zoomSpeed = 20.0f;
    private float minOrtho = 20.0f;
    //TODO:  FIX MAX ORTHO TO 8*numberrows
    private float maxOrtho = 500.0f;
       
    void Update()
    {
        Vector3 currentFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        //HANDLE SCREEN DRAGGING
        if (Input.GetMouseButton(1)) //RIGHT MOUSE BUTTON
        {
            Vector3 delta = lastFramePosition - currentFramePosition;
            Camera.main.transform.Translate(delta);
        }
        lastFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //HANDLE ZOOMING WITH MOUSE WHEEL
        if (Mathf.Abs(scroll) > 0.01f)
        {
            Vector3 V3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Camera.main.orthographicSize -= zoomSpeed * scroll;
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize,
                minOrtho, maxOrtho);
            V3 = V3 - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Camera.main.transform.position += V3;
        }

        //CENTER MAP ON SHIFT-RIGHT-MOUSE CLICK
        //TODO
        if (Input.GetKey(KeyCode.LeftShift) == true || Input.GetKey(KeyCode.RightShift) == true)
        {

            if (Input.GetMouseButtonDown(1))
            {
                //TODO
            }
        }

    }
}
