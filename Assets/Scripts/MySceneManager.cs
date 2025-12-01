using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) // Q키로 현재 씬 확인 가능
        {
            string sceneName = SceneManager.GetActiveScene().name; // 현재 실행 중인 씬을 확인
            if (sceneName != "GameScene") // 현재 씬이 "GameScene"이 아닐 경우 
            {
                LoadGameScene(); // GameScene으로 이동
            }
        }
    }

    void LoadGameScene() // GameScene(게임 씬) 로드 함수
    {
        SceneManager.LoadScene("GameScene"); // GameScene 실행
    }

    public void LoadVictoryScene() // LoadVictoryScene(게임 승리 화면) 로드 함수
    {
        SceneManager.LoadScene("VictoryScene"); // LoadVictoryScene 실행
    }

    public void LoadGameEndScene() // GameEndScene(게임 오버 화면) 로드 함수
    {
        SceneManager.LoadScene("GameEndScene"); // GameEndScene 실행
    }
}