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
using Leaf.xNet;
using Newtonsoft.Json.Linq;

namespace HTB_code_generator
{
    public partial class MainWindow : Window
    {
        private static string Code;
        private void Start()
        {
            log.Items.Add("~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            log.Items.Add("Made by Nullcheats");
            log.Items.Add("~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        }
        private string Base64(string Encoded)
        {
            byte[] data = Convert.FromBase64String(Encoded);
            return Encoding.UTF8.GetString(data);
        }
        public MainWindow()
        {
            InitializeComponent();
            Start();
        }
        private void Generate_Click(object sender, RoutedEventArgs e)
        {
            HttpRequest req = new HttpRequest();
            Code = null;
            try
            {
                log.Items.Add("Attempting to get encoded token");
                string GetEncoded = req.Post("https://www.hackthebox.eu/api/invite/generate").ToString();
                log.Items.Add("Attempting to parse JSON object 'code'");
                JObject o = JObject.Parse(GetEncoded);
                string Encoded = (o.SelectToken("data.code").ToString());
                log.Items.Add("Grabbed encoded token");
                log.Items.Add(Encoded);
                log.Items.Add("Attempting to decode base64");
                log.Items.Add("Decoded Base64");
                Code = Base64(Encoded);
                log.Items.Add("Code -> "+Code);
            }
            catch (Exception ex)
            {
                log.Items.Add("Error making POST request :(");
            }
        }
        private void copyCode_Click(object sender, RoutedEventArgs e)
        {
            if(Code.Length>0)
            {
                Clipboard.SetText(Code);
            }
            else
            {
                MessageBox.Show("Error code length =< 0", "Error");
            }
        }

    }
}
