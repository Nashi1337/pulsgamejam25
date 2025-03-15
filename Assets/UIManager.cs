using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI armLabel;
    public TextMeshProUGUI legLabel;
    public TextMeshProUGUI unusedLabel;

    public void UpdateArmLabel(int value)
    {
        armLabel.text = value.ToString();
    }

    public void UpdateLegLabel(int value)
    {
        legLabel.text = value.ToString();
    }

    public void UpdateUnusedLabel(int value)
    {
        unusedLabel.text = value.ToString();
    }
}
