using System;

namespace DeliveryRushExam.Data
{
    [Serializable]
    public class PlayerProgressData
    {
        public string playerName = "Guest Courier";
        public int bestScore;
        public int totalCoins;
        public int completedOrders;
        public int unlockedLevel = 1;
        public string lastSaveDate;

        public void TouchSaveDate()
        {
            lastSaveDate = DateTime.UtcNow.ToString("O");
        }
    }
}
