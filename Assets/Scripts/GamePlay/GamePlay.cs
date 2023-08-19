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
using Assets.Scripts.Settings;
using System;
using Assets.Scripts.Players.Difficulty.Enums;
using Assets.Scripts.Players.GameModes.Enums;

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
    public GameObject __LastNonSTCard;
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
    public Sprite[] __SeeThroughSprites;
    public Sprite[] __SwapDeckCardSprites;

    //Co-routine is used to update canvas mid method rather than waiting until end
    public void CardClicked(GameObject currentCardClicked)
    {
        StartCoroutine(UserPlaysCard(currentCardClicked));
    }

    private IEnumerator CPUPlaysCard()
    {
        __CPU.HasCPUPlayedCard = false;
        //While loop in-case the CPU plays a reset - in this class so it is updated on canvas and there is a pause between next play
        while (!__PlayCard.IsPlayerTurn)
        {
            //CPU Delay - to look like it is thinking
            yield return new WaitForSeconds(2f);
            __Deck = __PlayCard.CPUPlaysCard();
            __Player = __PlayCard.Player;
            __CPU = __PlayCard.CPU;
            SetPlayerHandCardSprites();
            SetCPUHandCardSprites();
            SetLastPlayedCardSprite(__Deck.LastCardPlayed, __Deck.getLastNonSTCard());
        }
    }

    public void DealCardsOnGameStart()
    {
        __Player.Cards = __Deck.Deal(NO_OF_CARDS_TO_DEAL);

        __CPU.Cards = __Deck.Deal(NO_OF_CARDS_TO_DEAL);

        SetPlayerHandCardSprites();

        SetCPUHandCardSprites();

        ICard _StartingCard = __Deck.DrawInitialCard();
        __LastPlayedCard.SetActive(true);
        __LastNonSTCard.SetActive(true);
        SetLastPlayedCardSprite(_StartingCard, _StartingCard);
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
            yield return new WaitForSeconds(.01f);
            __Player.HasPlayerPlayedCard = false;
            StartCoroutine(CPUPlaysCard());
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
        __CPU = new CPU(__Player) { CPUDifficulty = GameSettings.SelectedDifficulty};
        // Create a new instance of a Deck
        __Deck = new Deck();
        __PlayCard = new PlayCard(__Deck, __CPU, __Player);

        // Create the deck of cards
        __Deck.CreateDeck();

        // shuffle the deck
        __Deck.Shuffle();

        // Deal cards to the player
        //DealCards();

        __RenderSprites = new RenderSprites(__GreenCardSprites, __OrangeCardSprites, __PinkCardSprites, __PurpleCardSprites, __SeeThroughSprites, __ResetCardSprites, __SwapDeckCardSprites, __MinusTwoCardSprites);
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

    private void SetLastPlayedCardSprite(ICard playedCard, ICard nonSTCard)
    {
        __LastPlayedCard.GetComponent<Image>().sprite = __RenderSprites.GetSprite(playedCard);
        __LastNonSTCard.GetComponent<Image>().sprite = __RenderSprites.GetSprite(nonSTCard);
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
            __CPU.Player = __Player;
            SetPlayerHandCardSprites();
            SetCPUHandCardSprites();
            SetLastPlayedCardSprite(__Deck.LastCardPlayed, __Deck.getLastNonSTCard());
            //Used for testing - comment out
            Winner("Player");
            if (__Player.Cards.Count == 0)
            {
                Winner("Player");
            }
        }
        if (!__PlayCard.IsPlayerTurn)
        {
            //User has selected a valid card - CPU's turn
            yield return new WaitForSeconds(.01f);

            StartCoroutine(CPUPlaysCard());
            if (__CPU.Cards.Count == 0)
            {
                Winner("CPU");
            }
        }
        //User has selected an invalid card - User's turn
    }

    private void Winner(string winner)
    {
        GameMode _GameMode = GameSettings.SelectedMode;
        if(_GameMode == GameMode.SingleRound)
        {
            if (winner == "Player")
            {
                GameSettings.Winner = WinnerObject.Player;
                SceneManager.LoadScene(SceneNames.WINNER_SCREEN);
            }
            else
            {
                GameSettings.Winner = WinnerObject.CPU;
                SceneManager.LoadScene(SceneNames.GAME_OVER);
            }
        }
        if(_GameMode == GameMode.MultipleRounds)
        {
            int _currentPlayerScore = GameSettings.PlayerScore;
            GameSettings.PlayerScore = _currentPlayerScore + __Player.CalculateScore();
            int _currentCPUScore = GameSettings.CPUScore;
            GameSettings.CPUScore = _currentCPUScore + __CPU.CalculateScore();
            SceneManager.LoadScene(SceneNames.ROUND_SCORE);
        }
    }
}
