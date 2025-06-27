using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace POEPart3
{
    /// <summary>
    /// Interaction logic for TaskManger.xaml
    /// </summary>
    public partial class TaskManger : Window

    {
        ObservableCollection<string> tasks = new ObservableCollection<string>();
        ObservableCollection<string> doneTasks = new ObservableCollection<string>();

        public TaskManger()
        {
            InitializeComponent();
            TaskListBox.ItemsSource = tasks;
            DoneTaskListBox.ItemsSource = doneTasks;
           


        }


        private void add_task(object sender, RoutedEventArgs e)
        {
            string desc = TaskDescriptionBox.Text;
            var date = TaskDatePicker.SelectedDate;
            if (!string.IsNullOrWhiteSpace(desc) && date.HasValue)
            {
                tasks.Add($"{desc} (Remind: {date.Value.ToShortDateString()})");
                TaskDescriptionBox.Clear();
                TaskDatePicker.SelectedDate = null;
            }
            else
            {
                MessageBox.Show("Please enter a task description and select a date.");
            }

        }

        private void mark_done(object sender, RoutedEventArgs e)
        {
            if (TaskListBox.SelectedItem != null)
            {
                string completedTask = TaskListBox.SelectedItem.ToString();
                tasks.Remove(completedTask);
                doneTasks.Add(completedTask);
            }
            else
            {
                MessageBox.Show("Select a task to mark as done.");
            }
        }

        private void TaskDescriptionBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

    }

}
    

