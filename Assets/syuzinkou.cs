using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class syuzinkou : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5.0f;
    [SerializeField] float grabRange = 1.5f;
    [SerializeField] Transform holdPoint;

    Rigidbody2D rb;
    Vector2 input;
    GameObject grabbedObject;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (grabbedObject == null) TryGrab();
            else Drop();
        }
    }

    void TryGrab()
    {
        if (holdPoint == null) return;

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, grabRange);

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Carryable"))
            {
                Rigidbody2D targetRb = hit.GetComponent<Rigidbody2D>();
                Collider2D targetCollider = hit.GetComponent<Collider2D>();

                if (targetRb != null && targetCollider != null)
                {
                    grabbedObject = hit.gameObject;

                    // 【対策①】つかんだ瞬間に、溜まっていた「吹っ飛ぶ勢い（速度）」を完全にゼロにする
                    targetRb.linearVelocity = Vector2.zero;
                    targetRb.angularVelocity = 0f;
                    targetRb.simulated = false;

                    // 【対策②】つかんでいる間は、物の当たり判定を完全にオフにしてすり抜けさせる
                    targetCollider.enabled = false;

                    // 親子関係にして位置を固定
                    grabbedObject.transform.SetParent(holdPoint);
                    grabbedObject.transform.localPosition = Vector3.zero;
                    grabbedObject.transform.localRotation = Quaternion.identity; // 回転もリセット
                    break;
                }
            }
        }
    }

    void Drop()
    {
        if (grabbedObject == null) return;

        Rigidbody2D targetRb = grabbedObject.GetComponent<Rigidbody2D>();
        Collider2D targetCollider = grabbedObject.GetComponent<Collider2D>();

        if (targetRb != null && targetCollider != null)
        {
            // 置くときも、変な勢いが乗らないように速度をリセット
            targetRb.linearVelocity = Vector2.zero;
            targetRb.angularVelocity = 0f;
            targetRb.simulated = true;

            // 【対策②の解除】手放したので、当たり判定を元に戻す
            targetCollider.enabled = true;
        }

        grabbedObject.transform.SetParent(null);
        grabbedObject = null;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = input * moveSpeed;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, grabRange);
    }
}