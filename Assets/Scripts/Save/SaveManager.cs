using System;
using System.Threading.Tasks;
using DeliveryRushExam.Data;
using DeliveryRushExam.UGS;
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
            saveService = ServiceLocator.Get<ISaveService>();
            await WaitForCloudInitializationIfNeeded();
            await LoadProgressAsync();
        }
        private async Task WaitForCloudInitializationIfNeeded()
        {
            if (!(saveService is UgsCloudSaveService))
            {
                return;
            }

            UgsInitializer initializer =
                FindFirstObjectByType<UgsInitializer>();

            if (initializer == null)
            {
                Debug.LogError(
                    "[SaveManager] UgsInitializer not found.");

                return;
            }

            while (!initializer.IsReady)
            {
                await Task.Yield();
            }
        }

        public async Task LoadProgressAsync()
        {
            Debug.Log("[SaveManager] Loading Progress...");
            
            CurrentProgress = await saveService.LoadAsync();
            
            Debug.Log(
                $"[SaveManager] Progress Loaded | " +
                $"BestScore:{CurrentProgress.bestScore} | " +
                $"TotalCoins:{CurrentProgress.totalCoins} | " +
                $"CompletedOrders:{CurrentProgress.completedOrders} | " +
                $"UnlockedLevel:{CurrentProgress.unlockedLevel} | " +
                $"LastSaveDate:{CurrentProgress.lastSaveDate}");
            
            ProgressLoaded?.Invoke(CurrentProgress);
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
