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
using TechService_WPF.Models;

namespace TechService_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Контекст базы данных для обращения к ней
        /// </summary>
        private TechServiceDBContext _dbContext;

        public MainWindow()
        {
            InitializeComponent();
            _dbContext = new TechServiceDBContext();
        }

        private void B_Login_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TB_Login.Text) && !string.IsNullOrWhiteSpace(TB_Login.Text))
            {
                if (!string.IsNullOrEmpty(PB_Password.Password) && !string.IsNullOrWhiteSpace(PB_Password.Password))
                {
                    UserService authorizationUser = _dbContext.UserServices.FirstOrDefault(findUser => findUser.LoginUser == TB_Login.Text && findUser.PasswordUser == PB_Password.Password);

                    if(authorizationUser != null)
                    {
                        switch (authorizationUser.IdUserRole)
                        {
                            case 1:
                                ServiceWindow serviceWindow = new ServiceWindow();
                                this.Visibility = Visibility.Hidden;
                                serviceWindow.Show();
                                break;
                            case 2:
                                ExecutorWindow executorWindow = new ExecutorWindow(authorizationUser);
                                this.Visibility = Visibility.Hidden;
                                executorWindow.Show();
                                break;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Неверный логин или пароль!", "Ошибка входа", MessageBoxButton.OK, MessageBoxImage.Error);
                        TB_Login.Text = "";
                        PB_Password.Password = "";
                    }
                }
                else
                {
                    MessageBox.Show("Поле пароль не может быть пустым!", "Ошибка входа", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Поле логин не может быть пустым!", "Ошибка входа", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
