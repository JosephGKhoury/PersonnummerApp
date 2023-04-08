using System;
using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace P1PersonnummerJoe
{
    public partial class MainWindow : Window
    {
        private int[] Personnummer;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += Window_Loaded;
            TextBox.TextChanged += TextChangedEventHandler;
            ValideraBtn.Click += Button_Click;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ValideraBtn.IsEnabled = false;
        }

        private void TextChangedEventHandler(object sender, TextChangedEventArgs args)
        {
            Personnummer = GetDigits(TextBox.Text);
            ValideraBtn.IsEnabled = Personnummer.Length == 10 || Personnummer.Length == 9;
        }

        void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Personnummer.Length == 10)
            {
                var PersonnummerSumma = PersonnummerProdukt();
                var digitSum = SifrerSumma(PersonnummerSumma);

                if (digitSum % 10 == 0)
                {
                    MessageBox.Show(" Personnumret " + TextBox.Text + " är ett giltigt personnummer");
                }
                else
                {
                    MessageBox.Show("Personnummret " + TextBox.Text + " är INTE ett gilltigt personnummer", "Fel");
                }
            }
            else if (Personnummer.Length == 9)
            {
                var PersonnummerSumma = PersonnummerProdukt();
                var digitSum = SifrerSumma(PersonnummerSumma);

                for (int sistaSifra = 0; sistaSifra < 10; sistaSifra++)
                {
                    if ((digitSum + sistaSifra) % 10 == 0)
                    {
                        MessageBox.Show("Sista siffran av personnummret är " + sistaSifra + ": " + TextBox.Text + sistaSifra);
                        return;
                    }
                }

                MessageBox.Show("Personnummret existerar inte", "Fel");
            }
        }

        private int[] PersonnummerProdukt()
        {
            var PersonnummerSumma = new int[Personnummer.Length];
            for (int i = 0; i < Personnummer.Length; i++)
            {
                PersonnummerSumma[i] = i % 2 == 0 ? Personnummer[i] * 2 : Personnummer[i] * 1;
            }
            return PersonnummerSumma;
        }

        private int SifrerSumma(int[] PersonnummerSumma)
        {
            var digitSum = 0;
            foreach (var digit in PersonnummerSumma)
            {
                digitSum += digit < 10 ? digit : digit - 9;
            }
            return digitSum;
        }

        private int[] GetDigits(string text)
        {
            var ns = new List<int>();
            foreach (System.Text.RegularExpressions.Match match in System.Text.RegularExpressions.Regex.Matches(text, @"\d"))
            {
                ns.Add(int.Parse(match.Value));
            }
            return ns.ToArray();
        }
    }
}
