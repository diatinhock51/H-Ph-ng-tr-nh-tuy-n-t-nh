using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HPTTT
{
    public partial class Form1 : Form
    {
        int labelLength;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {

        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnCalc_Click_2(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            var numVar = int.Parse(txtNumVar.Text);
            var tmpStr = "X" + numVar.ToString() + " x ";
            labelLength = tmpStr.Length * 2 + 25;
            flowLayoutPanel1.Width = (numVar + 1) * (labelLength + 37);
            flowLayoutPanel1.Height = (numVar + 1) * 20;
            int count = 0;
            for (int i = 0; i < numVar; i++)
            {
                for (int j = 0; j < numVar; j++)
                {
                    Label tmpLabel = new Label();
                    tmpLabel.Text = "X" + (j+1).ToString() + " x ";
                    tmpLabel.Font = new Font("Arial", 10, FontStyle.Regular);
                    tmpLabel.Size = new Size(labelLength, 15);
                    tmpLabel.Padding = new System.Windows.Forms.Padding(0);
                    tmpLabel.Margin = new System.Windows.Forms.Padding(0);
                    ///////                    
                    TextBox tmpTxt = new TextBox();
                    tmpTxt.Text = "0";
                    tmpTxt.BorderStyle = BorderStyle.None;
                    tmpTxt.Size = new Size(30, 15);
                    tmpTxt.TabIndex = count;
                    tmpTxt.Name = i.ToString() + "," + j.ToString();
                    count++;
                    flowLayoutPanel1.Controls.Add(tmpLabel);
                    flowLayoutPanel1.Controls.Add(tmpTxt);
                }
                Label addLabel = new Label();
                addLabel.Text = @" = ";
                addLabel.Font = new Font("Arial", 10, FontStyle.Regular);
                addLabel.Size = new Size(labelLength, 15);
                addLabel.Padding = new System.Windows.Forms.Padding(0);
                addLabel.Margin = new System.Windows.Forms.Padding(0);
                ///////                    
                TextBox addTxt = new TextBox();
                addTxt.BorderStyle = BorderStyle.None;
                addTxt.Text = "0";
                addTxt.Size = new Size(30, 15);
                addTxt.TabIndex = count;
                addTxt.Name = i.ToString() + "," + numVar.ToString();
                count++;
                flowLayoutPanel1.Controls.Add(addLabel);
                flowLayoutPanel1.Controls.Add(addTxt);
            }

        }

        void Calc()
        {
            var numVar = int.Parse(txtNumVar.Text);
            string[,] Kteam = new string[2, 3];
            double[,] matrix = new double[numVar, numVar + 1];
            foreach (var item in flowLayoutPanel1.Controls)
            {
                if (item is TextBox)
                {
                    TextBox tmp = item as TextBox;
                    //int index= tmp.TabIndex;
                    string[] name = new string[2];
                    name = tmp.Name.Split(',');
                    //int a = index / numVar;
                    matrix[Convert.ToInt32(name[0]), Convert.ToInt32(name[1])] = double.Parse(tmp.Text);
                }

            }
            for (int i = 0; i < numVar; i++)
            {
                for (int k = i + 1; k < numVar; k++)
                {
                    if (Math.Abs(matrix[i, i]) < Math.Abs(matrix[k, i]))
                    {
                        for (int j = 0; j <= numVar; j++)
                        {
                            double tmp = matrix[i, j];
                            matrix[i, j] = matrix[k, j];
                            matrix[k, j] = tmp;
                        }
                    }
                }
            }

            for (int i = 0; i < numVar - 1; i++)
            {
                for (int k = i + 1; k < numVar; k++)
                {
                    double t = matrix[k, i] / matrix[i, i];
                    for (int j = 0; j <= numVar; j++)
                    {
                        matrix[k, j] = matrix[k, j] - t * matrix[i, j];
                    }
                }
            }
            double[] result = new double[numVar];
            for (int i = numVar - 1; i >= 0; i--)
            {
                result[i] = matrix[i, numVar];
                for (int j = i + 1; j < numVar; j++)
                {
                    if (j != i)
                    {
                        result[i] = result[i] - matrix[i, j] * result[j];
                    }
                }
                result[i] = result[i] / matrix[i, i];
                result[i] = Math.Round(result[i], 3);               
            }
            string strResult = "Kết quả:  ";
            for (int i = 0; i < numVar; i++)
            {
                strResult += "X" +(i+1).ToString() + " = " + result[i].ToString() + "  ";
            }
            lbResult.Text = strResult;
        }

        private void exitBtn_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Calc();
        }
    }
}
