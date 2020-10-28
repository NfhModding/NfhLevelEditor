using Mvvm.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Nfh.Editor
{
    public class TextModel
    {
        public string Text { get; set; } = string.Empty;
    }

    public class TextViewModel : ViewModelBase
    {
        public string Text 
        { 
            get => tm.Text; 
            set => ur.Execute(new ModelPropertyChangeCommand<TextModel, string>(
                ModelChangeNotifier, tm, tm => tm.Text, value)); 
        }
        public string SaveStatus => ur.HasUnsavedChanges ? "Unsaved changes!" : "Saved";
        public ICommand AppendChar { get; }
        public ICommand Undo { get; }
        public ICommand Redo { get; }
        public ICommand Save { get; }

        private char charCounter = 'A';
        private IUndoRedoStack ur;
        private TextModel tm;

        public TextViewModel(IModelChangeNotifier n, IUndoRedoStack ur, TextModel tm)
            : base(n, tm)
        {
            this.ur = ur;
            this.tm = tm;

            AppendChar = new RelayCommand<object>(_ =>
            {
                Text += charCounter;
                charCounter = (char)((charCounter - ('A' - 1)) % ('Z' - 'A' + 1) + 'A');
            });
            Undo = new RelayCommand<object>(_ => ur.Undo(), _ => ur.CanUndo);
            Redo = new RelayCommand<object>(_ => ur.Redo(), _ => ur.CanRedo);
            Save = new RelayCommand<object>(
                _ => 
                { 
                    ur.Save(); 
                    NotifyPropertyChanged("SaveStatus"); 
                }, 
                _ => ur.HasUnsavedChanges);
        }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IModelChangeNotifier modelChangeNotifier = new ModelChangeNotifier();
        private IUndoRedoStack undoRedoStack = new UndoRedoStack();

        public TextViewModel TextViewModel { get; }

        public MainWindow()
        {
            TextViewModel = new TextViewModel(modelChangeNotifier, undoRedoStack, new TextModel());
            InitializeComponent();
        }
    }
}
