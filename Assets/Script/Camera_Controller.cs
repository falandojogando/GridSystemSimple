using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    Vector2 rotate;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void LateUpdate()
    {
        float moveX = Input.GetAxis("Mouse X");
        float moveY = Input.GetAxis("Mouse Y");
        rotate.x += moveX;
        rotate.y -= moveY;
        transform.localRotation = Quaternion.Euler(Vector3.right * rotate.y + Vector3.up * rotate.x);
    }
}
