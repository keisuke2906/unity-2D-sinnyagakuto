using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class syuzinkou : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5.0f;

    Rigidbody2D rb;
    Vector2 input;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        if (input.sqrMagnitude > 1f)
        {
            input = input.normalized;
        }
    }

    void FixedUpdate()
    {
        // 入力方向 × 移動速度 を計算して、キャラクターの「速度」として代入する
       
        rb.linearVelocity = input * moveSpeed;
    }
}