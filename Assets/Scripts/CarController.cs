using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEditor.Experimental.GraphView;

public class CarController : MonoBehaviour
{
    float speed = 0f; //속도를 저장할 변수
    float forcePower = 0f;

    [SerializeField] private SliderController sliderController; //해당 컴포넌트(스크립트)를 가진 오브젝트를 인스펙터 창에서 연결한다 
    [SerializeField] private ArrowController arrowController;
    [SerializeField] private GameDirector gameDirector;

    Rigidbody2D rigidbody2d;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        Application.targetFrameRate = 60; //프레임 설정
        sliderController.OnSliderStop += HandleSliderStop; // 이벤트를 구독한다.
        gameDirector.OnReset += HandleReset;
    }

    void Update()
    {
        rigidbody2d.linearVelocity *= 0.98f; //스피드에 특정 숫자를 곱해 속도를 줄인다. 이 경우 절대로 0은 되지 않고 그에 한없이 가까운 수가 나오기 때문에 주의해야한다.
    }

    void HandleSliderStop(object slider, EventArgs e) // 이벤트 신호를 받으면
    {
        speed = sliderController.sValue; //스피드를 설정한다.
        forcePower = sliderController.sValue * 60f; //파워를 설정한다.
        //여기서 참조하기 위해서는 반드시 해당 타입의 빈 변수를 만들고 인스펙터에서 연결해야 한다.
        //인스펙터 연결 말고 자체적으로 찾는 기능도 있으나 이 편이 더 좋다.

        MoveCar();
        GetComponent<AudioSource>().Play(); //효과음을 재생한다.
    }

    void MoveCar()
    {
        Vector2 dir = new Vector2(Mathf.Cos(arrowController.Angle * Mathf.Deg2Rad),
        Mathf.Sin(arrowController.Angle * Mathf.Deg2Rad));
        rigidbody2d.linearVelocity = dir * speed;
        rigidbody2d.AddForce(dir * forcePower, ForceMode2D.Impulse);
    }

    void HandleReset(object GameDirector, EventArgs e)
    {
        speed = 0f;
        forcePower = 0f;
        rigidbody2d.linearVelocity = Vector2.zero;
        transform.position = new Vector3(-7, -3, 0);
    }
}
