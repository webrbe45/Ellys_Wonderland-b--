using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PokerGate : MonoBehaviour
{
    public string nextSceneName;

    public GameObject correctCardPrefab;  // ���� ī�� (Tag: "O")
    public GameObject wrongCardPrefab;    // ���� ī�� (Tag: "X")
    public Transform[] cardSpawnPoints;   // ī�� ���� ��ġ�� (5��)

    private int correctCardCount = 0;
    private bool hasWrongCard = false;
    private bool isGateOpen = false;

    private List<GameObject> collectedCards = new List<GameObject>();

    void Update()
    {
        if (correctCardCount == 5)
        {
            if (hasWrongCard)
            {
                ResetCards(); // ���� �� ī�� ����
            }
            else
            {
                isGateOpen = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isGateOpen)
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadScene(nextSceneName);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = Vector3.zero;
        }
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void CollectCard(GameObject card)
    {
        if (card.CompareTag("O"))
        {
            correctCardCount++;
        }
        else if (card.CompareTag("X"))
        {
            hasWrongCard = true;
        }

        collectedCards.Add(card);
        card.SetActive(false);
    }

    private void ResetCards()
    {
        correctCardCount = 0;
        hasWrongCard = false;
        isGateOpen = false;

        foreach (GameObject card in collectedCards)
        {
            if (card != null) Destroy(card);
        }

        collectedCards.Clear();

        // ī�� ���ȯ
        foreach (Transform spawnPoint in cardSpawnPoints)
        {
            float rand = Random.value;
            GameObject cardPrefab = rand < 0.7f ? correctCardPrefab : wrongCardPrefab;

            Instantiate(cardPrefab, spawnPoint.position, Quaternion.identity);
        }
    }
}
