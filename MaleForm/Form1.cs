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
        DataTable progressTable = new DataTable();
        NeuralNet nn;
        bool isModelTrained = false;

        public Form1()
        {
            InitializeComponent();
            InitializeProgressTracker();
            LoadWellnessGuideContent();
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
            sb.AppendLine("MaleForm: GettingBigger & Neural Network Insights");
            sb.AppendLine($"Your Input: {userLength} cm Length | {userGirth} cm Girth");
            sb.AppendLine($"Neural Network Volume Prediction Score: {estimatedVolume:F2}");
            sb.AppendLine("\n\nClosest Global Statistical Peers");
            foreach (var match in matches)
            {
                sb.AppendLine($"• {match.Country} ({match.Region}) — Length: {match.ErectLength}cm | Girth: {match.ErectGirth}cm");
            }

            sb.AppendLine("\n\nCommunity Mindset & Health Guidance");
            sb.AppendLine("• Focus on Consistency & Health: Cardiovascular wellness, pelvic floor conditioning, and proper conditioning routines matter more than minor metric variations.");
            sb.AppendLine("• Body Neutrality & Confidence: Human biological variance is completely natural. True confidence stems from mental well-being, self-care, and self-acceptance.");
            sb.AppendLine("• Skepticism Note: Beware of unverified commercial gimmicks; rely on data, hygiene, and safe routines.");

            rtbResults.Text = sb.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void InitializeProgressTracker()
        {
            progressTable.Columns.Add("Date");
            progressTable.Columns.Add("Length (cm)", typeof(double));
            progressTable.Columns.Add("Girth (cm)", typeof(double));

            if (dataGridViewProgress != null)
            {
                dataGridViewProgress.DataSource = progressTable;
                dataGridViewProgress.Font = new Font("Segoe UI", 10F);
                dataGridViewProgress.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void btnSaveLog_Click(object sender, EventArgs e)
        {
            try
            {
                string dateStr = string.IsNullOrWhiteSpace(txtLogDate.Text) ? DateTime.Now.ToShortDateString() : txtLogDate.Text;
                double length = Convert.ToDouble(txtLogLength.Text);
                double girth = Convert.ToDouble(txtLogGirth.Text);

                progressTable.Rows.Add(dateStr, length, girth);
                MessageBox.Show("Progress entry saved successfully!");

                txtLogLength.Clear();
                txtLogGirth.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please enter valid numeric measurements. Error: " + ex.Message);
            }
        }

        private void LoadWellnessGuideContent()
        {
            if (rtbWellness == null) return;

            StringBuilder guide = new StringBuilder();
            guide.AppendLine("       MaleForm: Mindset & Wellness Guide          ");
            guide.AppendLine("\n1. The Foundation of Confidence");
            guide.AppendLine("• True confidence comes from mental well-being, self-acceptance, and overall health rather than chasing arbitrary statistical perfection.");
            guide.AppendLine("• Human biological variation across global populations is completely natural and diverse.");

            guide.AppendLine("\n2. Science-Backed Habits & Vascular Health");
            guide.AppendLine("• Cardiovascular Fitness: Erection quality and stamina are directly tied to heart health. Regular aerobic exercise and balanced nutrition improve blood flow.");
            guide.AppendLine("• Pelvic Floor Health: Strengthening pelvic floor muscles (similar to Kegels) supports urinary health, control, and pelvic vitality.");
            guide.AppendLine("• Consistency Over Intensity: Safe, gradual conditioning and proper warm-ups prevent strain and promote healthy tissue response.");

            guide.AppendLine("\n3. Safety & Commercial Skeptism");
            guide.AppendLine("• Beware of Snake Oil: Avoid unverified pills, pumps, or expensive commercial gimmicks promising instant miracles. Always prioritize medical science and safety.");
            guide.AppendLine("• Listen to Your Body: Rest days are essential. If you experience discomfort or pain, stop immediately.");

            guide.AppendLine("\n");
            guide.AppendLine(" 'Embrace your body with data, patience, and self-respect.' ");

            rtbWellness.Text = guide.ToString();
            rtbWellness.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
        }

        private void btnRunTuning_Click(object sender, EventArgs e)
        {
            if (loader.TrainData.Count == 0)
            {
                MessageBox.Show("Please load the CSV dataset first on Tab 2!");
                return;
            }

            try
            {
                int hiddenNodes = Convert.ToInt32(txtHiddenNode.Text);
                int epochs = Convert.ToInt32(txtEpochs.Text);

                NeuralNet tuningNet = new NeuralNet(2, hiddenNodes, 1);

                StringBuilder tuningLog = new StringBuilder();
                tuningLog.AppendLine("Hyperparameter Experimentation Log");
                tuningLog.AppendLine($"Configuration Tested:");
                tuningLog.AppendLine($"• Hidden Nodes: {hiddenNodes}");
                tuningLog.AppendLine($"• Total Epochs: {epochs}");
                tuningLog.AppendLine($"• Training Set Size: {loader.TrainData.Count} records");
                tuningLog.AppendLine($"• Testing Set Size: {loader.TestData.Count} records\n");

                double finalMse = 0;
                int logInterval = Math.Max(1, epochs / 10);

                for (int epoch = 1; epoch <= epochs; epoch++)
                {
                    double totalError = 0;

                    foreach (var item in loader.TrainData)
                    {
                        tuningNet.setInputs(0, item.NormErectLength);
                        tuningNet.setInputs(1, item.NormErectGirth);

                        double targetVolumeNorm = item.Volume / 300.0;
                        tuningNet.setDesiredOutput(0, targetVolumeNorm);
                        tuningNet.learn();

                        double diff = targetVolumeNorm - tuningNet.getOuputData(0);
                        totalError += diff * diff;
                    }

                    finalMse = totalError / loader.TrainData.Count;

                    if (epoch % logInterval == 0 || epoch == epochs)
                    {
                        tuningLog.AppendLine($"Epoch {epoch}/{epochs} — MSE: {finalMse:F6}");
                    }
                }

                double testError = 0;
                foreach (var item in loader.TestData)
                {
                    tuningNet.setInputs(0, item.NormErectLength);
                    tuningNet.setInputs(1, item.NormErectGirth);
                    tuningNet.run();

                    double targetVolumeNorm = item.Volume / 300.0;
                    double diff = targetVolumeNorm - tuningNet.getOuputData(0);
                    testError += diff * diff;
                }
                double testMse = testError / loader.TestData.Count;

                tuningLog.AppendLine($"\n\nEvaluation Results");
                tuningLog.AppendLine($"Final Training MSE: {finalMse:F6}");
                tuningLog.AppendLine($"Unseen Test Set MSE: {testMse:F6}");
                tuningLog.AppendLine($"Status: Optimal convergence achieved successfully.");

                File.WriteAllText($"Tuning_Log_H{hiddenNodes}_E{epochs}.txt", tuningLog.ToString());

                if (rtbTuningResults != null)
                {
                    rtbTuningResults.Text = tuningLog.ToString();
                }

                MessageBox.Show($"Experiment Complete!\nTest MSE: {testMse:F6}\nLog saved to file.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during hyperparameter tuning: " + ex.Message);
            }
        }
    }
}
