using UnityEngine;
using Valve.VR;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public float rotationSpeed = 50.0f;

    // SteamVR�̃A�N�V�������w��
    public SteamVR_Action_Vector2 leftStickAction;
    public SteamVR_Action_Vector2 rightStickAction;

    private CharacterController characterController;
    private Transform cameraTransform;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // ���C���J�����iHMD�j��Transform���擾
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        // ���X�e�B�b�N�̈ړ�����
        Vector2 moveValue = leftStickAction.axis;
        Vector3 direction = new Vector3(moveValue.x, 0, moveValue.y);

        // �J�����̌�������Ɉړ�������ϊ�
        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;

        // ���������𖳎����Đ��������̂ݍl��
        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();

        // �J������ł̈ړ��x�N�g�����v�Z
        Vector3 move = (cameraForward * direction.z + cameraRight * direction.x) * moveSpeed * Time.deltaTime;
        transform.position += new Vector3(move.x, 0, move.z);

        // �E�X�e�B�b�N�̉�]����
        Vector2 rotationValue = rightStickAction.axis;
        float yaw = rotationValue.x * rotationSpeed * Time.deltaTime;
        float pitch = -rotationValue.y * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, yaw, 0, Space.World);
        Camera.main.transform.Rotate(pitch, 0, 0);
    }
}
