using Cysharp.Threading.Tasks;
using Game.ChapterOne.PlayerOne.Managers;

namespace Game.States
{
    public class POneChapterOneState : State
    {
        private POneChapterOneManager chapterOneManager;

        public POneChapterOneState(int stateId, bool observeUpdate = false) : base(stateId, observeUpdate)
        {

        }

        public override async UniTask PreLoadState()
        {
            await AssetManager.Instance.LoadView<POneChapterOneManager>("PlayerOneChapterOne", AssetParentType.GameRoot,
                (manager) =>
                {
                    chapterOneManager = manager;
                    chapterOneManager.InitializeManager();
                });
        }
    }
}
