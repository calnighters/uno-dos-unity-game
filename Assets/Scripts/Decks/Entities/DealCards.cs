using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnoDos.Decks.Entities;
using UnoDos.Cards.Entities;
using UnoDos.Cards.Interfaces;
using UnoDos.Cards.Enums;

public class DealCards : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Card1;
    public GameObject PlayerArea;
    public List<ICard> playerHand;

    private GamePlay gamePlay;

    void Start()
    {
        
    }

    // Update is called once per frame
    public void OnClick()
    {
        gamePlay.DealCards();

    }
}
