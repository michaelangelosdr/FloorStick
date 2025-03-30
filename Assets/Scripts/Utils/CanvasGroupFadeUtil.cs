using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Utils
{
    public static class CanvasGroupFadeUtil
    {
        public static async UniTask FadeIn(CanvasGroup canvasGroup, float duration)
        {
            await Fade(canvasGroup, canvasGroup.alpha, 1, duration);
        }

        public static async UniTask FadeOut(CanvasGroup canvasGroup, float duration)
        {
            await Fade(canvasGroup, canvasGroup.alpha, 0, duration);
        }

        private static async UniTask Fade(CanvasGroup canvasGroup, float startAlpha, float endAlpha, float duration)
        {
            float elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
                await UniTask.Yield();
            }

            // Ensure the final alpha is set
            canvasGroup.alpha = endAlpha;
        }
    }
}