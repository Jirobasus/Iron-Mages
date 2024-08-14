using TMPro;
using UnityEngine;

public class SetRecord : MonoBehaviour
{
    public TextMeshProUGUI recordText;

    private void Start()
    {
        recordText.gameObject.GetComponent<TextMeshProUGUI>().text = "������: " + Record.Instance.playerInfo.waveRecord;
    }
    public void SetRecordText()
    {
        recordText.gameObject.GetComponent<TextMeshProUGUI>().text = "������: " + Record.Instance.playerInfo.waveRecord;
    }
}
