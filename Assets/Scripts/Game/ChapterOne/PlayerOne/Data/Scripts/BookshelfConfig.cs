using UnityEngine;
using System.Collections.Generic;
using Game.ChapterOne.PlayerOne.Controllers;

namespace Game.ChapterOne.PlayerOne.Data
{
    [CreateAssetMenu(fileName = "Bookshelf config", menuName = "Game/Chapter One/Player One/Bookshelf config")]
    public class BookshelfConfig : ScriptableObject
    {
        [SerializeField]
        public List<BookshelfBookConfig> bookConfigs = new List<BookshelfBookConfig>();
    }
}