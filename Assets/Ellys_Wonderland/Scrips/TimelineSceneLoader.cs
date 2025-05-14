using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

public class TimelineSceneLoader : MonoBehaviour
{
    [Header("로딩할 씬 이름")]
    public string nextSceneName;

    // 시그널 리시버가 이 메서드를 호출
    public void LoadNextScene()
    {
        Debug.Log($"[TimelineSceneLoader] Loading scene: {nextSceneName}");
        SceneManager.LoadScene(nextSceneName);
    }
}
