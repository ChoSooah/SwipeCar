using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using TMPro;
using UnityEditor.Experimental.GraphView;

public class GameDirector : MonoBehaviour
{
    int gameChance = 5;
    // 게임에서 주어진 총 도전 횟수, 기본값은 5
    // int: 정수형 자료형
    // gameChance: 플레이어의 남은 기회를 나타내는 변수

    // find 대신 SerializeField로 인스펙터 창에서 직접 오브젝트를 설정
    [SerializeField] private GameObject car; // 자동차 오브젝트
    [SerializeField] private GameObject flag; // 깃발 오브젝트
    [SerializeField] private GameObject distance; // 자동차-깃발 사이 측정한 거리를 표시해 주는 UI 텍스트
    [SerializeField] private GameObject count; // 남은 도전 횟수를 표시해 주는 UI 텍스트
    [SerializeField] private GameObject score; // 현재 점수를 표시해 주는 UI 텍스트
    [SerializeField] private CarTriggerCheck carTriggerCheck; // 자동차가 깃발 or 바닥에 닿았는지 체크하는 Trigger 스크립트
    [SerializeField] private MySceneManager mySceneManager; // 씬 이동 스크립트

    public event EventHandler OnReset;
    // 다른 스크립트(CarController)가 이 이벤트에 반응하도록 만들기 위한 스크립트
    // GameDirector.Reset()이 호출될 때 자동으로 전달됨
    // CarController에 HandleReset()이 연결되어 있음

    void Start()
    {

    }

    void Update()
    {
        float length = this.flag.transform.position.x - this.car.transform.position.x; // 자동차와 깃발 사이 거리 계산(깃발 x좌표 - 자동차 x좌표 = 남은 거리)
        this.distance.GetComponent<TextMeshProUGUI>().text = "Distance: " + length.ToString("F2") + "m"; // 컴포넌트를 가져와 UI 텍스트 내용 업데이트(소수점 둘째 자리까지 표시)
        this.count.GetComponent<TextMeshProUGUI>().text = "Chance: " + gameChance.ToString(); // 남은 도전 기회 UI 텍스트 내용 업데이트
        this.score.GetComponent<TextMeshProUGUI>().text = "Score: " + carTriggerCheck.Score.ToString(); // 얻은 점수 UI 텍스트 내용 업데이트
    }

    public void Reset()
    {
        OnReset?.Invoke(this, EventArgs.Empty); //이벤트를 호출
        gameChance -= 1; // 남은 도전 기회 1씩 감소

        if (carTriggerCheck.Score == 300) // 게임 승리 조건 = 300점 이상
        {
            mySceneManager.LoadVictoryScene(); // 게임 승리 씬으로 이동
        }
        else if (gameChance == 0) // 게임 오버 조건 = 300점 미만 (0)
        {
            mySceneManager.LoadGameEndScene(); // 게임 오버 씬으로 이동
        }
    }
}