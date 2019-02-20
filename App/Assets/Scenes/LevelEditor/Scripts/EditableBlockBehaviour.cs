﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class EditableBlockBehaviour : MonoBehaviour
{
    public XmlBlock xmlBlock;

    private ArrowBehaviour[] arrows = new ArrowBehaviour[6];

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

    public void Select()
    {
        CreateMovementArrow();
    }

    public void UnSelect()
    {
        DestroyMovementArrow();
    }

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

    private void DestroyMovementArrow()
    {
        foreach (ArrowBehaviour arrow in arrows)
        {
            Destroy(arrow.gameObject);
        }
    }

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
