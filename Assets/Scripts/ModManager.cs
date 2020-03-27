using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Temporary just for testing the different modifiers on/off
// before mechanics are in place
public class ModManager : MonoBehaviour
{
    GameObject player;

    MOD_DrainOnMove moveDrain;
    MOD_DrainOnShot shotDrain;
    MOD_DrainOverTime timeDrain;
    MOD_TakesDamage damage;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        moveDrain = player.GetComponent<MOD_DrainOnMove>();
        shotDrain = player.GetComponent<MOD_DrainOnShot>();
        timeDrain = player.GetComponent<MOD_DrainOverTime>();
        damage = player.GetComponent<MOD_TakesDamage>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            damage.enabled = !damage.enabled;
            ScoreManager.Instance.InvalidateStats();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            moveDrain.enabled = !moveDrain.enabled;
            ScoreManager.Instance.InvalidateStats();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            shotDrain.enabled = !shotDrain.enabled;
            ScoreManager.Instance.InvalidateStats();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            timeDrain.enabled = !timeDrain.enabled;
            ScoreManager.Instance.InvalidateStats();
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            damage.enabled = false;
            moveDrain.enabled = false;
            shotDrain.enabled = false;
            timeDrain.enabled = false;
            ScoreManager.Instance.InvalidateStats();
        }
    }
}
