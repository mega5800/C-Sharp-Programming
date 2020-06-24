namespace Ex04.Menus.Test
{
    public class Program
    {
        public static void Main()
        {
            runExercise4();
        }

        private static void runExercise4()
        {
            InterfaceMenu interfaceMenu = new InterfaceMenu();
            DelegateMenu delegateMenu = new DelegateMenu();

            interfaceMenu.RunInterfaceMenu();
            delegateMenu.RunDelegateMenu();
        }
    }
}