using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace POEPart3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void conversation(object sender, RoutedEventArgs e)
        {
            Conversation conversationwindow = new Conversation();
            conversationwindow.Show();

        }

        private void task_manager(object sender, RoutedEventArgs e)
        {
            TaskManger taskmangerwindow = new TaskManger();
            taskmangerwindow.Show();    

        }

        private void quiz_game(object sender, RoutedEventArgs e)
        {
            Quiz_game quizwindow = new Quiz_game();
            quizwindow.Show();

        }

        private void activity_log(object sender, RoutedEventArgs e)
        {
            Activity_Log activitylogwindow = new Activity_Log();
            activitylogwindow.Show();

        }

        private void exit(object sender, RoutedEventArgs e)
        {

        }
    }
}