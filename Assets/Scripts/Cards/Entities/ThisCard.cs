using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnoDos.Cards.Entities;
using UnoDos.Cards.Interfaces;
using UnoDos.Cards.Enums;

public class ThisCard : MonoBehaviour
{

    public int cardID;
    public CardColour cardColor;
    public CardType cardType;
    public int cardScore;

    public Card card = new Card();
    
    // Start is called before the first frame update
    void Start()
    {
        card.CreateCard(cardID, cardColor, cardType, cardScore);

        
    }

    // Update is called once per frame
    /*
    void Update()
    {
        
    }
    */
}
