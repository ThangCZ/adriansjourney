using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerStatsHolder : MonoBehaviour
{

    public int healthMax;
    public int manaMax;

    public Text healthBar;
    public Text manaBar;

    private int healthActual;
    private int manaActual;

    void Start()
    {
        healthActual = healthMax;
        manaActual = manaMax;

        onStatsChange();
    }

    /// <summary>
    /// Modifies health by healthDelta value.
    /// </summary>
    /// <param name="healthDelta"></param>
    /// <returns>True if health was modified, false otherwise.</returns>
    public bool modifyHealth(int healthDelta)
    {
        int oldHealth = healthActual;
        healthActual = Mathf.Min(Mathf.Max(0, healthActual + healthDelta), healthMax);

        if (healthActual == 0)
        {
            Debug.Log("Player is dead");
        }

        if (oldHealth != healthActual)
        {
            onStatsChange();
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool modifyMana(int manaDelta)
    {
        int oldMana = manaActual;
        manaActual = Mathf.Min(Mathf.Max(0, manaActual + manaDelta), manaMax);

        if (oldMana != manaActual)
        {
            onStatsChange();
            return true;
        }
        else
        {
            return false;
        }
    }

    private void onStatsChange()
    {
        healthBar.text = "Health: " + healthActual + "/" + healthMax;
        manaBar.text = "Mana: " + manaActual + "/" + manaMax;
    }
}
