using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEditor.Experimental.GraphView;
using Unity.VisualScripting;

public class ArrowController : MonoBehaviour
{
    bool isRotaringAngle = true;
    [SerializeField] float angle = 0;
    public float Angle => angle;

    public event EventHandler OnArrowStop;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isRotaringAngle = false;
            OnArrowStop?.Invoke(this, EventArgs.Empty); //이벤트를 호출한다.
        }

        if (isRotaringAngle)
        {
            ArrowAxis();
        }
    }

    void ArrowAxis()
    {
        Vector3 mouseworldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //마우스의 월드 좌표를 구함
        mouseworldPos.z = 0; //z축 초기화
        Vector2 mouseAxis = mouseworldPos - transform.position; //방향을 구함

        angle = Mathf.Atan2(mouseAxis.y, mouseAxis.x) * Mathf.Rad2Deg; //방향을 각도로 변환한다.

        if (angle >= -1 && angle <= 91)
        {
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
