using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
namespace HackTheBoxEmdeeFive
{
    public partial class MainWindow : Window
    {
        private static string HTBresult; 

        public MainWindow()
        {
            InitializeComponent();
        }

        #region Form load / listbox population
        // This is just simply for a "Load" log <3
        // You can remove this if needed
        private void TopBox()
        {
            Log.Items.Add("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Log.Items.Add("EmdeeFive -> HackTheBox.eu");
            Log.Items.Add("made by Nullcheats <3");
            Log.Items.Add("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TopBox();
        }
        #endregion

        #region GetHTB token process :)
        // Stage 1. Make GET request to server to get response data
        // Stage 2. Use regex to extract text that needs to be "hashed"
        // Stage 3. Hash the string with MD5 (done in MD5.cs) class
        // Stage 4. Make POST request with MD5 hash and validate it
        // Stage 5. Use regex to extract "Success token"
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            HttpRequest req = new HttpRequest();
            HTBresult = null;
            if((Domain.Text != "http://IP:PORT/") || (Domain.Text != null) && (Domain.Text.Contains("http")))
            {
                try
                {
                    Log.Items.Add("--> " + Domain.Text + " <--");
                    string getText = req.Get(Domain.Text).ToString();
                    Log.Items.Add("Attempting to get unhashed unhashed MD5");


                    string pattern = @"[<]*h3 align='center'>[A-Z0-9a-z]{1,30}";
                    Regex myRegex = new Regex(pattern, RegexOptions.IgnoreCase);
                    Match m = myRegex.Match(getText);  
                    if(m.Success)
                    {
                        string unhashed = m.Value.Replace("<h3 align='center'>", "");
                        string hashed = MD5.MD5Hash(unhashed);
                        Log.Items.Add("Located unhashed text -> " + unhashed);
                        Log.Items.Add("Attempting to hash string with MD5 !");
                        Log.Items.Add("MD5 hashed value -> "  + hashed);
                        try
                        {
                            Log.Items.Add("Attempting to POST hash for validation");
                            string checkCode = req.Post(Domain.Text, "hash="+hashed, "application/x-www-form-urlencoded").ToString();


                            string patternCode = @"[>]*HTB[\d \w . {!} _ - & * $]{1,45}";
                            Regex myRegex2 = new Regex(patternCode, RegexOptions.IgnoreCase);
                            Match m2 = myRegex2.Match(checkCode);
                            if(m2.Success)
                            {
                                HTBresult = m2.Value.Replace(">", "");
                                Log.Items.Add("Success HTB code -> " + HTBresult);
                            }
                            else
                            {
                                Log.Items.Add("Failed to get HTB code :( ");
                            }
                        }
                        catch (Exception ex)
                        {
                            Log.Items.Add("Error with POST data" + ex);
                        }

                    }
                    else
                    {
                        Log.Items.Add("Failed to locate unhashed text :( ");
                    }
                }
                catch (Exception ex)
                {
                    Log.Items.Add("Error with initial GET request" + ex);
                }
            }
            else
            {
                MessageBox.Show("Please ensure you have added your domain !" , "Error - domain issue");
            }
        }
        #endregion region


        #region Clipboard setter
        // This simply copies the string "HTBresult" to clipboard if it is not "Null"
        // String will be set if value can be grabbed from start button 
        private void ButtonCopy_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(HTBresult) == true)
            {
                Clipboard.SetText(HTBresult);
                MessageBox.Show("Copied result to clipboard !", "Success");
            }
            else
            {
                Clipboard.SetText("No result yet :)");
            }
        }
        #endregion
    }
}
