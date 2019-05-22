using System;
using System.Runtime.CompilerServices;
using UnityEngine;

/// <summary>
/// The EditableBlock is a Block used in the Editor scene.
/// </summary>
/// It contains methods for edition and an instance to a variable containing a serialized version of the block <see cref="xmlBlock"/>.
public class EditableBlockBehaviour : MonoBehaviour
{
    public XmlBlock xmlBlock;

    private LayerMask oldLayerMask;

    private ArrowBehaviour[] arrows = new ArrowBehaviour[6];
    private GameObject blockPlaceholder;
    private GameObject placeholderInstance;

    private WithItemBehaviour blockWithItemBehaviour;
    private bool isBlockWithItem;

    private bool selected;

    void Start()
    {
        blockPlaceholder = Resources.Load<GameObject>("Prefabs/BlockPlaceholder");

        this.xmlBlock.objectType = this.gameObject.name.Replace("editable_", "").Replace("(Clone)", "");
        this.xmlBlock.id = Map.currentBlockId;

        this.blockWithItemBehaviour = this.gameObject.GetComponent<WithItemBehaviour>();
        Fruit fruitBehaviour = this.gameObject.GetComponentInChildren<Fruit>();
        this.isBlockWithItem = blockWithItemBehaviour != null;
        bool isBlockWithFruit = fruitBehaviour != null;

        if (isBlockWithItem)
        {
            this.xmlBlock.itemPosition = blockWithItemBehaviour.itemPosition;
        }
        else
        {
            this.xmlBlock.itemPosition = WithItemBehaviour.Positions.none;
        }

        if (isBlockWithFruit)
        {
            this.xmlBlock.fruit = fruitBehaviour.type;
        }
        else
        {
            this.xmlBlock.fruit = Fruit.fruits.none;
        }

        if (!Map.AddBlock(this.xmlBlock))
        {
            Canvas canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            GameObject error = Instantiate(Resources.Load<GameObject>("Prefabs/Error"), canvas.transform);
            Destroy(error, 5);

            EditorManager.Instance.ClearPreSelection();

            Destroy(placeholderInstance);
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        if (selected && isBlockWithItem)
        {
            WithItemBehaviour.Positions previousPosition = blockWithItemBehaviour.itemPosition;

            if (Input.GetKeyDown(KeyCode.Z))
            {
                GUIBehaviour.Instance.Toggle(ref GUIBehaviour.Instance.up);
                blockWithItemBehaviour.itemPosition = WithItemBehaviour.Positions.up;
                blockWithItemBehaviour.UpdateItemPosition();
                Map.ChangeItemPosition(this.xmlBlock, WithItemBehaviour.Positions.up, this);
            }

            else if (Input.GetKeyDown(KeyCode.S))
            {
                GUIBehaviour.Instance.Toggle(ref GUIBehaviour.Instance.down);
                blockWithItemBehaviour.itemPosition = WithItemBehaviour.Positions.down;
                blockWithItemBehaviour.UpdateItemPosition();
                Map.ChangeItemPosition(this.xmlBlock, WithItemBehaviour.Positions.down, this);
            }

            else if (Input.GetKeyDown(KeyCode.Q))
            {
                GUIBehaviour.Instance.Toggle(ref GUIBehaviour.Instance.left);
                blockWithItemBehaviour.itemPosition = WithItemBehaviour.Positions.left;
                blockWithItemBehaviour.UpdateItemPosition();
                Map.ChangeItemPosition(this.xmlBlock, WithItemBehaviour.Positions.left, this);
            }

            else if (Input.GetKeyDown(KeyCode.D))
            {
                GUIBehaviour.Instance.Toggle(ref GUIBehaviour.Instance.right);
                blockWithItemBehaviour.itemPosition = WithItemBehaviour.Positions.right;
                blockWithItemBehaviour.UpdateItemPosition();
                Map.ChangeItemPosition(this.xmlBlock, WithItemBehaviour.Positions.right, this);
            }

            else if (Input.GetKeyDown(KeyCode.E))
            {
                GUIBehaviour.Instance.Toggle(ref GUIBehaviour.Instance.back);
                blockWithItemBehaviour.itemPosition = WithItemBehaviour.Positions.back;
                blockWithItemBehaviour.UpdateItemPosition();
                Map.ChangeItemPosition(this.xmlBlock, WithItemBehaviour.Positions.back, this);
            }

            else if (Input.GetKeyDown(KeyCode.A))
            {
                GUIBehaviour.Instance.Toggle(ref GUIBehaviour.Instance.front);
                blockWithItemBehaviour.itemPosition = WithItemBehaviour.Positions.front;
                blockWithItemBehaviour.UpdateItemPosition();
                Map.ChangeItemPosition(this.xmlBlock, WithItemBehaviour.Positions.front, this);
            }
        }

        if (selected)
        {
            GeneratePlaceholder();
        }
    }

    public void Select()
    {
        selected = true;
        Hide();
    }

    public void UnSelect()
    {
        selected = false;
        Show();

        if (Map.mapInstance.CanBlockMoveTo(placeholderInstance.transform.position))
        {
            if (EditorManager.Instance.canPlaceBlock)
            {
                this.transform.position = placeholderInstance.transform.position;
                Map.MoveBlockTo(this, transform.position);
            }
        }

        if (this.transform.position == Vector3.zero)
        {
            Map.DeleteBlock(this.xmlBlock);
            Destroy(gameObject);
        }

        Destroy(placeholderInstance);
    }

    public void Cancel()
    {
        selected = false;
        Destroy(placeholderInstance);
        Show();
    }

    private void Hide()
    {
        oldLayerMask = gameObject.layer;
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");

        this.GetComponent<Renderer>().enabled = false;

        SetRendererStateInChildren(false, transform);
    }

    private void Show()
    {
        gameObject.layer = oldLayerMask;

        this.GetComponent<Renderer>().enabled = true;

        SetRendererStateInChildren(true, transform);
    }

    private void SetRendererStateInChildren(bool newState, Transform parent)
    {
        foreach (Transform child in parent)
        {
            try
            {
                child.GetComponent<Renderer>().enabled = newState;
            }
            catch (Exception exception)
            {
                SetRendererStateInChildren(newState, child);
            }
        }
    }

    private void GeneratePlaceholder()
    {
        RaycastHit hit = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.collider.gameObject;
            BlockBehaviour hitObjectBehaviour = hitObject.GetComponent<BlockBehaviour>();

            bool isBlock = hitObjectBehaviour != null;

            if (isBlock)
            {
                Destroy(this.placeholderInstance);
                this.placeholderInstance = Instantiate(blockPlaceholder, roundPointToBlock(hit.point),
                    Quaternion.Euler(0, 0, 0));
            }
        }
    }

    private Vector3 roundPointToBlock(Vector3 point)
    {
        var roundedVector = new Vector3((float) Math.Round(point.x, MidpointRounding.AwayFromZero),
            (float) Math.Round(point.y, MidpointRounding.AwayFromZero),
            (float) Math.Round(point.z, MidpointRounding.AwayFromZero));
        return roundedVector;
    }
}