using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class BlackJackManager : MonoBehaviour
{

    public Text statusText;
    public GameObject tryAgain;
    public string loadScene;

    public void PlayerBusted()
    {
        HidePlayerButtons();
        GameOverText("YOU BUST", Color.red);
    }

    public void DealerBusted()
    {
        GameOverText("DEALER BUSTS!", Color.green);
    }

    public void PlayerWin()
    {
        GameOverText("YOU WIN!", Color.green);
    }

    public void PlayerLose()
    {
        GameOverText("YOU LOSE.", Color.red);
    }


    public void BlackJack()
    {
        GameOverText("Black Jack!", Color.green);
        HidePlayerButtons();
    }

    public void GameOverText(string str, Color color)
    {
        statusText.text = str;
        statusText.color = color;

        tryAgain.SetActive(true);
    }

    public void HidePlayerButtons()
    {
        GameObject.Find("HitButton").SetActive(false);
        GameObject.Find("StayButton").SetActive(false);
    }

    public void TryAgain()
    {
        SceneManager.LoadScene(loadScene); // this is a suprising way to loadScene
    }

    public virtual int GetHandValue(List<DeckOfCards.Card> hand)//
    {
        int handValue = 0;
        int AceCount = 0;

        foreach (DeckOfCards.Card handCard in hand)
        {
            handValue += handCard.GetCardHighValue();
            if (handCard.GetCardHighValue() == 11) AceCount++; // ace count
        }

        for (int i = 0; i < AceCount; i++)
        {
            if (handValue > 21) handValue -= 10;
        }
        //if player has Black Jack, and the handValue is > 21, the A card value should be 1, 
        //if (!ReferenceEquals((DeckOfCards.Card.Type.A), null) && handValue > 21) return handValue - 10;

        return handValue;
    }
}
