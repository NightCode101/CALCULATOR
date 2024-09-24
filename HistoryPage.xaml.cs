using Microsoft.Maui.Controls;
using System.Collections.Generic;

namespace Calculator
{
    public partial class HistoryPage : ContentPage
    {
        public HistoryPage(List<string> history)
        {
            InitializeComponent();

            HistoryList.ItemsSource = history;  // Bind the history list to the CollectionView

        }
    }
}
