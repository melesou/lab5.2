using System;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics.Eventing.Reader;

namespace _5._2_mine
{
    public partial class Form1 : Form
    {
        string currDay = "";
        string currMonth = "";
        string currYear = "";
        public Form1()
        {
            InitializeComponent();
            dataProcessing();
        }

        public void dataProcessing()
        {
            string line;
            string word = "";
            int count = 0;
            int day = 0, year = 0, month = 0;
            FileStream fs1 = new FileStream("Календарь температур.txt", FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs1);
            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] == '.' || line[i] == ':' || i + 1 == line.Length)
                    {
                        if (i + 1 == line.Length)
                            word += line[i];
                        if (count == 0)
                            day = int.Parse(word);
                        if (count == 1)
                            month = int.Parse(word);
                        if (count == 2)
                            year = int.Parse(word);
                        word = "";
                        count++;
                        if (count > 3)
                            count = 0;
                    }
                    else
                        word += line[i];
                }
                monthCalendar1.AddBoldedDate(new DateTime(year, month, day));
            }
            sr.Close();
            fs1.Close();
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            currDay = e.Start.Day.ToString();
            currMonth = e.Start.Month.ToString();
            currYear = e.Start.Year.ToString();
            string line;
            string word = "";
            int count = 0;
            int day = 0, year = 0, month = 0;
            int temp = 0;
            FileStream fs1 = new FileStream("Календарь температур.txt", FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs1);
            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] == '.' || line[i] == ':' || i + 1 == line.Length)
                    {
                        if (i + 1 == line.Length)
                            word += line[i];
                        if (count == 0)
                            day = int.Parse(word);
                        if (count == 1)
                            month = int.Parse(word);
                        if (count == 2)
                            year = int.Parse(word);
                        if (count == 3)
                            temp = int.Parse(word);
                        word = "";
                        count++;
                        if (count > 3)
                            count = 0;
                    }
                    else
                        word += line[i];
                }

                if (int.Parse(e.Start.Day.ToString()) == day && int.Parse(e.Start.Month.ToString()) == month && int.Parse(e.Start.Year.ToString()) == year)
                {
                    label1.Text = "Температура:" + temp;
                    break;
                }
                else
                    label1.Text = "Температура: не указана";

            }
            sr.Close();
            fs1.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            FileStream fs = new FileStream("Календарь температур.txt", FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            if (currDay != "" && currMonth != "" && currYear != "" && textBox1.Text != "")
                sw.WriteLine(currDay + "." + currMonth + "." + currYear + ":" + textBox1.Text);
            else
                MessageBox.Show("Введите дату и температуру");
            sw.Close();
            fs.Close();
            dataProcessing();
            monthCalendar1.Refresh();
        }
    }
}