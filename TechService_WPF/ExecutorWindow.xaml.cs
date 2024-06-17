using Microsoft.EntityFrameworkCore;
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
using System.Windows.Shapes;
using TechService_WPF.Models;

namespace TechService_WPF
{
    /// <summary>
    /// Логика взаимодействия для ExecutorWindow.xaml
    /// </summary>
    public partial class ExecutorWindow : Window
    {
        private UserService _currentExecutor;
        private TechServiceDBContext _dbContext;
        private Request _selectedRequest;

        public ExecutorWindow(UserService userService)
        {
            InitializeComponent();
            _currentExecutor = userService;
            _dbContext = new TechServiceDBContext();

            _dbContext.Requests.Load();
            DG_Requests.ItemsSource = _dbContext.Requests.Local.ToBindingList();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.Visibility == Visibility.Hidden)
                {
                    window.Visibility = Visibility.Visible;
                }
            }
        }

        private void B_Logout_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void B_AddComment_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedRequest != null)
            {
                if(!string.IsNullOrEmpty(TB_Comment.Text) && !string.IsNullOrWhiteSpace(TB_Comment.Text))
                {
                    Executor executor = _dbContext.Executors.FirstOrDefault(executor => executor.IdUser == _currentExecutor.IdUser && executor.IdRequest == _selectedRequest.IdRequest);
                    if (executor != null)
                    {
                        ExecutorComment newExecutorComment = new ExecutorComment();
                        newExecutorComment.IdExecutor = executor.IdExecutor;
                        newExecutorComment.CommentExecutorComment = TB_Comment.Text;

                        try
                        {
                            _dbContext.ExecutorComments.Add(newExecutorComment);

                            await _dbContext.SaveChangesAsync();

                            _dbContext.ExecutorComments.Load();

                            DG_Comments.ItemsSource = _dbContext.ExecutorComments.Local.ToBindingList();
                            DG_Comments.Items.Refresh();

                            MessageBox.Show("Комментарий успешно добавлен!", "Добавление комментария");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Ошибка добавления комментария", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Вы не являетесь исполнителем данной заявки!", "Ошибка добавления комментария", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Поле комментарий должен быть заполнен!", "Ошибка добавления комментария", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Чтобы оставить комментарий, необходимо выбрать заявку!", "Ошибка добавления комментария", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DG_Requests_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedRequest = (Request)DG_Requests.SelectedItems[0];
            Executor executor = _dbContext.Executors.FirstOrDefault(executor => executor.IdRequest == _selectedRequest.IdRequest);

            DG_Comments.ItemsSource = _dbContext.ExecutorComments.Where(executorComment => executorComment.IdExecutor == executor.IdExecutor).ToList();
        }
    }
}
