namespace SchoolManager
{
    public class UndoManager
    {
        // Stack pour stocker les actions annulables
        private static Stack<IUndoableAction> undoStack = new Stack<IUndoableAction>();

        // Pousser l'action dans la stack
        public static void RecordAction(IUndoableAction action)
        {
            undoStack.Push(action);
        }

        // Vérifier si une action peut être annulée
        public static bool CanUndo()
        {
            return undoStack.Count > 0;
        }

        public static void UndoLastAction()
        {
            if (undoStack.Count > 0)
            {
                // Récupérer et annuler la dernière action
                var lastAction = undoStack.Pop();

                // Appelle Undo() sur l'action
                lastAction.Undo();
                
                Console.WriteLine($"Undid action --> {lastAction.Description}");
            }
            else
            {
                Console.WriteLine("No actions to undo.");
            }
        }
    }
}
