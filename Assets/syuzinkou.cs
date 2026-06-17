using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class syuzinkou : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5.0f;
    [SerializeField] float grabRange = 1.5f; // 物体を検知する範囲
    [SerializeField] Transform holdPoint;   // 物体がくっつく位置（空のオブジェクトをプレイヤーの子で作って指定）
    Rigidbody2D rb;
    Vector2 input;
    GameObject grabbedObject; // 今持っている物体を入れる変数

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {//inputは方向ごとの配列である？
        
        input.x = Input.GetAxisRaw("Horizontal");//横方向の入力を監視する
        input.y = Input.GetAxisRaw("Vertical");//縦方向の入力を監視する

        if (input.sqrMagnitude > 1f)
        {
            input = input.normalized;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (grabbedObject == null) TryGrab();
            else Drop();
        }
    }

    void FixedUpdate()
    {
        // 入力方向 × 移動速度 を計算して、キャラクターの「速度」として代入する
       
        rb.linearVelocity = input * moveSpeed;
    }

    void TryGrab()
    {
        // 周囲の「Carryable」タグが付いた物体を探す
        Collider2D hit = Physics2D.OverlapCircle(transform.position, grabRange, LayerMask.GetMask("Default"));
        if (hit != null && hit.CompareTag("Carryable"))
        {
            grabbedObject = hit.gameObject;
            // 親子関係にしてプレイヤーにくっつける
            grabbedObject.transform.SetParent(holdPoint);
            grabbedObject.transform.localPosition = Vector3.zero;
            // 物理演算を停止（重力オフ）
            grabbedObject.GetComponent<Rigidbody2D>().simulated = false;
        }
    }

    void Drop()
    {
        // 物理演算を再開して、親子関係を解除
        grabbedObject.GetComponent<Rigidbody2D>().simulated = true;
        grabbedObject.transform.SetParent(null);
        grabbedObject = null;
    }

}
