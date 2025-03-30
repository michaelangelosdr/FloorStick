using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Game.ChapterOne.PlayerOne.Data;

namespace Game.ChapterOne.PlayerOne.Controllers
{ 
    public class BookshelfBook : MonoBehaviour
    {
        [SerializeField]
        public Button bookButton;

        [SerializeField]
        public TextMeshProUGUI bookTitle;

        private UnityAction<BookshelfBookConfig> onButtonClicked;
        private BookshelfBookConfig config;

        public void InitializeController(UnityAction<BookshelfBookConfig> onButtonClicked, BookshelfBookConfig config)
        {
            this.onButtonClicked = onButtonClicked;
            this.config = config;
            bookButton.onClick.AddListener(OnBookButtonClicked);
            bookTitle.text = config.BookTitle;
        }

        private void OnBookButtonClicked()
        {
            onButtonClicked?.Invoke(config);
        }
    }
}