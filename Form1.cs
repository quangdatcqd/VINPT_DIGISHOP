using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace VNPT_LIST
{
    public partial class Form1 : Form
    {
        private static readonly object writeDataLock = new object();
        private static bool runningShop = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGetShop_Click(object sender, EventArgs e)
        {
            new Thread(() =>
            {
                if (!runningShop)
                {
                    statusApp("Đang chạy...");
                    Invoke(new Action(() =>
                    {
                        runningShop = true;
                        btnGetShop.Text = "Stop";
                    }));
                    startApp("shop");
                }
                else
                {
                    Invoke(new Action(() =>
                    {
                        runningShop = false;
                        btnGetShop.Text = "Start";
                        btnGetShop.Visible = false;
                        statusApp("Đang dừng, hãy đợi hoàn tất!");
                    }));
                } 
              
            }).Start();
        }
         
        private void getDigishop_Click(object sender, EventArgs e)
        {
            new Thread(() =>
            {
                if (!runningShop)
                {
                    statusApp("Đang chạy...");
                    Invoke(new Action(() =>
                    {
                        runningShop = true;
                        getDigishop.Text = "Stop";
                    }));
                    startApp("digishop");
                }
                else
                {
                    Invoke(new Action(() =>
                    {
                        runningShop = false;
                        getDigishop.Text = "Start";
                        getDigishop.Visible = false;
                        statusApp("Đang dừng, hãy đợi hoàn tất!");
                    }));
                }

            }).Start();
        }

        public void startApp(string type)
        {
           
               
                try
                { 
                DateTime now = DateTime.Now;
                string formattedDateTime = now.ToString("yyyy_MM_dd_HH_mm_ss");
                string path1 = @"data\digishop_vnpt_vn" + formattedDateTime + ".txt" ;
                string path2 = @"data\shop_vnpt_vn" + formattedDateTime + ".txt" ;
                string[] prefixes = { "85", "84", "83", "82", "81", "88", "94", "91" };

                    //Thread[] threads = new Thread[100];
                    int startPrefix = 0;
                   
                    if (txtPrefix.Text != "")
                    {
                        for (int i = 0; i < prefixes.Length; i++)
                        {
                            if (prefixes[i] == txtPrefix.Text)
                            {
                                startPrefix = i;
                                break;
                            }
                        }  
                    }

                    int startIndex = 0;
                    if (txtIndex.Text != "") startIndex = Convert.ToInt32(txtIndex.Text);
                    for (int pr = startPrefix; pr < prefixes.Length; pr++)
                    {
                        string prefix = prefixes[pr];
                        if (!runningShop) break;
                        for (int i = startIndex; i <= 9999; i++)
                        {
                            string search = i.ToString();
                            if (search.Length <= 1) search  = "000"+ search;
                            if (search.Length <= 2) search  = "00" + search;
                            if (search.Length <= 3) search  = "0" + search;


                            //threads[i] = new Thread(() =>
                            //{
                            if (!runningShop) break;
                            try
                            {
                                string phoneStringList = "";
                               
                                if (type == "digishop")
                                { 
                                    string data = GetDigishop("84" + prefix, search);
                                    dynamic jsonData = JsonConvert.DeserializeObject(data);
                                    if (jsonData.message == "success" && jsonData.data.Count >0)
                                    {
                                        foreach (var item in jsonData.data)
                                        {
                                        
                                            phoneStringList += item.so_tb + Environment.NewLine;
                                        }

                                        writeData(path1, phoneStringList );
                                    }
                                }
                                if (type == "shop")
                                {
                                    string CookieShop = GetCookieShop();

                                statusApp("Lấy đầu số " + prefix + " | q: " + search);
                                string data = "";
                                    for (int count = 0; count <= 10; count++)
                                    {
                                        if (!runningShop) break;
                                        data = GetShop("84" + prefix, search, CookieShop);
                                           
                                        if (data.Contains("Hiển thị"))
                                        {
                                            //statusApp("Có dữ liệu!");

                                            for (int page = 1; page <= 5; page++)
                                            {
                                            reGetPage:
                                                
                                                string stringdata = GetShopPage(page.ToString(), "84" + prefix, search, CookieShop);
                                                if (stringdata == null) goto reGetPage;
                                                var matches = Regex.Matches(stringdata, ">(\\d{10})<");

                                                foreach (dynamic match in matches)
                                                {
                                                    phoneStringList += match.Groups[1].Value + Environment.NewLine;
                                                }
                                            }


                                            break;
                                        }

                                        if (count >= 10)
                                        {
                                            break;
                                        }
                                    }
                                writeData(path2, phoneStringList);
                                }
                            }catch(Exception ex)
                            {
                                statusApp(ex.Message);
                            }

                            Invoke(new Action(() =>
                            { 
                                lbStt.Text = "Đầu: "+ prefix.ToString() + " | Thứ tự: " + search.ToString();
                                txtPrefix.Text = prefix.ToString();
                                txtIndex.Text = i.ToString();
                            }));
                            File.WriteAllText(@"data\settings.txt", prefix.ToString() + "|" + i.ToString());
                            //});
                            //threads[i].Start();

                            //if (i >= 50)
                            //{
                            //    // Đợi cho tất cả các luồng kết thúc
                            //    for (int j = 0; j <= 49; j++)
                            //    {
                            //        threads[j].Join();
                            //    } 
                            //    statusApp("50 cái đầu: " + prefix.ToString());
                            //}
                        }
                        // Đợi cho tất cả các luồng kết thúc
                        //for (int i = 0; i < threads.Length; i++)
                        //{
                        //    threads[i].Join();

                        //}
                        //statusApp("Xong đầu: " + prefix.ToString()); ;
                    }
                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            Invoke(new Action(() =>
            {
                getDigishop.Visible = true; 
            }));
            statusApp("Đã dừng ứng dụng!");
            statusApp("Chạy xong hết rồi!");

        }

        void writeData(string path,string data)
        {
            lock(writeDataLock){ 
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(data);
                } 
            }
        }
        void statusApp(string text)
        {
            Invoke(new Action(() =>
            {
                txtStatus.Text = "=> " + text + Environment.NewLine + txtStatus.Text;
            }));
        }
        public string  GetDigishop(string prefix, string search )
        { 
            try
            {
                var options = new RestClientOptions("https://digishop.vnpt.vn")
                {
                    MaxTimeout = -1,
                    UserAgent = " Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/119.0.0.0 Safari/537.36",
                };
                var client = new RestClient(options);
                var request = new RestRequest($"/apiprod/sim/num_search2?prefix={prefix}&search=*{search}&commit=0", Method.Get);
                request.AddHeader("Accept", " application/json, text/plain, */*");
                request.AddHeader("Accept-Encoding", " gzip, deflate, br");
                request.AddHeader("Accept-Language", " vi-VN,vi;q=0.9,fr-FR;q=0.8,fr;q=0.7,en-US;q=0.6,en;q=0.5");
                request.AddHeader("Authorization", " Bearer");
                request.AddHeader("Connection", " keep-alive");
                request.AddHeader("Cookie", " _gcl_au=1.1.801498801.1694083210; _tt_enable_cookie=1; _ttp=-BsO6pddJOqMLEwj4pStlLU8_dE; _fbp=fb.1.1694083210971.521227054; _hjSessionUser_1160287=eyJpZCI6ImYxM2JlZWRmLWNkZGEtNWIxYS04MGE2LWUxNzYxNzkyY2VmZiIsImNyZWF0ZWQiOjE2OTQwODMyMTA1ODIsImV4aXN0aW5nIjp0cnVlfQ==; _hjIncludedInSessionSample_1160287=1; _gid=GA1.2.1154090031.1701850496; clientId=a16872880134d0669d920850144e2de6abd7f1c0d9fff1eb05a11281f411e6c3; BX_USER_ID=2ffc6b7ddb223f4fa6c7f4d1b194877f; _ga_BFVVXDGX3E=GS1.1.1701851899.1.1.1701852277.0.0.0; _ga_TFG0BKYWYY=GS1.1.1701850496.2.1.1701852930.51.0.0; _ga=GA1.1.1449990052.1694083210; _ga_F9HL65897E=GS1.1.1701850496.2.1.1701853078.0.0.0; _ga_E81TGK6000=GS1.1.1701850496.2.1.1701853078.0.0.0; _ga_PN88XMGHZE=GS1.1.1701850496.2.1.1701853078.60.0.0; secureKey=eyJhbGciOiJSUzI1NiJ9.eyJyZXF1ZXN0VGltZSI6IjIwMjMxMjA2MTY0MjEzIiwiY2xpZW50SWQiOiJhMTY4NzI4ODAxMzRkMDY2OWQ5MjA4NTAxNDRlMmRlNmFiZDdmMWMwZDlmZmYxZWIwNWExMTI4MWY0MTFlNmMzIiwib3MiOiJNb3ppbGxhLzUuMCAoV2luZG93cyBOVCAxMC4wOyBXaW42NDsgeDY0KSBBcHBsZVdlYktpdC81MzcuMzYgKEtIVE1MLCBsaWtlIEdlY2tvKSBDaHJvbWUvMTE5LjAuMC4wIFNhZmFyaS81MzcuMzYiLCJqdGkiOiIyZjJkZDMwYy02MmY3LTRjODktYTkwMC0zNzlhYTFiOWM4NjEiLCJpYXQiOjE3MDE4NTIxMzMsImV4cCI6MTcwMTg1NjMzM30.ZjfMI3_EOIg4p1ScZ5khHrXuj-5ebnw79N8nULFqE747B-NGz2XC73w33sLFxRRArfZakCLIHKlO8UZCt9PHC1K_MiqPs7jlsKFyVTL97xIlnSoOHU8oWjXmCDDKl7I1O3b77_wmCUANlIuiN1Lb3LPZnGKR3Ah4gw3Fjjl7IiIv8cuZk2J840cXFCtYuiDHOE2Ya66XRD5ZUcaDaFODADrgeyNm1dha0mOyUPRQ0ePtU_WnqfOYmd2YRMuAw1QZMgInlKce5gW_30CgDU_XetBciGjl8QkU9MsInTvHQpnJoTMQn_8IlW7FOO-NdvgoxksyoYNHXIBy7G7CLhX7ZKUa0eXPJsePyQ728kVDYYLimlB00_IxiTchjrKn8mf6hd2kacPZNiIiwvx-Opat4nxfb81O1XV44uzs6Wb01TcjDvTDFAFQElT_vuNR5veqivM4mxyN2H4gyaL5yXz20n0vLY_xh__HZCuv6bPqeEa9RcR5v4vHiB6T55a1JEYtkGQz8qJu-hUwQ2lvvdrvXa4pQdO5nqkpHls5ninawpNwDlDhxzJff-XZ5vJRHJ5jQ0yD24BVsVak6Atp6l_YVm9nwGBlUMxdfG4vQumTsAhTrfz7wqN83ZQZyhrPZ_lpirhvFB5b6cNoAu32kJg5emOzeWt3vZpLCaUFH8kSE9o");
                
                request.AddHeader("Sec-Fetch-Dest", " empty");
                request.AddHeader("Sec-Fetch-Mode", " cors");
                request.AddHeader("Sec-Fetch-Site", " same-origin");
                request.AddHeader("clientId", " a16872880134d0669d920850144e2de6abd7f1c0d9fff1eb05a11281f411e6c3");
                request.AddHeader("os", " Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/119.0.0.0 Safari/537.36");
                request.AddHeader("sec-ch-ua", " \"Google Chrome\";v=\"119\", \"Chromium\";v=\"119\", \"Not?A_Brand\";v=\"24\"");
                request.AddHeader("sec-ch-ua-mobile", " ?0");
                request.AddHeader("sec-ch-ua-platform", " \"Windows\"");
                request.AddHeader("secureKey", "eyJhbGciOiJSUzI1NiJ9.eyJyZXF1ZXN0VGltZSI6IjIwMjMxMjA2MTY0MjEzIiwiY2xpZW50SWQiOiJhMTY4NzI4ODAxMzRkMDY2OWQ5MjA4NTAxNDRlMmRlNmFiZDdmMWMwZDlmZmYxZWIwNWExMTI4MWY0MTFlNmMzIiwib3MiOiJNb3ppbGxhLzUuMCAoV2luZG93cyBOVCAxMC4wOyBXaW42NDsgeDY0KSBBcHBsZVdlYktpdC81MzcuMzYgKEtIVE1MLCBsaWtlIEdlY2tvKSBDaHJvbWUvMTE5LjAuMC4wIFNhZmFyaS81MzcuMzYiLCJqdGkiOiI4MjEyMGEzZS1lYWFmLTQ5MDEtOWIyNS02MWQ0MGZlMWNkODciLCJpYXQiOjE3MDE4NTI2MjcsImV4cCI6MTcwMTg1NjgyN30.BNn3rPDRvP6lVO-RTsrzJOARwdVti1jt1S2cjePsRTVZbvoLoPUJEcKZ0ULYhz-XAbAG8UZY47-COaK_ZqPrMUDNcT_hZs0Cmbi8bOEBc-CXBZ6Qxn9c9SYc5kLD7OMWYt58DHBoucWBMoUH9ekdm4lG9VI4oNQqkP3EgNh258H6qwg_WQ5d_ZxKdxiYX84uP4qgOXyAoNvin02EPEMoYnAcnhyLW_BQQc5C-jXhYa1ZhsV0iYd7njHTUwmHUlG7N1THFnBbGfA7GEIXYLUMHdYDaIgBHqFBPVlgp6bdEovKLCR_LzQ31Dmd1JPZM43gzOf1q_L2ctcvfKXI0L3JWOgOADRC6kFViBJmtA_fbiQBT8Hz_kR3rFJpbd0TeK0TlEyjttp3O93_TzR5VkmtNHoZhnc2JqDBjYXXGI2IAgbwJbgrbtVJ9CqNU1PAdT9_UVTyrNpILvkyJKIj8_-hyl8yGLArnYvMzje8JXn-3YexZHaOBBaf4PLZ1VchweiEePfW0y_dM_LYmG_uSUUo7dxuBfcgNutq0iVEnAHsNp9pzgVJFutfF04z3PnKTgboBLbCoqokcWXdnrIJ67IM1enFJw5hACXJUKoCMZjGIZpeXvsZBKVJHTmdsWm7-wQkpSA0vRiRsUFm9_2z4XzSIa-BZTHEPpK-7xNiyMxArKw");
           RestResponse response =   client.Execute(request); 
                return response.Content;
            }
            catch (Exception ex)
            {
                return "";
            } 
        }

        public string GetShop(string prefix, string search,string cookie)
        {
        reget:
            try
            {
                string[] cookies = cookie.Split('|');
                var options = new RestClientOptions("https://shop.vnpt.vn")
                {
                    MaxTimeout = -1,
                    UserAgent = " Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/119.0.0.0 Safari/537.36",
                };
                var client = new RestClient(options);
                var request = new RestRequest("/sim/searchAjax.html", Method.Post);
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddHeader("Cookie", cookies[0]);
                request.AddParameter("YII_CSRF_TOKEN", cookies[1]);
                request.AddParameter("SearchForm[msisdn_type]", "");
                request.AddParameter("SearchForm[msisdn_type]", "1");
                request.AddParameter("SearchForm[prefix_msisdn]", "");
                request.AddParameter("SearchForm[prefix_msisdn]", prefix);
                request.AddParameter("SearchForm[commitment_fee]", "");
                request.AddParameter("SearchForm[commitment_fee]", "");
                request.AddParameter("SearchForm[source]", "toanquoc");
                request.AddParameter("SearchForm[package_kit]", "");
                request.AddParameter("SearchForm[suffix_msisdn]", "*"+ search);
                  RestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() != "OK") goto reget;
                return response.Content;
            }
            catch (Exception ex)
            {
                statusApp(ex.Message);
                goto reget;
            }

        }
        public string GetShopPage(string page, string prefix, string search,string cookie)
        {
            reget:
            try
            {
                string[] cookies = cookie.Split('|');
                var options = new RestClientOptions("https://shop.vnpt.vn")
                {
                    MaxTimeout = -1,
                    UserAgent = " Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/119.0.0.0 Safari/537.36",
                };
                var client = new RestClient(options);
                var request = new RestRequest($"/sim/searchajax.html?ajax=msisdn-grid&captcha=&commitment_fee=&commitment_month=&msisdn_status=&msisdn_type=1&package_kit=&page={page}&prefix_msisdn=${prefix}&source=toanquoc&stock_id=&suffix_msisdn=*{search}", Method.Get);
             
                request.AddHeader("Cookie", cookies[0]);
                request.AddHeader("Accept", " text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");

                RestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() != "OK") goto reget;
                return response.Content;
            }
            catch (Exception ex)
            {
                statusApp(ex.Message);
                goto reget;
            }
        }

        string GetCookieShop()
        {
            reget:
            string data = "";
            try
            {
                var options = new RestClientOptions("https://shop.vnpt.vn")
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest("/sim-so.html?source=", Method.Get);
                RestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() != "OK") goto reget;
                string code = response.StatusCode.ToString();
                string cookie = "";
                foreach (var header in response.Headers)
                {
                    string hd = header.ToString();
                    if (hd.Contains("Set-Cookie"))
                    {
                        // Tìm vị trí của dấu bằng(=)
                        int equalsIndex = hd.IndexOf("="); 
                        // Tìm vị trí của dấu chấm phẩy (;) sau giá trị "Set-Cookie"
                        int semicolonIndex = hd.IndexOf(";"); 
                        // Lấy phần tử giữa dấu bằng (=) và dấu chấm phẩy (;)
                        string setCookieValue = hd.Substring(equalsIndex + 1, semicolonIndex - equalsIndex); 
                        cookie += setCookieValue + " ";

                    }
                }
                data  += cookie + "|";
                string pattern = "value=\"(.*?)\" name=\"YII_CSRF_TOKEN\"";  // Mẫu regex để tìm giá trị trong cặp dấu ngoặc kép 
                Match match = Regex.Match(response.Content, pattern); 
                if (match.Success)
                {
                    data += match.Groups[1].Value;
                }
                return data;
            }
            catch(Exception ex)
            { 
                statusApp(ex.Message); 
                goto reget; 
            }
            
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
            Application.Exit();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void btnData_Click(object sender, EventArgs e)
        {
            new Thread(() => { 
                string folderPath = @"data";
                Process.Start(folderPath);
            }).Start();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            string currentPath = Directory.GetCurrentDirectory();
            string folderPath = Path.Combine(currentPath, "data");  
            Directory.CreateDirectory(folderPath);
            string pathSetting = @"data\settings.txt";
            if (File.Exists(pathSetting))
            {
                string setting = File.ReadAllText(pathSetting).ToString();
                string[]  settings = setting.Split('|');
                Invoke(new Action(() =>
                {
                    txtPrefix.Text = settings[0].ToString();
                    txtIndex.Text = settings[1].ToString();
                    lbStt.Text = "Tiếp tục từ : " + settings[0].ToString() + " | q: " + settings[1].ToString();
                }));
            }

        }
    }
}
