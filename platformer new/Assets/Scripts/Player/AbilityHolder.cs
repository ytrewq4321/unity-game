using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    public Ability Ability;
    public KeyCode Key;

    [SerializeField] private AudioSource sound;
    private float activeTime;
    private float coolDownTime;

    enum AbilityState
    {
        ready,
        active,
        coolDown
    }

    AbilityState state = AbilityState.ready;

    private void Update()
    {
        switch(state)
        {
            case AbilityState.ready:
                if(Input.GetKeyDown(Key))
                {
                    Ability.Activate(gameObject);
                    sound.Play();
                    state = AbilityState.active;
                    activeTime = Ability.ActiveTime;
                }
            break;

            case AbilityState.active:
                if(activeTime>0)
                {
                    activeTime -= Time.deltaTime;
                }
                else
                {
                    Ability.BeginCoolDonw(gameObject);
                    state = AbilityState.coolDown;
                    coolDownTime = Ability.CooldownTime;
                }
            break;

            case AbilityState.coolDown:
                if (coolDownTime > 0)
                {
                    coolDownTime -= Time.deltaTime;
                }
                else
                {
                    state = AbilityState.ready;
                }
            break;
        }
    }
}
