namespace Tetris.CLI.Keyboard
{
    public class KeyboardListener
    {
        private static KeyboardListener _instance;
        public static KeyboardListener Instance
        {
            get => _instance ??= new KeyboardListener();
        }

        private ConsoleKeyInfo ConsoleKeyInfo { get; set; }
        public KeyboardListener Listen()
        {
            ConsoleKeyInfo pressKey = new ConsoleKeyInfo();
            if (Console.KeyAvailable)
                pressKey = Console.ReadKey(true);
            ConsoleKeyInfo = pressKey;
            return this;
        }

        public KeyboardAction GetAction()
        {
            if (ConsoleKeyInfo.Key.ToString() == "RightArrow")
                return KeyboardAction.RightArrow;

            if (ConsoleKeyInfo.Key.ToString() == "LeftArrow")
                return KeyboardAction.LeftArrow;

            if (ConsoleKeyInfo.Key.ToString() == "DownArrow")
                return KeyboardAction.DownArrow;

            if (ConsoleKeyInfo.Key.ToString() == "UpArrow")
                return KeyboardAction.UpArrow;

            if (ConsoleKeyInfo.Key.ToString().ToUpper() == "A")
                return KeyboardAction.A;

            if (ConsoleKeyInfo.Key.ToString().ToUpper() == "B")
                return KeyboardAction.B;

            return KeyboardAction.None;
        }
    }
}