public class Container : FixInteractable {
    protected override void InteractionAction() {
        GarbageManager.Instance.EmptyGarbage();
    }
}
