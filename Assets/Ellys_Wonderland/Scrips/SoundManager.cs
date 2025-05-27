using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("효과음 설정")]
    [SerializeField] private AudioClip sfxClip;        // 사용할 효과음
    [SerializeField] private AudioSource sfxPlayer;    // 효과음 재생기

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void PlaySE()
    {
        if (sfxPlayer == null || sfxClip == null)
        {
            Debug.LogError("효과음 클립 또는 플레이어가 설정되지 않았습니다.");
            return;
        }

        sfxPlayer.PlayOneShot(sfxClip);
    }
}