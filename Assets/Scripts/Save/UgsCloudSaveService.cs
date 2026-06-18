using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeliveryRushExam.Data;
using UnityEngine;

#if DELIVERY_RUSH_UGS
using Unity.Services.CloudSave;
#endif

namespace DeliveryRushExam.Save
{
    public class UgsCloudSaveService : ISaveService
    {
        private const string ProgressKey = "delivery_rush_progress";

        public async Task<PlayerProgressData> LoadAsync()
        {
#if DELIVERY_RUSH_UGS

            try
            {
                Debug.Log("[UGS] Attempting Cloud Load...");

                var result =
                    await CloudSaveService.Instance
                        .Data
                        .Player
                        .LoadAsync(
                            new HashSet<string>
                            {
                                ProgressKey
                            });

                Debug.Log($"[UGS] Result Count: {result.Count}");

                if (!result.TryGetValue(
                        ProgressKey,
                        out var item))
                {
                    Debug.LogWarning(
                        $"[UGS] Key '{ProgressKey}' not found in Cloud Save.");

                    return new PlayerProgressData();
                }

                string json = item.Value.GetAsString();

                Debug.Log(
                    $"[UGS] Raw Json Loaded: {json}");

                PlayerProgressData data =
                    JsonUtility.FromJson<PlayerProgressData>(json);

                if (data == null)
                {
                    Debug.LogWarning(
                        "[UGS] Json deserialized as NULL.");

                    return new PlayerProgressData();
                }

                Debug.Log(
                    $"[UGS] Loaded Data | " +
                    $"BestScore:{data.bestScore} | " +
                    $"TotalCoins:{data.totalCoins} | " +
                    $"CompletedOrders:{data.completedOrders} | " +
                    $"UnlockedLevel:{data.unlockedLevel}");

                return data;
            }
            catch (Exception ex)
            {
                Debug.LogError(
                    $"[UGS] Load failed:\n{ex}");

                return new PlayerProgressData();
            }

#else

            Debug.LogWarning(
                "[UGS] Cloud Save disabled by scripting define.");

            await Task.Yield();

            return new PlayerProgressData();

#endif
        }

        public async Task SaveAsync(PlayerProgressData progressData)
        {
#if DELIVERY_RUSH_UGS

            try
            {
                progressData.TouchSaveDate();

                string json =
                    JsonUtility.ToJson(progressData);

                Debug.Log(
                    $"[UGS] Saving Json: {json}");

                var saveData =
                    new Dictionary<string, object>
                    {
                        {
                            ProgressKey,
                            json
                        }
                    };

                await CloudSaveService.Instance
                    .Data
                    .Player
                    .SaveAsync(saveData);

                Debug.Log(
                    $"[UGS] Save completed successfully.");
            }
            catch (Exception ex)
            {
                Debug.LogError(
                    $"[UGS] Save failed:\n{ex}");
            }

#else

            Debug.LogWarning(
                "[UGS] Cloud Save disabled by scripting define.");

            await Task.Yield();

#endif
        }
    }
}