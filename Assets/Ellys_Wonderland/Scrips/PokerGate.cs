using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PokerGate : MonoBehaviour
{
    public string nextSceneName;

    public GameObject correctCardPrefab;  // 정답 카드 (Tag: "O")
    public GameObject wrongCardPrefab;    // 오답 카드 (Tag: "X")
    public Transform[] cardSpawnPoints;   // 카드 생성 위치들 (5개)

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
                ResetCards(); // 실패 시 카드 리셋
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

        // 카드 재소환
        foreach (Transform spawnPoint in cardSpawnPoints)
        {
            float rand = Random.value;
            GameObject cardPrefab = rand < 0.7f ? correctCardPrefab : wrongCardPrefab;

            Instantiate(cardPrefab, spawnPoint.position, Quaternion.identity);
        }
    }
}
