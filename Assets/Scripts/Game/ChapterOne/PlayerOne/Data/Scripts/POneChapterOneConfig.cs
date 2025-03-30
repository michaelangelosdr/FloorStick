using UnityEngine;

[CreateAssetMenu(fileName = "POneChapterOne Config", menuName = "Game/Chapter Configs/Player One Chapter One", order = 0)]
public class POneChapterOneConfig : ScriptableObject
{

    public POneChapterOneStateIdEnum InitialState;

    public int GetInitialState()
    {
        return (int)InitialState;
    }
}

/*public class PlayerOneChapterOneManager
{
    // Book shelf
    // -> Books in a row that has symbols
    // -> Book[x].IsRevealed
    // -> 
    // Table in front of window
    // -> 
    // Cryptex holder
    // Cryptex 
    // Pneumatic tube + Numpad + Analog screen
    // Door with an inactive numpad




}*/
