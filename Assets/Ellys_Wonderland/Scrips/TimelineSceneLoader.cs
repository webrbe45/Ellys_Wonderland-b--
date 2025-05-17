using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

public class TimelineSceneLoader : MonoBehaviour
{
    [Header("�ε��� �� �̸�")]
    public string nextSceneName;


    public void LoadNextScene()
    {
        Debug.Log($"[TimelineSceneLoader] Loading scene: {nextSceneName}");
        SceneManager.LoadScene(nextSceneName);
    }
}
