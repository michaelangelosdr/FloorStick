using UnityEngine;

namespace Game.ChapterOne.PlayerOne.Data
{
    [CreateAssetMenu(fileName = "Bookshelf book config", menuName = "Game/Chapter One/Player One/book Config")]
    public class BookshelfBookConfig : ScriptableObject
    {
        [TextArea(10, 20)]
        public string BookTitle;

        [TextArea(10, 20)]
        public string BookDescription;

        [TextArea(5, 15)]
        public string BookAnswer;
    }
}