using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("ȿ���� ����")]
    [SerializeField] private AudioClip sfxClip;        // ����� ȿ����
    [SerializeField] private AudioSource sfxPlayer;    // ȿ���� �����

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void PlaySE()
    {
        if (sfxPlayer == null || sfxClip == null)
        {
            Debug.LogError("ȿ���� Ŭ�� �Ǵ� �÷��̾ �������� �ʾҽ��ϴ�.");
            return;
        }

        sfxPlayer.PlayOneShot(sfxClip);
    }
}