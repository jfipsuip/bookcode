﻿namespace PlaneMP
{
    partial class MainInterface
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainInterface));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ToolStripMenuItem_M_openTXT = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_M_Cal = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_SaDXF = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_SaBMP = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_TopenTXT = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_T_Cal = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_SaReport = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_Clear = new System.Windows.Forms.ToolStripButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dvg = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.opTxt = new System.Windows.Forms.OpenFileDialog();
            this.saDXF = new System.Windows.Forms.SaveFileDialog();
            this.saTXT = new System.Windows.Forms.SaveFileDialog();
            this.saBMP = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dvg)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_M_openTXT,
            this.ToolStripMenuItem_M_Cal,
            this.ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(649, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ToolStripMenuItem_M_openTXT
            // 
            this.ToolStripMenuItem_M_openTXT.Name = "ToolStripMenuItem_M_openTXT";
            this.ToolStripMenuItem_M_openTXT.Size = new System.Drawing.Size(68, 21);
            this.ToolStripMenuItem_M_openTXT.Text = "打开文件";
            this.ToolStripMenuItem_M_openTXT.Click += new System.EventHandler(this.ToolStripMenuItem_M_openTXT_Click);
            // 
            // ToolStripMenuItem_M_Cal
            // 
            this.ToolStripMenuItem_M_Cal.Name = "ToolStripMenuItem_M_Cal";
            this.ToolStripMenuItem_M_Cal.Size = new System.Drawing.Size(44, 21);
            this.ToolStripMenuItem_M_Cal.Text = "解算";
            this.ToolStripMenuItem_M_Cal.Click += new System.EventHandler(this.ToolStripMenuItem_M_Cal_Click);
            // 
            // ToolStripMenuItem
            // 
            this.ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_SaDXF,
            this.ToolStripMenuItem_SaBMP});
            this.ToolStripMenuItem.Name = "ToolStripMenuItem";
            this.ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.ToolStripMenuItem.Text = "保存";
            // 
            // ToolStripMenuItem_SaDXF
            // 
            this.ToolStripMenuItem_SaDXF.Name = "ToolStripMenuItem_SaDXF";
            this.ToolStripMenuItem_SaDXF.Size = new System.Drawing.Size(152, 22);
            this.ToolStripMenuItem_SaDXF.Text = "DXF";
            this.ToolStripMenuItem_SaDXF.Click += new System.EventHandler(this.ToolStripMenuItem_SaDXF_Click);
            // 
            // ToolStripMenuItem_SaBMP
            // 
            this.ToolStripMenuItem_SaBMP.Name = "ToolStripMenuItem_SaBMP";
            this.ToolStripMenuItem_SaBMP.Size = new System.Drawing.Size(152, 22);
            this.ToolStripMenuItem_SaBMP.Text = "BMP";
            this.ToolStripMenuItem_SaBMP.Click += new System.EventHandler(this.ToolStripMenuItem_SaBMP_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(30, 30);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_TopenTXT,
            this.toolStripButton_T_Cal,
            this.toolStripButton_SaReport,
            this.toolStripButton1,
            this.toolStripButton_Clear});
            this.toolStrip1.Location = new System.Drawing.Point(0, 25);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(649, 37);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton_TopenTXT
            // 
            this.toolStripButton_TopenTXT.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_TopenTXT.Image")));
            this.toolStripButton_TopenTXT.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_TopenTXT.Name = "toolStripButton_TopenTXT";
            this.toolStripButton_TopenTXT.Size = new System.Drawing.Size(90, 34);
            this.toolStripButton_TopenTXT.Text = "打开文件";
            this.toolStripButton_TopenTXT.Click += new System.EventHandler(this.toolStripButton_TopenTXT_Click);
            // 
            // toolStripButton_T_Cal
            // 
            this.toolStripButton_T_Cal.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_T_Cal.Image")));
            this.toolStripButton_T_Cal.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_T_Cal.Name = "toolStripButton_T_Cal";
            this.toolStripButton_T_Cal.Size = new System.Drawing.Size(66, 34);
            this.toolStripButton_T_Cal.Text = "解算";
            this.toolStripButton_T_Cal.Click += new System.EventHandler(this.toolStripButton_T_Cal_Click);
            // 
            // toolStripButton_SaReport
            // 
            this.toolStripButton_SaReport.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_SaReport.Image")));
            this.toolStripButton_SaReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_SaReport.Name = "toolStripButton_SaReport";
            this.toolStripButton_SaReport.Size = new System.Drawing.Size(114, 34);
            this.toolStripButton_SaReport.Text = "保存计算报告";
            this.toolStripButton_SaReport.Click += new System.EventHandler(this.toolStripButton_SaReport_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(90, 34);
            this.toolStripButton1.Text = "显示图形";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton_Clear
            // 
            this.toolStripButton_Clear.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_Clear.Image")));
            this.toolStripButton_Clear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_Clear.Name = "toolStripButton_Clear";
            this.toolStripButton_Clear.Size = new System.Drawing.Size(66, 34);
            this.toolStripButton_Clear.Text = "清除";
            this.toolStripButton_Clear.Click += new System.EventHandler(this.toolStripButton_Clear_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 62);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(649, 375);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dvg);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(641, 349);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "数据";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dvg
            // 
            this.dvg.AllowUserToAddRows = false;
            this.dvg.AllowUserToDeleteRows = false;
            this.dvg.AllowUserToResizeColumns = false;
            this.dvg.AllowUserToResizeRows = false;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dvg.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dvg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dvg.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.dvg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dvg.Location = new System.Drawing.Point(3, 3);
            this.dvg.Name = "dvg";
            this.dvg.RowTemplate.Height = 23;
            this.dvg.Size = new System.Drawing.Size(635, 343);
            this.dvg.TabIndex = 0;
            // 
            // Column1
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle7;
            this.Column1.HeaderText = "点名";
            this.Column1.Name = "Column1";
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column2
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column2.DefaultCellStyle = dataGridViewCellStyle8;
            this.Column2.HeaderText = "X(M)";
            this.Column2.Name = "Column2";
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column2.Width = 150;
            // 
            // Column3
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column3.DefaultCellStyle = dataGridViewCellStyle9;
            this.Column3.HeaderText = "Y(M)";
            this.Column3.Name = "Column3";
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column3.Width = 150;
            // 
            // Column4
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column4.DefaultCellStyle = dataGridViewCellStyle10;
            this.Column4.HeaderText = "Z(M)";
            this.Column4.Name = "Column4";
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column4.Width = 150;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.chart1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(641, 349);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "图形";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // chart1
            // 
            chartArea2.AxisX.IsStartedFromZero = false;
            chartArea2.AxisY.IsStartedFromZero = false;
            chartArea2.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea2);
            this.chart1.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.chart1.Location = new System.Drawing.Point(8, 15);
            this.chart1.Name = "chart1";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series2.MarkerColor = System.Drawing.Color.Red;
            series2.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series2.Name = "Series1";
            this.chart1.Series.Add(series2);
            this.chart1.Size = new System.Drawing.Size(405, 248);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            this.chart1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.chart1_MouseDown);
            this.chart1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart1_MouseMove);
            this.chart1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.chart1_MouseUp);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.richTextBox1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(641, 349);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "计算报告";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(3, 3);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(635, 343);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // MainInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(649, 437);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainInterface";
            this.Text = "空间平面平整度测定";
            this.Load += new System.EventHandler(this.MainInterface_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dvg)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_M_openTXT;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_M_Cal;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_SaDXF;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_SaBMP;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton_TopenTXT;
        private System.Windows.Forms.ToolStripButton toolStripButton_SaReport;
        private System.Windows.Forms.ToolStripButton toolStripButton_T_Cal;
        private System.Windows.Forms.ToolStripButton toolStripButton_Clear;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView dvg;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.OpenFileDialog opTxt;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.SaveFileDialog saDXF;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.SaveFileDialog saTXT;
        private System.Windows.Forms.SaveFileDialog saBMP;
    }
}

