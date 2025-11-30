using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class CarController : MonoBehaviour
{
    float speed = 0f; //속도를 저장할 변수
    //float swipeLength = 0f;

    [SerializeField] private SliderController sliderController; //해당 컴포넌트(스크립트)를 가진 오브젝트를 인스펙터 창에서 연결한다 

    void Start()
    {
        Application.targetFrameRate = 60; //프레임 설정
        sliderController.OnSliderStop += HandleSliderStop; // 이벤트를 구독한다.
    }

    void Update()
    {
        /*if (Input.GetMouseButtonDown(0)) //마우스를 누른 순간
        {
            swipeLength = Input.mousePosition.x; //지금의 x좌표를 저장한다.
        }
        else if (Input.GetMouseButtonUp(0)) //마우스 버튼에서 손을 땐 순간
        {
            swipeLength = Input.mousePosition.x - swipeLength; //지금의 x좌표에 방금 전 저장한 x좌표를 빼 스와이프한 길이를 구한다.

            this.speed = swipeLength / 500.0f; //길이를 계산한 후 적용

            GetComponent<AudioSource>().Play(); //효과음 재생
        }*/

        transform.Translate(this.speed, 0, 0); //좌표를 변경해 이동한다.
        this.speed *= 0.98f; //스피드에 특정 숫자를 곱해 속도를 줄인다. 이 경우 절대로 0은 되지 않고 그에 한없이 가까운 수가 나오기 때문에 주의해야한다.
    }

    void HandleSliderStop(object slider, EventArgs e) // 이벤트 신호를 받으면
    {
        speed = sliderController.sValue; //스피드를 설정한다.
        //여기서 참조하기 위해서는 반드시 해당 타입의 빈 변수를 만들고 인스펙터에서 연결해야 한다.
        //인스펙터 연결 말고 자체적으로 찾는 기능도 있으나 이 편이 더 좋다.
        GetComponent<AudioSource>().Play(); //효과음을 재생한다.
    }


}
