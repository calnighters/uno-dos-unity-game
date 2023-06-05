using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnoDos.Cards.Entities;
using UnoDos.Cards.Enums;
using UnoDos.Cards.Interfaces;
using UnoDos.Decks.Interfaces;
using UnoDos.Players.Interfaces;

public class PlayerInput : MonoBehaviour
{

    private GamePlay gamePlay;
    // Start is called before the first frame update
    void Start()
    {
        gamePlay = FindObjectOfType<GamePlay>();

    }

    // Update is called once per frame
    void Update()
    {
        GetMouseClick();

    }

    void GetMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10));
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit)
            {
                switch (hit.collider.tag)
                {
                    case ("Deck"):
                        Deck();
                        break;
                    case ("Card"):
                        Card();
                        break;
                    case ("PlayedPile"):
                        PlayedPile();
                        break;
                    default:
                        break;

                }
            }
        }
    }

    void Deck()
    {
        print("Clicked on the Deck");
        gamePlay.DeckClicked();

    }

    void Card()
    {
        print("Clicked on the Card");

    }

    void PlayedPile()
    {
        print("Clicked on the played pile");
    }
}
