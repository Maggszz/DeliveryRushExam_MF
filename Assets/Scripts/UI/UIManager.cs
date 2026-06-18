using System.Collections.Generic;
using DeliveryRushExam.Core;
using DeliveryRushExam.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Pool;

namespace DeliveryRushExam.UI
{
    public class UIManager : MonoBehaviour
    {
        [Header("Managers")]
        [SerializeField] private GameManager gameManager;
        [SerializeField] private OrderManager orderManager;
        [SerializeField] private ScoreManager scoreManager;

        [Header("HUD")]
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text coinsText;
        [SerializeField] private TMP_Text timerText;
        [SerializeField] private TMP_Text ordersCountText;

        [Header("Orders")]
        [SerializeField] private RectTransform ordersContainer;
        [SerializeField] private OrderButtonView orderButtonPrefab;

        [Header("Popups")]
        [SerializeField] private PopupPool popupPool;

        [Header("Panels")]
        [SerializeField] private GameObject gameplayPanel;
        [SerializeField] private GameObject resultsPanel;
        [SerializeField] private TMP_Text resultsText;
        
        private int lastScore = -1;
        private int lastCoins = -1;
        private int lastOrdersCount = -1;
        private int lastTime = -1;

        private readonly List<OrderButtonView> orderViews = new List<OrderButtonView>();

        private void Awake()
        {
            if (gameManager == null)
            {
                gameManager = FindFirstObjectByType<GameManager>();
            }

            if (orderManager == null)
            {
                orderManager = FindFirstObjectByType<OrderManager>();
            }

            if (scoreManager == null)
            {
                scoreManager = FindFirstObjectByType<ScoreManager>();
            }
        }

        private void OnEnable()
        {
            orderManager.OrdersChanged += RefreshOrderList;
            scoreManager.OrderScored += ShowScorePopup;
        }

        private void OnDisable()
        {
            if (orderManager != null)
            {
                orderManager.OrdersChanged -= RefreshOrderList;
            }

            if (scoreManager != null)
            {
                scoreManager.OrderScored -= ShowScorePopup;
            }
        }

        private void Update()
        {
            if (scoreManager == null || gameManager == null)
            {
                return;
            }

            if (scoreManager.Score != lastScore)
            {
                lastScore = scoreManager.Score;

                scoreText.text =
                    "Score: " + lastScore;
            }

            if (scoreManager.Coins != lastCoins)
            {
                lastCoins = scoreManager.Coins;

                coinsText.text =
                    "Coins: " + lastCoins;
            }

            int currentTime =
                Mathf.CeilToInt(
                    gameManager.RemainingTime);

            if (currentTime != lastTime)
            {
                lastTime = currentTime;

                timerText.text =
                    "Time: " + currentTime;

                for (int i = 0; i < orderViews.Count; i++)
                {
                    orderViews[i].Refresh();
                }
            }

            int ordersCount =
                orderManager.ActiveOrders.Count;

            if (ordersCount != lastOrdersCount)
            {
                lastOrdersCount = ordersCount;

                ordersCountText.text =
                    "Orders: " + ordersCount;
            }
        }

        public void ShowGameplay()
        {
            gameplayPanel.SetActive(true);
            resultsPanel.SetActive(false);
            RefreshOrderList();
        }

        public void ShowResults(int score, int coins, int completedOrders, PlayerProgressData progressData)
        {
            gameplayPanel.SetActive(false);
            resultsPanel.SetActive(true);

            resultsText.text =
                "Delivery Rush Results\n" +
                "Score: " + score + "\n" +
                "Coins earned: " + coins + "\n" +
                "Completed orders: " + completedOrders + "\n" +
                "Best score: " + progressData.bestScore + "\n" +
                "Total coins: " + progressData.totalCoins;
        }

        private void RefreshOrderList()
        {
            OrderManager runtimeOrderManager = FindFirstObjectByType<OrderManager>();
            if (runtimeOrderManager != null)
            {
                orderManager = runtimeOrderManager;
            }

            for (int i = 0; i < orderViews.Count; i++)
            {
                Destroy(orderViews[i].gameObject);
            }

            orderViews.Clear();

            IReadOnlyList<OrderData> orders = orderManager.ActiveOrders;
            for (int i = 0; i < orders.Count; i++)
            {
                OrderButtonView view = Instantiate(orderButtonPrefab, ordersContainer);
                view.gameObject.SetActive(true);
                view.Setup(orders[i], orderManager.CompleteOrder);
                orderViews.Add(view);
            }
        }

        private void ShowScorePopup(OrderData order)
        {
            if (popupPool == null)
            {
                Debug.LogError(
                    "PopupPool reference missing.");

                return;
            }

            ScorePopupView popup = popupPool.Get();
            popup.transform.localPosition = new Vector3(Random.Range(-90f, 90f), Random.Range(-25f, 35f), 0f);
            popup.Setup("+" + order.rewardPoints + " points");
        }
    }
}
