using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PokerGate : MonoBehaviour
{
    public string nextSceneName;
    public List<CardItem> allCards = new List<CardItem>();

    private int correctCardCount = 0;
    private bool hasWrongCard = false;
    private bool isGateOpen = false;

    void Update()
    {
        if (correctCardCount == 5 && !hasWrongCard)
        {
            isGateOpen = true;
        }
    }

    public void CollectCard(GameObject cardObj)
    {
        CardItem card = cardObj.GetComponent<CardItem>();

        if (cardObj.CompareTag("O"))
        {
            correctCardCount++;
        }
        else if (cardObj.CompareTag("X"))
        {
            hasWrongCard = true;
        }

        card.Hide();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        if (isGateOpen)
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadScene(nextSceneName);
        }
        else if (hasWrongCard)
        {
            ResetGateState();

            foreach (CardItem card in allCards)
            {
                card.Show();
            }
        }
    }

    private void ResetGateState()
    {
        correctCardCount = 0;
        hasWrongCard = false;
        isGateOpen = false;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = new Vector3(0f, 0f, 0f);
        }
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
