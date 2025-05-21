using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonCameraController : MonoBehaviour
{
    /*
     * ����Ī ī�޶�� ���õ� ����� �����ϴ� ��ũ��Ʈ�Դϴ�.
     */

    #region Variables

    [Tooltip("ī�޶� ����")]
    [field: SerializeField, Header("Camera Settings")] public float CameraSensitivity { get; set; } = 50f;
    [field: SerializeField] public Vector3 CameraOffset { get; set; } = new Vector3(0f, 1.63f, 0f);
    [field: SerializeField] public Quaternion CameraRotation { get; set; } = new Quaternion(0f, 0f, 0f, 1f);
    [field: SerializeField] public float CameraFOV { get; set; } = 75f;
    [field: SerializeField] public float MaxPitchAngle { get; set; } = 90f;
    [field: SerializeField] public float MinPitchAngle { get; set; } = -90f;
    public float CurrentPitch { get => CurrentPitchAngle; set { CurrentPitchAngle = Mathf.Clamp(value, MinPitchAngle, MaxPitchAngle); } }
    [field: SerializeField] public bool LockCursor { get; set; } = true;
    [field: SerializeField] public bool InvertX { get; set; } = false;
    [field: SerializeField] public bool InvertY { get; set; } = false;

    [Tooltip("ī�޶� ȿ��")]
    [field: SerializeField, Header("Camera Effects")] public float MaxLeanAngle { get; set; } = 8f;
    [field: SerializeField] public float LeanSpeed { get; set; } = 10f;
    [field: SerializeField] public float LeanOffset { get; set; } = 0.5f;
    [field: SerializeField] public bool UseLean { get; set; } = true;
    [field: SerializeField] public bool ToggleLean { get; set; } = false;
    [field: SerializeField] public float RecoilDampTime { get; set; } = 10f;
    [field: SerializeField] public Vector3 RecoilAmout { get; set; } = new(-3f, 4f, 4f);
    [field: SerializeField] public bool UseCameraRecoil { get; set; } = true;

    [Tooltip("�����: ī�޶� ��� ���")]
    [field: SerializeField, Header("Camera Result")] public Vector3 ResultLeanPosition { get; set; } = Vector3.zero;
    [field: SerializeField] public Vector3 ResultLeanRotation { get; set; } = Vector3.zero;
    [field: SerializeField] public float CurrentPitchAngle { get; set; } = 0f;
    [field: SerializeField] public float CurrentLeanAngle { get; set; } = 0f;
    [field: SerializeField] public Vector3 ResultRecoilRotation { get; set; } = Vector3.zero;
    [field: SerializeField] public Vector3 CurrentRecoil { get; set; } = Vector3.zero;

    [Tooltip("�����: �÷��̾� �Է�")]
    [field: SerializeField, Header("Player Input")] public Vector2 LookInput { get; set; } = Vector2.zero;
    [field: SerializeField] public float LeanLeft { get; set; } = 0f;
    public float LeanLeftToggle { get => LeanLeft; set { if (ToggleLean && value != 0f) LeanLeft = Mathf.Abs(LeanLeft - value); else if (!ToggleLean) LeanLeft = value; } }
    [field: SerializeField] public float LeanRight { get; set; } = 0f;
    public float LeanRightToggle { get => LeanRight; set { if (ToggleLean && value != 0f) LeanRight = Mathf.Abs(LeanRight - value); else if (!ToggleLean) LeanRight = value; } }

    [Tooltip("������Ʈ")]
    [field: SerializeField, Header("Components")] Camera PlayerCamera { get; set; }
    [field: SerializeField] CharacterController CharacterController { get; set; }
    [field: SerializeField] CameraManager CameraManager { get; set; }
    [field: SerializeField] MeshManager MeshManager { get; set; }

    #endregion

    #region Camera Methods

    private void CameraSettingsUpdate()
    {
        PlayerCamera.fieldOfView = Mathf.Lerp(PlayerCamera.fieldOfView, CameraFOV, Time.deltaTime);
    }

    private void LeanUpdate()
    {
        if (!UseLean) return;

        if (LeanLeft != 0f && LeanRight != 0f)
        {
            LeanLeft = 0f;
            LeanRight = 0f;
        }

        float rotationAngle = (LeanLeft + (-1f * LeanRight)) * MaxLeanAngle;
        Vector3 positionOffset = ((-1f * LeanLeft) + LeanRight) * new Vector3(LeanOffset, 0f, 0f);

        CurrentLeanAngle = Mathf.Lerp(CurrentLeanAngle, rotationAngle, Time.deltaTime * LeanSpeed);
        ResultLeanPosition = Vector3.Lerp(ResultLeanPosition, positionOffset, Time.deltaTime * LeanSpeed);
        ResultLeanRotation = new Vector3(0f, 0f, CurrentLeanAngle);
    }

    private void LookUpdate()
    {
        Vector2 lookInput = CameraSensitivity * Time.deltaTime * new Vector2(LookInput.x, LookInput.y);
        CurrentPitch -= lookInput.y * (InvertY ? -1f : 1f);

        transform.Rotate((InvertX ? -1f : 1f) * lookInput.x * Vector3.up);
        CameraManager.transform.localRotation = Quaternion.Euler(CurrentPitch, 0f, 0f);
        CameraManager.transform.localRotation *= Quaternion.Euler(ResultLeanRotation + ResultRecoilRotation);
        CameraManager.transform.localPosition = CameraOffset + ResultLeanPosition;
    }

    private void ApplyRecoilDamping()
    {
        CurrentRecoil = Vector3.Lerp(CurrentRecoil, Vector3.zero, Time.deltaTime * 35f);
        ResultRecoilRotation = Vector3.Lerp(ResultRecoilRotation, CurrentRecoil, Time.fixedDeltaTime * RecoilDampTime);
    }

    private void ApplyRecoil(float vertical, float horizontal, float shakeMultiplier)
    {
        if (!UseCameraRecoil) return;

        float multiplier = 0.7f;
        LookInput += new Vector2(vertical * multiplier, horizontal * multiplier);
        CurrentRecoil += multiplier * shakeMultiplier * new Vector3(RecoilAmout.x, Random.Range(-RecoilAmout.y, RecoilAmout.y), Random.Range(-RecoilAmout.z, RecoilAmout.z));
    }

    #endregion

    #region Unity Methods

    public void Awake()
    {
        if (PlayerCamera == null)
        {
            PlayerCamera = GetComponentInChildren<Camera>();
        }
        if (CharacterController == null)
        {
            CharacterController = GetComponent<CharacterController>();
        }
        if (CameraManager == null)
        {
            CameraManager = GetComponentInChildren<CameraManager>();
        }
        if (MeshManager == null)
        {
            MeshManager = GetComponentInChildren<MeshManager>();
        }

        if (LockCursor)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void Start()
    {
        PlayerCamera.fieldOfView = CameraFOV;

        CameraManager.transform.localPosition = CameraOffset;
        MeshManager.transform.localPosition = -CameraOffset;
    }

    public void OnValidate()
    {
        if (PlayerCamera == null)
        {
            PlayerCamera = GetComponentInChildren<Camera>();
        }
        if (CharacterController == null)
        {
            CharacterController = GetComponent<CharacterController>();
        }
        if (CameraManager == null)
        {
            CameraManager = GetComponentInChildren<CameraManager>();
        }
        if (MeshManager == null)
        {
            MeshManager = GetComponentInChildren<MeshManager>();
        }
    }

    public void FixedUpdate()
    {
        if (UseCameraRecoil) ApplyRecoilDamping();
    }

    public void Update()
    {
        // ī�޶� ���� ������Ʈ
        CameraSettingsUpdate();
        // ī�޶� ����̱�
        LeanUpdate();
        // ī�޶� ������Ʈ
        LookUpdate();
    }

    #endregion
}
