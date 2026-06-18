using UnityEngine;

namespace DeliveryRushExam.Save
{
    public class SaveServicesInstaller : MonoBehaviour
    {
        public enum SaveMode
        {
            Local,
            Cloud
        }

        [SerializeField] private SaveMode saveMode = SaveMode.Local;

        private void Awake()
        {
            ServiceLocator.Clear();

            switch (saveMode)
            {
                case SaveMode.Local:
                    ServiceLocator.Register<ISaveService>(
                        new LocalSaveService());
                    break;

                case SaveMode.Cloud:
                    ServiceLocator.Register<ISaveService>(
                        new UgsCloudSaveService());
                    break;
                }
        }
    } 
}
