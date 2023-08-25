using System.Collections;
using UnityEngine;
using UnoDos.Decks.Entities;
using UnoDos.Cards.Interfaces;
using Assets.Scripts.Sprites;
using System.Linq;
using UnoDos.Decks.Interfaces;
using UnoDos.Players.Interfaces;
using UnoDos.Players.Entities;
using UnityEngine.UI;
using Assets.Scripts.Screen_Navigation.StaticClasses;
using UnityEngine.SceneManagement;
using Assets.Scripts.Settings;
using Assets.Scripts.Players.GameModes.Enums;
using System.Collections.Generic;
using TMPro;

public class GamePlay : MonoBehaviour
{
    private const string CPU_TURN = "CPU Turn";
    private const string CPU_PLAYER_TURN = "CPU Player {0} Turn";
    private const string CPU_OUT_OF_HP = "CPU Player {0} Has ran out of HP!";
    private const int NO_OF_CARDS_TO_DEAL = 10;
    private const int NO_OF_LIVES = 4;
    private const string PLAYER_TURN = "Player Turn";

    // Start is called before the first frame update
    public Sprite __BackCardSprite;
    public GameObject __CurrentTurnUI;
    public TMP_Text __CurrentTurnText;
    public GameObject __CardSprite;
    private List<ICPU> __CPUPlayers;
    public IDeck __Deck;
    public GameObject __DeckButton;
    public GameObject __GameCanvas;
    public GameSettings __GameSettings;
    public Sprite[] __GreenCardSprites;
    public GameObject __LastPlayedCard;
    public GameObject __LastNonSTCard;
    public Sprite[] __MinusTwoCardSprites;
    public GameObject __OpponentArea;
    public List<GameObject> __OpponentAreas;
    public GameObject __OpponentAreaThree;
    public GameObject __OpponentAreaTwo;
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
        for (int i = 0; i < __CPUPlayers.Count; i++)
        {
            ICPU _TempLastCPU = __CPUPlayers.Last();
            ICPU _CPU = __CPUPlayers[i];
            __CurrentTurnText.text = __GameSettings.CPUPlayerCount > 1 ? string.Format(CPU_PLAYER_TURN, _CPU.CPUPlayerNumber) : CPU_TURN;
            __CurrentTurnUI.SetActive(true);
            __GameCanvas.SetActive(false);
            yield return new WaitForSeconds(2f);
            __CurrentTurnUI.SetActive(false);
            __GameCanvas.SetActive(true);

            _CPU.HasCPUPlayedCard = false;
            __PlayCard.CPU = _CPU;
            //While loop in-case the CPU plays a reset - in this class so it is updated on canvas and there is a pause between next play
            while (!__PlayCard.IsPlayerTurn)
            {
                //CPU Delay - to look like it is thinking
                yield return new WaitForSeconds(2f);
                __Deck = __PlayCard.CPUPlaysCard();
                __Player = __PlayCard.Player;
                __CPUPlayers[i] = _CPU;
                SetPlayerHandCardSprites();
                SetCPUHandCardSprites();
                SetLastPlayedCardSprite(__Deck.LastCardPlayed, __Deck.getLastNonSTCard());

                if (_CPU.RemainingHP == 0 && _CPU.IsHPMode)
                {
                    __CurrentTurnText.text = string.Format(CPU_OUT_OF_HP, _CPU.CPUPlayerNumber);
                    __GameSettings.CPUScores[_CPU.CPUPlayerNumber - 1] = _CPU.CalculateScore();
                    string _OpponentAreaName = __OpponentAreas[i].name;
                    __OpponentAreas.RemoveAt(i);
                    __CPUPlayers.RemoveAt(i);
                    Destroy(GameObject.Find(_OpponentAreaName));

                    __CurrentTurnUI.SetActive(true);
                    __GameCanvas.SetActive(false);
                    yield return new WaitForSeconds(2f);
                    __CurrentTurnUI.SetActive(false);
                    __GameCanvas.SetActive(true);
                    i = i < __CPUPlayers.Count ? i - 1 : i;
                }
            }
            if (__CPUPlayers.Count == 0 || _TempLastCPU != _CPU)
            {
                __PlayCard.IsPlayerTurn = false;
            }
            yield return new WaitForSeconds(1f);

            if (_CPU.Cards.Count == 0)
            {
                Winner(__CPUPlayers.Count > 1 ? $"CPU Player {i + 1} Won." : "The CPU Won.");
            }

            if (__CPUPlayers.Count == 0)
            {
                Winner("Player");
            }
        }

        if (__CPUPlayers.Count != 0)
        {
            __CurrentTurnText.text = PLAYER_TURN;
            __CurrentTurnUI.SetActive(true);
            __GameCanvas.SetActive(false);
            yield return new WaitForSeconds(2f);
            __CurrentTurnUI.SetActive(false);
            __GameCanvas.SetActive(true);
        }
    }

    public void DealCardsOnGameStart()
    {
        __Player.Cards = __Deck.Deal(NO_OF_CARDS_TO_DEAL);

        foreach (ICPU _CPU in __CPUPlayers)
        {
            _CPU.Cards = __Deck.Deal(NO_OF_CARDS_TO_DEAL);
        }

        SetPlayerHandCardSprites();

        SetCPUHandCardSprites();

        ICard _StartingCard = __Deck.DrawInitialCard();
        __LastPlayedCard.SetActive(true);
        __LastNonSTCard.SetActive(true);
        SetLastPlayedCardSprite(_StartingCard, _StartingCard);
    }

    public IEnumerator DealCards()
    {
        if (__Player.Cards == null || __CPUPlayers.Any(cpu => cpu.Cards == null))
        {
            yield return new WaitForSeconds(.01f);
            DealCardsOnGameStart();
        }

        else if (__PlayCard.IsPlayerTurn)
        {
            ICard _DrawnCard = __Deck.DrawCard();
            if (__Player.IsHPMode)
            {
                __Player.RemainingHP -= 1;
                if (__Player.RemainingHP <= 0)
                {
                    Winner("You ran out of HP!");
                }
            }

            GameObject _PlayerDrawnCard = Instantiate(__CardSprite, new Vector3(0, 0, 0), Quaternion.identity);
            _PlayerDrawnCard.GetComponent<Image>().sprite = __RenderSprites.GetSprite(_DrawnCard);
            _PlayerDrawnCard.transform.SetParent(__PlayerArea.transform, false);
            _PlayerDrawnCard.name = _DrawnCard.ToString();

            __Player.Cards.Add(_DrawnCard);
            __PlayCard.IsPlayerTurn = false;
            SetPlayerHandCardSprites();

            //User has selected to pick up a card - CPU's turn
            if (!__Player.IsHPMode || __Player.IsHPMode && __Player.RemainingHP != 0)
            {
                yield return new WaitForSeconds(.01f);
                __Player.HasPlayerPlayedCard = false;
                StartCoroutine(CPUPlaysCard());
            }
        }
    }

    //Co-routine is used to update canvas mid method rather than waiting until end
    public void DeckClicked()
    {
        StartCoroutine(DealCards());
    }

    public void Start()
    {
        __GameSettings = SaveGameSettings.LoadSettings() ?? new GameSettings();
        __OpponentAreas = new List<GameObject> { __OpponentArea, __OpponentAreaTwo, __OpponentAreaThree };
        __Player = new Player();
        __CPUPlayers = new List<ICPU>();
        __Player.IsHPMode = __GameSettings.IsHPMode;
        __Player.RemainingHP = __Player.IsHPMode ? NO_OF_LIVES : 0;
        if (__GameSettings.CPUScores == null)
        {
            __GameSettings.CPUScores = new List<int> { 0, 0, 0 };
        }
        for (int i = 0; i < __GameSettings.CPUPlayerCount; i++)
        {
            __CPUPlayers.Add(new CPU(__Player) { CPUDifficulty = __GameSettings.SelectedDifficulty, IsHPMode = __GameSettings.IsHPMode, RemainingHP = __GameSettings.IsHPMode ? NO_OF_LIVES : 0, CPUPlayerNumber = i + 1, PlayerScore = __GameSettings.CPUScores[i] });
        }
        // Create a new instance of a Deck
        __Deck = new Deck();
        __PlayCard = new PlayCard(__Deck, __CPUPlayers.First(), __Player);

        // Create the deck of cards
        __Deck.CreateDeck();

        // shuffle the deck
        __Deck.Shuffle();

        __RenderSprites = new RenderSprites(__GreenCardSprites, __OrangeCardSprites, __PinkCardSprites, __PurpleCardSprites, __SeeThroughSprites, __ResetCardSprites, __SwapDeckCardSprites, __MinusTwoCardSprites);
    }


    private void SetCPUHandCardSprites()
    {
        for (int i = 0; i < __CPUPlayers.Count; i++)
        {
            GameObject _CurrentOpponentArea = __OpponentAreas[i];
            _CurrentOpponentArea.transform.DetachChildren();

            foreach (ICard _Card in __CPUPlayers[i].Cards)
            {
                GameObject _CPUDrawnCard = Instantiate(__CardSprite, new Vector3(0, 0, 0), Quaternion.identity);
                _CPUDrawnCard.GetComponent<Image>().sprite = __BackCardSprite;
                _CPUDrawnCard.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(169.15f, 225.45f);
                if (__CPUPlayers[i].Cards.Count > 1)
                {
                    _CurrentOpponentArea.GetComponent<HorizontalLayoutGroup>().spacing = (_CurrentOpponentArea.GetComponent<RectTransform>().rect.width - (__CPUPlayers[i].Cards.Count * _CPUDrawnCard.GetComponent<RectTransform>().rect.width)) / (__CPUPlayers[i].Cards.Count - 1);
                }
                _CPUDrawnCard.transform.SetParent(_CurrentOpponentArea.transform, false);
                _CPUDrawnCard.name = _Card.ToString();
            }
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
            _PlayerDrawnCard.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(169.15f, 225.45f);
            //Instead of calling the method directly use a co-routine otherwise the canvas only gets updated after the method is finished
            _PlayerDrawnCard.GetComponent<Button>().onClick.AddListener(() => CardClicked(_PlayerDrawnCard));
            if (__Player.Cards.Count > 1)
            {
                __PlayerArea.GetComponent<HorizontalLayoutGroup>().spacing = (__PlayerArea.GetComponent<RectTransform>().rect.width - (__Player.Cards.Count * _PlayerDrawnCard.GetComponent<RectTransform>().rect.width)) / (__Player.Cards.Count - 1);
            }
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
            SetPlayerHandCardSprites();
            SetCPUHandCardSprites();
            SetLastPlayedCardSprite(__Deck.LastCardPlayed, __Deck.getLastNonSTCard());
            if (__Player.Cards.Count == 0)
            {
                Winner("Player");
                yield break;
            }
            yield return new WaitForSeconds(1.5f);
        }
        if (!__PlayCard.IsPlayerTurn && (!__Player.IsHPMode || __Player.IsHPMode && __Player.RemainingHP != 0))
        {
            //User has selected a valid card - CPU's turn
            yield return new WaitForSeconds(.01f);

            StartCoroutine(CPUPlaysCard());
        }
        //User has selected an invalid card - User's turn
        
    }

    private void Winner(string winner)
    {
        RoundMode _GameMode = __GameSettings.SelectedRound;
        if (_GameMode == RoundMode.SingleRound)
        {
            if (winner == "Player")
            {
                __GameSettings.Winner = WinnerObject.Player;
                SaveGameSettings.SaveSettings(__GameSettings);
                SceneManager.LoadScene(SceneNames.WINNER_SCREEN);
            }
            else
            {
                __GameSettings.Winner = WinnerObject.CPU;
                __GameSettings.CPUWinnerText = winner;
                SaveGameSettings.SaveSettings(__GameSettings);
                SceneManager.LoadScene(SceneNames.GAME_OVER);
            }
        }
        if (_GameMode == RoundMode.MultipleRounds)
        {
            int _currentPlayerScore = __GameSettings.PlayerScore;
            __GameSettings.PlayerScore = _currentPlayerScore + __Player.CalculateScore();
            for (int i = 0; i < __CPUPlayers.Count; i++)
            {
                int _currentCPUScore = __GameSettings.CPUScores[i];
                __GameSettings.CPUScores[i] = _currentCPUScore + __CPUPlayers[i].CalculateScore();
            }
            SaveGameSettings.SaveSettings(__GameSettings);
            SceneManager.LoadScene(SceneNames.ROUND_SCORE);
        }
    }
}
