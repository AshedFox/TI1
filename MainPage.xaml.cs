using Ciphers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace laba1
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        string currentCipher = "RailFenceCipher";

/*        readonly List<string> ciphers = new List<string>
                    { "RailFenceCipher", "ColumnarCipher", "GrilleCipher", "PlayfairCipher" };*/

        public MainPage()
        {
            this.InitializeComponent();
        }


        private void DecryptButton_Click(object sender, RoutedEventArgs e)
        {
            if (InputText.Text != string.Empty && Key.Text != string.Empty)
            {
                switch (currentCipher)
                {
                    case "RailFenceCipher":
                        if (int.TryParse(Key.Text, out int key))
                            ResultText.Text = DecryptRailFence(InputText.Text, key);
                        break;
                    case "ColumnarCipher":
                        ResultText.Text = DecryptColumnar(InputText.Text, Key.Text);
                        break;
                    case "GrilleCipher":
                        ResultText.Text = DecryptGrille(InputText.Text);
                        break;
                    case "PlayfairCipher":
                        ResultText.Text = DecryptPlayfair(InputText.Text, Key.Text);
                        break;
                }
            }
        }

        private void EncryptButton_Click(object sender, RoutedEventArgs e)
        {
            if (InputText.Text != string.Empty && Key.Text != string.Empty)
            {
                switch(currentCipher)
                {
                    case "RailFenceCipher":
                        if (int.TryParse(Key.Text, out int key))
                            ResultText.Text = EncryptRailFence(InputText.Text, key);
                        break;
                    case "ColumnarCipher":
                        ResultText.Text = EncryptColumnar(InputText.Text, Key.Text);
                        break;
                    case "GrilleCipher":
                        ResultText.Text = EncryptGrille(InputText.Text);
                        break;
                    case "PlayfairCipher":
                        ResultText.Text = EncryptPlayfair(InputText.Text, Key.Text);
                        break;
                }
            }
        }

        private void CipherMethod_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton item)
            {
                currentCipher = item.Name;


                if (currentCipher == "GrilleCipher")
                    Key.IsEnabled = false;
                else
                    Key.IsEnabled = true;


                if (currentCipher == "RailFenceCipher")               
                    Key.PlaceholderText = "Высота";               
                else                
                    Key.PlaceholderText = "Ключ";                
            }
        }


        string EncryptRailFence(string input, int key)
        {
            RailFenceCipher cipher = new RailFenceCipher();

            return cipher.Encrypt(input, key);
        }

        string EncryptColumnar(string input, string key)
        {
            ColumnarCipher cipher = new ColumnarCipher();

            return cipher.Encrypt(input, key);
        }

        string EncryptGrille(string input)
        {
            GrilleCipher cipher = new GrilleCipher();

            return cipher.Encrypt(input);
        }

        string EncryptPlayfair(string input, string key)
        {
            PlayfairCipher cipher = new PlayfairCipher();

            return cipher.Encrypt(input, key);
        }        
        
        string DecryptRailFence(string input, int key)
        {
            RailFenceCipher cipher = new RailFenceCipher();

            return cipher.Decrypt(input, key);
        }

        string DecryptColumnar(string input, string key)
        {
            ColumnarCipher cipher = new ColumnarCipher();

            return cipher.Decrypt(input, key);
        }

        string DecryptGrille(string input)
        {
            GrilleCipher cipher = new GrilleCipher();

            return cipher.Decrypt(input);
        }

        string DecryptPlayfair(string input, string key)
        {
            PlayfairCipher cipher = new PlayfairCipher();

            return cipher.Decrypt(input, key);
        }
    }
}
