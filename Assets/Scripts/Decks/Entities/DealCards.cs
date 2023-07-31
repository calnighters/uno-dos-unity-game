using System.Collections.Generic;
using UnityEngine;
using UnoDos.Cards.Interfaces;

public class DealCards : MonoBehaviour
{
    public GameObject __Card1;
    private GamePlay __GamePlay;
    public GameObject __PlayedCard;
    public GameObject __PlayerArea;
    public List<ICard> __PlayerHand;

    public void OnClick()
    {
        __PlayedCard.SetActive(true);
        __GamePlay.DeckClicked();

    }
}