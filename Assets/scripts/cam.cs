using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam : MonoBehaviour
{

    public float mousesensitivity = 100f;
    public Transform Playerbody;
    float xRotation = 0f;
    public bool ok = true;
    private void Start()
    {

    }
    private void Update()
    {
            float mouseX = Input.GetAxis("Mouse X") * mousesensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mousesensitivity * Time.deltaTime;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            Playerbody.Rotate(Vector3.up * mouseX);
    }
}