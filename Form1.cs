using System;
using System.Windows.Forms;

namespace ProbabilityTheoryGameForBirhday
{
    public interface IMainForm
    {
        Button ButPrefab { get; }
        Control.ControlCollection CenterControl { get; }

        event EventHandler GeneratingButtons;
        event EventHandler ClickButtonHint;
        event EventHandler ClickButtonRestart;
    }
    public partial class MainForm : Form, IMainForm
    {
        public Button ButPrefab => butPrefab.Copy();
        public Control.ControlCollection CenterControl => tableLayoutPanelCenter.Controls;

        public MainForm()
        {
            InitializeComponent();
            butPrefab.Visible = false;
            this.Load += MainForm_Load;
            butHint.Click += ButHint_Click;
            butRestart.Click += ButRestart_Click;
        }

        #region event forwarding
        private void MainForm_Load(object sender, EventArgs e)
        {
            if (GeneratingButtons != null) GeneratingButtons(this, EventArgs.Empty);
        }

        private void ButHint_Click(object sender, EventArgs e)
        {
            if (ClickButtonHint != null) ClickButtonHint(this, EventArgs.Empty);
        }

        private void ButRestart_Click(object sender, EventArgs e)
        {
            if (ClickButtonRestart != null) ClickButtonRestart(this, EventArgs.Empty);
        }
        #endregion

        #region interface events
        public event EventHandler GeneratingButtons;
        public event EventHandler ClickButtonHint;
        public event EventHandler ClickButtonRestart;
        #endregion
    }
}