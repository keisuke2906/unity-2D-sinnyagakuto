using UnityEngine;

public class suikomi : MonoBehaviour
{
    [SerializeField] float snapRadius = 2.0f; // 吸い寄せる範囲

    void Update()
    {
        // 🟢【修正】2D用の円形センサーを使用します
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, snapRadius);

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Carryable"))
            {
                // プレイヤーが持っていない状態（親オブジェクトが設定されていない）か確認
                if (hit.transform.parent == null)
                {
                    // 座標を台座にピッタリ合わせる
                    hit.transform.position = transform.position;
                    hit.transform.rotation = transform.rotation;

                    // 2D用のRigidbodyを取得し、変な勢いで飛んでいかないように速度をゼロにする
                    Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        rb.linearVelocity = Vector2.zero;
                        rb.angularVelocity = 0f;
                    }
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, snapRadius);
    }
}