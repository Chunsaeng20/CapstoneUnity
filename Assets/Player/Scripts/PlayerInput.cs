using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(FirstPersonCameraController))]
public class PlayerInput : MonoBehaviour
{
    /*
     * [�÷��̾��� �Է��� ó���ϴ� ��ũ��Ʈ�Դϴ�.]
     * �ϵ���� �Է��� ������ �����ϰ� ��ȯ�Ͽ� �ʿ��� �Լ��� �����մϴ�.
     */

    #region Variables

    [Tooltip("������Ʈ")]
    [field: SerializeField, Header("Components")] FirstPersonCameraController CameraController { get; set; }
    [field: SerializeField] Animator Animator { get; set; }
    [field: SerializeField] SoundManager SoundManager { get; set; }

    #endregion

    #region Input Handlers

    // ���콺 �������� ó���մϴ�.
    public void OnLook(InputValue value)
    {
        CameraController.LookInput = value.Get<Vector2>();
    }

    // ���콺 ���� ��ư�� ó���մϴ�.
    public void OnAttack()
    {
        Animator.SetTrigger("Fire");
        SoundManager.GunFire();
        CameraController.ApplyRecoil();
    }

    // Q ��ư�� ó���մϴ�.
    public void OnLeanLeft(InputValue value)
    {
        CameraController.LeanLeftToggle = value.Get<float>();
    }

    // E ��ư�� ó���մϴ�.
    public void OnLeanRight(InputValue value)
    {
        CameraController.LeanRightToggle = value.Get<float>();
    }

    #endregion

    #region Unity Methods

    public void Start()
    {
        if(CameraController == null)
        {
            CameraController = GetComponent<FirstPersonCameraController>();
        }
        if(Animator == null)
        {
            Animator = GetComponentInChildren<Animator>();
        }
        if(SoundManager == null)
        {
            SoundManager = GetComponentInChildren<SoundManager>();
        }
    }

    public void OnValidate()
    {
        if (CameraController == null)
        {
            CameraController = GetComponent<FirstPersonCameraController>();
        }
        if (Animator == null)
        {
            Animator = GetComponentInChildren<Animator>();
        }
        if (SoundManager == null)
        {
            SoundManager = GetComponentInChildren<SoundManager>();
        }
    }

    #endregion
}
