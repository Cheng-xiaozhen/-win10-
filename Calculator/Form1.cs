using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        public enum CalcuType
        {

            //由于英语太菜，乘法和除法单词混了，所以看下面在运算符按钮的源码时，不要被单词 混淆
            None,

            /// <summary>
            /// 加法
            /// </summary>
            Addition,

            /// <summary>
            /// 减法
            /// </summary>
            Substraction,

            /// <summary>
            /// 除法
            /// </summary>
            Multiplication,

            /// <summary>
            /// 乘法
            /// </summary>
            Division,

            /// <summary>
            /// 模除
            /// </summary>
            Mod

           
        }

        private bool _IsFirst = true;
        private bool _IsVisible = false;
        private CalcuType calcuType = CalcuType.None;
        private double num1;
        private double num2;
        private string record = "";

        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btn0.Click += new EventHandler(btnVal_Click);
            btn1.Click += new EventHandler(btnVal_Click);
            btn2.Click += new EventHandler(btnVal_Click);
            btn3.Click += new EventHandler(btnVal_Click);
            btn4.Click += new EventHandler(btnVal_Click);
            btn5.Click += new EventHandler(btnVal_Click);
            btn6.Click += new EventHandler(btnVal_Click);
            btn7.Click += new EventHandler(btnVal_Click);
            btn8.Click += new EventHandler(btnVal_Click);
            btn9.Click += new EventHandler(btnVal_Click);

            panel3.Visible = false;
        }

        /// <summary>
        /// 数字按钮点击方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnVal_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (_IsFirst)
            {             
                string number = this.txtDisplay.Text;
                if (number == "0")
                {
                    number = btn.Text;
                }
                else
                {
                    number += btn.Text;
                }
                this.txtDisplay.Text = number;
            }
           else
            {
                _IsFirst = true;
                txtDisplay.Text = btn.Text;
            }
        }

        /// <summary>
        /// 小数点 点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPoint_Click(object sender, EventArgs e)
        {
            if (txtDisplay.Text.Contains("."))
            {
                return;
            }
            else
            {
                txtDisplay.Text += ".";
            }
        }

        /// <summary>
        /// 取相反数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPlusMinus_Click(object sender, EventArgs e)
        {
            if (txtDisplay.Text.StartsWith("-"))
            {
                txtDisplay.Text = txtDisplay.Text.Substring(1);
            }
            else
            {
                if (txtDisplay.Text.Equals("0"))
                {
                    return;
                }
                txtDisplay.Text = "-" + txtDisplay.Text;
            }
        }

        /// <summary>
        /// 等于 求结果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEquals_Click(object sender, EventArgs e)
        {
            num2 = Convert.ToDouble(txtDisplay.Text);
            num1 = Convert.ToDouble(lblShowOps.Text.Substring(0, lblShowOps.Text.Length - 1));
            lblShowOps.Text = "";
            record += (" "+txtDisplay.Text);
            switch(calcuType)
            {
                case CalcuType.Addition:
                    txtDisplay.Text = (num1 + num2).ToString();
                    break;
                case CalcuType.Substraction:
                    txtDisplay.Text = (num1 - num2).ToString();
                    break;
                case CalcuType.Division:
                    txtDisplay.Text = (num1 * num2).ToString();
                    break;
                case CalcuType.Multiplication:
                    txtDisplay.Text = (num1 / num2).ToString();
                    break;
                case CalcuType.Mod:
                    txtDisplay.Text = ((int)num1 % (int)num2).ToString();
                    break;
                default:
                    break;
            }
            record += " = " + txtDisplay.Text;
            textBox1.Text += record + "\r\n";
            record = "";

            _IsFirst = false;
        }

        /// <summary>
        /// 判断是不是数字
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool IsNumber(string value)
        {
            return Regex.IsMatch(value, @"^-?[0-9]*[.]?[0-9]*$");
        }

        //开方
        private void btnSqrt_Click(object sender, EventArgs e)
        {
            if (IsNumber(txtDisplay.Text))
            {
                double num = Convert.ToDouble(txtDisplay.Text);
                if (num>=0)
                {
                    txtDisplay.Text = Math.Sqrt(num).ToString();
                }
                else
                {
                    lblShowOps.Text = "";
                    txtDisplay.Text = "无效输入";
                }
            }
            else
            {
                txtDisplay.Text = "无效输入";
            }
        }

        //平方
        private void btnSquare_Click(object sender, EventArgs e)
        {
            if (IsNumber(txtDisplay.Text))
            {
                double num = Convert.ToDouble(txtDisplay.Text);
                txtDisplay.Text = Math.Pow(num, 2).ToString();
            }
        }

        //求倒数
        private void btnInverse_Click(object sender, EventArgs e)
        {
            if (IsNumber(txtDisplay.Text))
            {
                if (txtDisplay.Text!="0")
                {
                    double num = Convert.ToDouble(txtDisplay.Text);
                    txtDisplay.Text = (1 / num).ToString();
                }
                else
                {
                    txtDisplay.Text = "无效输入";
                }
            }
        }

        //退位键
        private void btnBackSpace_Click(object sender, EventArgs e)
        {
            if (txtDisplay.TextLength==1)
            {
                txtDisplay.Text = "0";
            }
            else
            {
                txtDisplay.Text = txtDisplay.Text.Substring(0, txtDisplay.TextLength - 1);
            }
        }

        //清除 C
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtDisplay.Text = "0";
            btnEquals.Enabled = true;
            lblShowOps.Text = "";
            calcuType = CalcuType.None;
            _IsFirst = true;
            record = "";
            unLock();
        }

        //CE键
        private void btnCE_Click(object sender, EventArgs e)
        {
            txtDisplay.Text = "0";
        }

        /// <summary>
        /// 乘法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMul_Click(object sender, EventArgs e)
        {
            if (lblShowOps.Text.Length==0)
            {
                calcuType = CalcuType.Division;
                num1 = Convert.ToDouble(txtDisplay.Text);
                lblShowOps.Text = txtDisplay.Text + " x";
                txtDisplay.Text = "0";
                
            }
            else
            {
                num2 = Convert.ToDouble(txtDisplay.Text);
                num1 = Convert.ToDouble(lblShowOps.Text.Substring(0, lblShowOps.Text.Length- 1));
                string result = GetResult(num1, num2);
                lblShowOps.Text = result+" x";
                calcuType = CalcuType.Division;
                txtDisplay.Text = "0";
            }
        }

        /// <summary>
        /// 获取中间值
        /// </summary>
        /// <param name="num1"></param>
        /// <param name="num2"></param>
        /// <returns></returns>
        private string GetResult(double num1,double num2)
        {
            string result="0";
            switch (calcuType)
            {
                case CalcuType.Addition:
                    result = (num1 + num2).ToString();
                    break;
                case CalcuType.Substraction:
                    result = (num1 - num2).ToString();
                    break;
                case CalcuType.Division:
                   result = (num1 * num2).ToString();
                    break;
                case CalcuType.Multiplication:
                    result = (num1 / num2).ToString();
                    break;
                case CalcuType.Mod:
                   result = ((int)num1 % (int)num2).ToString();
                    break;
                default:
                    break;
            }
            return result;
        }

        /// <summary>
        /// 减法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMinus_Click(object sender, EventArgs e)
        {
            if (lblShowOps.Text.Length == 0)
            {
                calcuType = CalcuType.Substraction;
                num1 = Convert.ToDouble(txtDisplay.Text);
                lblShowOps.Text = txtDisplay.Text + " -";
                txtDisplay.Text = "0";

            }
            else
            {
                num2 = Convert.ToDouble(txtDisplay.Text);
                num1 = Convert.ToDouble(lblShowOps.Text.Substring(0, lblShowOps.Text.Length - 1));
                string result = GetResult(num1, num2);
                lblShowOps.Text = result + " -";
                calcuType = CalcuType.Substraction;
                txtDisplay.Text = "0";
            }
        }

        /// <summary>
        /// 加法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (lblShowOps.Text.Length == 0)
            {
                calcuType = CalcuType.Addition;
                num1 = Convert.ToDouble(txtDisplay.Text);
                lblShowOps.Text = txtDisplay.Text + " +";
                txtDisplay.Text = "0";

            }
            else
            {
                num2 = Convert.ToDouble(txtDisplay.Text);
                num1 = Convert.ToDouble(lblShowOps.Text.Substring(0, lblShowOps.Text.Length - 1));
                string result = GetResult(num1, num2);
                lblShowOps.Text = result + " +";
                calcuType = CalcuType.Addition;
                txtDisplay.Text = "0";
            }
        }

        /// <summary>
        /// 除法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDiv_Click(object sender, EventArgs e)
        {
            if (lblShowOps.Text.Length == 0)
            {
                calcuType = CalcuType.Multiplication;
                num1 = Convert.ToDouble(txtDisplay.Text);
                lblShowOps.Text = txtDisplay.Text + " /";
                txtDisplay.Text = "0";

            }
            else
            {
                num2 = Convert.ToDouble(txtDisplay.Text);
                num1 = Convert.ToDouble(lblShowOps.Text.Substring(0, lblShowOps.Text.Length - 1));
                string result = GetResult(num1, num2);
                lblShowOps.Text = result + " /";
                calcuType = CalcuType.Multiplication;
                txtDisplay.Text = "0";
            }
        }

        /// <summary>
        /// 模除取余
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPercent_Click(object sender, EventArgs e)
        {
            if (lblShowOps.Text.Length == 0)
            {
                calcuType = CalcuType.Mod;
                num1 = Convert.ToDouble(txtDisplay.Text);
                lblShowOps.Text = txtDisplay.Text + " %";
                txtDisplay.Text = "0";

            }
            else
            {
                num2 = Convert.ToDouble(txtDisplay.Text);
                num1 = Convert.ToDouble(lblShowOps.Text.Substring(0, lblShowOps.Text.Length - 1));
                string result = GetResult(num1, num2);
                lblShowOps.Text = result + " %";
                calcuType = CalcuType.Mod;
                txtDisplay.Text = "0";
            }
        }

        /// <summary>
        /// 历史记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRecord_Click(object sender, EventArgs e)
        {
            if (!_IsVisible)
            {
                panel3.Visible = true;
                _IsVisible = true;
            }
            else
            {
                panel3.Visible = false;
                _IsVisible = false;
            }
        }

        /// <summary>
        /// 获得label中的记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblShowOps_TextChanged(object sender, EventArgs e)
        {
            if (lblShowOps.Text!="")
            {
                record += lblShowOps.Text;
            }
        }

        private void txtDisplay_TextChanged(object sender, EventArgs e)
        {
            if (!IsNumber(txtDisplay.Text))
            {
                Lock();
            }
        }

        /// <summary>
        /// 锁住按钮
        /// </summary>
        private void Lock()
        {
            btn0.Enabled = false;
            btn1.Enabled = false;
            btn2.Enabled = false;
            btn3.Enabled = false;
            btn4.Enabled = false;
            btn5.Enabled = false;
            btn6.Enabled = false;
            btn7.Enabled = false;
            btn8.Enabled = false;
            btn9.Enabled = false;
            btnPoint.Enabled = false;
            btnMinus.Enabled = false;
            btnAdd.Enabled = false;
            btnInverse.Enabled = false;
            btnDiv.Enabled = false;
            btnPercent.Enabled = false;
            btnCE.Enabled = false;
            btnEquals.Enabled = false;
            btnSqrt.Enabled = false;
            btnSquare.Enabled = false;
            btnBackSpace.Enabled = false;
            btnMul.Enabled = false;
            btnPlusMinus.Enabled = false;
        }

        /// <summary>
        /// 解锁按钮
        /// </summary>
        private void unLock()
        {
            btn0.Enabled = true;
            btn1.Enabled = true;
            btn2.Enabled = true;
            btn3.Enabled = true;
            btn4.Enabled = true;
            btn5.Enabled = true;
            btn6.Enabled = true;
            btn7.Enabled = true;
            btn8.Enabled = true;
            btn9.Enabled = true;
            btnPoint.Enabled = true;
            btnMinus.Enabled = true;
            btnAdd.Enabled = true;
            btnInverse.Enabled = true;
            btnDiv.Enabled = true;
            btnPercent.Enabled = true;
            btnCE.Enabled = true;
            btnEquals.Enabled = true;
            btnSqrt.Enabled = true;
            btnSquare.Enabled = true;
            btnBackSpace.Enabled = true;
            btnMul.Enabled = true;
            btnPlusMinus.Enabled = true;
        }

        /// <summary>
        /// 菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("该功能正在开发");
        }
    }
}
