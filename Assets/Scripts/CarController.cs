using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEditor.Experimental.GraphView;

public class CarController : MonoBehaviour
{
    float speed = 0f;
    // public: 외부 스크립트에서도 접근 가능
    // float: 실수형 자료형
    // speed: 자동차의 이동 속도를 저장하는 변수
    // 초기값 0f: 게임 시작 시 자동차는 멈춘 상태

    float forcePower = 0f;
    // forcePowe: 자동차의 추진력을 적용할 때 사용하는 변수

    [SerializeField] private SliderController sliderController;
    // 해당 컴포넌트(스크립트)를 가진 오브젝트를 인스펙터 창에서 연결

    [SerializeField] private ArrowController arrowController;
    // ArrowController: 플레이어가 선택한 각도를 관리하는 스크립트
    // 자동차 발사 궤도를 조정하는 화살표 컨트롤러

    [SerializeField] private GameDirector gameDirector;
    // GameDirector: 게임의 전체 진행을 관리하는 스크립트
    // Reset 이벤트를 받아 자동차 위치를 초기화하기 위해 필요


    Rigidbody2D rigidbody2d;
    // Rigidbody2D 캐시, 이동과 속도 조절에 사용됨

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>(); // Car 오브젝트에 붙어있는 Rigidbody2D를 가져와 저장
        Application.targetFrameRate = 60;  // 게임 프레임을 60FPS로 고정
        sliderController.OnSliderStop += HandleSliderStop; // 슬라이더 조작 종료에 반응하도록 연결
        gameDirector.OnReset += HandleReset;  // GameDirector가 보낸 리셋을 받을 수 있도록 연결
    }

    void Update()
    {
        rigidbody2d.linearVelocity *= 0.98f; // 스피드에 특정 숫자를 곱해 감속
        // 이 경우 절대로 0은 되지 않고 그에 한없이 가까운 수가 나오기 때문에 주의해야 함.
    }

    void HandleSliderStop(object slider, EventArgs e) // 이벤트 신호를 받을 시
    {
        speed = sliderController.sValue; // 스피드를 설정
        forcePower = sliderController.sValue * 60f; // 자동차의 추진력을 설정
        // 여기서 참조하기 위해서는 반드시 해당 타입의 빈 변수를 만들고 인스펙터에서 연결해야 함.
        // 인스펙터 연결 말고 자체적으로 찾는 기능도 존재하나, 이 편이 좋다.

        MoveCar();
        GetComponent<AudioSource>().Play(); // 효과음을 재생
    }

    void MoveCar() //자동차가 이동할 시
    {
        Vector2 dir = new Vector2(Mathf.Cos(arrowController.Angle * Mathf.Deg2Rad),
        Mathf.Sin(arrowController.Angle * Mathf.Deg2Rad)); // 궤도(화살표)가 가리키는 각도를 계산
        rigidbody2d.linearVelocity = dir * speed; // 기본 속도를 궤도(화살표)에 적용하여 초기 속도 부여
        rigidbody2d.AddForce(dir * forcePower, ForceMode2D.Impulse); // 자동차의 추진력을 설정한 궤도(화살표) 방향로 가함
    }

    void HandleReset(object GameDirector, EventArgs e) // 자동차 상태를 게임 초기 상태로 되돌림
    {
        speed = 0f; // 속도 초기화
        forcePower = 0f; // 추진력 초기화
        rigidbody2d.linearVelocity = Vector2.zero; // 실제 이동 속도 정지
        transform.position = new Vector3(-7, -3, 0); // 자동차가 시작 위치로 이동
    }
}