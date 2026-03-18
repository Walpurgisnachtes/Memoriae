using UnityEngine;
using UnityEngine.UI;

namespace Memoriae
{
    public class HPBar : MonoBehaviour
    {
        [SerializeField] private Image hpBarFill;

        public void SetHPBarFill(float fillAmount)
        {
            hpBarFill.fillAmount = fillAmount;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}