using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Lottery.Business.Interfaces;
using Lottery.Data.Interfaces;
using Lottery.Domain;

namespace Lottery.UI
{
    public partial class LotteryWindow : Window
    {
        ILotteryGameRepository _lotteryGameRepository;
        IDrawRepository _drawRepository;
        IDrawService _drawService;
        public LotteryWindow(ILotteryGameRepository lotteryGameRepository,
            IDrawRepository drawRepository, IDrawService drawService)
        {
            InitializeComponent();

            _lotteryGameRepository = lotteryGameRepository;
            _drawRepository = drawRepository;
            _drawService = drawService;

            GameComboBox.ItemsSource = lotteryGameRepository.GetAll();
            GameComboBox.SelectedIndex = 0;
        }

        private void ShowDrawsButton_Click(object sender, RoutedEventArgs e)
        {
            //Do NOT change any code in this method

            RetrieveDraws();

            NewDrawButton.Visibility = Visibility.Visible;
            DrawsListView.Visibility = Visibility.Visible;
        }

        private void NewDrawButton_OnClick(object sender, RoutedEventArgs e)
        {
            //TODO: create the draw
            _drawService.CreateDrawFor((LotteryGame)GameComboBox.SelectedItem);
            RetrieveDraws(); //Refresh the draws that are shown in the ListView
        }

        private void GameComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Do NOT change any code in this method

            NewDrawButton.Visibility = Visibility.Hidden;
            DrawsListView.Visibility = Visibility.Hidden;
        }

        private void RetrieveDraws()
        {
            int id = ((LotteryGame)GameComboBox.SelectedItem).Id;
            DrawsListView.ItemsSource = _drawRepository.Find(id, FromDatePicker.SelectedDate, UntilDatePicker.SelectedDate);
        }
    }
}
