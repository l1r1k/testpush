using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using TechService_WPF.Models;

namespace TechService_WPF
{
    /// <summary>
    /// Логика взаимодействия для ServiceWindow.xaml
    /// </summary>
    public partial class ServiceWindow : Window
    {

        private TechServiceDBContext _dbContext;

        private Request selectedRequest;
        private Executor selectedExecutor;

        public ServiceWindow()
        {
            InitializeComponent();
            _dbContext = new TechServiceDBContext();

            _dbContext.Requests.Load();

            DG_Requests.ItemsSource = _dbContext.Requests.Local.ToBindingList();

            DP_DateAddingRequest.SelectedDate = DateTime.Now;
            DP_DateAddingRequest.DisplayDateEnd = DateTime.Now;

            _dbContext.Equipment.Load();
            _dbContext.TypeErrorSupplies.Load();
            _dbContext.Clients.Load();
            _dbContext.StatusRequests.Load();
            _dbContext.UserServices.Load();

            CB_UpdStatusRequest.ItemsSource = _dbContext.StatusRequests.Local.ToList();
            CB_Executor.ItemsSource = _dbContext.UserServices.Local.Where(user => user.IdUserRole == 2).ToList();
            CB_EquipmentRequest.ItemsSource = _dbContext.Equipment.Local.ToList();
            CB_TypeErrorRequest.ItemsSource = _dbContext.TypeErrorSupplies.Local.ToList();
            CB_ClientRequest.ItemsSource = _dbContext.Clients.Local.ToList();
            CB_StatusRequest.ItemsSource = _dbContext.StatusRequests.Local.ToList();

            List<string> stats = GetStats();
            TB_CountEndedWorks.Text = stats[0];
            TB_StatErrors.Text = stats[1];
        }

        private async void B_AddRequest_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TB_NumberRequest.Text) && !string.IsNullOrWhiteSpace(TB_NumberRequest.Text))
            {
                if (!string.IsNullOrEmpty(TB_InfoErrorRequest.Text) && !string.IsNullOrWhiteSpace(TB_InfoErrorRequest.Text))
                {
                    string dateFromDatePicker = DP_DateAddingRequest.Text;

                    int indexOfPointInDate = dateFromDatePicker.IndexOf('.');
                    string dayFromDataPicker = dateFromDatePicker.Substring(0, indexOfPointInDate);
                    dateFromDatePicker = dateFromDatePicker.Remove(0, indexOfPointInDate + 1);
                    indexOfPointInDate = dateFromDatePicker.IndexOf('.');
                    string monthFromDataPicker = dateFromDatePicker.Substring(0, indexOfPointInDate);
                    dateFromDatePicker = dateFromDatePicker.Remove(0, indexOfPointInDate + 1);
                    string yearFromDataPicker = dateFromDatePicker.Substring(0, dateFromDatePicker.Length);

                    Request newRequest = new Request();
                    newRequest.NumberRequest = TB_NumberRequest.Text;
                    newRequest.DateAddingRequest = new DateTime(Convert.ToInt32(yearFromDataPicker), Convert.ToInt32(monthFromDataPicker), Convert.ToInt32(dayFromDataPicker));
                    newRequest.IdEquipment = (int)CB_EquipmentRequest.SelectedValue;
                    newRequest.IdTypeErrorSupply = (int)CB_TypeErrorRequest.SelectedValue;
                    newRequest.InfoAboutProblemRequest = TB_InfoErrorRequest.Text;
                    newRequest.IdClient = (int)CB_ClientRequest.SelectedValue;
                    newRequest.IdStatusRequest = (int)CB_StatusRequest.SelectedValue;

                    try
                    {
                        _dbContext.Requests.Add(newRequest);
                        await _dbContext.SaveChangesAsync();
                        MessageBox.Show("Заявка успешно создана!", "Добавление заявки");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Поле описание проблемы не может быть пустым!", "Ошибка добавления клиента", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Поле номер заявки не может быть пустым!", "Ошибка добавления клиента", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void B_AddClient_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TB_SurnameClient.Text) && !string.IsNullOrWhiteSpace(TB_SurnameClient.Text))
            {
                if (!string.IsNullOrEmpty(TB_NameClient.Text) && !string.IsNullOrWhiteSpace(TB_NameClient.Text))
                {
                    if (!string.IsNullOrEmpty(TB_PhoneNumberClient.Text) && !string.IsNullOrWhiteSpace(TB_PhoneNumberClient.Text))
                    {
                        Client newClient = new Client();
                        newClient.SurnameClient = TB_SurnameClient.Text;
                        newClient.NameClient = TB_NameClient.Text;
                        newClient.PatronymicClient = TB_PatronymicClient.Text;
                        newClient.PhoneNumberClient = TB_PhoneNumberClient.Text;

                        _dbContext.Clients.Add(newClient);

                        await _dbContext.SaveChangesAsync();

                        MessageBox.Show($"Клиент {TB_SurnameClient.Text} {TB_NameClient.Text} успешно добавлен!", "Добавление клиента");

                        TB_SurnameClient.Text = "";
                        TB_NameClient.Text = "";
                        TB_PatronymicClient.Text = "";
                        TB_PhoneNumberClient.Text = "";

                        _dbContext.Clients.Load();

                        CB_ClientRequest.ItemsSource = _dbContext.Clients.Local.ToList();
                    }
                    else
                    {
                        MessageBox.Show("Поле номер телефона не может быть пустым!", "Ошибка добавления клиента", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Поле имя не может быть пустым!", "Ошибка добавления клиента", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Поле фамилия не может быть пустым!", "Ошибка добавления клиента", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            foreach(Window window in Application.Current.Windows)
            {
                if (window.Visibility == Visibility.Hidden)
                {
                    window.Visibility = Visibility.Visible;
                }
            }
        }

        private void DG_Requests_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedRequest = (Request)DG_Requests.SelectedItems[0];
            CB_UpdStatusRequest.SelectedValue = selectedRequest.IdStatusRequest;
            TB_UpdInfoErrorRequest.Text = selectedRequest.InfoAboutProblemRequest;

            _dbContext.Executors.Load();

            DG_Executors.ItemsSource = _dbContext.Executors.Where(executor => executor.IdRequest == selectedRequest.IdRequest).ToList();
        }

        private void DG_Executors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedExecutor = (Executor)DG_Executors.SelectedItems[0];
            CB_Executor.SelectedValue = selectedExecutor.IdExecutor;
        }

        private List<string> GetStats()
        {
            List<Request> requestsFromDb = _dbContext.Requests.Local.ToList();
            List<TypeErrorSupply> typeErrorSupplies = _dbContext.TypeErrorSupplies.Local.ToList();
            int countExecutedWorks = requestsFromDb.Count();
            string statsTypeErrors = "";

            foreach (TypeErrorSupply typeErrorSupply in typeErrorSupplies)
            {
                int countRequestsWithTypeErrorSupply = requestsFromDb.Where(request => request.IdTypeErrorSupply == typeErrorSupply.IdTypeErrorSupply).Count();
                statsTypeErrors += $"'{typeErrorSupply.NameTypeErrorSupply}' количество заявок: {countExecutedWorks},";
            }

            return new List<string>() { countExecutedWorks.ToString(), statsTypeErrors};
        }

        private void B_UpdateData_Click(object sender, RoutedEventArgs e)
        {
            List<string> stats = GetStats();
            TB_CountEndedWorks.Text = stats[0];
            TB_StatErrors.Text = stats[1];
        }

        private void B_Logout_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void B_UpdRequest_Click(object sender, RoutedEventArgs e)
        {
            if (selectedRequest != null)
            {
                selectedRequest.IdStatusRequest = (int)CB_UpdStatusRequest.SelectedValue;
                selectedRequest.InfoAboutProblemRequest = TB_UpdInfoErrorRequest.Text;

                if ((int)CB_UpdStatusRequest.SelectedValue == 3)
                {
                    selectedRequest.DateClosingRequest = DateTime.Now;
                }

                try
                {
                    _dbContext.Entry(selectedRequest).State = EntityState.Modified;

                    await _dbContext.SaveChangesAsync();

                    _dbContext.Requests.Load();
                    DG_Requests.ItemsSource = _dbContext.Requests.Local.ToBindingList();

                    DG_Requests.Items.Refresh();

                    MessageBox.Show("Изменение заявки прошло успешно!", "Изменение заявки");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка изменения заявки", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Чтобы изменить заявку, выберите ее справа!", "Ошибка изменения заявки", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void B_UpdExecutor_Click(object sender, RoutedEventArgs e)
        {
            if(selectedExecutor != null)
            {
                selectedExecutor.IdUser = (int)CB_Executor.SelectedValue;

                try
                {
                    _dbContext.Entry(selectedExecutor).State = EntityState.Modified;

                    await _dbContext.SaveChangesAsync();

                    _dbContext.Executors.Load();

                    DG_Executors.ItemsSource = _dbContext.Executors.Local.ToBindingList();

                    DG_Executors.Items.Refresh();

                    MessageBox.Show("Исполнитель успешно изменен!", "Изменение исполнителя");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка изменения исполнителя", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Чтобы изменить исполнителя, выберите его справа!", "Ошибка изменения исполнителя", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void B_AddExecutor_Click(object sender, RoutedEventArgs e)
        {
            if (selectedRequest != null)
            {
                Executor newExecutor = new Executor();
                newExecutor.IdRequest = selectedRequest.IdRequest;
                newExecutor.IdUser = (int)CB_Executor.SelectedValue;

                try
                {
                    _dbContext.Executors.Add(newExecutor);

                    await _dbContext.SaveChangesAsync();

                    _dbContext.Executors.Load();

                    DG_Executors.ItemsSource = _dbContext.Executors.Where(executor => executor.IdRequest == selectedRequest.IdRequest).ToList();

                    DG_Executors.Items.Refresh();

                    MessageBox.Show("Исполнитель успешно добавлен!", "Добавление исполнителя");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка добавления исполнителя", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Чтобы добавить исполнителя, выберите заявку!", "Ошибка добавления исполнителя", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
