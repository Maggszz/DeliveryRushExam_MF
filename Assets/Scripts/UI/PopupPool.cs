using System.Collections.Generic;
using UnityEngine;

namespace DeliveryRushExam.UI
{
    public class PopupPool : MonoBehaviour
    {
        [SerializeField]
        private ScorePopupView popupPrefab;

        [SerializeField]
        private RectTransform parent;

        [SerializeField]
        private int initialSize = 10;

        private readonly Queue<ScorePopupView> available = new();

        private void Awake()
        {
            for (int i = 0; i < initialSize; i++)
            {
                CreatePopup();
            }
        }

        private void CreatePopup()
        {
            ScorePopupView popup = Instantiate(popupPrefab, parent);

            popup.gameObject.SetActive(false);
            popup.SetPool(this);
            available.Enqueue(popup);
        }

        public ScorePopupView Get()
        {
            if (available.Count == 0)
            {
                CreatePopup();
            }

            ScorePopupView popup = available.Dequeue();

            popup.gameObject.SetActive(true);
            return popup;
        }

        public void Return(ScorePopupView popup)
        {
            popup.gameObject.SetActive(false);

            available.Enqueue(popup);
        }
    }
}