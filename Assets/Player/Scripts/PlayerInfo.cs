using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    /*
     * �÷��̾��� ������ �����ϴ� ��ũ��Ʈ�Դϴ�.
     */

    #region Variables

    [Tooltip("�÷��̾� ����")]
    [field: SerializeField, Header("Plyaer Info")] public string PlayerName { get; set; } = "Player";
    [field: SerializeField] public int PlayerScore { get; set; } = 0;
    [field: SerializeField] public int Kills { get; set; } = 0;

    #endregion
}
