
namespace Game.ChapterOne.PlayerOne.States
{
    public class POneChapterOneStateId
    {
        public static readonly int Initialization = 0; // Reset everything to defaults.
        public static readonly int Start = 1;
        public static readonly int Ptube_Solved_1 = 2; // Cryptex + note is in the inventory.
        public static readonly int Cryptex_Placed = 3; // Cryptex is not in inventory, spawn cryptex in table. 
        public static readonly int Cryptex_Solved_1 = 4; // Change PTube's Analog Screen
        public static readonly int PTube_Solved_2 = 5; // Gem is now in the inventory
        public static readonly int Cryptex_Open = 6; // Key is in inventory
        public static readonly int PTube_Solved_3 = 7; // Key is removed from inventory, Analog screen changes to answer E2
        public static readonly int Door_Sequence_idle = 8; // Emergency Escape button unlocks/Activates
        public static readonly int Door_Sequence_solved = 9; // Screen fades to black, Clean up game objects, Activate cutscenes, etc.,
        public static readonly int POneChapterOneEndState = 10; // Call Game Manager, Clean up states and managers.
    }
}
