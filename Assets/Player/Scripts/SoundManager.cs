using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    /*
     * ���� ���� ����� �����ϴ� ��ũ��Ʈ�Դϴ�.
     */

    #region Variables

    [field: SerializeField, Header("Audio Settings")] public List<AudioSource> AudioSourceList { get; set; }

    #endregion

    #region User Methods

    // �ѱ� �߻� �Ҹ��� ����մϴ�.
    public void GunFire()
    {
        AudioSourceList[0].PlayOneShot(AudioSourceList[0].clip);
    }

    #endregion
}
