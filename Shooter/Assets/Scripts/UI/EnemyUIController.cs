using UnityEngine;
using UnityEngine.UI;

public class EnemyUIController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform canvasTransform;
    [SerializeField] private Image healthBar;

    private void FixedUpdate()
    {
        canvasTransform.LookAt(target);
    }

    public void healthBarChange(float value)
    {
        healthBar.fillAmount = value;
    }
}
