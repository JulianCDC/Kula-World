using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorActions : MonoBehaviour
{
    public Transform fovTransform;
    private GameObject selectedBlock;
    private EditableBlockBehaviour selectedBlockBehaviour;

    void Update()
    {
        CameraControls();
        SelectBlockListener();
        DeleteBlockListener();
    }

    private void SelectBlockListener()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit = new RaycastHit();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.collider.gameObject;
                EditableBlockBehaviour hitObjectBehaviour = hitObject.GetComponent<EditableBlockBehaviour>();

                if (hitObjectBehaviour != null)
                {
                    if (hitObject != this.selectedBlock)
                    {
                        if (this.selectedBlockBehaviour != null)
                        {
                            this.selectedBlockBehaviour.UnSelect();
                        }
                        this.selectedBlock = hitObject;
                        this.selectedBlockBehaviour = hitObjectBehaviour;

                        hitObjectBehaviour.Select();
                    }
                }
                else
                {
                    ClearSelectedObject();
                }
            }
            else
            {
                ClearSelectedObject();
            }
        }
    }

    private void ClearSelectedObject()
    {
        if (this.selectedBlockBehaviour != null)
        {
            this.selectedBlockBehaviour.UnSelect();
        }
        this.selectedBlock = null;
        this.selectedBlockBehaviour = null;
    }

    private void DeleteBlockListener()
    {
        if (Input.GetKeyDown(KeyCode.Backspace) || Input.GetKeyDown(KeyCode.Delete))
        {
            Destroy(this.selectedBlock);
            ClearSelectedObject();
        }
    }

    private void CameraControls()
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
        const float sensibilityChanger = 1.62f;

        switch (direction)
        {
            case 0:
                translation = new Vector3(value / sensibilityChanger, 0);
                break;
            case 1:
                translation = new Vector3(0, value / sensibilityChanger);
                break;
            case 2:
                translation = new Vector3(0, 0, value / sensibilityChanger);
                break;
        }


        this.fovTransform.Translate(translation);
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


        this.fovTransform.Rotate(rotation);
    }
}
