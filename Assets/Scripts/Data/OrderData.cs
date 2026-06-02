using System;

namespace DeliveryRushExam.Data
{
    [Serializable]
    public class OrderData
    {
        public string id;
        public string customerName;
        public int rewardPoints;
        public int rewardCoins;
        public float timeLimit;
        public float remainingTime;

        public OrderData(string id, string customerName, int rewardPoints, int rewardCoins, float timeLimit)
        {
            this.id = id;
            this.customerName = customerName;
            this.rewardPoints = rewardPoints;
            this.rewardCoins = rewardCoins;
            this.timeLimit = timeLimit;
            remainingTime = timeLimit;
        }
    }
}
