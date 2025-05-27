using UnityEngine;

public class CardItem : MonoBehaviour
{
    private Vector3 originalPosition;
    private SpriteRenderer sr;
    private Collider2D col;

    void Start()
    {
        originalPosition = transform.position;
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    public void Hide()
    {
        sr.enabled = false;
        col.enabled = false;
        SoundManager.instance.PlaySE();
    }

    public void Show()
    {
        transform.position = originalPosition;
        sr.enabled = true;
        col.enabled = true;
    }
    

}
