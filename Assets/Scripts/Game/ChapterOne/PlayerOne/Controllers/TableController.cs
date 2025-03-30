using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;
using Game.Puzzles.Inputs;
using Game.ChapterOne.PlayerOne.Managers;

namespace Game.ChapterOne.PlayerOne.Controllers
{
    public class TableController : GameScreenController
    {
        [SerializeField]
        private GameScreenController cryptexGameScreen;

        [SerializeField]
        private GameObject cryptexGameobject;

        [SerializeField]
        private GameObject gemGameObject;

        [SerializeField]
        private SpinnerInputController spinnerInput;

        private UnityAction onCryptexComplete;

        public void Initialize(UnityAction onCryptexComplete)
        {
            this.onCryptexComplete = onCryptexComplete;
            cryptexGameScreen.Initialize();
            spinnerInput.Initialize(null, OnCryptexComplete);
            base.Initialize();
        }

        private void OnCryptexComplete()
        {
            spinnerInput.SetCryptexLockState(true);
            onCryptexComplete?.Invoke();
        }

        public void SetCryptexState(bool isShow)
        {
            cryptexGameobject.SetActive(isShow);
        }

        public void SetGemState(bool isShow)
        {
            gemGameObject.SetActive(isShow);
        }

        public void Destroy()
        {

        }
    }
}