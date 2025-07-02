using Template;
using Template.Stamina;
using Template.User;
using UnityEngine;

public class Cheat : MonoBehaviour
{
    [SerializeField] KeyCode win = KeyCode.W;

    [SerializeField] KeyCode race = KeyCode.R;

    [SerializeField] KeyCode SpendStamina = KeyCode.S;
    [SerializeField] KeyCode UnlimitStamina = KeyCode.D;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(win))
        {
            //GameService.PostEvent(EventID.Win);
            TemplateEventManager.WinEvent.Post(this, 0);
            UserManager.LevelUp();
        }

        if (Input.GetKeyDown(race))
        {
            switch (RaceManager.EventState)
            {
                case EventState.Deactivated:
                    RaceManager.EventActivate();
                    break;
                case EventState.Preparing:
                    RaceManager.EventJoin();
                    break;
                case EventState.Processing:
                    RaceManager.EventComplete();
                    break;
                case EventState.Completed:
                    RaceManager.ClaimReward();
                    break;
            }
        }

        if (Input.GetKeyDown(SpendStamina))
        {
            StaminaManager.SpendStamina(1);
        }

        if (Input.GetKeyDown(UnlimitStamina))
        {
            StaminaManager.UnlimitStamina();
        }
    }
}