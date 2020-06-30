using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Text.RegularExpressions;


namespace 진법계산기
{
    public partial class Form1 : Form
    {
        public Form1()
        {

            InitializeComponent();
        }
        string[] Arr = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        string JinC(string Num1, string Num2, int Jin)
        {
            string Re = null;
            int N2Len = Num2.Length, De = 0;
            int N1 = 0, N2 = 0;
            for (int i = 0; i < Num1.Length - N2Len; i++)
            {
                Num2 = "0" + Num2;
            }
            int x;
            for (int i = Num1.Length; i > 0; i--)
            {
                for (x = 0; x < 26; x++)
                {
                    if (Strings.Mid(Num1, i, 1) == Arr[x])
                    {
                        N1 = x + 10;
                        goto N2G;
                    }
                } // Num1 A~
                N1 = Convert.ToInt32(Strings.Mid(Num1, i, 1));
            N2G:
                for (x = 0; x < 26; x++)
                {
                    if (Strings.Mid(Num2, i, 1) == Arr[x])
                    {
                        N2 = x + 10;
                        goto CLG;
                    }
                } // Num2 A~
                N2 = Convert.ToInt32(Strings.Mid(Num2, i, 1));
            CLG:
                if (N1 - N2 - De < 0)
                {
                    string Temp = null;
                    if ((N1 + Jin - De - N2) >= 10)
                    {
                        Temp = Arr[(N1 + Jin - De - N2) - 10];
                    }
                    else
                    {
                        Temp = (N1 + Jin - De - N2).ToString();
                    }
                    Re = Temp + Re;
                    De = 1;
                }
                else
                {
                    string Temp = null;
                    if ((N1 - De - N2) >= 10)
                    {
                        Temp = Arr[(N1 - De - N2) - 10];
                    }
                    else
                    {
                        Temp = (N1 - De - N2).ToString();
                    }
                    Re = Temp + Re;
                    De = 0;
                }
            }
            return Re;
        }


        string JinChagne(string Str, int Jin)
        {
            string Result = null;
            int Status = 0;
            if (Strings.InStr(Str, "-") != 0) // 음수면
            {
                Status = 0; // 0음수
                Str = Strings.Replace(Str, "-", "");
            }
            else
            {
                Status = 1; // 1양수
            }
            Jin = Convert.ToInt32(Strings.Split(Strings.Split(comboBox1.Text, "[")[1], "]")[0]);
            if (Strings.InStr(Str, ".") != 0)
            { // 소수점시
                int Cnt = 0;
                int Front = Convert.ToInt32(Strings.Split(Str, ".")[0]);
                if (Front != 0)
                {
                    for (int i = 0; Front != 0; i++)
                    { // 정수처리
                        if ((Front % Jin) >= 10)
                            Result = Arr[(Front % Jin) - 10] + Result;
                        else
                            Result = (Front % Jin) + Result;

                        Front /= Jin;
                    }
                    Result = Result + ".";
                }
                else
                {
                    Result = Result + "0.";
                }


                double Back = Convert.ToDouble("0." + Strings.Split(Str, ".")[1]);
                for (int i = 0; Back != 0; i++) // 소수처리
                {
                    Back *= Jin;
                    if ((Convert.ToInt32(Back - 0.5) >= 10))
                    {
                        Result = Result + Arr[Convert.ToInt32(Back - 0.499999) - 10];
                    }
                    else
                    {
                        Result = Result + (Convert.ToInt32(Back - 0.499999));
                    }
                    try
                    {
                        Back = Back - (Back - Convert.ToDouble("0." + Regex.Split(Back.ToString().Replace(".", "&"), "&")[1]));
                    }
                    catch
                    {
                        Back -= Convert.ToInt32(Back - 0.499999);
                    }
                    // Back = Back - ();

                    if ((++Cnt >= 40) || Back < 0)
                        break;
                }
            }
            else
            { // 소수점이 아닐시
                int Front = Convert.ToInt32(Strings.Split(Str, ".")[0]);
                for (int i = 0; Front != 0; i++)
                {
                    if ((Front % Jin) >= 10)
                        Result = Result + Arr[(Front % Jin) - 10];
                    else
                        Result = (Front % Jin) + Result;
                    Front /= Jin;
                }
            }

            if (Status == 0)
            { // 음수처리                            
                Str = textBox1.Text;
                if (Strings.InStr(Str, ".") != 0) // 소수점시
                {

                }
                else
                { // 소수가 아닐시
                    int Front = Convert.ToInt32(Strings.Split(Str, ".")[0]);
                    string Temp = Str;
                    Temp = Strings.Replace(Temp, "-", "");
                    int N = JinChagne(Temp,Jin).Length;
                    Result = "-" + JinC(JinChagne(Math.Pow(Jin, N).ToString(), Jin), Result, Jin);
                    string Bosu = null;
                    if (Jin >= 10)
                    {
                        Bosu = Arr[Jin - 11];
                    }
                    else
                    {
                        Bosu = (Jin-1).ToString();
                    }
                    Result = "-" + Strings.Replace(Result, "-0", Bosu);
                }
            }
            return Result;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Text = JinChagne(textBox1.Text, Convert.ToInt32(Strings.Split(Strings.Split(comboBox1.Text,"[")[1],"]")[0]));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 2; i < 37; i++)
            {
                comboBox1.Items.Add("[" + i + "]진법");
                comboBox2.Items.Add("[" + i + "]진법");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string Str = textBox3.Text;
            int Jin = Convert.ToInt32(Strings.Split(Strings.Split(comboBox2.Text, "[")[1], "]")[0]);
            if (Strings.InStr(Str, ".") != 0)
            { // 소수점시
                string Front = Strings.Split(Str, ".")[0]; // 정수처리부분
                double Sum = 0.0;
                for (int i = 0; i < Front.Length; i++)
                {
                    try
                    {
                        Sum += (Math.Pow(Jin, Front.Length - i - 1) * Convert.ToInt32(Strings.Mid(Front, i + 1, 1)));
                    }
                    catch
                    {
                        int x;
                        int Temps = 0;
                        for (x = 0; x < 26; x++)
                        {
                            if (Arr[x] == Strings.Mid(Front, i + 1, 1))
                            {
                                Temps = x + 10;
                                break;
                            }
                        }
                        Sum += (Math.Pow(Jin, Front.Length - i - 1) * Temps);
                    }
                }


                string Back = Strings.Split(Str, ".")[1];
                textBox4.Text = Sum + "";
                for (int i = 0; i < Back.Length; i++)
                {
                    try
                    {
                        Sum += (Math.Pow(Jin, (i + 1) * -1) * Convert.ToInt32(Strings.Mid(Back, i + 1, 1)));
                    }
                    catch
                    {
                        int x;
                        int Temps = 0;
                        for (x = 0; x < 26; x++)
                        {
                            if (Arr[x] == Strings.Mid(Back, i + 1, 1))
                            {
                                Temps = x + 10;
                                break;
                            }
                        }
                        Sum += (Math.Pow(Jin, (i + 1) * -1) * Temps);
                    }
                }
                textBox4.Text = Sum + "";
            }
            else
            { // 소수점 아니면
                double Sum = 0.0;
                for (int i = 0; i < Str.Length; i++)
                {
                    try
                    {
                        Sum += (Math.Pow(Jin, Str.Length - i - 1) * Convert.ToInt32(Strings.Mid(Str, i + 1, 1)));
                    }
                    catch
                    {
                        int x;
                        for (x = 0; x < 36; x++)
                        {
                            if (Strings.Mid(Str, i + 1, 1) == Arr[x])
                            {
                                break;
                            }
                        }

                        Sum += (Math.Pow(Jin, Str.Length - i - 1) * (10 + x));
                    }
                }
                textBox4.Text = Sum + "";
            }

        }
    }
}
