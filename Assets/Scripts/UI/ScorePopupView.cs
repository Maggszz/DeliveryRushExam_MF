using TMPro;
using UnityEngine;

namespace DeliveryRushExam.UI
{
    public class ScorePopupView : MonoBehaviour
    {
        [SerializeField] private TMP_Text messageText;
        [SerializeField] private float lifetime = 1.1f;
        [SerializeField] private float moveSpeed = 55f;

        private float age;
        private PopupPool pool;
        
        public void SetPool(PopupPool popupPool)
        {
            pool = popupPool;
        }

        public void Setup(string message)
        {
            age = 0f;
            transform.localPosition = Vector3.zero;
            messageText.text = message;
            CanvasGroup canvasGroup = GetComponent<CanvasGroup>();

            if (canvasGroup != null)
            {
                canvasGroup.alpha = 1f;
            }
        }

        private void Update()
        {
            age += Time.deltaTime;
            transform.localPosition += Vector3.up * moveSpeed * Time.deltaTime;

            CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
            if (canvasGroup != null)
            {
                canvasGroup.alpha = 1f - age / lifetime;
            }

            if (age >= lifetime)
            {
                if (pool != null)
                {
                    pool.Return(this);
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
