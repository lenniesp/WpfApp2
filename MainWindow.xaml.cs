using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.Windows.Forms;
using System.Windows.Media;


namespace VoorbeeldVoorLennard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Haal alle MP3 bestanden uit een mapje. Kan je later instelbaar maken als je dat wilt.

            var MP3 = WpfApp2.Properties.Settings.Default.MP3;
            var path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "liedjes");
            var mp3Files = Directory.GetFiles(path);
                    

            // Voor elk mp3 bestand in de map, doe een ding.
            foreach (var file in mp3Files)
            {

                // Maak een nieuwe button.
                var button = new System.Windows.Controls.Button();

                // Wat moet ie doen als op een button geklikt wordt. De += voegt een zogenaamde “eventhandler” toe. Als er een Click event afgaat, roept het framework alle gekoppelde eventhandlers ook aan.
                button.Click += Button_Click;

                // Stop de filename in een tag. Niet waar het voor bedoeld is, maar het werkt het simpelste.
                button.Tag = file;

                // Zet de tekst die je in het scherm op de button ziet.
                button.Content = System.IO.Path.GetFileName(file);
                button.Background = Brushes.Aqua;
                //button.CornerRadius = 
                // Voeg deze button toe aan het gridje, en ga door met de andere files.
                ContentGridje.Children.Add(button);
                
            }
            Load_MP3();
        }

        public void Load_MP3()
        {
            if (WpfApp2.Properties.Settings.Default.MP3 != "TEST")
            {
                var EigenMP3 = Directory.GetFiles(WpfApp2.Properties.Settings.Default.MP3, "*.mp3");
                foreach (var file in EigenMP3)
                {
                    // Maak een nieuwe button.
                    var button = new System.Windows.Controls.Button();

                    // Wat moet ie doen als op een button geklikt wordt. De += voegt een zogenaamde “eventhandler” toe. Als er een Click event afgaat, roept het framework alle gekoppelde eventhandlers ook aan.
                    button.Click += Button_Click;

                    // Stop de filename in een tag. Niet waar het voor bedoeld is, maar het werkt het simpelste.
                    button.Tag = file;

                    // Zet de tekst die je in het scherm op de button ziet.
                    button.Content = System.IO.Path.GetFileName(file);

                    // Voeg deze button toe aan het gridje, en ga door met de andere files.
                    EigenGridje.Children.Add(button);
                }
            }
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Haal de filename weer van de Tag af. Hiervoor moet je de Sender eerst als button uitkleden, zodat we de tag op kunnen vragen.
            // Tag is in de default button van het verkeerde type, dus die casten we als string.
            var file = (sender as System.Windows.Controls.Button).Tag as string;

            // Stop spelen, zet de source op de file die bij de huidige button hoort, en play.
            MediaDinges.Stop();
            MediaDinges.Source = new Uri(file);
            MediaDinges.Play();
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        
       
        
        private void Import_Click(object sender, RoutedEventArgs e)
         {
                FolderBrowserDialog OBJ = new FolderBrowserDialog();
          if (OBJ.ShowDialog()==  System.Windows.Forms.DialogResult.OK)
            {
                System.Windows.Forms.MessageBox.Show(OBJ.SelectedPath);
                WpfApp2.Properties.Settings.Default.MP3 = OBJ.SelectedPath;
                WpfApp2.Properties.Settings.Default.Save();
                Load_MP3();
            }       
            
         }

        
    }
}