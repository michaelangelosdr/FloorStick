using UnityEngine;
using System.Collections.Generic;

namespace Game.ChapterOne.PlayerOne.Controllers
{
    [CreateAssetMenu(fileName = "PTube State Keys", menuName = "Game/Chapter One/Player One/PTube State Keys")]
    public class PTubeStateKeys : ScriptableObject
    {
        public List<string> tubeStateKeyes = new List<string>();
        public List<NumpadConfig> numpadKeys = new List<NumpadConfig>();
    }
}