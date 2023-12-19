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
    /// 

    public partial class MainWindow : Window
    {
        Ceg wayteq = new Ceg("WAYTEQ Europe Kft", "14699517243");
        Ceg miStore = new Ceg("Mi Stores Hungary Kft.", "24792697213");


        public string eleresiUt=null;
        public string text { get; set; } = "default";
        public string text2 { get; set; } = @"c:\";
        public int counter = 0;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

       

        public void btnOpenXLS_Click(object sender, RoutedEventArgs e)
        {
            

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            eleresiUt=openFileDialog.FileName;
            text2 = System.IO.Path.GetDirectoryName(eleresiUt) + @"\xml\";
            txtBoxHashKod.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
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
                nev = "WAYTEQ Europe Kft.";
                
            }
            else
            {
               
                nev = "Mi Stores Hungary Kft.";
                
            }

            if (txtBoxAdoszam.Text != null) aszam = txtBoxAdoszam.Text;
            else aszam = "default";

            Ceg ceg = new Ceg(nev, aszam);

            Fajlmegnyitas fajlmegnyitas = new Fajlmegnyitas();
            List<Matrica> matricas = fajlmegnyitas.XlsMegnyitas(eleresiUt);

            Fajlmentes.XMLMentes(matricas, ceg, text2);
            MessageBox.Show("XML elmentve ide: " + text2);
        }
        private void rb_wayteq(object sender, RoutedEventArgs e)
        {
           
            if(rbWayteq.IsChecked==true)
            {
                text = "14699517243";
      
                if(counter !=0)
                {
                    txtBoxAdoszam.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
                }

                counter++;
            }



        }
        private void rb_mistores(object sender, RoutedEventArgs e)
        {
            if(rbMiStores.IsChecked == true)
            {
                text = "24792697213";
            

                txtBoxAdoszam.GetBindingExpression(TextBox.TextProperty).UpdateTarget();

                counter++;
            }



        }

        private void txtBoxAdoszam_TextChanged(object sender, TextChangedEventArgs e)
        {
            text = txtBoxAdoszam.Text;
        }

        private void txtBoxHashKod_TextChanged(object sender, TextChangedEventArgs e)
        {
            text2 = txtBoxHashKod.Text;
        }
    }
}
