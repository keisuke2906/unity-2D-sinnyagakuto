using UnityEngine;
using TMPro; // 🔴【超重要】TextMeshProを操作するための合言葉です

public class TimerManager : MonoBehaviour
{
    [SerializeField] float timeLimit = 60.0f; // 制限時間（秒）
    [SerializeField] TextMeshProUGUI timerText; // UIのテキストを入れる箱

    void Update()
    {
        // 制限時間が0より大きい時だけタイマーを動かす
        if (timeLimit > 0)
        {
            // 🔴 Time.deltaTime は「前のフレームから何秒経ったか」です。これを引くことで現実の時間と同じ速度で減ります。
            timeLimit -= Time.deltaTime;

            // 0秒未満にならないようにストッパーをかける
            if (timeLimit < 0)
            {
                timeLimit = 0;
            }

            // 残り時間を「整数（切り捨て）」にしてテキストに表示する
            int seconds = Mathf.FloorToInt(timeLimit);
            timerText.text = "Time: " + seconds.ToString();

            // もし0秒になったら？
            if (timeLimit == 0)
            {
                Debug.Log("タイムアップ！");
                // ここにゲームオーバーなどの処理を追加します
            }
        }
    }
}
