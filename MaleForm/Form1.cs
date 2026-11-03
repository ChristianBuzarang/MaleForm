using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Backprop;

namespace MaleForm
{
    public partial class Form1 : Form
    {
        DataLoader loader = new DataLoader();
        NeuralNet nn;
        bool isModelTrained = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnLoadCsv_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "CSV Files (*.csv)|*.csv";
            
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                loader.LoadAndProcessCsv(dialog.FileName);
                nn = new NeuralNet(2, 8, 1);

                DataTable dt = new DataTable();
                dt.Columns.Add("Country");
                dt.Columns.Add("Region");
                dt.Columns.Add("Erect Length (cm)", typeof(double));
                dt.Columns.Add("Erect Girth (cm)", typeof(double));
                dt.Columns.Add("Volume (cm3)", typeof(double));

                foreach (var item in loader.AllData)
                {
                    dt.Rows.Add(item.Country, item.Region, item.ErectLengthCm, item.ErectGirthCm, item.Volume);
                }

                if (dataGridView1 != null)
                {
                    dataGridView1.DataSource = dt;
                }

                MessageBox.Show($"MaleForm: Loaded {loader.AllData.Count} records successfully!\nRaw data populated in Tab 2.");
            }
        }

        private void btnTrainNN_Click(object sender, EventArgs e)
        {
            if (loader.TrainData.Count == 0)
            {
                MessageBox.Show("Please load the CSV dataset first!");
                return;
            }

            Thread trainThread = new Thread(new ThreadStart(TrainModelRoutine));
            trainThread.Start();
        }

        private void TrainModelRoutine()
        {
            StringBuilder lossLog = new StringBuilder();
            lossLog.AppendLine("Epoch,MeanSquaredError");

            int epochs = 2000;
            for (int epoch = 1; epoch <= epochs; epoch++)
            {
                double totalError = 0;
                foreach (var item in loader.TrainData)
                {
                    nn.setInputs(0, item.NormErectLength);
                    nn.setInputs(1, item.NormErectGirth);
                    
                    double targetVolumeNorm = item.Volume / 300.0;
                    nn.setDesiredOutput(0, targetVolumeNorm);
                    nn.learn();

                    double diff = targetVolumeNorm - nn.getOuputData(0);
                    totalError += diff * diff;
                }
                double mse = totalError / loader.TrainData.Count;

                if (epoch % 100 == 0)
                {
                    string logLine = $"Epoch {epoch}: MSE = {mse:F6}";
                    lossLog.AppendLine($"{epoch},{mse:F6}");

                    this.Invoke((MethodInvoker)delegate
                    {
                        if (rtbResults != null)
                        {
                            rtbResults.AppendText(logLine + Environment.NewLine);
                        }
                    });
                }
            }

            File.WriteAllText("MaleForm_Training_Loss.csv", lossLog.ToString());
            isModelTrained = true;

            this.Invoke((MethodInvoker)delegate
            {
                MessageBox.Show("Training Complete! Training loss saved to 'MaleForm_Training_Loss.csv'.");
            });
        }

        private void btnEvaluateUser_Click(object sender, EventArgs e)
        {
            if (loader.AllData.Count == 0 || !isModelTrained)
            {
                MessageBox.Show("Please load the dataset and train the Neural Network model first.");
                return;
            }

            double userLength = Convert.ToDouble(txtUserLength.Text);
            double userGirth = Convert.ToDouble(txtUserGirth.Text);

            double normLen = loader.ScaleLength(userLength);
            double normGirth = loader.ScaleLength(userGirth);

            nn.setInputs(0, normLen);
            nn.setInputs(1, normGirth);
            nn.run();

            double predictedNormValue = nn.getOuputData(0);
            double estimatedVolume = predictedNormValue * 300.0; 

            var matches = loader.AllData
                .Select(c => new
                {
                    Country = c.Country,
                    Region = c.Region,
                    ErectLength = c.ErectLengthCm,
                    ErectGirth = c.ErectGirthCm,
                    Diff = Math.Abs(c.ErectLengthCm - userLength) + Math.Abs(c.ErectGirthCm - userGirth)
                })
                .OrderBy(m => m.Diff)
                .Take(5)
                .ToList();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("=== MaleForm: GettingBigger & Neural Network Insights ===");
            sb.AppendLine($"Your Input: {userLength} cm Length | {userGirth} cm Girth");
            sb.AppendLine($"Neural Network Volume Prediction Score: {estimatedVolume:F2}");
            sb.AppendLine("\n--- Closest Global Statistical Peers ---");
            foreach (var match in matches)
            {
                sb.AppendLine($"• {match.Country} ({match.Region}) — Length: {match.ErectLength}cm | Girth: {match.ErectGirth}cm");
            }

            sb.AppendLine("\n--- Community Mindset & Health Guidance ---");
            sb.AppendLine("• Focus on Consistency & Health: Cardiovascular wellness, pelvic floor conditioning, and proper conditioning routines matter more than minor metric variations.");
            sb.AppendLine("• Body Neutrality & Confidence: Human biological variance is completely natural. True confidence stems from mental well-being, self-care, and self-acceptance.");
            sb.AppendLine("• Skepticism Note: Beware of unverified commercial gimmicks; rely on data, hygiene, and safe routines.");

            rtbResults.Text = sb.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
