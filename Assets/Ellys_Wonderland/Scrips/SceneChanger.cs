using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void GoToMainRobby()
    {
        SceneManager.LoadScene("MainRobby");
    }
}
