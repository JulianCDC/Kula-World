using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    public Transform FovTransform;

    void Start()
    {
        
    }

    void Update()
    {
        float xMovement = Input.GetAxis("Mouse X");
        float yMovement = Input.GetAxis("Mouse Y");
        float zMovement = Input.GetAxis("Mouse ScrollWheel");

        if (xMovement != 0)
        {
            if (Input.GetMouseButton(1) && Input.GetMouseButton(2))
            {
                // do nothing
            }
            else if (Input.GetMouseButton(2) == true)
            {
                MoveCamera(0, xMovement);
            }
            else if (Input.GetMouseButton(1) == true)
            {
                PivotCamera(0, xMovement);
            }
        }

        if (yMovement != 0)
        {
            if (Input.GetMouseButton(1) && Input.GetMouseButton(2))
            {
                // do nothing
            }
            else if (Input.GetMouseButton(2) == true)
            {
                MoveCamera(1, yMovement);
            }
            else if (Input.GetMouseButton(1) == true)
            {
                PivotCamera(1, yMovement);
            }
        }

        if (zMovement != 0)
        {
            MoveCamera(2, zMovement);
        }
    }

    private void MoveCamera(int direction, float value)
    {
        Vector3 translation = new Vector3();

        switch (direction)
        {
            case 0:
                translation = new Vector3(value, 0);
                break;
            case 1:
                translation = new Vector3(0, value);
                break;
            case 2:
                translation = new Vector3(0, 0, value);
                break;
        }


        this.FovTransform.Translate(translation);
    }

    private void PivotCamera(int direction, float value)
    {
        Vector3 rotation = new Vector3();

        switch (direction)
        {
            case 0:
                rotation = new Vector3(0, value);
                break;
            case 1:
                // invert value, otherwise rotation axis is inverted
                rotation = new Vector3(-value, 0);
                break;
        }


        this.FovTransform.Rotate(rotation);
    }
}
