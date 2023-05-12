using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PlayerInterface
{ //the interface the is inherited by the player objs
    void turn(List<Card> disardPile);
    void addCards(Card other);
    string getName();
    bool Equals(PlayerInterface other);
    int getCardsLeft();
}
