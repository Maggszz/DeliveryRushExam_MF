using System;
using System.Threading.Tasks;
using DeliveryRushExam.Data;
using UnityEngine;

namespace DeliveryRushExam.Save
{
    public class SaveManager : MonoBehaviour
    {
        public PlayerProgressData CurrentProgress { get; private set; } = new PlayerProgressData();

        public event Action<PlayerProgressData> ProgressLoaded;

        private ISaveService saveService;

        private async void Awake()
        {
            saveService = ResolveSaveService();
            await LoadProgressAsync();
        }
        private ISaveService ResolveSaveService()
        {
            if (ServiceLocator.TryGet<ISaveService>(
                    out ISaveService service))
            {
                return service;
            }

            Debug.LogWarning(
                "ISaveService not registered. Falling back to LocalSaveService.");

            return new LocalSaveService();
        }

        public async Task LoadProgressAsync()
        {
            CurrentProgress = await saveService.LoadAsync();
            ProgressLoaded?.Invoke(CurrentProgress);
            Debug.Log(
                $"[SaveManager] Progress Loaded | " +
                $"BestScore:{CurrentProgress.bestScore} | " +
                $"TotalCoins:{CurrentProgress.totalCoins} | " +
                $"CompletedOrders:{CurrentProgress.completedOrders} | " +
                $"UnlockedLevel:{CurrentProgress.unlockedLevel} | " +
                $"LastSaveDate:{CurrentProgress.lastSaveDate}");
        }

        public async Task SaveMatchResultAsync(int score, int coins, int completedOrders)
        {
            CurrentProgress.bestScore = Mathf.Max(CurrentProgress.bestScore, score);
            CurrentProgress.totalCoins += coins;
            CurrentProgress.completedOrders += completedOrders;

            // Nivel simple para tener un dato extra persistido.
            CurrentProgress.unlockedLevel = Mathf.Max(CurrentProgress.unlockedLevel, 1 + CurrentProgress.completedOrders / 10);
            Debug.Log(
                $"[SaveManager] Saving Match Result | " +
                $"BestScore:{CurrentProgress.bestScore} | " +
                $"TotalCoins:{CurrentProgress.totalCoins} | " +
                $"CompletedOrders:{CurrentProgress.completedOrders} | " +
                $"UnlockedLevel:{CurrentProgress.unlockedLevel} | " +
                $"LastSaveDate:{CurrentProgress.lastSaveDate}");

            await saveService.SaveAsync(CurrentProgress);
        }
    }
}
