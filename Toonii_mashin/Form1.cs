using System;
using System.Globalization;
using System.Windows.Forms;

namespace Toonii_mashin
{
    public partial class Form1 : Form
    {
        private double firstNumber = 0;
        private string operation = "";
        private bool newNumber = true;
        private double memory = 0;

        public Form1()
        {
            InitializeComponent();

            txtDisplay.Text = "0";

            // Тоон товчлуурууд
            btn0.Click += Number_Click;
            btn1.Click += Number_Click;
            btn2.Click += Number_Click;
            btn3.Click += Number_Click;
            btn4.Click += Number_Click;
            btn5.Click += Number_Click;
            btn6.Click += Number_Click;
            btn7.Click += Number_Click;
            btn8.Click += Number_Click;
            btn9.Click += Number_Click;

            // Үйлдлийн товчлуурууд
            btnAdd.Click += Operation_Click;
            btnSubtract.Click += Operation_Click;
            btnMultiply.Click += Operation_Click;
            btnDivide.Click += Operation_Click;

            // Бусад товчлуурууд
            btnEquals.Click += Equals_Click;
            btnClear.Click += Clear_Click;
            btnCE.Click += CE_Click;
            btnBackspace.Click += Backspace_Click;
            btnSign.Click += Sign_Click;
            btnSqrt.Click += Sqrt_Click;
            btnPercent.Click += Percent_Click;
            btnReciprocal.Click += Reciprocal_Click;

            // Санах ойн товчлуурууд
            btnMC.Click += MC_Click;
            btnMR.Click += MR_Click;
            btnMS.Click += MS_Click;
            btnMPlus.Click += MPlus_Click;
        }

        // Дэлгэцээс тоо унших
        private double GetDisplayNumber()
        {
            string text = txtDisplay.Text;

            // Windows-ийн тоон формат болон цэгтэй бичлэгийг дэмжинэ
            if (double.TryParse(
                text,
                NumberStyles.Float,
                CultureInfo.CurrentCulture,
                out double number))
            {
                return number;
            }

            if (double.TryParse(
                text,
                NumberStyles.Float,
                CultureInfo.InvariantCulture,
                out number))
            {
                return number;
            }

            MessageBox.Show(
                "Дэлгэц дээрх тоог уншиж чадсангүй.",
                "Алдаа",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);

            return 0;
        }

        // Дэлгэц дээр тоо харуулах
        private void ShowNumber(double number)
        {
            txtDisplay.Text = number.ToString(
                "G15",
                CultureInfo.CurrentCulture);
        }

        // Тоон товчлуур дарах
        private void Number_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            if (newNumber || txtDisplay.Text == "0")
            {
                txtDisplay.Text = btn.Text;
                newNumber = false;
            }
            else
            {
                txtDisplay.Text += btn.Text;
            }
        }

        // Үйлдэл сонгох: +, -, *, /
        private void Operation_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            firstNumber = GetDisplayNumber();
            operation = btn.Text;
            newNumber = true;
        }

        // Тэнцүү товчлуур
        private void Equals_Click(object sender, EventArgs e)
        {
            if (operation == "")
            {
                return;
            }

            double secondNumber = GetDisplayNumber();
            double result;

            switch (operation)
            {
                case "+":
                    result = firstNumber + secondNumber;
                    break;

                case "-":
                case "−":
                    result = firstNumber - secondNumber;
                    break;

                case "*":
                case "×":
                    result = firstNumber * secondNumber;
                    break;

                case "/":
                case "÷":
                    if (secondNumber == 0)
                    {
                        MessageBox.Show(
                            "Тэгээр хувааж болохгүй!",
                            "Алдаа",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);

                        return;
                    }

                    result = firstNumber / secondNumber;
                    break;

                default:
                    return;
            }

            ShowNumber(result);

            firstNumber = result;
            operation = "";
            newNumber = true;
        }

        // C - бүхнийг цэвэрлэх
        private void Clear_Click(object sender, EventArgs e)
        {
            txtDisplay.Text = "0";
            firstNumber = 0;
            operation = "";
            newNumber = true;
        }

        // CE - одоогийн тоог цэвэрлэх
        private void CE_Click(object sender, EventArgs e)
        {
            txtDisplay.Text = "0";
            newNumber = true;
        }

        // Backspace - сүүлийн цифрийг арилгах
        private void Backspace_Click(object sender, EventArgs e)
        {
            if (newNumber)
            {
                return;
            }

            if (txtDisplay.Text.Length > 1)
            {
                txtDisplay.Text = txtDisplay.Text.Substring(
                    0,
                    txtDisplay.Text.Length - 1);

                if (txtDisplay.Text == "-")
                {
                    txtDisplay.Text = "0";
                }
            }
            else
            {
                txtDisplay.Text = "0";
            }
        }

        // +/- - эерэг, сөрөг тэмдэг солих
        private void Sign_Click(object sender, EventArgs e)
        {
            double number = GetDisplayNumber();

            ShowNumber(-number);
        }

        // √ - квадрат язгуур
        private void Sqrt_Click(object sender, EventArgs e)
        {
            double number = GetDisplayNumber();

            if (number < 0)
            {
                MessageBox.Show(
                    "Сөрөг тооны квадрат язгуур бодит тоон дээр байхгүй.",
                    "Алдаа",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            ShowNumber(Math.Sqrt(number));
            newNumber = true;
        }

        // % - хувиар илэрхийлэх
        private void Percent_Click(object sender, EventArgs e)
        {
            double number = GetDisplayNumber();

            ShowNumber(number / 100);
            newNumber = true;
        }

        // 1/x - урвуу тоо
        private void Reciprocal_Click(object sender, EventArgs e)
        {
            double number = GetDisplayNumber();

            if (number == 0)
            {
                MessageBox.Show(
                    "Тэгийн урвуу тоо тодорхойгүй.",
                    "Алдаа",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            ShowNumber(1 / number);
            newNumber = true;
        }

        // MC - санах ойг цэвэрлэх
        private void MC_Click(object sender, EventArgs e)
        {
            memory = 0;
        }

        // MR - санах ойгоос тоо авах
        private void MR_Click(object sender, EventArgs e)
        {
            ShowNumber(memory);
            newNumber = true;
        }

        // MS - тоог санах ойд хадгалах
        private void MS_Click(object sender, EventArgs e)
        {
            memory = GetDisplayNumber();
        }

        // M+ - дэлгэц дээрх тоог санах ойд нэмэх
        private void MPlus_Click(object sender, EventArgs e)
        {
            memory += GetDisplayNumber();
        }
    }
}