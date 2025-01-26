public class Fountain : FixInteractable {

    protected override void InteractionAction() {
        weaponHandler.Current.ReduceDirtLevel();
    }
}
