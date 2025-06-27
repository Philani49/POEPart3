using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace POEPart3
{
    /// <summary>
    /// Interaction logic for Activity_Log.xaml
    /// </summary>
    public partial class Activity_Log : Window
    {
        // Shared observable list to hold activity entries
        public static ObservableCollection<ActivityEntry> ActivityLog { get; } = new ObservableCollection<ActivityEntry>();

        public Activity_Log()
        {
            InitializeComponent();

            // Default list when the window loads
            ActivityListView.ItemsSource = ActivityLog;
        }

        private void FilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FilterComboBox.SelectedItem is ComboBoxItem selected)
            {
                string filter = selected.Content.ToString();

                // Avoid crash by verifying null safety
                if (filter == "All Activities")
                {
                    ActivityListView.ItemsSource = ActivityLog;
                }
                else
                {
                    var filtered = new ObservableCollection<ActivityEntry>(
                        ActivityLog.Where(a => a.ActivityType.Equals(filter, StringComparison.OrdinalIgnoreCase))
                    );

                    ActivityListView.ItemsSource = filtered;
                }
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ActivityLog.Clear();
        }
    }
}
