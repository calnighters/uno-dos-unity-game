using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnoDos.Decks.Entities;
using UnoDos.Cards.Entities;
using UnoDos.Cards.Interfaces;
using UnoDos.Cards.Enums;
using NsUnityEngineUI = UnityEngine.UI;
using Assets.Scripts.Sprites;
using System.Linq;
using UnoDos.Decks.Interfaces;
using UnoDos.Players.Interfaces;
using UnoDos.Players.Entities;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;
using System.Threading;

public class GamePlay : MonoBehaviour
{
    // Start is called before the first frame update
    public IDeck deck;

    private IPlayer __Player;
    private ICPU __CPU;

    public GameObject CardSprite;
    public GameObject PlayerArea;

    public GameObject OpponentArea;

    public GameObject deckButton;

    public Sprite[] GreenCardSprites;
    public Sprite[] OrangeCardSprites;
    public Sprite[] PinkCardSprites;
    public Sprite[] PurpleCardSprites;
    public Sprite[] SpecialCardSprites;
    public Sprite BackCardSprite;

    public RenderSprites RenderSprites;

    public GameObject LastPlayedCard;

    void Start()
    {
        __Player = new Player();
        __CPU = new CPU();
        // Create a new instance of a Deck
        deck = new Deck();

        // Create the deck of cards
        deck.CreateDeck();

        // shuffle the deck
        deck.Shuffle();

        // Deal cards to the player
        //DealCards();

        RenderSprites = new RenderSprites(GreenCardSprites, OrangeCardSprites, PinkCardSprites, PurpleCardSprites, SpecialCardSprites);
    }

    public void DealCardsOnGameStart()
    {

        int numberOfCardsToDeal = 10;

        __Player.Cards = deck.Deal(numberOfCardsToDeal);

        __CPU.Cards = deck.Deal(numberOfCardsToDeal);

        SetPlayerHandCardSprites();

        SetCPUHandCardSprites();

        ICard _StartingCard = deck.DrawInitialCard();

        LastPlayedCard.SetActive(true);
        SetLastPlayedCardSprite(_StartingCard);

    }

    private void SetPlayerHandCardSprites()
    {
        PlayerArea.transform.DetachChildren();

        foreach (ICard card in __Player.Cards)
        {
            GameObject playerDrawnCard = Instantiate(CardSprite, new Vector3(0, 0, 0), Quaternion.identity);
            playerDrawnCard.GetComponent<NsUnityEngineUI.Image>().sprite = RenderSprites.GetSprite(card);
            //Instead of calling the method directly use a co-routine otherwise the canvas only gets updated after the method is finished
            playerDrawnCard.GetComponent<Button>().onClick.AddListener(() => CardClicked(playerDrawnCard));
            playerDrawnCard.transform.SetParent(PlayerArea.transform, false);
            playerDrawnCard.name = card.ToString();

        }
    }

    private void SetCPUHandCardSprites()
    {
        OpponentArea.transform.DetachChildren();

        foreach (ICard card in __CPU.Cards)
        {
            GameObject cpuDrawnCard = Instantiate(CardSprite, new Vector3(0, 0, 0), Quaternion.identity);
            cpuDrawnCard.GetComponent<NsUnityEngineUI.Image>().sprite = BackCardSprite;
            cpuDrawnCard.transform.SetParent(OpponentArea.transform, false);
            cpuDrawnCard.name = card.ToString();
        }
    }

    private void SetLastPlayedCardSprite(ICard card)
    {
        LastPlayedCard.GetComponent<NsUnityEngineUI.Image>().sprite = RenderSprites.GetSprite(card);
    }

    //Co-routine is used to update canvas mid method rather than waiting until end
    public void DeckClicked()
    {
        StartCoroutine(DealCards());
    }

    public IEnumerator DealCards()
    {
        if (__Player.Cards == null || __CPU.Cards == null)
        {
            yield return new WaitForSeconds(.01f);
            DealCardsOnGameStart();
        }

        else
        {
            ICard _DrawnCard = deck.DrawCard();

            GameObject playerDrawnCard = Instantiate(CardSprite, new Vector3(0, 0, 0), Quaternion.identity);
            playerDrawnCard.GetComponent<NsUnityEngineUI.Image>().sprite = RenderSprites.GetSprite(_DrawnCard);
            playerDrawnCard.transform.SetParent(PlayerArea.transform, false);
            playerDrawnCard.name = _DrawnCard.ToString();

            //User has selected to pick up a card - CPU's turn
            yield return new WaitForSeconds(2f);
            CPUPlaysCard();
        }

    }

    //Co-routine is used to update canvas mid method rather than waiting until end
    public void CardClicked(GameObject currentCardClicked)
    {
        StartCoroutine(UserPlaysCard(currentCardClicked));
    }

    public IEnumerator UserPlaysCard(GameObject currentCardClicked)
    {
        yield return new WaitForSeconds(.01f);
        ICard _PreviousLastPlayedCard = deck.LastCardPlayed;
        PlayCard _PlayCard = new PlayCard(deck, currentCardClicked.name, __Player, __CPU);
        deck = _PlayCard.PlayerPlaysCard();
        __Player = _PlayCard.getPlayer();
        __CPU = _PlayCard.getCPU();
        if (_PreviousLastPlayedCard != deck.LastCardPlayed)
        {
            SetPlayerHandCardSprites();
            SetLastPlayedCardSprite(deck.LastCardPlayed);
            //User has selected a valid card - CPU's turn
            yield return new WaitForSeconds(2f);
            CPUPlaysCard();
        }
        //User has selected an invalid card - User's turn
    }

    private void CPUPlaysCard()
    {
        //__CPU.PlayCardCPU(deck);
        ICard _PreviousLastPlayedCard = deck.LastCardPlayed;
        PlayCard _PlayCard = new PlayCard(deck, __CPU, __Player);
        deck = _PlayCard.CPUPlaysCard();
        __Player = _PlayCard.getPlayer();
        __CPU = _PlayCard.getCPU();

        SetLastPlayedCardSprite(deck.LastCardPlayed);
        SetCPUHandCardSprites();
    }
}
