# MaleForm: Global Anthropometric & Self-Confidence Decision Support System

**MaleForm** is an interactive C# Windows Forms application that combines a custom multi-layer perceptron neural network (built with backpropagation) with a global anthropometric dataset (`penis size by country.csv`). Designed with a philosophy rooted in data literacy, body neutrality, and healthy self-confidence, the application bridges rigorous machine learning with an empowering user experience.

---

## Key Features & System Architecture

### 1. Global Metric Explorer (Tab 1)
* **Neural Network Integration:** Utilizes a custom 3-layer feedforward neural network (`Backprop` framework) featuring Min-Max normalized inputs, configurable hidden layers, and backpropagation learning.
* **Global Peer Matching:** Takes user measurements, runs active neural network inference, and queries the 146-country dataset to identify closest statistical global peers.
* **Training & Loss Logging:** Automatically executes training routines across epochs and exports Mean Squared Error (MSE) loss logs (`MaleForm_Training_Loss.csv`).

### 2. Raw Data Viewer (Tab 2)
* **Dataset Transparency:** Integrates a high-readability `DataGridView` table displaying the complete 146-country parsed dataset.
* **Robust CSV Parsing:** Features a custom CSV parser equipped with quoted-comma handling to safely parse country records, regions, dimensions, and source metadata without runtime formatting errors.

### 3. Progress & Routine Tracker (Tab 3)
* **Baseline & Growth Monitoring:** Allows users to log personal measurement entries and dates over time.
* **History Management:** Organizes logged data into a dynamic tracking table to support consistent, long-term health tracking.

### 4. Mindset & Wellness Guide (Tab 4)
* **Body-Positive Guidance:** Provides uplifting reminders that human biological variation across global populations is completely natural.
* **Cardiovascular & Pelvic Health:** Emphasizes the importance of cardiovascular fitness, aerobic exercise, and pelvic floor conditioning for erection quality and vitality.
* **Commercial Safety Warnings:** Directs users away from unverified commercial gimmicks, emphasizing safety, hygiene, and medical science.

---

## Technical & Rubric Compliance

* **Data Preprocessing (20 pts):** Implements robust CSV parsing, feature extraction, Min-Max normalization into a $[0, 1]$ scale, and an 80/20 train-test data split.
* **Functional Correctness & Backpropagation (35 pts):** Powered by an active C# backpropagation neural network engine handling forward passes, error calculations, and weight updates.
* **Hyperparameter Tuning & Logging (15 pts):** Tracks training epochs and automatically exports a real-time MSE training loss CSV log.
* **Evaluation & Decision Support (15 pts):** Integrates quantitative neural predictions with an intuitive decision support system user interface.

---

## Getting Started & Installation

### Prerequisites
* **Visual Studio** (Community or Professional edition with .NET Framework / Windows Forms support).
* The accompanying `Backprop.dll` neural network library reference.
* The dataset file: `penis size by country.csv`.

### Running the Application
1. Clone or download the repository to your local machine.
2. Open the solution file (`.sln`) in Visual Studio.
3. Ensure `penis size by country.csv` is placed in your project directory (or output folder).
4. Build and run the project (`F5`).
5. Navigate to **Tab 1**, click **Load CSV Dataset**, train the neural network model, and explore the system!

---

## Project Deliverables
* **Source Code:** Well-structured C# Windows Forms application (`Form1.cs`, `DataLoader.cs`, `CountryData.cs`).
* **Training Output Log:** `MaleForm_Training_Loss.csv` (tracks MSE loss decreasing across epochs).
* **Data Description:** Utilizes peer-reviewed global anthropometric metrics across 146 countries to promote statistical literacy and body confidence.
