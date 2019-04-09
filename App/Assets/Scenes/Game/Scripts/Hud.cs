using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{    
    private static Hud hudInstance;
    public Text score;
    [SerializeField] private GameObject key;
    public Texture2D filledKey;
    [SerializeField] private Texture2D blackKey;
    public GameObject hourglass;

    [SerializeField] private RawImage apple;
    [SerializeField] private RawImage banana;
    [SerializeField] private RawImage pumpkin;
    [SerializeField] private RawImage watermelon;
    [SerializeField] private RawImage strawberry;

    private Dictionary<Fruit.fruits, RawImage> fruits;

    public static Hud GetHud()
    {
        return hudInstance;
    }

    private void Start()
    {
        hudInstance = this;
        this.fruits = new Dictionary<Fruit.fruits, RawImage>
        {
            [Fruit.fruits.apple] = apple,
            [Fruit.fruits.banana] = banana,
            [Fruit.fruits.pumpkin] = pumpkin,
            [Fruit.fruits.watermelon] = watermelon,
            [Fruit.fruits.strawberry] = strawberry
        };
    }

    public static void CollectFruit(Fruit.fruits fruitType)
    {
        RawImage fruit = hudInstance.fruits[fruitType];
        Color currentColor = fruit.color;

        fruit.color = new Color(currentColor.r, currentColor.g, currentColor.b, 1.0f);
    }

    public static void CollectKey()
    {
        hudInstance.key.GetComponent<RawImage>().texture = hudInstance.filledKey;
    }

    public static void ResetHud()
    {
        
    }
}