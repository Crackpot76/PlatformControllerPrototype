
public interface IStateMachine {

    AttackDetails GetCurrentAttackDetails();

    void EventTrigger(string parameter);
}
