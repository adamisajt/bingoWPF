using System.IO;
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

namespace bingoWPF
{
    public partial class MainWindow : Window
    {
        private TextBox[,] mezok = new TextBox[5, 5];
        private Random rnd = new Random();

        public MainWindow()
        {
            InitializeComponent();
        }

        // kartya gen
        private void btnGeneral_Click(object sender, RoutedEventArgs e)
        {
            // ha mar volt torles
            gridKartyak.Children.Clear();

            mezok = new TextBox[5, 5];

            for (int r = 0; r < 5; r++)
            {
                for (int c = 0; c < 5; c++)
                {
                    TextBox tb = new TextBox();
                    tb.HorizontalContentAlignment = HorizontalAlignment.Center;
                    tb.VerticalContentAlignment = VerticalAlignment.Center;
                    tb.Margin = new Thickness(2);

                    mezok[r, c] = tb;
                    gridKartyak.Children.Add(tb);
                }
            }
            // osz egyedi
            int[][] tartomanyok =
            {
                Enumerable.Range(1, 15).ToArray(),      // 1: 1-15
                Enumerable.Range(16, 15).ToArray(),     // 2: 16-30
                Enumerable.Range(31, 15).ToArray(),     // 3: 31-45
                Enumerable.Range(46, 15).ToArray(),     // 4: 46-60
                Enumerable.Range(61, 15).ToArray()      // 5: 61-75
            };

            for (int c = 0; c < 5; c++)
            {
                List<int> lista = tartomanyok[c].ToList();
                Kever(lista);

                for (int r = 0; r < 5; r++)
                {
                    // J
                    if (r == 2 && c == 2)
                    {
                        mezok[r, c].Text = "X";
                        mezok[r, c].IsReadOnly = true;
                    }
                    else
                    {
                        int ertek = lista[r];
                        mezok[r, c].Text = ertek.ToString();
                    }
                }
            }
        }

        // mentes
        private void btnMentes_Click(object sender, RoutedEventArgs e)
        {
            string fajlNev = txtFajlNev.Text.Trim();
            if (string.IsNullOrEmpty(fajlNev))
            {
                MessageBox.Show("adj meg egy fajlnevet");
                return;
            }

            try
            {
                using (StreamWriter sw = new StreamWriter(fajlNev))
                {
                    for (int r = 0; r < 5; r++)
                    {
                        List<string> sor = new List<string>();
                        for (int c = 0; c < 5; c++)
                        {
                            sor.Add(mezok[r, c].Text);
                        }
                        sw.WriteLine(string.Join(";", sor));
                    }
                }

                MessageBox.Show("mentes kesz.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("hiba mentes kozben: " + ex.Message);
            }
        }
        private void Kever(List<int> lista)
        {
            for (int i = lista.Count - 1; i > 0; i--)
            {
                int j = rnd.Next(i + 1);
                int tmp = lista[i];
                lista[i] = lista[j];
                lista[j] = tmp;
            }
        }
    }
}