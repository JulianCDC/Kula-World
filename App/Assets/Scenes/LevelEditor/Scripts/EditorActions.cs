using UnityEngine;
using System;
using UnityEngine.EventSystems;

/// <summary>
/// The actions performable by the user in the editor 
/// </summary>
public class EditorActions : MonoBehaviour
{
    public Transform fovTransform;
    [SerializeField] private Camera mainCamera;
    private bool cameraIsUnlocked;
    private GameObject currentlyPlacing;

    private float cameraYaw = 0;
    private float cameraPitch = 0;

    private void Start()
    {
        LockCamera();
        Map.mapInstance = new Map();
    }

    void Update()
    {
        CameraLockListener();

        if (!cameraIsUnlocked)
        {
            CameraControls();
        }

        SelectListener();
        DeleteBlockListener();
        CancelListener();

        if (EditorManager.Instance.newBlock != null && EditorManager.Instance.SelectedBlock == null)
        {
            EditorManager.Instance.SelectedBlock = Instantiate(EditorManager.Instance.newBlock, Vector3.zero,
                Quaternion.Euler(Vector3.up * 90));
            EditorManager.Instance.selectedBlockBehaviour.Select();
            EditorManager.Instance.SelectedBlock.transform.parent = GameObject.Find("Map").transform;
        }
    }

    private void CameraLockListener()
    {
        if (Input.GetKeyUp(KeyCode.G))
        {
            if (cameraIsUnlocked)
            {
                LockCamera();
            }
            else
            {
                UnlockCamera();
            }
        }
    }

    private void UnlockCamera()
    {
        EditorManager.Instance.canPlaceBlock = false;
        cameraIsUnlocked = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void LockCamera()
    {
        EditorManager.Instance.canPlaceBlock = true;
        cameraIsUnlocked = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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

    public void ManageTime(string userTime)
    {
        int time;
        bool isInt = int.TryParse(userTime, out time);
        if (isInt && time > 0)
        {
            Map.mapInstance.metadata.timeToFinish = time;
        }
    }

    private void CameraControls()
    {
        RotateCamera();
        MoveCamera();
    }

    private void RotateCamera()
    {
        float sensibility = 1.0f;

        cameraYaw += Input.GetAxis("Mouse X") * sensibility;
        cameraPitch -= Input.GetAxis("Mouse Y") * sensibility;

        mainCamera.transform.eulerAngles = new Vector3(cameraPitch, cameraYaw, mainCamera.transform.rotation.z);
    }

    private void MoveCamera()
    {
        float sensibility = 0.5f;

        mainCamera.transform.Translate(Input.GetAxis("Horizontal editor") * sensibility, 0,
            Input.GetAxis("Vertical editor") * sensibility);
    }
}