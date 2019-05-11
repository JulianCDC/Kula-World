using System;
using UnityEngine;

/// <summary>
/// The EditableBlock is a Block used in the Editor scene.
/// </summary>
/// It contains methods for edition and an instance to a variable containing a serialized version of the block <see cref="xmlBlock"/>.
public class EditableBlockBehaviour : MonoBehaviour
{
    /// <summary>
    /// The serialized version of the Block
    /// </summary>
    public XmlBlock xmlBlock;

    /// <summary>
    /// An array containing the 6 Arrows' ArrowBehaviour created when clicking on an EditableBlock
    /// </summary>
    private ArrowBehaviour[] arrows = new ArrowBehaviour[6];

    private bool selected;

    /// <summary>
    /// Set the serialized properties of the block
    /// </summary>
    void Start()
    {        
        this.xmlBlock.objectType = this.gameObject.name.Replace("editable_", "").Replace("(Clone)", "");
        this.xmlBlock.id = Map.currentBlockId;

        WithItemBehaviour withItemBehaviour = this.gameObject.GetComponent<WithItemBehaviour>();
        Fruit fruitBehaviour = this.gameObject.GetComponentInChildren<Fruit>();
        bool isBlockWithItem = withItemBehaviour != null;
        bool isBlockWithFruit = fruitBehaviour != null;

        if (isBlockWithItem)
        {
            this.xmlBlock.itemPosition = withItemBehaviour.itemPosition;
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
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        if (selected)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                WithItemBehaviour withItemBehaviour = this.gameObject.GetComponent<WithItemBehaviour>();
                withItemBehaviour.itemPosition = WithItemBehaviour.Positions.up;
                withItemBehaviour.setItemPosition();
            }

            else if (Input.GetKeyDown(KeyCode.S))
            {
                WithItemBehaviour withItemBehaviour = this.gameObject.GetComponent<WithItemBehaviour>();
                withItemBehaviour.itemPosition = WithItemBehaviour.Positions.down;
                withItemBehaviour.setItemPosition();
                Map.ChangeItemPosition(withItemBehaviour, this);
            }

            else if (Input.GetKeyDown(KeyCode.Q))
            {
                WithItemBehaviour withItemBehaviour = this.gameObject.GetComponent<WithItemBehaviour>();
                withItemBehaviour.itemPosition = WithItemBehaviour.Positions.left;
                withItemBehaviour.setItemPosition();
                Map.ChangeItemPosition(withItemBehaviour, this);
            }

            else if (Input.GetKeyDown(KeyCode.D))
            {
                WithItemBehaviour withItemBehaviour = this.gameObject.GetComponent<WithItemBehaviour>();
                withItemBehaviour.itemPosition = WithItemBehaviour.Positions.right;
                withItemBehaviour.setItemPosition();
                Map.ChangeItemPosition(withItemBehaviour, this);
            }

            else if (Input.GetKeyDown(KeyCode.E))
            {
                WithItemBehaviour withItemBehaviour = this.gameObject.GetComponent<WithItemBehaviour>();
                withItemBehaviour.itemPosition = WithItemBehaviour.Positions.back;
                withItemBehaviour.setItemPosition();
                Map.ChangeItemPosition(withItemBehaviour, this);
            }

            else if (Input.GetKeyDown(KeyCode.A))
            {
                WithItemBehaviour withItemBehaviour = this.gameObject.GetComponent<WithItemBehaviour>();
                withItemBehaviour.itemPosition = WithItemBehaviour.Positions.front;
                withItemBehaviour.setItemPosition();
                Map.ChangeItemPosition(withItemBehaviour, this);
            }


        }


    }

    /// <summary>
    /// Called when the Block is selected
    /// </summary>
    /// Call the method for creating the Arrows used for moving the object. See <see cref="ArrowBehaviour"/>
    public void Select()
    {
        selected = true;       

        CreateMovementArrow();
    }

    /// <summary>
    /// Called when the Block is unselected
    /// </summary>
    /// Call the method for destroying the Arrows used for moving the object. See <see cref="ArrowBehaviour"/>
    public void UnSelect()
    {
        selected = false;
        DestroyMovementArrow();
    }

    /// <summary>
    /// Create the Arrows used for moving the object. See <see cref="ArrowBehaviour"/>
    /// </summary>
    /// Check the available direction for an ArrowBehaviour and instantiate an Arrow for each for the directions
    private void CreateMovementArrow()
    {
        ArrowBehaviour.Direction[] possibleDirections =
            (ArrowBehaviour.Direction[]) Enum.GetValues(typeof(ArrowBehaviour.Direction));

        int i = 0;
        foreach (ArrowBehaviour.Direction direction in possibleDirections)
        {
            GameObject arrow = Instantiate(Resources.Load<GameObject>("prefabs/arrow"), this.gameObject.transform);
            arrows[i] = arrow.GetComponent<ArrowBehaviour>();
            arrows[i].direction = direction;
            i++;
        }
    }

    /// <summary>
    /// Destroy the Arrows used for moving the object. See <see cref="ArrowBehaviour"/>
    /// </summary>
    private void DestroyMovementArrow()
    {
        foreach (ArrowBehaviour arrow in arrows)
        {
            Destroy(arrow.gameObject);
        }
    }

    /// <summary>
    /// Move the EditableBlock GameObject in the specified direction
    /// </summary>
    /// <param name="direction">The direction of the Arrow that was clicked to trigger this function</param>
    /// Create a Vector3 from the direction and add this Vector3 to the GameObject's transform<br/>
    /// Change the Block position inside the Map Object
    public void Move(ArrowBehaviour.Direction direction)
    {
        Vector3 movement = Vector3.zero;

        switch (direction)
        {
            case ArrowBehaviour.Direction.up:
                movement = Vector3.up;
                break;
            case ArrowBehaviour.Direction.down:
                movement = Vector3.down;
                break;
            case ArrowBehaviour.Direction.right:
                movement = Vector3.right;
                break;
            case ArrowBehaviour.Direction.left:
                movement = Vector3.left;
                break;
            case ArrowBehaviour.Direction.front:
                movement = Vector3.back;
                break;
            case ArrowBehaviour.Direction.back:
                movement = Vector3.forward;
                break;
        }

        Vector3 newPosition = this.gameObject.transform.position + movement;

        Map.MoveBlockTo(this, newPosition);
    }
}
