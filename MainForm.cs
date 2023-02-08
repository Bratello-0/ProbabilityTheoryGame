﻿using System;
using System.Linq;
using System.Windows.Forms;

namespace ProbabilityTheoryGameForBirhday
{
    public interface IMainForm
    {
        Button ButPrefab { get; }

        int GetIdActiveRadioButPeople { get; }

        int RowsTableLayout { get; set; }
        int ColumnsTableLayout { get; set; }

        void AddControlTableLayout(Control control);
        void ClearControlTableLayout();

        void SetCenterAllUI();

        event EventHandler GeneratingButtons;
        event EventHandler ClickButtonHint;
        event EventHandler ClickButtonRestart;
        event EventHandler LoadForm;
    }

    public partial class MainForm : Form, IMainForm
    {
        #region interface properties
        public Button ButPrefab => butPrefab.Copy();

        public int GetIdActiveRadioButPeople { get =>
            panelDifficulty.Controls
                .OfType<RadioButton>()
                .Where(radioBut=>radioBut.Checked == true)
                .First().TabIndex;
        }

        public int RowsTableLayout
        {
            get => tableLayoutPanelCenter.RowCount;
            set
            { if (value > 0) {
                    tableLayoutPanelCenter.RowCount = value;
                }
                else{
                    throw new NotImplementedException();
                }
            }
        }
        public int ColumnsTableLayout
        {
            get => tableLayoutPanelCenter.ColumnCount;
            set 
            { if (value > 0) {
                    tableLayoutPanelCenter.ColumnCount = value;
                }
                else {
                    throw new NotImplementedException();
                }
            }
        }
        #endregion

        #region interface method
        public void AddControlTableLayout(Control control) => tableLayoutPanelCenter.Controls.Add(control);

        public void SetCenterAllUI()
        {
            SetCenter(labelRulesHeader);
            SetCenter(labelDifficultyLevel);
            SetCenter(new Control[] { radioButton100People, radioButton50People, radioButton10People });
        }
        public void ClearControlTableLayout()
        {
            tableLayoutPanelCenter.Controls.Clear();
        }
        #endregion

        public MainForm()
        {
            InitializeComponent();
            butPrefab.Visible = false;
            this.Load += MainForm_Load;
            butHint.Click += ButHint_Click;
            butRestart.Click += ButRestart_Click;
            this.Load += MainForm_Load1;
        }

        #region MainForm method
        private void SetCenter(Control control)
        {
            control.Location = new System.Drawing.Point(
                    (control.Parent.Width - control.Width) / 2,
                    (control.Parent.Height - control.Height) / 2
                    );
        }

        private void SetCenter(Control control, bool isHorizontal, bool isVertical = false)
        {
            if (isHorizontal && isVertical) {
                if (isHorizontal == false) {
                    return;
                }

                SetCenter(control);
                return;
            }

            if (isHorizontal) {
                control.Location = new System.Drawing.Point(
                        (control.Parent.Width - control.Width) / 2,
                        control.Location.Y
                   );
            } else {
                control.Location = new System.Drawing.Point(
                        control.Location.X,
                        (control.Parent.Height - control.Height) / 2
                   );
            }
        }

        private void SetCenter(Control[] controls) {
            if (!controls.Any())
            {
                throw new NotImplementedException();
            }

            if (controls.Length > 1) 
            {
                int MinSizeToShift_X = controls.First().Width;
                controls.ToList().ForEach(control =>
                {
                    if(control.Width < MinSizeToShift_X)
                        MinSizeToShift_X = control.Width;
                });

                controls.ToList().ForEach(control =>
                {
                    control.Location = new System.Drawing.Point(
                        (control.Parent.Width - MinSizeToShift_X) / 2,
                        control.Location.Y
                   );
                });
            }
        }
        #endregion

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

        private void MainForm_Load1(object sender, EventArgs e)
        {
            if (LoadForm != null) LoadForm(this, EventArgs.Empty);
        }
        #endregion

        #region interface events
        public event EventHandler GeneratingButtons;
        public event EventHandler ClickButtonHint;
        public event EventHandler ClickButtonRestart;
        public event EventHandler LoadForm;
        #endregion
    }
}