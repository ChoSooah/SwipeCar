using UnityEngine;

public class CarTriggerCheck : MonoBehaviour
{
    // Inspector에서 설정
    [SerializeField] int score = 0; // 자동차가 깃발에 닿으면 점수 +100, 바닥에 닿으면 +0
    public int Score => score;

    [SerializeField] private GameDirector gameDirector;   // 점수 관리, GameDirector 스크립트 참조

    void Update()
    {
        CheckOutCamera();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            Debug.Log("땅");
            gameDirector.Reset();
        }
        else if (collision.collider.CompareTag("Flag"))
        {
            Debug.Log("깃발");
            score += 100;
            gameDirector.Reset();
        }
    }

    void CheckOutCamera()
    {
        Vector2 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        bool isOut = viewPos.x < 0 || viewPos.x > 1 || viewPos.y < 0 || viewPos.y > 1;

        if (isOut)
        {
            Debug.Log("화면벗어남");
            gameDirector.Reset();
        }
    }
}