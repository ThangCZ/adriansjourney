using UnityEngine;
using System.Collections;

public class EnemyStatsHolder : MonoBehaviour {

    public int healthMax;
    private int healthActual;

    void Start()
    {
        healthActual = healthMax;
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
            Debug.Log("Enemy is dead");
            Destroy(gameObject);
        }

		return oldHealth != healthActual;

        /*if (oldHealth != healthActual)
        {
            return true;
        }
        else
        {
            return false;
        }*/
    }
}
