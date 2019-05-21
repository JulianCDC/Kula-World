using System;
using UnityEngine;

/// <summary>
/// The EditableBlock is a Block used in the Editor scene.
/// </summary>
/// It contains methods for edition and an instance to a variable containing a serialized version of the block <see cref="xmlBlock"/>.
public class EditableBlockBehaviour : MonoBehaviour
{
    public XmlBlock xmlBlock;

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
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        if (selected && isBlockWithItem)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                blockWithItemBehaviour.itemPosition = WithItemBehaviour.Positions.up;
                blockWithItemBehaviour.UpdateItemPosition();
                Map.ChangeItemPosition(this.xmlBlock, WithItemBehaviour.Positions.up);
            }

            else if (Input.GetKeyDown(KeyCode.S))
            {
                blockWithItemBehaviour.itemPosition = WithItemBehaviour.Positions.down;
                blockWithItemBehaviour.UpdateItemPosition();
                Map.ChangeItemPosition(this.xmlBlock, WithItemBehaviour.Positions.down);
            }

            else if (Input.GetKeyDown(KeyCode.Q))
            {
                blockWithItemBehaviour.itemPosition = WithItemBehaviour.Positions.left;
                blockWithItemBehaviour.UpdateItemPosition();
                Map.ChangeItemPosition(this.xmlBlock, WithItemBehaviour.Positions.left);
            }

            else if (Input.GetKeyDown(KeyCode.D))
            {
                blockWithItemBehaviour.itemPosition = WithItemBehaviour.Positions.right;
                blockWithItemBehaviour.UpdateItemPosition();
                Map.ChangeItemPosition(this.xmlBlock, WithItemBehaviour.Positions.right);
            }

            else if (Input.GetKeyDown(KeyCode.E))
            {
                blockWithItemBehaviour.itemPosition = WithItemBehaviour.Positions.back;
                blockWithItemBehaviour.UpdateItemPosition();
                Map.ChangeItemPosition(this.xmlBlock, WithItemBehaviour.Positions.back);
            }

            else if (Input.GetKeyDown(KeyCode.A))
            {
                blockWithItemBehaviour.itemPosition = WithItemBehaviour.Positions.front;
                blockWithItemBehaviour.UpdateItemPosition();
                Map.ChangeItemPosition(this.xmlBlock, WithItemBehaviour.Positions.front);
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
    }

    public void UnSelect()
    {
        selected = false;

        if (Map.mapInstance.CanBlockMoveTo(placeholderInstance.transform.position))
        {
            this.transform.position = placeholderInstance.transform.position;
            Map.MoveBlockTo(this, transform.position);

            Destroy(placeholderInstance);
        }

        if (this.transform.position == Vector3.zero)
        {
            Map.DeleteBlock(this.xmlBlock);
            Destroy(placeholderInstance);
            Destroy(gameObject);
        }
    }

    public void Cancel()
    {
        selected = false;
        Destroy(placeholderInstance);
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
        var roundedVector = new Vector3((float) Math.Round(point.x), (float) Math.Round(point.y),
            (float) Math.Round(point.z));
        return roundedVector;
    }
}