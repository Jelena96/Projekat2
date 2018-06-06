using Contracts;
using LocalControler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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

namespace AssetManagementSistem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {



        public MainWindow()
        {
            InitializeComponent();
            OpenServer();
            OpenServerFromDevice();
        }

        void OpenServer()
        {

            ServiceHost svc = new ServiceHost(typeof(WriteAms));
            NetTcpBinding binding = new NetTcpBinding();
            binding.TransactionFlow = true;

            svc.AddServiceEndpoint(typeof(IWriteAms), binding, new Uri(String.Format("net.tcp://localhost:10100/WriteAms")));
            svc.Open();

        }

        void OpenServerFromDevice()
        {
            ServiceHost svc = new ServiceHost(typeof(WriteAms));
            NetTcpBinding binding = new NetTcpBinding();
            binding.TransactionFlow = true;

            svc.AddServiceEndpoint(typeof(IWriteAms), binding, new Uri(String.Format("net.tcp://localhost:10102/WriteAms")));
            svc.Open();

        }

        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void buttonShowGraph_Click(object sender, RoutedEventArgs e)
        {

            graph.Children.Clear();  //ocistimo sve sto je bilo

            //DateTime? selectedDate = dp.SelectedDate;
            //if (selectedDate.HasValue)
            //{
            //    string formatted = selectedDate.Value.ToString("dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);
            //}

            DateTime help = (DateTime)dp.SelectedDate;
            DateTime help1 = (DateTime)dp1.SelectedDate;

            int timestamp1 = (Int32)(help.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            int timestamp2 = (Int32)(help1.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

            Device d = new Device(Int32.Parse(textBoxGraph.Text), timestamp1, timestamp2);
            int brMjerenja = d.BrMjerenja;
            List<int> mjerenja = d.Measurments;                        // mjerenja ucitana iz log fajla


            double width = graph.Width;   //sirina kanvasa u kom se nalazi graf
            double widthBar = 20; //sirinabara 20
            double mjesto = 30; //u odnosu na pocetak canvasa,dole lijevo //pozicija

            Line x_osa = new Line();                                // crtam x osu
            x_osa.X1 = 30;  //OK 30
            x_osa.X2 = width - 50;  //OK 50
            x_osa.Y1 = 398;    //350
            x_osa.Y2 = 398;   //350  gleda odozgo
            x_osa.Stroke = Brushes.Black; //koje boje je linija
            x_osa.StrokeThickness = 2;
            graph.Children.Add(x_osa);  //dodajemo canvasu children X osu

            Line y_osa = new Line();                                // crtam  y osu
            y_osa.X1 = 30; //OK
            y_osa.X2 = 30;
            y_osa.Y1 = 284;  //398
            y_osa.Y2 = 0;
            y_osa.Stroke = Brushes.Black;
            y_osa.StrokeThickness = 2;
            graph.Children.Add(y_osa);
            //Zavrseno crtanje osa,sada crtamo podoke

            double stepX = (width - 82) / 24;   //OK                  // crtanje podioka na x osi   
            for (int i = 1; i <= 24; i++)                           //24h pa zato 24 podeoka
            {
                Line podiok = new Line();
                podiok.X1 += x_osa.X1 + i * stepX;
                podiok.X2 += x_osa.X1 + i * stepX;
                podiok.Y1 = 400;
                podiok.Y2 = 396;
                podiok.Stroke = Brushes.Black;
                podiok.StrokeThickness = 3;
                graph.Children.Add(podiok);

                TextBlock brojUzPodeok = new TextBlock();           // broj koji pise uz podiok
                brojUzPodeok.Text = i.ToString();
                brojUzPodeok.Foreground = Brushes.Black;
                graph.Children.Add(brojUzPodeok);
                Canvas.SetLeft(brojUzPodeok, podiok.X1); //udaljenost elementa od lijeve strane njegovog roditelja Canvasa
                Canvas.SetBottom(brojUzPodeok, 1);      //udaljenost elementa od dna njegovog roditelja Canvasa
            }

            double stepY = 10;               //OK          // 300 je maksimum, hocu deset podeoka, znaci korak je 30
            for (int i = 1; i <= 30; i++)               //hocu 10 podeoka na Y osi
            {
                Line podiok = new Line();
                podiok.X1 = 33;             //OK za - linijicu podeoka
                podiok.X2 = 28;             //OK za - linijicu podeoka
                podiok.Y1 += y_osa.Y1 - i * stepY; //- posto ide odozgo
                podiok.Y2 += y_osa.Y1 - i * stepY;
                podiok.Stroke = Brushes.Black;
                podiok.StrokeThickness = 3;
                graph.Children.Add(podiok);

                TextBlock broj = new TextBlock();       // broj koji se pise uz podeok
                broj.Text = (i * 10).ToString();        //da prikazuje 10,20,30..
                broj.Foreground = Brushes.Black;
                graph.Children.Add(broj);
                Canvas.SetLeft(broj, 3);
                Canvas.SetTop(broj, podiok.Y1 - 8);
            }

            if (brMjerenja <= 100)   // maskimalno se prikazuje 100 merenja, ako ih ima vise, onda prikazujem poslednjih 35
            {
                foreach (int m in mjerenja)
                {

                    Rectangle bar = new Rectangle();                            // za svako merenje potrebno je napraviti jedan bar
                    bar.Width = widthBar;                                     // definisanje sirine widthBar
                    bar.Height = m;                                          // definisanje visine (-37 da bi se ljepse uklopilo u visinu prozora)

                    // ako je vrijednost izvan opsega bar je ljubicast, ako je dobra onda je bijel

                    bar.Fill = Brushes.Blue;




                    graph.Children.Add(bar);                                    // dodavanje novog djeteta canvasu
                    Canvas.SetBottom(bar, 18);                                   // pozicioniranje na odgovarajuce mjesto na canvasu
                    Canvas.SetLeft(bar, mjesto + 3);
                    mjesto += widthBar + 5;                                 // pomjeranje pozicije za crtanje sledeceg bara
                                                                            //}
                }
            }


        }


        private void buttonShowDetails_Click_1(object sender, RoutedEventArgs e)
        {
            int suma = 0;

            DateTime help3 = (DateTime)dp2.SelectedDate;
            DateTime help4 = (DateTime)dp3.SelectedDate;

            int timestamp3 = (Int32)(help3.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            int timestamp4 = (Int32)(help4.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

            Device d = new Device(Int32.Parse(textBoxDetalji.Text), timestamp3, timestamp4);
            List<int> mjerenja = d.Measurments;

            for (int i = 0; i < mjerenja.Count; i++) //da bi bili sortirani uredjaji 
            {
               textBoxChanges.Text += "Actual state: " + d.ActualState + "| Actual value: " + d.ActualValue  + "| Measurment: " + i + "\n";
                suma += i;
            }

            textBoxSummary.Text += "Suma svih mjerenja uredjaja sa ID-jem " + Int32.Parse(textBoxDetalji.Text) + " je: " + suma;

        }
    }
}
