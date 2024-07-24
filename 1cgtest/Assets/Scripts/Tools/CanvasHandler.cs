using DG.Tweening;
using UnityEngine;

namespace Tools
{
    public static class CanvasHandler
    {
        public static void ShowCanvas(CanvasGroup canvasGroup, float duration = 0.5f)
        {
            if (canvasGroup != null)
            {
                canvasGroup.DOFade(1f, duration);
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
            }
        }

        public static void HideCanvas(CanvasGroup canvasGroup, float duration = 0.5f)
        {
            if (canvasGroup != null)
            {
                canvasGroup.DOFade(0f, duration);
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
            }
        }

        public static void ToggleCanvas(CanvasGroup canvasGroup, bool isActive, float duration = 0.5f)
        {
            if (canvasGroup != null)
            {
                var endValue = isActive ? 1f : 0f;
                
                canvasGroup.DOFade(endValue, duration);
                canvasGroup.interactable = isActive;
                canvasGroup.blocksRaycasts = isActive;
            }
        }
    }
}