using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [Header("References")]

    [SerializeField] private float sensX = 2f;
    [SerializeField] private float sensY = 2f;
    [SerializeField] private float startingCamXRot;
    [SerializeField] private float startingCamYRot;

    private Camera cam;
    [SerializeField] private Transform orientation;

    private float mouseX;
    private float mouseY;

    [SerializeField] private float multiplier = 0.01f;

    private float xRotation;
    private float yRotation;

    public bool allowLooking;

    private void Start()
    {
        multiplier = PlayerPrefs.GetFloat("sensitivity", .01f);
        cam = GetComponentInChildren<Camera>();
        xRotation = startingCamXRot;
        yRotation = startingCamYRot;
        orientation.transform.rotation = Quaternion.Euler(0, startingCamYRot, 0);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        allowLooking = true;
    }

    private void Update()
    {
        if (allowLooking)
        {
            PlayerInput();
        }

        cam.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    public void PlayerInput()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        yRotation += mouseX * sensX * multiplier;
        xRotation -= mouseY * sensY * multiplier;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
    }

    public void setSensitivity(float sensitivity) {
        this.multiplier = sensitivity;
        PlayerPrefs.SetFloat("sensitivity", this.multiplier);
    }
}
