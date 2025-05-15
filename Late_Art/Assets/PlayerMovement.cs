using UnityEngine;
using Valve.VR;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public float rotationSpeed = 50.0f;

    // SteamVRのアクションを指定
    public SteamVR_Action_Vector2 leftStickAction;
    public SteamVR_Action_Vector2 rightStickAction;

    private CharacterController characterController;
    private Transform cameraTransform;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // メインカメラ（HMD）のTransformを取得
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        // 左スティックの移動処理
        Vector2 moveValue = leftStickAction.axis;
        Vector3 direction = new Vector3(moveValue.x, 0, moveValue.y);

        // カメラの向きを基準に移動方向を変換
        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;

        // 高さ成分を無視して水平方向のみ考慮
        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();

        // カメラ基準での移動ベクトルを計算
        Vector3 move = (cameraForward * direction.z + cameraRight * direction.x) * moveSpeed * Time.deltaTime;
        transform.position += new Vector3(move.x, 0, move.z);

        // 右スティックの回転処理
        Vector2 rotationValue = rightStickAction.axis;
        float yaw = rotationValue.x * rotationSpeed * Time.deltaTime;
        float pitch = -rotationValue.y * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, yaw, 0, Space.World);
        Camera.main.transform.Rotate(pitch, 0, 0);
    }
}
