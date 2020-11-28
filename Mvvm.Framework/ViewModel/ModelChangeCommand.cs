namespace Mvvm.Framework.ViewModel
{
    /// <summary>
    /// Base class for <see cref="IModelChangeCommand"/>s that auto-raise modelview property changed 
    /// notifications.
    /// </summary>
    public abstract class ModelChangeCommand : IModelChangeCommand
    {
        /// <summary>
        /// The <see cref="IModelChangeNotifier"/> to use for notifying property changes.
        /// </summary>
        protected IModelChangeNotifier ModelChangeNotifier { get; }

        private object[] changedModels;

        /// <summary>
        /// Initializes a new <see cref="ModelChangeCommand"/>.
        /// </summary>
        /// <param name="modelChangeNotifier">The <see cref="IModelChangeNotifier"/> to use for
        /// notifying property changes.</param>
        /// <param name="changedModels">The relevant changed models.</param>
        public ModelChangeCommand(IModelChangeNotifier modelChangeNotifier, params object[] changedModels)
        {
            ModelChangeNotifier = modelChangeNotifier;
            this.changedModels = changedModels;
        }

        public void Execute()
        {
            ExecuteWithoutNotify();
            ModelChangeNotifier.Notify(changedModels);
        }

        /// <summary>
        /// The execution of the command without notifying anything.
        /// </summary>
        protected abstract void ExecuteWithoutNotify();

        public abstract IModelChangeCommand GetUndoCommand();
    }
}
