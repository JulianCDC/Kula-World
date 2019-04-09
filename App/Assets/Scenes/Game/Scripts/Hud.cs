using System;
using System.Collections.Generic;
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

    private Dictionary<string, RawImage> fruits;

    public static Hud GetHud()
    {
        return hudInstance;
    }

    private void Start()
    {
        hudInstance = this;
        this.fruits = new Dictionary<string, RawImage>
        {
            ["apple"] = apple,
            ["banana"] = banana,
            ["pumpkin"] = pumpkin,
            ["watermelon"] = watermelon,
            ["strawberry"] = strawberry
        };
    }

    public static void CollectFruit(string fruitName)
    {
        RawImage fruit = hudInstance.fruits[fruitName];
    }

    public static void ResetHud()
    {
        
    }
}