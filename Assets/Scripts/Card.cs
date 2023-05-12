using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class Card : MonoBehaviour
{

    /*
	* 0-9 are regular
	* 10 is reset
	* 11 is lose two
	* 12 is swap deck
	* 13 is see through
	*/

    int number;
    string color;
    GameObject cardObj;

    public Card(int numb, string color, GameObject obj)
    { //defines the object
        number = numb;
        this.color = color;
        cardObj = obj;
    }
    public GameObject loadCard(int x, int y, Transform parent)
    { //when ran, it tells where to load the card on the screen
        GameObject temp = loadCard(parent);
        temp.transform.localPosition = new Vector2(x, y + 540);
        return temp;
    }
    public GameObject loadCard(Transform parent)
    { //does all the setup for loading. Used if card doesn't need a specific position		
        GameObject temp = Instantiate(cardObj);
        temp.name = color + number;
        if (number < 10)
        {
            foreach (Transform childs in temp.transform)
            {
                if (childs.name.Equals("Cover"))
                    break;
                childs.GetComponent<Text>().text = number.ToString();
            }
            temp.transform.GetChild(1).GetComponent<Text>().color = returnColor(color);
        }
        else if (number == 10 || number == 11 || number == 12)
        {
            temp.transform.GetChild(1).GetComponent<RawImage>().color = returnColor(color);
        }
        else if (number == 13)
        {
            temp.transform.GetChild(1).GetComponent<Text>().color = returnColor(color);
        }

        temp.GetComponent<RawImage>().texture = Resources.Load(color + "Card") as Texture2D;
        temp.transform.SetParent(parent);
        temp.transform.localScale = new Vector3(1, 1, 1);
        return temp;
    }
    Color returnColor(string what)
    { //returns a color based on the color string
        switch (what)
        {
            case "Green":
                return new Color32(65, 226, 76, 255);
            case "Pink":
                return new Color32(252, 49, 211, 255);
            case "Orange":
                return new Color32(252, 150, 49, 255);
            case "Purple":
                return new Color32(204, 49, 252, 255);
        }
        return new Color(0, 0, 0);
    }
    public int getNumb()
    { //accessor for getting the number
        return number;
    }
    public string getColor()
    { //accessor for getting the color
        return color;
    }
    public bool Equals(Card other)
    { //overides the original Equals so that color or number must be equal
        return other.getNumb() == number || other.getColor().Equals(color);
    }
    public void changeColor(string newColor)
    { //mutator that changes the color of a wild card to make the color noticable
        color = newColor;
    }

    public bool canBePlayedOn(Card card)
    {
        bool colourMatches = card.getColor().Equals(color);
        if (number > 9)
        {
            if (colourMatches || number == 13)
            {
                return true;
            }
            return false;
        }
        if (card.getNumb() > 9)
        {
            if (colourMatches || number == 13)
            {
                return true;
            }
            return false;
        }
        return (Math.Abs(card.getNumb() - this.number) == 1 && colourMatches);
    }
}
