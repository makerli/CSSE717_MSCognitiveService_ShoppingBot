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
using System.Threading;
using Microsoft.CognitiveServices.SpeechRecognition;
using System.Net.Http;
using Newtonsoft.Json;


namespace SaladCo_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int index = 1;
        AutoResetEvent _FinalResponseEvent;
        MicrophoneRecognitionClient _microphoneRecognitionClient;

        public MainWindow()
        {
            InitializeComponent();
            initializeView();
            _FinalResponseEvent = new AutoResetEvent(false);
        }

        public void LoginView()
        {
            this.InitializeComponent();

            ////txtusername.Focus();//聚焦在用户名输入框中
            //                    // 在此点之下插入创建对象所需的代码。
            //ImageBrush b = new ImageBrush();
            //b.ImageSource = new BitmapImage(new Uri("pack://application:,,,/机器人.jpg"));
            //b.Stretch = Stretch.Fill;
            //this.Background = b;
        }

        public void initializeView()
        {
            image1_bubble.Visibility = Visibility.Hidden;
            image1_head.Visibility = Visibility.Hidden;
            textBox1.Visibility = Visibility.Hidden;

            image2_bubble.Visibility = Visibility.Hidden;
            image2_head.Visibility = Visibility.Hidden;
            textBox2.Visibility = Visibility.Hidden;

            image3_bubble.Visibility = Visibility.Hidden;
            image3_head.Visibility = Visibility.Hidden;
            textBox3.Visibility = Visibility.Hidden;

            image4_bubble.Visibility = Visibility.Hidden;
            image4_head.Visibility = Visibility.Hidden;
            textBox4.Visibility = Visibility.Hidden;

            image5_bubble.Visibility = Visibility.Hidden;
            image5_head.Visibility = Visibility.Hidden;
            textBox5.Visibility = Visibility.Hidden;

            image6_bubble.Visibility = Visibility.Hidden;
            image6_head.Visibility = Visibility.Hidden;
            textBox6.Visibility = Visibility.Hidden;
        }


        private void ConvertSpeechToText()
        {
            var speechRecognitionMode = SpeechRecognitionMode.ShortPhrase;
            string language = "zh-cn";
            string subscriptionKey = "507d88bbe0ba4873be5cd2aba2b08dde";
            //string subscriptionKey = ConfigurationSettings.AppSettings["be82707d52a2402aa287e55cb404af4c"].ToString();
            _microphoneRecognitionClient
                = SpeechRecognitionServiceFactory.CreateMicrophoneClient(speechRecognitionMode, language, subscriptionKey);

            _microphoneRecognitionClient.OnPartialResponseReceived += OnPartialResponseReceiveHandler;
            _microphoneRecognitionClient.OnResponseReceived += OnMicShortPhraseResponseReceiveHandeler;
            _microphoneRecognitionClient.StartMicAndRecognition();
        }

        /// <summary>
        /// Speaker has finished speaking. Server connection to server, stop listening and clean up.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMicShortPhraseResponseReceiveHandeler(object sender, SpeechResponseEventArgs e)
        {
            Console.WriteLine(e.PhraseResponse + "" + sender);
            Dispatcher.Invoke((Action)(async () =>
            {
                _FinalResponseEvent.Set();
                _microphoneRecognitionClient.EndMicAndRecognition();
                _microphoneRecognitionClient.Dispose();
                _microphoneRecognitionClient = null;
                button.IsEnabled = true;
                textBox.Background = Brushes.White;
                textBox.Foreground = Brushes.Black;
                switch (index)
                {
                    case 1:
                        textBox1.Text = textBox.Text;
                        textBox1.Text += ("\n");
                        image1_bubble.Visibility = Visibility.Visible;
                        image1_head.Visibility = Visibility.Visible;
                        textBox1.Visibility = Visibility.Visible;

                        textBox2.Text = await MessagesController.getResponse(textBox.Text);
                        textBox2.Text += ("\n");
                        image2_bubble.Visibility = Visibility.Visible;
                        image2_head.Visibility = Visibility.Visible;
                        textBox2.Visibility = Visibility.Visible;

                        index += 2;
                        break;
                    case 3:
                        textBox3.Text = textBox.Text;
                        textBox3.Text += ("\n");
                        image3_bubble.Visibility = Visibility.Visible;
                        image3_head.Visibility = Visibility.Visible;
                        textBox3.Visibility = Visibility.Visible;

                        textBox4.Text = await MessagesController.getResponse(textBox.Text);
                        textBox4.Text += ("\n");
                        image4_bubble.Visibility = Visibility.Visible;
                        image4_head.Visibility = Visibility.Visible;
                        textBox4.Visibility = Visibility.Visible;

                        index += 2;
                        break;
                    case 5:
                        textBox5.Text = textBox.Text;
                        textBox5.Text += ("\n");
                        image5_bubble.Visibility = Visibility.Visible;
                        image5_head.Visibility = Visibility.Visible;
                        textBox5.Visibility = Visibility.Visible;

                        textBox6.Text = await MessagesController.getResponse(textBox.Text);
                        textBox6.Text += ("\n");
                        image6_bubble.Visibility = Visibility.Visible;
                        image6_head.Visibility = Visibility.Visible;
                        textBox6.Visibility = Visibility.Visible;

                        index = 1;

                        break;
                    default:
                        break;
                }
            }));
        }


        /// <summary>
        ///  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnPartialResponseReceiveHandler(object sender, PartialSpeechResponseEventArgs e)
        {
            string result = e.PartialResult;
            Dispatcher.Invoke(() =>
            {
                Console.WriteLine(e.PartialResult);
                textBox.Text = (e.PartialResult);
                //textBox1.Text = (e.PartialResult);
                textBox.Text += ("\n");
                //textBox1.Text += ("\n");
                //switch (index)
                //{
                //    case 1:
                //        textBox1.Text = (e.PartialResult);
                //        textBox1.Text += ("\n");
                //        image1_bubble.Visibility = Visibility.Visible;
                //        image1_head.Visibility = Visibility.Visible;
                //        textBox1.Visibility = Visibility.Visible;
                //        index += 2;
                //        break;
                //    case 3:
                //        textBox3.Text = (e.PartialResult);
                //        textBox3.Text += ("\n");
                //        image3_bubble.Visibility = Visibility.Visible;
                //        image3_head.Visibility = Visibility.Visible;
                //        textBox3.Visibility = Visibility.Visible;
                //        index += 2;
                //        break;
                //    case 5:
                //        textBox5.Text = (e.PartialResult);
                //        textBox5.Text += ("\n");
                //        image5_bubble.Visibility = Visibility.Visible;
                //        image5_head.Visibility = Visibility.Visible;
                //        textBox5.Visibility = Visibility.Visible;
                //        break;
                //    default:
                //        break;
                //}

            });
        }

        private void textBoxOne_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void RecordButton_Click(object sender, RoutedEventArgs e)
        {
            //RecordButton.Content = "Listening......";
            //RecordButton.IsEnabled = false;
            //textBoxOne.Background = Brushes.Green;
            //textBoxOne.Foreground = Brushes.White;
            //ConvertSpeechToText();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            button.IsEnabled = false;
            textBox.Background = Brushes.Green;
            textBox.Foreground = Brushes.White;
            if (index == 1)
            {
                initializeView();
            }
            ConvertSpeechToText();
        }

    }

    public static class Luis_call
    {
        public static async Task<Luis_JSON> GetEntityFromLUIS(string Query)
        {
            Query = Uri.EscapeDataString(Query);
            Luis_JSON Data = new Luis_JSON();
            using (HttpClient client = new HttpClient())
            {
                string RequestURI = "https://api.projectoxford.ai/luis/v1/application?id=038d176f-e645-4b41-86b8-f92b081335d8&subscription-key=6800a40f195e47dcab2af8cf29e97c11&q=" + Query;
                HttpResponseMessage msg = await client.GetAsync(RequestURI);

                if (msg.IsSuccessStatusCode)
                {
                    var JsonDataResponse = await msg.Content.ReadAsStringAsync();
                    Data = JsonConvert.DeserializeObject<Luis_JSON>(JsonDataResponse);
                }
            }
            return Data;
        }
    }

    /// <summary>
    /// 以下三个是返回的Luis_json
    /// </summary>
    public class Luis_JSON
    {
        public string query { get; set; }
        public Intent[] intents { get; set; }
        public Entity[] entities { get; set; }
    }
    public class Intent
    {
        public string intent { get; set; }
        public float score { get; set; }
    }
    public class Entity
    {
        public string entity { get; set; }
        public string type { get; set; }
        public int startIndex { get; set; }
        public int endIndex { get; set; }
        public float score { get; set; }
    }

    public class MessageHandler : IDisposable
    {
        public string query { get; set; }
        public string intent { get; set; }
        public string entity { get; set; }

        public MessageHandler(string query, string intent, string entity)
        {
            this.query = query;
            this.intent = intent;
            this.entity = entity;
        }

        public string handleMessage()
        {
            string response = null;
            switch (intent)
            {
                case "问好":
                    response = $"{entity}哟\n" + "我是导购机器人GOGO , 欢迎为你服务";
                    break;

                case "查询商品位置":
                    if (entity == "0")
                    {
                        response = "抱歉，我还不懂你在讲什么呢";
                    }
                    else
                    {
                        response = searchGoods(entity);
                        MessagesController.EndLocation = entity;
                    }
                    break;

                case "查询路线":
                    if (MessagesController.StartLocation != null && MessagesController.EndLocation != null)
                        try
                        {
                            response = searchPath(MessagesController.StartLocation, MessagesController.EndLocation);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            response = $"{MessagesController.EndLocation}就在你的附近哦，向四周看一看就发现了哈";
                        }
                    //调用searchPath方法算出
                    else if (MessagesController.StartLocation == null && MessagesController.EndLocation != null)
                        response = "我还不知道你现在的位置哦，把你现在的位置告诉我或者扫一下旁边的二维码吧";
                    else if (MessagesController.StartLocation != null && MessagesController.EndLocation == null)
                        response = "我不清楚你要去哪里呢，告诉我你要去哪里吧";
                    else response = "我还不知道你要去哪里也不知道你在什么地方呢，把这些告诉我就带你去哈";
                    break;

                case "确认":
                    MessagesController.EndLocation = null;
                    MessagesController.StartLocation = null;
                    response = "好的，很高兴帮到你";
                    break;

                case "获取位置":
                    MessagesController.StartLocation = entity;
                    //如果最后顾客找到了商品位置要将这个位置信息删掉；
                    response = "ok, 我已经知道了哈";
                    break;
                default:
                    response = "我不懂你的意图是什么呢";
                    break;
            }

            return response;
        }

        public string searchGoods(string productName)
        {
            int location_Code = MessagesController.pro_Dic[productName];
            return $"你要找的{productName}就在第{Y(location_Code) + 1}区域的第{X(location_Code) + 1}货架上哟 ";
        }

        public string searchPath(string StartLoacation, string EndLocation)
        {
            int Start_Code = MessagesController.pro_Dic[StartLoacation];
            int End_Code = MessagesController.pro_Dic[EndLocation];
            int y = Y(Start_Code);
            int x = X(Start_Code);
            int y2 = Y(End_Code);
            int x2 = X(End_Code);
            string near_name = null;
            string middle_name = null;
            //if (MessagesController.array[x,y] != 0)
            //{
            //}
            bool up = (x > x2);
            bool right = (y2 > y);
            if (up && right)  //右上
            {
                if (MessagesController.array[x - 1, y] != 0)   //向上
                {
                    near_name = MessagesController.back_Dic[back_Num(x - 1, y)];
                    middle_name = MessagesController.back_Dic[back_Num(x2, y)];
                    return $"你可以向着{near_name}的方向走{(x - x2) * 50}米，"
                        + $"到达{middle_name}，"
                        + $"然后再向右拐{(y2 - y) * 50}米，"
                        + $"然后你就能见到{EndLocation}了";
                }
                else if (MessagesController.array[x, (y + 1)] != 0)  //向右
                {
                    near_name = MessagesController.back_Dic[back_Num(x, y + 1)];
                    middle_name = MessagesController.back_Dic[back_Num(x, y2)];
                    return $"你可以向着{near_name}的方向走{(y2 - y) * 50}米，"
                        + $"到达{middle_name}，"
                        + $"然后再向左拐{(x - x2) * 50}米，"
                        + $"然后你就能见到{EndLocation}了";
                }
                else return "what?";
            }
            else if ((!up) && (right))  //右下
            {
                if (MessagesController.array[x + 1, y] != 0)   //向下
                {
                    near_name = MessagesController.back_Dic[back_Num(x + 1, y)];
                    middle_name = MessagesController.back_Dic[back_Num(x2, y)];
                    return $"你可以向着{near_name}的方向走{(x2 - x) * 50}米，"
                        + $"到达{middle_name}，"
                        + $"然后再向左拐{(y2 - y) * 50}米，"
                        + $"然后你就能见到{EndLocation}了";
                }
                else if (MessagesController.array[x, (y + 1)] != 0)  //向右
                {
                    near_name = MessagesController.back_Dic[back_Num(x, y + 1)];
                    middle_name = MessagesController.back_Dic[back_Num(x, y2)];
                    return $"你可以向着{near_name}的方向走{(y2 - y) * 50}米，"
                        + $"到达{middle_name}，"
                        + $"然后再向右拐{(x - x2) * 50}米，"
                        + $"然后你就能见到{EndLocation}了";
                }
                else return "what?";
            }
            else if ((!up) && (!right))  //左下
            {
                if (MessagesController.array[x + 1, y] != 0)   //向下
                {
                    near_name = MessagesController.back_Dic[back_Num(x + 1, y)];
                    middle_name = MessagesController.back_Dic[back_Num(x2, y)];
                    return $"你可以向着{near_name}的方向走{(x2 - x) * 50}米，"
                        + $"到达{middle_name}，"
                        + $"然后再向右拐{(y - y2) * 50}米，"
                        + $"然后你就能见到{EndLocation}了";
                }
                else if (MessagesController.array[x, (y + 1)] != 0)  //向左
                {
                    near_name = MessagesController.back_Dic[back_Num(x, y + 1)];
                    middle_name = MessagesController.back_Dic[back_Num(x, y2)];
                    return $"你可以向着{near_name}的方向走{(y - y2) * 50}米，"
                        + $"到达{middle_name}，"
                        + $"然后再向左拐{(x - x2) * 50}米，"
                        + $"然后你就能见到{EndLocation}了";
                }
                else return "what?";
            }
            else if ((up) && (!right))  //左上
            {
                if (MessagesController.array[x - 1, y] != 0)   //向上
                {
                    near_name = MessagesController.back_Dic[back_Num(x - 1, y)];
                    middle_name = MessagesController.back_Dic[back_Num(x2, y)];
                    return $"你可以向着{near_name}的方向走{(x - x2) * 50}米，"
                        + $"到达{middle_name}，"
                        + $"然后再向右拐{(y - y2) * 50}米，"
                        + $"然后你就能见到{EndLocation}了";
                }
                else if (MessagesController.array[x, (y - 1)] != 0)  //向左
                {
                    near_name = MessagesController.back_Dic[back_Num(x, y - 1)];
                    middle_name = MessagesController.back_Dic[back_Num(x, y2)];
                    return $"你可以向着{near_name}的方向走{(y - y2) * 50}米，"
                        + $"到达{middle_name}，"
                        + $"然后再向右拐{(x - x2) * 50}米，"
                        + $"然后你就能见到{EndLocation}了";
                }
                else return "what?";
            }
            else return "what?";
        }

        public int Y(int code) { return code - 6 * ((int)(code / 6)); }

        public int X(int code) { return (int)(code / 6); }

        public int back_Num(int x, int y) { return (x * 6 + y); }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~MessageHandler() {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        void IDisposable.Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }
        #endregion

    }

    public static class MessagesController
    {
        public static Dictionary<string, int> pro_Dic = new Dictionary<string, int>();
        public static Dictionary<int, string> back_Dic = new Dictionary<int, string>();
        public static string StartLocation = null;
        public static string EndLocation = null;
        public static string ProductName = null;
        //public static Luis_JSON JSON_Obj0 = null;

        public static int[,] array =
        {
            {1,1,1,1,0,1 },
            {1,0,0,0,1,1 },
            {1,0,0,0,1,1 },
            {1,1,0,1,1,1 }
        };

        private static int flag = 0;
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// 程序的入口点
        /// </summary>
        //public async Task<HttpResponseMessage> Post( string message)
        //{
        //    Luis_JSON JSON_Obj1 = null;
        //    if (flag == 0)
        //    {
        //        Add_dic();
        //        Add_backDic();
        //        flag = 1;
        //    }
        //    if (activity.Type == Message)
        //    {
        //        //JSON_Obj0 = JSON_Obj1;
        //        try
        //        {
        //            JSON_Obj1 = await Luis_call.GetEntityFromLUIS(message);
        //        }
        //        catch (Exception e)
        //        {
        //            Console.WriteLine(e);
        //        }
        //        string answer_From_handler = null;
        //        try
        //        {
        //            if (JSON_Obj1.entities.Length > 0)
        //            {
        //                using (MessageHandler handler = new MessageHandler(JSON_Obj1.query, JSON_Obj1.intents[0].intent, JSON_Obj1.entities[0].entity))
        //                {
        //                    answer_From_handler = handler.handleMessage();
        //                }
        //            }
        //            else if (JSON_Obj1.entities.Length == 0)
        //            {
        //                using (MessageHandler handler = new MessageHandler(JSON_Obj1.query, JSON_Obj1.intents[0].intent, "0"))
        //                {
        //                    answer_From_handler = handler.handleMessage();
        //                }
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            Console.WriteLine(e);
        //        }
        //        //以下是将回复发送出去的程序块
        //        Activity reply = activity.CreateReply($"{answer_From_handler}");
        //        ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
        //        await connector.Conversations.ReplyToActivityAsync(reply);

        //        //// calculate something for us to return
        //        //int length = (activity.Text ?? string.Empty).Length;
        //        //// return our reply to the user
        //        //Activity reply = activity.CreateReply($"HeyBoy!You sent {activity.Text} which was {length} characters");
        //        ////await connector.Conversations.ReplyToActivityAsync(reply);
        //    }
        //    else
        //    {
        //        ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
        //        await connector.Conversations.ReplyToActivityAsync(HandleSystemMessage(activity));
        //    }
        //    var response = Request.CreateResponse(HttpStatusCode.OK);
        //    return response;
        //}

        public static async Task<string> getResponse(string message)
        {
            Luis_JSON JSON_Obj1 = null;
            if (flag == 0)
            {
                Add_dic();
                Add_backDic();
                flag = 1;
            }
            try
            {
                JSON_Obj1 = await Luis_call.GetEntityFromLUIS(message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            string answer_From_handler = null;
            try
            {
                if (JSON_Obj1.entities.Length > 0)
                {
                    using (MessageHandler handler = new MessageHandler(JSON_Obj1.query, JSON_Obj1.intents[0].intent, JSON_Obj1.entities[0].entity))
                    {
                        answer_From_handler = handler.handleMessage();
                    }
                }
                else if (JSON_Obj1.entities.Length == 0)
                {
                    using (MessageHandler handler = new MessageHandler(JSON_Obj1.query, JSON_Obj1.intents[0].intent, "0"))
                    {
                        answer_From_handler = handler.handleMessage();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return answer_From_handler;
        }

        public static void Add_dic()
        {
            pro_Dic.Add("拖鞋", 0);
            pro_Dic.Add("内衣", 1);
            pro_Dic.Add("零食", 2);
            pro_Dic.Add("酒水", 3);
            pro_Dic.Add("进口食品", 4);
            pro_Dic.Add("母婴用品", 5);
            pro_Dic.Add("沐浴用品", 6);
            pro_Dic.Add("电梯1", 7);
            pro_Dic.Add("天井1", 8);
            pro_Dic.Add("天井2", 9);
            pro_Dic.Add("饮料", 10);
            pro_Dic.Add("奶粉", 11);
            pro_Dic.Add("牙膏", 12);
            pro_Dic.Add("电梯2", 13);
            pro_Dic.Add("天井3", 14);
            pro_Dic.Add("电梯3", 15);
            pro_Dic.Add("肉类", 16);
            pro_Dic.Add("海鲜", 17);
            pro_Dic.Add("清洁用品", 18);
            pro_Dic.Add("纸巾", 19);
            pro_Dic.Add("休息区", 20);
            pro_Dic.Add("家电", 21);
            pro_Dic.Add("水果", 22);
            pro_Dic.Add("蔬菜", 23);
        }

        public static void Add_backDic()
        {
            back_Dic.Add(0, "拖鞋");
            back_Dic.Add(1, "内衣");
            back_Dic.Add(2, "零食");
            back_Dic.Add(3, "酒水");
            back_Dic.Add(4, "进口食品");
            back_Dic.Add(5, "母婴用品");
            back_Dic.Add(6, "沐浴用品");
            back_Dic.Add(7, "电梯1");
            back_Dic.Add(8, "天井1");
            back_Dic.Add(9, "天井2");
            back_Dic.Add(10, "饮料");
            back_Dic.Add(11, "奶粉");
            back_Dic.Add(12, "牙膏");
            back_Dic.Add(13, "电梯2");
            back_Dic.Add(14, "天井3");
            back_Dic.Add(15, "电梯3");
            back_Dic.Add(16, "肉类");
            back_Dic.Add(17, "海鲜");
            back_Dic.Add(18, "清洁用品");
            back_Dic.Add(19, "纸巾");
            back_Dic.Add(20, "休息区");
            back_Dic.Add(21, "家电");
            back_Dic.Add(22, "水果");
            back_Dic.Add(23, "蔬菜");
        }
    }


}
