using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnoDos.Decks.Entities;
using UnoDos.Cards.Entities;
using UnoDos.Cards.Interfaces;
using UnoDos.Cards.Enums;

public class DealCards : MonoBehaviour
{
    public GameObject Card1;
    public GameObject PlayerArea;
    public List<ICard> playerHand;
    public GameObject PlayedCard;

    private GamePlay gamePlay;

    public void OnClick()
    {
        PlayedCard.SetActive(true);
        gamePlay.DealCards();

    }
}
