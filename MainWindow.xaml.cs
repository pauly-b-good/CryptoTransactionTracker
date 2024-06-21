using Microsoft.Win32;
using System.IO.Packaging;
using System.IO;
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
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace CryptoTransactionTracker
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DisplayVersion();
        }

        private void DisplayVersion()
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version;
            VersionTextBlock.Text = $"Version {version}";
        }

        private void CheckBox_CheckedChanged(object sender, RoutedEventArgs e)
        {
            ShowWalletAddressTextBox(sender);
        }

        private void SaveExcelFileButton_Click(object sender, RoutedEventArgs e)
        {
            // Create and configure a SaveFileDialog
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*",
                DefaultExt = "xlsx",
                AddExtension = true,
                Title = "Save Excel File"
            };

            // Show the dialog and get the selected file path
            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                ExcelGenerator.GenerateExcel(filePath);     // submit the transactions to this method as well so it has the data to save to the excel file

                MessageBox.Show($"Excel file '{filePath}' created successfully.", "File Saved", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("File save operation was canceled.", "Save Canceled", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ShowWalletAddressTextBox(object sender)
        {
            if (sender is CheckBox checkBox)
            {
                TextBox associatedTextBox = null;

                switch (checkBox.Name)
                {
                    case "KaspaCheckBox":
                        associatedTextBox = KaspaWalletAddress;
                        break;
                    case "PugDagCheckBox":
                        associatedTextBox = PugDagWalletAddress;
                        break;
                    //case "ConsensusCheckBox":
                    //    associatedTextBox = ConsensusWalletAddress;
                    //    break;
                    case "NautilusCheckBox":
                        associatedTextBox = NautilusWalletAddress;
                        break;
                    //case "NexelliaCheckBox":
                    //    associatedTextBox = NexelliaWalletAddress;
                    //    break;
                    case "HoosatCheckBox":
                        associatedTextBox = HoosatWalletAddress;
                        break;
                    //case "PyrinCheckBox":
                    //    associatedTextBox = PyrinWalletAddress;
                    //    break;

                        // Add cases for other cryptocurrencies
                }

                if (associatedTextBox != null)
                {
                    associatedTextBox.Visibility = checkBox.IsChecked == true ? Visibility.Visible : Visibility.Collapsed;
                }
            }
        }

        private async void CheckTransactionsButton_Click(object sender, RoutedEventArgs e)
        {
            ResultTextBox.Clear();

            if (KaspaCheckBox.IsChecked == true)
            {
                string walletAddress = KaspaWalletAddress.Text;
                if (!string.IsNullOrEmpty(walletAddress))
                {
                    await FetchTransactions("Kaspa", walletAddress);
                }
            }
            if (PugDagCheckBox.IsChecked == true)
            {
                string walletAddress = PugDagWalletAddress.Text;
                if (!string.IsNullOrEmpty(walletAddress))
                {
                    await FetchTransactions("PugDag", walletAddress);
                }
            }
            //if (ConsensusCheckBox.IsChecked == true)
            //{
            //    string walletAddress = ConsensusWalletAddress.Text;
            //    if (!string.IsNullOrEmpty(walletAddress))
            //    {
            //        await FetchTransactions("Consensus", walletAddress);
            //    }
            //}
            if (NautilusCheckBox.IsChecked == true)
            {
                string walletAddress = NautilusWalletAddress.Text;
                if (!string.IsNullOrEmpty(walletAddress))
                {
                    await FetchTransactions("Nautilus", walletAddress);
                }
            }
            //if (NexelliaCheckBox.IsChecked == true)
            //{
            //    string walletAddress = NexelliaWalletAddress.Text;
            //    if (!string.IsNullOrEmpty(walletAddress))
            //    {
            //        await FetchTransactions("Nexell-ia", walletAddress);
            //    }
            //}
            if (HoosatCheckBox.IsChecked == true)
            {
                string walletAddress = HoosatWalletAddress.Text;
                if (!string.IsNullOrEmpty(walletAddress))
                {
                    await FetchTransactions("Hoosat", walletAddress);
                }
            }
            //if (PyrinCheckBox.IsChecked == true)
            //{
            //    string walletAddress = PyrinWalletAddress.Text;
            //    if (!string.IsNullOrEmpty(walletAddress))
            //    {
            //        await FetchTransactions("Pyrin", walletAddress);
            //    }
            //}
            // Add checks for other cryptocurrencies
        }

        private async Task FetchTransactions(string crypto, string walletAddress)
        {
            try
            {
                List<Transaction> transactions = await ApiHandler.GetTransactionsAsync(crypto, walletAddress);
                DisplayTransactions(crypto, transactions);
            }
            catch (Exception ex)
            {
                ResultTextBox.AppendText($"Error fetching transactions for {crypto} ({walletAddress}): {ex.Message}\n");
            }
        }

        private void DisplayTransactions(string crypto, List<Transaction> transactions)
        {
            ResultTextBox.AppendText($"Transactions for {crypto}:\n");

            foreach (Transaction transaction in transactions)
            {
                ResultTextBox.AppendText($"Amount: {transaction.Outputs.First().Amount * .00000001}\n");
            }
        }
    }
}