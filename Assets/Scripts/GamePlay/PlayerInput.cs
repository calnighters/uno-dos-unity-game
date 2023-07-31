using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private GamePlay __GamePlay;
    void Deck()
    {
        __GamePlay.DeckClicked();

    }

    void GetMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit)
            {
                switch (hit.collider.tag)
                {
                    case ("Deck"):
                        Deck();
                        break;
                    default:
                        break;
                }
            }
        }
    }

    // Start is called before the first frame update
    public void Start()
    {
        __GamePlay = FindObjectOfType<GamePlay>();
    }

    // Update is called once per frame
    public void Update()
    {
        GetMouseClick();
    }
}
