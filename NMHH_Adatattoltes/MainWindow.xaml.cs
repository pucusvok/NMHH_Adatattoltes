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
using System.IO;
using Microsoft.Win32;


namespace NMHH_Adatattoltes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Ceg wayteq = new Ceg("WAYTEQ Europe Kft", "14699517243");
        Ceg miStore = new Ceg("Mi Stores Hungary Kft.", "24792697213");


        public string eleresiUt=null;

        public MainWindow()
        {
            InitializeComponent();
        }

        public void btnOpenXLS_Click(object sender, RoutedEventArgs e)
        {
            

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            eleresiUt=openFileDialog.FileName;
            lblEleresiUt.Content = "Elerési út: " + eleresiUt;

            Fajlmegnyitas fajlmegnyitas = new Fajlmegnyitas();

            List<Matrica> matricas = fajlmegnyitas.XlsMegnyitas(eleresiUt);

            lblTest.Content = matricas[2].datum.Replace(".", "").Replace("-", "").Replace(" ", "");
            
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            string nev;
            string aszam;

            if (Convert.ToBoolean(rbWayteq.IsChecked))
            {
                nev = "WAYTEQ Europe Kft";
                aszam = "14699517243";
            }
            else
            {
               
                nev = "Mi Stores Hungary Kft.";
                aszam = "24792697213";
            }

            Ceg ceg = new Ceg(nev, aszam);

            Fajlmegnyitas fajlmegnyitas = new Fajlmegnyitas();
            List<Matrica> matricas = fajlmegnyitas.XlsMegnyitas(eleresiUt);

            Fajlmentes.XMLMentes(matricas, txtBoxHashKod.Text, ceg);
            MessageBox.Show("XML elmentve ide: D:\\NMHH\\kimentett_xls\\");
        }
    }
}
