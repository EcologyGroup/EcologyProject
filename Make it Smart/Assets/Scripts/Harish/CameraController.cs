using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Purchasing;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float offsetFactor = 5.0f;
    [SerializeField] private float moveFactor = 45.0f;
    [SerializeField] private float spanSpeed = 5.0f;
    private IEnumerator currentCoroutine;
    private float width, height, offset;//InPixels
    private float gwidth, gheight;//InGameUnits

    private Vector3 prevPointerPos;
    private Vector3 screenCenterPos;
    PauseMenu p;
    void Start()
    {
        height = Screen.height;
        width = height * mainCamera.aspect;
        offset = width * offsetFactor/100;

        gheight = mainCamera.orthographicSize * 2.0f;
        gwidth = gheight * mainCamera.aspect;

        prevPointerPos = Vector3.zero;
        screenCenterPos = mainCamera.transform.position;

        p=FindObjectOfType<PauseMenu>();
    }
    IEnumerator span(Vector3 end)
    {
        while (Vector3.Distance(mainCamera.transform.position, end) >= 0.01f) 
        {
            Vector3 disp = end - mainCamera.transform.position;
            disp.z = 0;
            mainCamera.transform.position += (disp) * spanSpeed * Time.fixedDeltaTime;
            yield return null;
        }
        //Debug.Log("Reached "+ Vector3.Distance(mainCamera.transform.position, end)+" "+(currentCoroutine==null));
    }
    void Update()
    {
        if (!p.isGamePaused())
        {
            float x = Input.mousePosition.x, y = Input.mousePosition.y;
            Boolean flag = ((x >= 0 && x <= offset) && (y >= 0 && y <= offset)) || ((x >= 0 && x <= offset) && (y >= height - offset && y <= height)) || ((x >= width - offset && x <= width) && (y >= 0 && y <= offset)) || ((x >= width - offset && x <= width) && (y >= height - offset && y <= height));
            if (flag)
            {
                if (currentCoroutine != null)
                {
                    StopCoroutine(currentCoroutine);
                    screenCenterPos = mainCamera.transform.position;
                }
                Vector2 newPosInPixels = new Vector2(x - width / 2, y - height / 2) * moveFactor / 100 + new Vector2(width / 2, height / 2) * 0;
                Vector3 newPointerPos = new Vector3(newPosInPixels.x / width * gwidth, newPosInPixels.y / height * gheight, screenCenterPos.z) + screenCenterPos;
                if (Vector3.Distance(prevPointerPos, newPointerPos) >= gwidth / 4.0f)
                {
                    prevPointerPos = newPointerPos;
                    currentCoroutine = span(newPointerPos);
                }
                StartCoroutine(currentCoroutine);
            }
        }
    }
}
//Error Pending : Consecutive spans in same direction
