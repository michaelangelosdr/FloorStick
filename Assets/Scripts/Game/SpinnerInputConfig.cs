using UnityEngine;
using System.Collections.Generic;

namespace Game.Puzzles.Data
{
    [CreateAssetMenu(fileName = "Number Spinner Config", menuName = "Game/Number Spinner Config")]
    public class SpinnerInputConfig : ScriptableObject
    {
        [TextArea(5, 10)]
        public string SpinnerTargetTxt;
        public List<string> inputChar;
    }
}