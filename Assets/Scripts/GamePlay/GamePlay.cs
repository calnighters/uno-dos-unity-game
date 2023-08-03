using System.Collections;
using UnityEngine;
using UnoDos.Decks.Entities;
using UnoDos.Cards.Interfaces;
using NsUnityEngineUI = UnityEngine.UI;
using Assets.Scripts.Sprites;
using System.Linq;
using UnoDos.Decks.Interfaces;
using UnoDos.Players.Interfaces;
using UnoDos.Players.Entities;
using UnityEngine.UI;
using Assets.Scripts.Screen_Navigation.StaticClasses;
using UnityEngine.SceneManagement;
using UnoDos.Cards.Enums;

public class GamePlay : MonoBehaviour
{
    private const int NO_OF_CARDS_TO_DEAL = 10;

    // Start is called before the first frame update
    public Sprite __BackCardSprite;
    public GameObject __CardSprite;
    private ICPU __CPU;
    public IDeck __Deck;
    public GameObject __DeckButton;
    public Sprite[] __GreenCardSprites;
    public GameObject __LastPlayedCard;
    public Sprite[] __MinusTwoCardSprites;
    public GameObject __OpponentArea;
    public Sprite[] __OrangeCardSprites;
    public Sprite[] __PinkCardSprites;
    private PlayCard __PlayCard;
    private IPlayer __Player;
    public GameObject __PlayerArea;
    public Sprite[] __PurpleCardSprites;
    public RenderSprites __RenderSprites;
    public Sprite[] __ResetCardSprites;
    public Sprite[] __SpecialCardSprites;
    public Sprite[] __SwapDeckCardSprites;

    //Co-routine is used to update canvas mid method rather than waiting until end
    public void CardClicked(GameObject currentCardClicked)
    {
        StartCoroutine(UserPlaysCard(currentCardClicked));
    }

    private void CPUPlaysCard()
    {
        __Deck = __PlayCard.CPUPlaysCard();
        __Player = __PlayCard.Player;
        __CPU = __PlayCard.CPU;
        SetPlayerHandCardSprites();
        SetCPUHandCardSprites();
        SetLastPlayedCardSprite(__Deck.LastCardPlayed);
    }

    public void DealCardsOnGameStart()
    {
        __Player.Cards = __Deck.Deal(NO_OF_CARDS_TO_DEAL);

        __CPU.Cards = __Deck.Deal(NO_OF_CARDS_TO_DEAL);

        SetPlayerHandCardSprites();

        SetCPUHandCardSprites();

        ICard _StartingCard = __Deck.DrawInitialCard();
        __LastPlayedCard.SetActive(true);
        SetLastPlayedCardSprite(_StartingCard);
    }

    public IEnumerator DealCards()
    {
        if (__Player.Cards == null || __CPU.Cards == null)
        {
            yield return new WaitForSeconds(.01f);
            DealCardsOnGameStart();
        }

        else if (__PlayCard.IsPlayerTurn)
        {
            ICard _DrawnCard = __Deck.DrawCard();

            GameObject _PlayerDrawnCard = Instantiate(__CardSprite, new Vector3(0, 0, 0), Quaternion.identity);
            _PlayerDrawnCard.GetComponent<Image>().sprite = __RenderSprites.GetSprite(_DrawnCard);
            _PlayerDrawnCard.transform.SetParent(__PlayerArea.transform, false);
            _PlayerDrawnCard.name = _DrawnCard.ToString();

            __Player.Cards.Add(_DrawnCard);
            __PlayCard.IsPlayerTurn = false;
            SetPlayerHandCardSprites();

            //User has selected to pick up a card - CPU's turn
            yield return new WaitForSeconds(2f);
            CPUPlaysCard();
        }
    }

    //Co-routine is used to update canvas mid method rather than waiting until end
    public void DeckClicked()
    {
        StartCoroutine(DealCards());
    }

    public void Start()
    {
        __Player = new Player();
        __CPU = new CPU();
        // Create a new instance of a Deck
        __Deck = new Deck();
        __PlayCard = new PlayCard(__Deck, __CPU, __Player);

        // Create the deck of cards
        __Deck.CreateDeck();

        // shuffle the deck
        __Deck.Shuffle();

        // Deal cards to the player
        //DealCards();

        __RenderSprites = new RenderSprites(__GreenCardSprites, __OrangeCardSprites, __PinkCardSprites, __PurpleCardSprites, __SpecialCardSprites, __ResetCardSprites, __SwapDeckCardSprites, __MinusTwoCardSprites);
    }

    private void SetCPUHandCardSprites()
    {
        __OpponentArea.transform.DetachChildren();

        foreach (ICard _Card in __CPU.Cards)
        {
            GameObject _CPUDrawnCard = Instantiate(__CardSprite, new Vector3(0, 0, 0), Quaternion.identity);
            _CPUDrawnCard.GetComponent<Image>().sprite = __BackCardSprite;
            _CPUDrawnCard.transform.SetParent(__OpponentArea.transform, false);
            _CPUDrawnCard.name = _Card.ToString();
        }
    }

    private void SetLastPlayedCardSprite(ICard card)
    {
        __LastPlayedCard.GetComponent<Image>().sprite = __RenderSprites.GetSprite(card);
    }

    private void SetPlayerHandCardSprites()
    {
        __PlayerArea.transform.DetachChildren();

        foreach (ICard _Card in __Player.Cards)
        {
            GameObject _PlayerDrawnCard = Instantiate(__CardSprite, new Vector3(0, 0, 0), Quaternion.identity);
            _PlayerDrawnCard.GetComponent<Image>().sprite = __RenderSprites.GetSprite(_Card);
            //Instead of calling the method directly use a co-routine otherwise the canvas only gets updated after the method is finished
            _PlayerDrawnCard.GetComponent<Button>().onClick.AddListener(() => CardClicked(_PlayerDrawnCard));
            _PlayerDrawnCard.transform.SetParent(__PlayerArea.transform, false);
            _PlayerDrawnCard.name = _Card.ToString();
        }
    }

    public IEnumerator UserPlaysCard(GameObject currentCardClickedGO)
    {

        if (__PlayCard.IsPlayerTurn)
        {
            yield return new WaitForSeconds(.01f);
            ICard _CurrentCardClickedCard = __Player.Cards.FirstOrDefault(card => card.ToString() == currentCardClickedGO.name);
            __PlayCard.PlayedCard = _CurrentCardClickedCard;
            __Deck = __PlayCard.PlayerPlaysCard();
            __Player = __PlayCard.Player;
            __CPU = __PlayCard.CPU;
            SetPlayerHandCardSprites();
            SetCPUHandCardSprites();
            SetLastPlayedCardSprite(__Deck.LastCardPlayed);
            if (__Player.Cards.Count == 0)
            {
                SceneManager.LoadScene(SceneNames.WINNER_SCREEN);
            }
        }
        if (!__PlayCard.IsPlayerTurn)
        {
            //User has selected a valid card - CPU's turn
            yield return new WaitForSeconds(2f);
            CPUPlaysCard();
            if (__CPU.Cards.Count == 0)
            {
                SceneManager.LoadScene(SceneNames.GAME_OVER);
            }
        }
        //User has selected an invalid card - User's turn
    }
}
