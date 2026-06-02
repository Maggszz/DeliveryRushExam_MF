using System;
using DeliveryRushExam.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DeliveryRushExam.UI
{
    public class OrderButtonView : MonoBehaviour
    {
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text rewardText;
        [SerializeField] private TMP_Text timerText;
        [SerializeField] private Button completeButton;

        private OrderData orderData;
        private Action<string> onCompleteClicked;

        public void Setup(OrderData order, Action<string> completeCallback)
        {
            orderData = order;
            onCompleteClicked = completeCallback;

            if (completeButton == null)
            {
                completeButton = GetComponent<Button>();
            }

            completeButton.onClick.RemoveAllListeners();
            completeButton.onClick.AddListener(HandleClick);

            Refresh();
        }

        public void Refresh()
        {
            if (orderData == null)
            {
                return;
            }

            // Texto directo para facilitar el seguimiento durante el examen.
            titleText.text = "Deliver to " + orderData.customerName;
            rewardText.text = "+" + orderData.rewardPoints + " pts / +" + orderData.rewardCoins + " coins";
            timerText.text = "Time " + Mathf.CeilToInt(orderData.remainingTime);
        }

        private void HandleClick()
        {
            if (orderData != null)
            {
                onCompleteClicked?.Invoke(orderData.id);
            }
        }
    }
}
