using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            string sceneName = SceneManager.GetActiveScene().name;
            if (sceneName != "GameScene")
            {
                LoadGameScene();
            }
        }
    }

    void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void LoadVictoryScene()
    {
        SceneManager.LoadScene("VictoryScene");
    }

    public void LoadGameEndScene()
    {
        SceneManager.LoadScene("GameEndScene");
    }
}