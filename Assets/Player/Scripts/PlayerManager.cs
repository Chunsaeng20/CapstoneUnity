using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerManager : MonoBehaviour
{
    /*
     * [�÷��̾� ���ݿ� ���� ��ũ��Ʈ�Դϴ�.]
     * ���� ��ũ��Ʈ���� �������� ���̱� ���� PlayerManager�� ����մϴ�.
     */

    #region Variables

    [Tooltip("������Ʈ")]
    [field: SerializeField, Header("Components")] public PlayerInfo PlayerInfo { get; set; }
    [field: SerializeField] public PlayerInput PlayerInput { get; set; }
    [field: SerializeField] public CharacterController CharacterController { get; set; }
    [field: SerializeField] public FirstPersonCameraController CameraController { get; set; }
    [field: SerializeField] public Camera PlayerCamera { get; set; }

    #endregion

    #region Unity Methods

    public void Start()
    {
        if (PlayerInfo == null)
        {
            PlayerInfo = GetComponent<PlayerInfo>();
        }
        if (PlayerInput == null)
        {
            PlayerInput = GetComponent<PlayerInput>();
        }
        if (CharacterController == null)
        {
            CharacterController = GetComponent<CharacterController>();
        }
        if (CameraController == null)
        {
            CameraController = GetComponent<FirstPersonCameraController>();
        }
        if (PlayerCamera == null)
        {
            PlayerCamera = GetComponentInChildren<Camera>();
        }
    }

    public void OnValidate()
    {
        if (PlayerInfo == null)
        {
            PlayerInfo = GetComponent<PlayerInfo>();
        }
        if (PlayerInput == null)
        {
            PlayerInput = GetComponent<PlayerInput>();
        }
        if (CharacterController == null)
        {
            CharacterController = GetComponent<CharacterController>();
        }
        if (CameraController == null)
        {
            CameraController = GetComponent<FirstPersonCameraController>();
        }
        if (PlayerCamera == null)
        {
            PlayerCamera = GetComponentInChildren<Camera>();
        }
    }

    #endregion
}
