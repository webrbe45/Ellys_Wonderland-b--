using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReloader : MonoBehaviour
{
    public void ReloadCurrentScene()
    {
        // ���� Ȱ��ȭ�� ���� �̸��� �����ɴϴ�.
        string currentSceneName = SceneManager.GetActiveScene().name;

        // ���� �ٽ� �ε��մϴ�.
        SceneManager.LoadScene(currentSceneName);
    }
}
