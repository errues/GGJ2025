namespace BZ.GamepadHints
{
    public abstract class InputHandler
    {
        protected InputChangeController _inputChangeController;

        public InputHandler(InputChangeController inputChangeController)
        {
            _inputChangeController = inputChangeController;
        }

        public abstract void AddInputChangeEvents();
        public abstract void RemoveInputChangeEvents();
    }
}