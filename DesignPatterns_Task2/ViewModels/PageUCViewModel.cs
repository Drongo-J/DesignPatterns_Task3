using DesignPatterns_Task2.Command;
using DesignPatterns_Task2.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DesignPatterns_Task2.ViewModels
{
    public class PageUCViewModel : BaseViewModel
    {
        public RelayCommand SubmitCommand { get; set; }
        public RelayCommand JsonCommand { get; set; }
        public RelayCommand XmlCommand { get; set; }

        private bool jsonChecked = false;

        public bool JsonChecked
        {
            get { return jsonChecked; }
            set { jsonChecked = value; OnPropertyChanged(); }
        }

        private bool xmlChecked = false;

        public bool XmlChecked
        {
            get { return xmlChecked; }
            set { xmlChecked = value; OnPropertyChanged(); }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(); }
        }

        private string surname;

        public string Surname
        {
            get { return surname; }
            set { surname = value; OnPropertyChanged(); }
        }

        private string age;

        public string Age
        {
            get { return age; }
            set { age = value; OnPropertyChanged(); }
        }

        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; OnPropertyChanged(); }
        }

        public PageUCViewModel()
        {
            SubmitCommand = new RelayCommand((s) =>
            {
                if (JsonChecked == false && XmlChecked == false)
                {
                    MessageBox.Show("Check File Type");
                    return;
                }

                if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Surname) || string.IsNullOrEmpty(Age) || string.IsNullOrEmpty(Email))
                {
                    MessageBox.Show("Fill All The Boxes");
                    return;
                }

                User user = new User()
                {
                    Name = Name,
                    Surname = Surname,
                    Age = Age,
                    Email = Email
                };

                if (JsonChecked)
                {
                    XmlChecked = false;
                    var jsonAdapter = new JsonAdapter();
                    App.Converter = new Converter(jsonAdapter);
                    string filename = user.Email + ".json";
                    App.Converter.Write(user, filename);
                    MessageBox.Show($"JSON File Named {filename} created!");
                    Process.Start(filename);
                }
                else if (XmlChecked)
                {
                    JsonChecked = false;
                    var xmlAdapter = new XmlAdapter();
                    App.Converter = new Converter(xmlAdapter);
                    string filename = user.Email + ".xml";
                    App.Converter.Write(user, filename);
                    MessageBox.Show($"XML File Named {filename} created!");
                    Process.Start(filename);
                }
            });

            JsonCommand = new RelayCommand((j) =>
            {
                JsonChecked = true;
                XmlChecked = false;
            });

            XmlCommand = new RelayCommand((j) =>
            {
                XmlChecked = true;
                JsonChecked = false;
            });
        }

        private static readonly Regex rgx = new Regex("[0-9-]+");
        public void IsAllowedInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }
        private static bool IsTextAllowed(string text)
        {
            return rgx.IsMatch(text);
        }
    }
}
