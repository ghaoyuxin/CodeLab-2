using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class BlackJackManager : MonoBehaviour
{

    public Text statusText;
    public Text round;
    public GameObject tryAgain;
    public string loadScene;

    public GameObject hitButton, stayButton; //public Hit Button, Stay Button

    void Start()
    {
        int placeholder = PublicVars.roundCount % 5 + 1; //load the scene with round count
        round.text = "Round: " + placeholder + "/5";
    }
    public void PlayerBusted()
    {
        HideShowPlayerButtons(false);
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
        GameOverText("Black Jack!", Color.black);
        HideShowPlayerButtons(false);
    }

    public void GameOverText(string str, Color color)
    {
        statusText.text = str;
        statusText.color = color;

        tryAgain.SetActive(true);
    }

    public void HideShowPlayerButtons(bool showButtons) //make this a switch
    {
        hitButton.SetActive(showButtons);
        stayButton.SetActive(showButtons);
    }

    public void TryAgain()
    {
        CountRound();
        //reload the scene
        SceneManager.LoadScene(loadScene);
    }

    public void CountRound()  //Round how many round you have played, update the canvas
    {
        PublicVars.roundCount++;
        print(PublicVars.roundCount);
    }

    public virtual int GetHandValue(List<DeckOfCards.Card> hand)//
    {
        int handValue = 0;
        int AceRound = 0;

        foreach (DeckOfCards.Card handCard in hand)
        {
            handValue += handCard.GetCardHighValue();
            if (handCard.GetCardHighValue() == 11) AceRound++; // ace Round
        }

        for (int i = 0; i < AceRound; i++)
        {
            if (handValue > 21) handValue -= 10;
        }
        //if player has Black Jack, and the handValue is > 21, the A card value should be 1, 
        //if (!ReferenceEquals((DeckOfCards.Card.Type.A), null) && handValue > 21) return handValue - 10;
        return handValue;
    }
}
