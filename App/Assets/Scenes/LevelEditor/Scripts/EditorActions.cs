using UnityEngine;
using System;
using UnityEngine.EventSystems;

/// <summary>
/// The actions performable by the user in the editor 
/// </summary>
public class EditorActions : MonoBehaviour
{
    public Transform fovTransform;
    private GameObject currentlyPlacing;
    
    private void Start()
    {
        Map.mapInstance = new Map();
    }
    
    void Update()
    {
        CameraControls();
        SelectListener();
        DeleteBlockListener();
        CancelListener();
        
        if (EditorManager.Instance.newBlock != null && EditorManager.Instance.SelectedBlock == null)
        {
            EditorManager.Instance.SelectedBlock = Instantiate(EditorManager.Instance.newBlock, Vector3.zero, Quaternion.Euler(Vector3.up * 90));
            EditorManager.Instance.selectedBlockBehaviour.Select();
            EditorManager.Instance.SelectedBlock.transform.parent = GameObject.Find("Map").transform;
        }
    }

    private void SelectListener()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit = new RaycastHit();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.collider.gameObject;
                EditableBlockBehaviour hitObjectBehaviour = hitObject.GetComponent<EditableBlockBehaviour>();

                bool isBlock = hitObjectBehaviour != null;

                if (EditorManager.Instance.SelectedBlock != null)
                {
                    EditorManager.Instance.selectedBlockBehaviour.UnSelect();
                }

                if (isBlock)
                {
                    EditorManager.Instance.SelectedBlock = hitObject;
                    EditorManager.Instance.selectedBlockBehaviour = hitObjectBehaviour;

                    hitObjectBehaviour.Select();
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
        if (EditorManager.Instance.selectedBlockBehaviour != null)
        {
            EditorManager.Instance.selectedBlockBehaviour.UnSelect();
        }

        EditorManager.Instance.SelectedBlock = null;
        EditorManager.Instance.selectedBlockBehaviour = null;
    }
    
    private void DeleteBlockListener()
    {
        if (Input.GetKeyDown(KeyCode.Backspace) || Input.GetKeyDown(KeyCode.Delete))
        {
            if (EditorManager.Instance.selectedBlockBehaviour == null) return;

            Map.DeleteBlock(EditorManager.Instance.selectedBlockBehaviour.xmlBlock);
            Destroy(EditorManager.Instance.SelectedBlock);
            ClearSelectedObject();
        }
    }

    private void CancelListener()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            EditorManager.Instance.ClearPreSelection();
        }
    }

    public void ManageTime( string userTime)
    {
        int time;
        bool isInt =  int.TryParse(userTime, out time);
        if (isInt && time > 0)
        {
            Map.mapInstance.metadata.timeToFinish = time;
        }


    }
    
    /// <summary>
    /// Listener for camera movement
    /// </summary>
    ///
    /// Listen for:
    /// <list type="bullet">
    ///    <item>Middle click for rotation and zoom</item>
    ///    <item>Right click for movement</item>
    /// </list>
    ///
    /// Call either <see cref="MoveCamera"/> or <see cref="PivotCamera"/>
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

    /// <summary>
    /// Move the camera in the specified direction
    /// </summary>
    /// <param name="direction">The direction in which to move the camera (0: x, 1: y, 2: z)</param>
    /// <param name="value">The distance to travel</param>
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
                translation = new Vector3(0, 0, value);
                break;
        }


        this.fovTransform.Translate(translation);
    }

    /// <summary>
    /// Rotate the camera in the specified direction
    /// </summary>
    /// <param name="direction">The direction in which to rotate the camera (0: y, 1: x)</param>
    /// <param name="value">The distance to travel</param>
    private void PivotCamera(int direction, float value)
    {
        Vector3 axis = Vector3.zero;

        switch (direction)
        {
            case 0:
                axis = Vector3.up;
                break;
            case 1:
                axis = Vector3.right;
                break;
        }

        this.fovTransform.RotateAround(this.fovTransform.position, axis, value);
    }
}