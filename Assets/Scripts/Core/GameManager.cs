using System;
using System.Threading.Tasks;
using DeliveryRushExam.Save;
using DeliveryRushExam.UI;
using UnityEngine;

namespace DeliveryRushExam.Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private float matchDurationSeconds = 90f;
        [SerializeField] private bool autoStart = true;
        [SerializeField] private bool verboseLogs;

        [Header("Scene References")]
        [SerializeField] private OrderManager orderManager;
        [SerializeField] private ScoreManager scoreManager;
        [SerializeField] private SaveManager saveManager;
        [SerializeField] private UIManager uiManager;

        private float remainingTime;
        private bool isPlaying;
        private bool isFinishing;

        public float RemainingTime => remainingTime;
        public float MatchDurationSeconds => matchDurationSeconds;
        public bool IsPlaying => isPlaying;

        public event Action MatchStarted;
        public event Action MatchEnded;

        private void Awake()
        {
            if (orderManager == null)
            {
                orderManager = FindFirstObjectByType<OrderManager>();
            }

            if (scoreManager == null)
            {
                scoreManager = FindFirstObjectByType<ScoreManager>();
            }

            if (saveManager == null)
            {
                saveManager = FindFirstObjectByType<SaveManager>();
            }

            if (uiManager == null)
            {
                uiManager = FindFirstObjectByType<UIManager>();
            }
        }

        private void Start()
        {
            if (autoStart)
            {
                StartMatch();
            }
        }

        private void Update()
        {
            if (!isPlaying)
            {
                return;
            }

            remainingTime -= Time.deltaTime;

            if (verboseLogs)
            {
                Debug.Log("Match time: " + remainingTime);
            }

            if (remainingTime <= 0f)
            {
                _ = FinishMatchAsync();
            }
        }

        public void StartMatch()
        {
            remainingTime = matchDurationSeconds;
            isPlaying = true;
            isFinishing = false;

            scoreManager.ResetMatch();
            orderManager.StartOrders();
            uiManager.ShowGameplay();

            MatchStarted?.Invoke();
        }

        public async Task FinishMatchAsync()
        {
            if (isFinishing)
            {
                return;
            }

            isFinishing = true;
            isPlaying = false;

            orderManager.StopOrders();

            await saveManager.SaveMatchResultAsync(
                scoreManager.Score,
                scoreManager.Coins,
                scoreManager.CompletedOrders);

            uiManager.ShowResults(scoreManager.Score, scoreManager.Coins, scoreManager.CompletedOrders, saveManager.CurrentProgress);
            MatchEnded?.Invoke();
        }
    }
}
