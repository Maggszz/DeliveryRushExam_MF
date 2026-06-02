using System;
using DeliveryRushExam.Data;
using UnityEngine;

namespace DeliveryRushExam.Core
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private bool verboseLogs;

        public int Score { get; private set; }
        public int Coins { get; private set; }
        public int CompletedOrders { get; private set; }

        public event Action<int, int, int> ScoreChanged;
        public event Action<OrderData> OrderScored;

        public void ResetMatch()
        {
            Score = 0;
            Coins = 0;
            CompletedOrders = 0;
            ScoreChanged?.Invoke(Score, Coins, CompletedOrders);
        }

        public void AddCompletedOrder(OrderData order)
        {
            Score += order.rewardPoints;
            Coins += order.rewardCoins;
            CompletedOrders++;

            if (verboseLogs)
            {
                Debug.Log("Completed order " + order.customerName + " score " + Score + " coins " + Coins);
            }

            OrderScored?.Invoke(order);
            ScoreChanged?.Invoke(Score, Coins, CompletedOrders);
        }
    }
}
