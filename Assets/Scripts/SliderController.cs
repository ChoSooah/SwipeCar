using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SliderController : MonoBehaviour
{
    float sliderSpeed = 1f; // 슬라이더의 속도
    bool isSliderPingPong = false; // 슬라이더가 이동 중인지 확인
    float newXPosition; // 슬라이더가 반복적으로 이동할 x좌표

    [SerializeField] Slider slider; // 슬라이더를 가져올 변수
    // SerializeField: private이나 Inspector에서 접근 가능
    [SerializeField] float sliderValue = 0f; // 슬라이더가 정지한 위치
    public float sValue => sliderValue; // 위의 변수를 외부에서 참조하기 위해 만든 public 변수, 외부에서 값 수정이 불가능함

    [SerializeField] private ArrowController arrowController; // // ArrowController에서 이벤트를 받기 위해 참조
    [SerializeField] private GameDirector gameDirector; // 게임이 리셋될 때 실린더도 초기화
    public event EventHandler OnSliderStop; // 슬라이더가 정지할 때 이벤트 생성

    void Start()
    {
        arrowController.OnArrowStop += HandleArrowStop; // 화살표가 멈추면 HandleArrowStop()을 호출
        gameDirector.OnReset += HandleReset; // 게임 리셋 시 HandleReset() 실행
    }

    void Update()
    {
        if (isSliderPingPong == true && Input.GetKeyDown(KeyCode.Space)) // 슬라이더가 이동 중일 때 스페이스 바를 누를 시
        {
            isSliderPingPong = false;
            PingPongStop(); // 슬라이더 멈춤
        }

        newXPosition = Mathf.PingPong(Time.time * sliderSpeed, slider.maxValue); // Mathf.PingPong를 이용해 정해진 속도로 정해진 값을 왕복

        if (isSliderPingPong) // isSliderPingPong이 true라면
        {
            slider.value = newXPosition; // 슬라이더의 값을 바꿈
        }
    }

    void PingPongStop() // 슬라이더가 멈출 때 불러오는 함수 
    {
        sliderValue = slider.value; // 슬라이더의 현 위치를 저장
        OnSliderStop?.Invoke(this, EventArgs.Empty); // 이벤트를 호출
    }

    void HandleArrowStop(object Arrow, EventArgs e)
    {
        isSliderPingPong = true; // 슬라이더 왕복 시작
    }

    void HandleReset(object GameDirector, EventArgs e)
    {
        isSliderPingPong = false; // 슬라이더 정지
        newXPosition = 0f; // 슬라이더 값 초기화
        slider.value = 0f; // 슬라이더 UI 초기화
    }
}
