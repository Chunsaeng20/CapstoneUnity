using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(FirstPersonCameraController))]
public class PlayerInput : MonoBehaviour
{
    /*
     * �÷��̾��� �Է��� ó���ϴ� ��ũ��Ʈ�Դϴ�.
     */

    #region Variables

    [Tooltip("������Ʈ")]
    [field: SerializeField, Header("Components")] FirstPersonCameraController CameraController { get; set; }
    [field: SerializeField] Animator Animator { get; set; }

    #endregion

    #region Input Handlers

    public void OnLook(InputValue value)
    {
        CameraController.LookInput = value.Get<Vector2>();
    }

    public void OnAttack()
    {
        Animator.SetTrigger("Fire");
    }

    public void OnLeanLeft(InputValue value)
    {
        CameraController.LeanLeftToggle = value.Get<float>();
    }

    public void OnLeanRight(InputValue value)
    {
        CameraController.LeanRightToggle = value.Get<float>();
    }

    #endregion

    #region Unity Methods

    public void Awake()
    {
        if(CameraController == null)
        {
            CameraController = GetComponent<FirstPersonCameraController>();
        }
        if(Animator == null)
        {
            Animator = GetComponentInChildren<Animator>();
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
    }

    #endregion
}
