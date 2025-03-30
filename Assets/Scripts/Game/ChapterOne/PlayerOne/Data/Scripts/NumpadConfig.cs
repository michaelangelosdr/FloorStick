using UnityEngine;

[CreateAssetMenu(fileName = "Numpad Config", menuName = "Game/Numpad Config")]
public class NumpadConfig : ScriptableObject
{
    [TextArea(5, 20)]
    public string designatedNumpadId;

    [TextArea(10, 20)]
    public string answerKey;
}
