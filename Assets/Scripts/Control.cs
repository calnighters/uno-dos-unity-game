using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Control : MonoBehaviour
{

    List<PlayerInterface> players = new List<PlayerInterface>();
    private Deck deck;
    private List<Card> discardPile;
    public GameObject playerHand;
    public GameObject gameLogObject;
    public Text gameLog;

    public GameObject discardPileObject;

    public GameObject regCardPrefab;
    public GameObject resetCardPrefab;
    public GameObject swapDeckPrefab;
    public GameObject loseTwoPrefab;
    public GameObject seeThroughPrefab;

    public GameObject[] colors = new GameObject[4];
    string[] colorsMatch = new string[4] { "Pink", "Green", "Orange", "Purple" };
    public GameObject[] aiPlayers = new GameObject[5];
    public GameObject colorText;
    public GameObject deckGO;
    public GameObject pauseCan;
    public GameObject rulesCan;
    public GameObject endCan;
    bool enabledStat = false;

    float timer = 0;

    public static int numbOfAI;

    private Game game;

    // Called at the start to initialise the game
    void Start()
    {
        // Create deck and discard pile
        discardPile = new List<Card>();
        deck = new Deck(regCardPrefab, resetCardPrefab, swapDeckPrefab, loseTwoPrefab, seeThroughPrefab);

        // Add players to game
        players.Add(new HumanPlayer("You"));
        for (int i = 0; i < numbOfAI; i++)
        {
            AiPlayer aiPlayer = new AiPlayer("AI " + (i + 1));
            players.Add(aiPlayer);
            aiPlayers[i].SetActive(true);
            aiPlayers[i].transform.Find("Name").GetComponent<Text>().text = aiPlayer.getName();
        }

        Card first = null;
        if (deck.cardAtIdx(0).getNumb() < 10)
        {
            first = deck.cardAtIdx(0);
        }
        else
        {
            while (deck.cardAtIdx(0).getNumb() >= 10)
            {
                deck.addCard(deck.cardAtIdx(0));
                deck.removeAtIdx(0);
            }
            first = deck.cardAtIdx(0);
        }
        discardPile.Add(first);
        discardPileObject = first.loadCard(725, -325, GameObject.Find("Main").transform);
        deck.removeAtIdx(0);

        foreach (PlayerInterface player in players)
        {
            for (int i = 0; i < 7; i++)
            {
                player.addCards(deck.cardAtIdx(0));
                deck.removeAtIdx(0);
            }
        }

        game = new Game(players, discardPile, deck);
    }

    void Update()
    { //this runs the players turns
        bool win = updateCardsLeft();
        if (win)
        {
            return;
        }
        game.takeTurn();
    }

    public bool updateCardsLeft()
    { //this updates the number below each ai, so the player knows how many cards they have left
        for (int i = 0; i < players.Count - 1; i++)
        {
            int temp = players[i + 1].getCardsLeft();
            aiPlayers[i].transform.Find("CardsLeft").GetComponent<Text>().text = temp.ToString();
        }
        foreach (PlayerInterface i in players)
        {
            if (i.getCardsLeft() == 0)
            {
                this.enabled = false;
                updateLog(string.Format("{0} won!", i.getName()));
                endCan.SetActive(true);
                endCan.transform.Find("WinnerTxt").gameObject.GetComponent<Text>().text = string.Format("{0} Won!", i.getName());
                return true;
            }
        }
        return false;
    }

    public void pause(bool turnOnOff)
    { //turns the pause canvas on/off
        if (turnOnOff)
        {
            pauseCan.SetActive(true);
            enabledStat = this.enabled;
            this.enabled = false;
        }
        else
        {
            pauseCan.SetActive(false);
            this.enabled = enabledStat;
        }
    }

    public void showRules(bool turnOnOff)
    { //turns the rules canvas on/off
        if (turnOnOff)
        {
            rulesCan.SetActive(true);
            enabledStat = this.enabled;
            this.enabled = false;
        }
        else
        {
            rulesCan.SetActive(false);
            this.enabled = enabledStat;
        }
    }

    public void returnHome()
    { //loads the home screen
        UnityEngine.SceneManagement.SceneManager.LoadScene("Start");
    }

    public void exit()
    { //quits the app
        Application.Quit();
    }

    public void playAgain()
    { //resets everything after a game has been played
        this.enabled = false;
        players.Clear();
        gameLog.text = "";
        gameLogObject.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
        endCan.SetActive(false);
        for (int i = playerHand.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(playerHand.transform.GetChild(i).gameObject);
        }
        Destroy(discardPileObject);
        Start();
        this.enabled = true;
    }

    // Update game log box on screen
    public void updateLog(string text)
    {
        gameLog.text += text + "\n";
        gameLogObject.GetComponent<RectTransform>().localPosition = new Vector2(0, gameLogObject.GetComponent<RectTransform>().sizeDelta.y);
    }

    public void updateDiscardPile(Card card)
    {
        game.updateDiscardPile(card);
    }

    public void playReset()
    {

    }

    public void loseTwoCards(List<Card> discardedCards)
    {
        game.addToBottomOfDeck(discardedCards);
    }

    public void swapDeck()
    {

    }

    public void playSeeThrough()
    {

    }

    public void drawCard(int amount, PlayerInterface player)
    {
        game.drawCard(amount, player);
    }
}
