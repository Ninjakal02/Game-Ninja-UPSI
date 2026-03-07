using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    public Image playerHpBar;
    public float maxHp = 30f;

    public void PlayerBar(float hp)
    {
        playerHpBar.fillAmount = hp / maxHp;
    }
}