using UnityEngine;

public class BT_RandomRotation : MonoBehaviour
{
    // 回転の範囲を指定する変数
    public Vector3 rotationRange = new Vector3(30f, 30f, 30f);

    void Start()
    {
        // ランダムな回転を設定
        SetRandomRotation();
    }

    // ランダムな回転を設定する関数
    public void SetRandomRotation()
    {
        // ランダムな回転を生成
        Vector3 randomRotation = new Vector3(
            Random.Range(-rotationRange.x, rotationRange.x),
            Random.Range(-rotationRange.y, rotationRange.y),
            Random.Range(-rotationRange.z, rotationRange.z)
        );

        // オブジェクトにランダムな回転を適用
        transform.rotation = Quaternion.Euler(randomRotation);
    }
}