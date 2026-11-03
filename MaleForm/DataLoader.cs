using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MaleForm
{
    public class DataLoader
    {
        public List<CountryData> AllData { get; private set; } = new List<CountryData>();
        public List<CountryData> TrainData { get; private set; } = new List<CountryData>();
        public List<CountryData> TestData { get; private set; } = new List<CountryData>();

        public double MinLength { get; private set; }
        public double MaxLength { get; private set; }
        public double MinGirth { get; private set; }
        public double MaxGirth { get; private set; }

        public void LoadAndProcessCsv(string filePath)
        {
            AllData.Clear();
            var lines = File.ReadAllLines(filePath);
            if (lines.Length < 2) return;

            var headers = ParseCsvLine(lines[0]);
            int idxCountry = Array.IndexOf(headers, "Country");
            int idxRegion = Array.FindIndex(headers, h => h.Contains("Continent") || h.Contains("Region"));
            int idxErectLen = Array.FindIndex(headers, h => h.Contains("Erect Length (cm)"));
            int idxErectGirth = Array.FindIndex(headers, h => h.Contains("Erect Girth (cm)") && !h.Contains("in"));
            int idxVolume = Array.FindIndex(headers, h => h.Contains("Volume"));

            idxCountry = idxCountry != -1 ? idxCountry : 0;
            idxRegion = idxRegion != -1 ? idxRegion : 1;
            idxErectLen = idxErectLen != -1 ? idxErectLen : 3;
            idxErectGirth = idxErectGirth != -1 ? idxErectGirth : 6;
            idxVolume = idxVolume != -1 ? idxVolume : 8;

            for (int i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i])) continue;

                var parts = ParseCsvLine(lines[i]);

                if (parts.Length > Math.Max(Math.Max(idxErectLen, idxErectGirth), idxVolume) &&
                    double.TryParse(parts[idxErectLen], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double erectLen) &&
                    double.TryParse(parts[idxErectGirth], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double erectGirth) &&
                    double.TryParse(parts[idxVolume], System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double volume))
                {
                    AllData.Add(new CountryData
                    {
                        Country = parts[idxCountry].Trim('"'),
                        Region = parts[idxRegion].Trim('"'),
                        ErectLengthCm = erectLen,
                        ErectGirthCm = erectGirth,
                        Volume = volume
                    });
                }
            }

            if (AllData.Count == 0)
            {
                MessageBox.Show("No valid data rows were loaded. Please check the CSV file path.");
                return;
            }

            MinLength = AllData.Min(d => d.ErectLengthCm);
            MaxLength = AllData.Max(d => d.ErectLengthCm);
            MinGirth = AllData.Min(d => d.ErectGirthCm);
            MaxGirth = AllData.Max(d => d.ErectGirthCm);

            foreach (var item in AllData)
            {
                item.NormErectLength = (item.ErectLengthCm - MinLength) / (MaxLength - MinLength);
                item.NormErectGirth = (item.ErectGirthCm - MinGirth) / (MaxGirth - MinGirth);
            }

            var rnd = new Random(42);
            var shuffled = AllData.OrderBy(x => rnd.Next()).ToList();
            int trainCount = (int)(shuffled.Count * 0.80);

            TrainData = shuffled.Take(trainCount).ToList();
            TestData = shuffled.Skip(trainCount).ToList();
        }

        private string[] ParseCsvLine(string line)
        {
            var result = new List<string>();
            bool inQuotes = false;
            int startIndex = 0;

            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == '"')
                {
                    inQuotes = !inQuotes;
                }
                else if (line[i] == ',' && !inQuotes)
                {
                    result.Add(line.Substring(startIndex, i - startIndex).Trim());
                    startIndex = i + 1;
                }
            }
            result.Add(line.Substring(startIndex).Trim());
            return result.ToArray();
        }

        public double ScaleLength(double lengthCm)
        {
            return Math.Max(0.0, Math.Min(1.0, (lengthCm - MinLength) / (MaxLength - MinLength)));
        }

        public double ScaleGirth(double girthCm)
        {
            return Math.Max(0.0, Math.Min(1.0, (girthCm - MinGirth) / (MaxGirth - MinGirth)));
        }
    }
}
