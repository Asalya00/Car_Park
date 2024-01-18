using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace car_park
{
    public partial class Form1 : Form
    {
        DateTime entryTime;
        DateTime currentTime = DateTime.Now;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            entryTime = dateTimePicker1.Value;
            CalculatePayment();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ExitProgram();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void CalculatePayment()
        {
            try
            {

                TimeSpan duration = DateTime.Now - entryTime;
                string vehicleType = radioButton1.Checked ? "Car" : "Truck";
                double payment = CalculatePaymentForVehicle(duration, vehicleType);

                textBox2.Text = $"${payment:F2}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExitProgram()
        {
            Application.Exit();
        }

        private double CalculatePaymentForVehicle(TimeSpan duration, string vehicleType)
        {
            double firstHourRate, additionalHourRate, maxRate;


            if (vehicleType == "Car")
            {
                firstHourRate = 5.00;
                additionalHourRate = 3.00;
                maxRate = 38.00;
            }
            else // Assume "Truck" if not "Car"
            {
                firstHourRate = 6.00;
                additionalHourRate = 3.50;
                maxRate = 44.50;
            }

            double totalHours = duration.TotalHours;
            double payment = 0.0;

            if (totalHours <= 1)
            {
                payment = firstHourRate;
            }
            else if (totalHours > 1 && totalHours <= 24)
            {
                payment = firstHourRate + Math.Ceiling((totalHours - 1) * additionalHourRate);
            }

            payment = Math.Min(payment, maxRate); // Ensure payment doesn't exceed max rate

            return payment;
        }
    }
}
