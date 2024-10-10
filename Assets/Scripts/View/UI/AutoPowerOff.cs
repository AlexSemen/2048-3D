using UnityEngine;

namespace View.UI
{
    public class AutoPowerOff : MonoBehaviour
    {
        [SerializeField] private float _timeLife = 10f;

        private float _currentTimeLife;

        private void OnEnable()
        {
            _currentTimeLife = _timeLife;
        }

        private void Update()
        {
            if (_currentTimeLife < 0 || Input.GetMouseButtonUp(0))
            {
                gameObject.SetActive(false);
                return;
            }
            else
            {
                _currentTimeLife -= Time.deltaTime;
            }
        }
    }
}
