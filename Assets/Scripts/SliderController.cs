using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SliderController : MonoBehaviour
{
    float sliderSpeed = 1f; //실린더의 속도
    bool isSliderPingPong = false; //실린더가 움직이는 중인가?
    float newXPosition; //핑퐁시킬 x좌표

    [SerializeField] Slider slider; //실린더를 가져올 변수
    //시리얼라이즈필드 : 프라이빗이나 인스펙터에서 접근 가능.
    [SerializeField] float sliderValue = 0f; //실린더가 멈춘 위치
    public float sValue => sliderValue; //위의 변수를 외부에서 참고하기 위해 만든 퍼블릭 변수. 이렇게 하면 외부에서 값 수정이 불가능하다.

    public event EventHandler OnSliderStop; //이벤트 생성

    void Update()
    {
        if (isSliderPingPong == false && Input.GetKeyDown(KeyCode.Space)) //슬라이더가 움직이지 않고 있고, 스페이스바가 눌렸다면
        {
            isSliderPingPong = true;
        }
        else if (isSliderPingPong == true && Input.GetKeyDown(KeyCode.Space)) //또는 슬라이더가 움직이고 있고 스페이스바가 눌렸다면.
        {
            isSliderPingPong = false;
            PingPongStop();
        }

        newXPosition = Mathf.PingPong(Time.time * sliderSpeed, slider.maxValue); //Mathf.PingPong를 이용해 정해진 속도로 정해진 값을 왕복한다.

        if (isSliderPingPong) //isSliderPingPong이 true라면
        {
            slider.value = newXPosition; //슬라이더의 값을 바꾼다.
        }
    }

    void PingPongStop() //슬라이더가 멈출 때 불러오는 함수 
    {
        sliderValue = slider.value; //슬라이더의 현 위치를 저장한다.
        OnSliderStop?.Invoke(this, EventArgs.Empty); //이벤트를 호출한다.
    }
}
