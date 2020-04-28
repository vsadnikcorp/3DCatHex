using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraKeyboardController : MonoBehaviour
{
    public WorldController world;
    float moveSpeed = 100f;
    //float minMoveSpeed = 100f;
    //float maxMoveSpeed = 400f;
    
    void Update()
    {
        Vector3 moveCamera = new Vector3
         (
          Input.GetAxis("Horizontal"),
          Input.GetAxis("Vertical")
        );
        Camera.main.transform.Translate(moveCamera * moveSpeed * Time.deltaTime, Space.World);

        ////CATLIKE CODE DOES NOT WORK, CONSTANT ERRORS ON PLAY
        //float deltaX = Input.GetAxis("Horizontal");
        //float deltaZ = Input.GetAxis("Vertical");
        //if (deltaX != 0f || deltaZ != 0f)
        //{
        //    AdjustCameraPosition(deltaX, deltaZ);
        //}

        //void AdjustCameraPosition(float deltax, float deltaz)
        //{
        //    float distance = moveSpeed * Time.deltaTime;
        //    Vector3 cameraPosition = transform.localPosition;
        //    cameraPosition += new Vector3(deltax, 0f, deltaz) * distance;
        //    transform.localPosition = ClampCameraPosition(cameraPosition);
        //}

        //Vector3 ClampCameraPosition (Vector3 cameraposition)
        //{
        //    Vector3 cameraPosition = new Vector3(Camera.main.transform.position.x, 0, Camera.main.transform.position.z);
        //    float xMax =
        //        world.chunkCountX * HexMetrics.chunkSizeX *
        //        (2f * HexMetrics.innerRadius);

        //    float zMax =
        //        world.chunkCountZ * HexMetrics.chunkSizeZ *
        //        (1.5f * HexMetrics.outerRadius);

        //    cameraPosition.x = Mathf.Clamp(cameraPosition.x, 0f, xMax);
        //    cameraPosition.z = Mathf.Clamp(cameraPosition.z, 0f, zMax);

        //    return cameraPosition;
        //}
    }
}
