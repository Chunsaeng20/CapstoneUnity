using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerManager : MonoBehaviour
{
    /*
     * �÷��̾� ���ݿ� ���� ��ũ��Ʈ�Դϴ�.
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
        PlayerInfo = GetComponent<PlayerInfo>();
        PlayerInput = GetComponent<PlayerInput>();
        CharacterController = GetComponent<CharacterController>();
        CameraController = GetComponent<FirstPersonCameraController>();
        PlayerCamera = GetComponentInChildren<Camera>();
    }

    #endregion
}
