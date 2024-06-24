﻿using Repository.Models;
using Repository.repository;
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

namespace BadmintonCourtApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DBContext _dbContext;
        private readonly UserRepository _userRepository;
        private readonly CourtRepository _courseRepository;
        public MainWindow()
        {
            InitializeComponent();
            var context = new DBContext();
            _userRepository = new UserRepository(context);
            _courseRepository = new CourtRepository(context);
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            // Placeholder for login authentication logic
            if (_userRepository.Login(username, password)!=null)
            {
                
                MessageBox.Show("Login successful!");
                User u = _userRepository.Login(username, password);
                if (u.Role == "Customer")
                {
                    CustomerHomeScreen customerHomeScreen = new CustomerHomeScreen(_courseRepository);
                    this.Close();
                    customerHomeScreen.ShowDialog();
                }
                else
                {
                    //plaplapla
                }
                // Navigate to appropriate home screen based on user role
                // Example: if (userRole == "Admin") { Open Admin Home Screen }
                // Example: if (userRole == "Customer") { Open Customer Home Screen }
            }
            else
            {
                ErrorMessageTextBlock.Text = "Invalid username or password. Please try again.";
            }
        }

        private void ForgotPasswordLink_Click(object sender, RoutedEventArgs e)
        {
            forgotPasswordWindow forgotPasswordWindow = new forgotPasswordWindow(_userRepository);
            this.Close();

            forgotPasswordWindow.ShowDialog();
            MessageBox.Show("Redirect to forgot password page or functionality.");
        }
        private void RegisterLink_Click(object sender, RoutedEventArgs e)
        {
            registerWindow registerWindow = new registerWindow(_userRepository);
            this.Close();

            registerWindow.ShowDialog();


        }
    }
}