using UnityEngine;
using UnityEngine.UI;
using Game.Utils;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.ChapterOne.PlayerOne.Data;
using TMPro;

namespace Game.ChapterOne.PlayerOne.Controllers
{
    public class BookshelfController : GameScreenController
    {
        [SerializeField]
        private Button bookCloseButton;

        [SerializeField]
        private CanvasGroup bookCanvasGroup;

        [SerializeField]
        private TextMeshProUGUI bookTitleTxt;

        [SerializeField]
        private TextMeshProUGUI bookBodyTxt;

        [SerializeField]
        private Transform bookshelfContainer;

        [SerializeField]
        private BookshelfConfig bookshelfConfig;

        private float fadeDuration = 0.5f;

        private List<BookshelfBook> bookshelfBooks;

        public override void Initialize()
        {
            bookshelfBooks = new List<BookshelfBook>();
            bookCloseButton.onClick.AddListener(OnBookCloseButtonClicked);
            LoadBooks();
            base.Initialize();
        }

        private void LoadBooks()
        {
            for (int i = 0; i < bookshelfConfig.bookConfigs.Count; i++)
            {
                int x = i;
                AssetManager.Instance.LoadView<BookshelfBook>("BookshelfBook", AssetParentType.UIRoot,
                (book) =>
                {
                    book.InitializeController(OnBookClicked, bookshelfConfig.bookConfigs[x]);
                    book.gameObject.transform.SetParent(bookshelfContainer);
                    bookshelfBooks.Add(book);
                }).Forget();
            }
        }

        private void OnBookClicked(BookshelfBookConfig bookContents)
        {
            bookTitleTxt.text = bookContents.BookTitle;
            bookBodyTxt.text = bookContents.BookDescription;
            ShowBook(true).Forget();
        }

        private void OnBookCloseButtonClicked()
        {
            ShowBook(false).Forget();
        }

        private async UniTask ShowBook(bool isShow)
        {
            if (isShow)
            {
                bookCanvasGroup.alpha = 0;
                bookCanvasGroup.interactable = true;
                bookCanvasGroup.blocksRaycasts = true;
                await CanvasGroupFadeUtil.FadeIn(bookCanvasGroup, fadeDuration);
            }
            else
            {
                bookCanvasGroup.alpha = 1;
                bookCanvasGroup.interactable = false;
                bookCanvasGroup.blocksRaycasts = false;
                await CanvasGroupFadeUtil.FadeOut(bookCanvasGroup, fadeDuration);
            }
        }
        
        public void Destroy()
        { 

        }
    }
}