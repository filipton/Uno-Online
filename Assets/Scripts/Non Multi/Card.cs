using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public string name;

    public CardType cardType;
    public CardColor cardColor;

    public Sprite Image;

    public Texture ImageTexture()
    {
        return Image.texture;
    }
}

public enum CardType
{
    One,
    Two,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Zero,
    ChangeColor,
    PlusFour,
    PlusTwo,
    Reverse,
    Stop,
    OtherOnAll
}

public enum CardColor
{
    Red,
    Green,
    Blue,
    Yellow,
    All
}