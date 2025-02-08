using System.Drawing;
using System.Windows.Forms;

namespace ColdWar
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.metroSetTabControl1 = new MetroSet_UI.Controls.MetroSetTabControl();
            this.main = new MetroSet_UI.Child.MetroSetSetTabPage();
            this.metroComboBox1 = new MetroFramework.Controls.MetroComboBox();
            this.metroCheckBox6 = new MetroFramework.Controls.MetroCheckBox();
            this.metroCheckBox5 = new MetroFramework.Controls.MetroCheckBox();
            this.metroCheckBox4 = new MetroFramework.Controls.MetroCheckBox();
            this.metroCheckBox3 = new MetroFramework.Controls.MetroCheckBox();
            this.metroCheckBox2 = new MetroFramework.Controls.MetroCheckBox();
            this.metroCheckBox1 = new MetroFramework.Controls.MetroCheckBox();
            this.PointsCheck = new MetroFramework.Controls.MetroCheckBox();
            this.metroSetSetTabPage2 = new MetroSet_UI.Child.MetroSetSetTabPage();
            this.metroSetSetTabPage3 = new MetroSet_UI.Child.MetroSetSetTabPage();
            this.metroSetSetTabPage4 = new MetroSet_UI.Child.MetroSetSetTabPage();
            this.ExitBtn = new MetroFramework.Controls.MetroButton();
            this.metroSetTabControl1.SuspendLayout();
            this.main.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroSetTabControl1
            // 
            this.metroSetTabControl1.AnimateEasingType = MetroSet_UI.Enums.EasingType.CubeOut;
            this.metroSetTabControl1.AnimateTime = 200;
            this.metroSetTabControl1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.metroSetTabControl1.Controls.Add(this.main);
            this.metroSetTabControl1.Controls.Add(this.metroSetSetTabPage2);
            this.metroSetTabControl1.Controls.Add(this.metroSetSetTabPage3);
            this.metroSetTabControl1.Controls.Add(this.metroSetSetTabPage4);
            this.metroSetTabControl1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.metroSetTabControl1.IsDerivedStyle = true;
            this.metroSetTabControl1.ItemSize = new System.Drawing.Size(100, 38);
            this.metroSetTabControl1.Location = new System.Drawing.Point(3, 28);
            this.metroSetTabControl1.Name = "metroSetTabControl1";
            this.metroSetTabControl1.SelectedIndex = 0;
            this.metroSetTabControl1.SelectedTextColor = System.Drawing.Color.White;
            this.metroSetTabControl1.Size = new System.Drawing.Size(636, 290);
            this.metroSetTabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.metroSetTabControl1.Speed = 100;
            this.metroSetTabControl1.Style = MetroSet_UI.Enums.Style.Dark;
            this.metroSetTabControl1.StyleManager = null;
            this.metroSetTabControl1.TabIndex = 0;
            this.metroSetTabControl1.ThemeAuthor = "Narwin";
            this.metroSetTabControl1.ThemeName = "MetroDark";
            this.metroSetTabControl1.UnselectedTextColor = System.Drawing.Color.Gray;
            this.metroSetTabControl1.UseAnimation = false;
            // 
            // main
            // 
            this.main.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.main.Controls.Add(this.metroComboBox1);
            this.main.Controls.Add(this.metroCheckBox6);
            this.main.Controls.Add(this.metroCheckBox5);
            this.main.Controls.Add(this.metroCheckBox4);
            this.main.Controls.Add(this.metroCheckBox3);
            this.main.Controls.Add(this.metroCheckBox2);
            this.main.Controls.Add(this.metroCheckBox1);
            this.main.Controls.Add(this.PointsCheck);
            this.main.Font = null;
            this.main.ImageIndex = 0;
            this.main.ImageKey = null;
            this.main.IsDerivedStyle = true;
            this.main.Location = new System.Drawing.Point(4, 42);
            this.main.Name = "main";
            this.main.Size = new System.Drawing.Size(628, 244);
            this.main.Style = MetroSet_UI.Enums.Style.Dark;
            this.main.StyleManager = null;
            this.main.TabIndex = 0;
            this.main.Tag = "MainTab";
            this.main.Text = "Main";
            this.main.ThemeAuthor = "Narwin";
            this.main.ThemeName = "MetroDark";
            this.main.ToolTipText = null;
            // 
            // metroComboBox1
            // 
            this.metroComboBox1.FormattingEnabled = true;
            this.metroComboBox1.ItemHeight = 23;
            this.metroComboBox1.Location = new System.Drawing.Point(328, 89);
            this.metroComboBox1.Name = "metroComboBox1";
            this.metroComboBox1.Size = new System.Drawing.Size(121, 29);
            this.metroComboBox1.TabIndex = 7;
            // 
            // metroCheckBox6
            // 
            this.metroCheckBox6.AutoSize = true;
            this.metroCheckBox6.Location = new System.Drawing.Point(328, 54);
            this.metroCheckBox6.Name = "metroCheckBox6";
            this.metroCheckBox6.Size = new System.Drawing.Size(52, 15);
            this.metroCheckBox6.TabIndex = 6;
            this.metroCheckBox6.Text = "Cycle";
            this.metroCheckBox6.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroCheckBox6.UseVisualStyleBackColor = true;
            this.metroCheckBox6.CheckedChanged += new System.EventHandler(this.metroCheckBox6_CheckedChanged);
            // 
            // metroCheckBox5
            // 
            this.metroCheckBox5.AutoSize = true;
            this.metroCheckBox5.Location = new System.Drawing.Point(328, 20);
            this.metroCheckBox5.Name = "metroCheckBox5";
            this.metroCheckBox5.Size = new System.Drawing.Size(85, 15);
            this.metroCheckBox5.TabIndex = 5;
            this.metroCheckBox5.Text = "Dark Aether";
            this.metroCheckBox5.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroCheckBox5.UseVisualStyleBackColor = true;
            this.metroCheckBox5.CheckedChanged += new System.EventHandler(this.metroCheckBox5_CheckedChanged);
            // 
            // metroCheckBox4
            // 
            this.metroCheckBox4.AutoSize = true;
            this.metroCheckBox4.Location = new System.Drawing.Point(0, 149);
            this.metroCheckBox4.Name = "metroCheckBox4";
            this.metroCheckBox4.Size = new System.Drawing.Size(115, 15);
            this.metroCheckBox4.TabIndex = 4;
            this.metroCheckBox4.Text = "Unlimited Ammo";
            this.metroCheckBox4.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroCheckBox4.UseVisualStyleBackColor = true;
            this.metroCheckBox4.CheckedChanged += new System.EventHandler(this.metroCheckBox4_CheckedChanged);
            // 
            // metroCheckBox3
            // 
            this.metroCheckBox3.AutoSize = true;
            this.metroCheckBox3.Location = new System.Drawing.Point(0, 119);
            this.metroCheckBox3.Name = "metroCheckBox3";
            this.metroCheckBox3.Size = new System.Drawing.Size(99, 15);
            this.metroCheckBox3.TabIndex = 3;
            this.metroCheckBox3.Text = "Tp to corsshair";
            this.metroCheckBox3.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroCheckBox3.UseVisualStyleBackColor = true;
            this.metroCheckBox3.CheckedChanged += new System.EventHandler(this.metroCheckBox3_CheckedChanged_1);
            // 
            // metroCheckBox2
            // 
            this.metroCheckBox2.AutoSize = true;
            this.metroCheckBox2.Location = new System.Drawing.Point(0, 89);
            this.metroCheckBox2.Name = "metroCheckBox2";
            this.metroCheckBox2.Size = new System.Drawing.Size(55, 15);
            this.metroCheckBox2.TabIndex = 2;
            this.metroCheckBox2.Text = "Speed";
            this.metroCheckBox2.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroCheckBox2.UseVisualStyleBackColor = true;
            this.metroCheckBox2.CheckedChanged += new System.EventHandler(this.metroCheckBox2_CheckedChanged);
            // 
            // metroCheckBox1
            // 
            this.metroCheckBox1.AutoSize = true;
            this.metroCheckBox1.Location = new System.Drawing.Point(0, 54);
            this.metroCheckBox1.Name = "metroCheckBox1";
            this.metroCheckBox1.Size = new System.Drawing.Size(76, 15);
            this.metroCheckBox1.TabIndex = 1;
            this.metroCheckBox1.Text = "Godmode";
            this.metroCheckBox1.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroCheckBox1.UseVisualStyleBackColor = true;
            this.metroCheckBox1.CheckedChanged += new System.EventHandler(this.metroCheckBox1_CheckedChanged);
            // 
            // PointsCheck
            // 
            this.PointsCheck.AutoSize = true;
            this.PointsCheck.Location = new System.Drawing.Point(0, 20);
            this.PointsCheck.Name = "PointsCheck";
            this.PointsCheck.Size = new System.Drawing.Size(111, 15);
            this.PointsCheck.TabIndex = 0;
            this.PointsCheck.Text = "Unlimited Points";
            this.PointsCheck.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.PointsCheck.UseVisualStyleBackColor = true;
            this.PointsCheck.CheckedChanged += new System.EventHandler(this.PointsCheck_CheckedChanged);
            // 
            // metroSetSetTabPage2
            // 
            this.metroSetSetTabPage2.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.metroSetSetTabPage2.Font = null;
            this.metroSetSetTabPage2.ImageIndex = 0;
            this.metroSetSetTabPage2.ImageKey = null;
            this.metroSetSetTabPage2.IsDerivedStyle = true;
            this.metroSetSetTabPage2.Location = new System.Drawing.Point(4, 42);
            this.metroSetSetTabPage2.Name = "metroSetSetTabPage2";
            this.metroSetSetTabPage2.Size = new System.Drawing.Size(628, 244);
            this.metroSetSetTabPage2.Style = MetroSet_UI.Enums.Style.Dark;
            this.metroSetSetTabPage2.StyleManager = null;
            this.metroSetSetTabPage2.TabIndex = 1;
            this.metroSetSetTabPage2.Text = "ALL PLAYERS";
            this.metroSetSetTabPage2.ThemeAuthor = "Narwin";
            this.metroSetSetTabPage2.ThemeName = "MetroDark";
            this.metroSetSetTabPage2.ToolTipText = null;
            // 
            // metroSetSetTabPage3
            // 
            this.metroSetSetTabPage3.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.metroSetSetTabPage3.Font = null;
            this.metroSetSetTabPage3.ImageIndex = 0;
            this.metroSetSetTabPage3.ImageKey = null;
            this.metroSetSetTabPage3.IsDerivedStyle = true;
            this.metroSetSetTabPage3.Location = new System.Drawing.Point(4, 42);
            this.metroSetSetTabPage3.Name = "metroSetSetTabPage3";
            this.metroSetSetTabPage3.Size = new System.Drawing.Size(628, 244);
            this.metroSetSetTabPage3.Style = MetroSet_UI.Enums.Style.Dark;
            this.metroSetSetTabPage3.StyleManager = null;
            this.metroSetSetTabPage3.TabIndex = 2;
            this.metroSetSetTabPage3.Text = "metroSetSetTabPage3";
            this.metroSetSetTabPage3.ThemeAuthor = "Narwin";
            this.metroSetSetTabPage3.ThemeName = "MetroDark";
            this.metroSetSetTabPage3.ToolTipText = null;
            // 
            // metroSetSetTabPage4
            // 
            this.metroSetSetTabPage4.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.metroSetSetTabPage4.Font = null;
            this.metroSetSetTabPage4.ImageIndex = 0;
            this.metroSetSetTabPage4.ImageKey = null;
            this.metroSetSetTabPage4.IsDerivedStyle = true;
            this.metroSetSetTabPage4.Location = new System.Drawing.Point(4, 42);
            this.metroSetSetTabPage4.Name = "metroSetSetTabPage4";
            this.metroSetSetTabPage4.Size = new System.Drawing.Size(628, 244);
            this.metroSetSetTabPage4.Style = MetroSet_UI.Enums.Style.Dark;
            this.metroSetSetTabPage4.StyleManager = null;
            this.metroSetSetTabPage4.TabIndex = 3;
            this.metroSetSetTabPage4.Text = "metroSetSetTabPage4";
            this.metroSetSetTabPage4.ThemeAuthor = "Narwin";
            this.metroSetSetTabPage4.ThemeName = "MetroDark";
            this.metroSetSetTabPage4.ToolTipText = null;
            // 
            // ExitBtn
            // 
            this.ExitBtn.AutoSize = true;
            this.ExitBtn.Location = new System.Drawing.Point(612, -5);
            this.ExitBtn.Name = "ExitBtn";
            this.ExitBtn.Size = new System.Drawing.Size(27, 27);
            this.ExitBtn.Style = MetroFramework.MetroColorStyle.Red;
            this.ExitBtn.TabIndex = 1;
            this.ExitBtn.Text = "X";
            this.ExitBtn.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.ExitBtn.Click += new System.EventHandler(this.ExitBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(642, 321);
            this.Controls.Add(this.ExitBtn);
            this.Controls.Add(this.metroSetTabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.metroSetTabControl1.ResumeLayout(false);
            this.main.ResumeLayout(false);
            this.main.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroSet_UI.Controls.MetroSetTabControl metroSetTabControl1;
        private MetroSet_UI.Child.MetroSetSetTabPage main;
        private MetroSet_UI.Child.MetroSetSetTabPage metroSetSetTabPage2;
        private MetroSet_UI.Child.MetroSetSetTabPage metroSetSetTabPage3;
        private MetroSet_UI.Child.MetroSetSetTabPage metroSetSetTabPage4;
        private MetroFramework.Controls.MetroButton ExitBtn;
        private MetroFramework.Controls.MetroCheckBox PointsCheck;
        private MetroFramework.Controls.MetroCheckBox metroCheckBox3;
        private MetroFramework.Controls.MetroCheckBox metroCheckBox2;
        private MetroFramework.Controls.MetroCheckBox metroCheckBox1;
        private MetroFramework.Controls.MetroCheckBox metroCheckBox4;
        private MetroFramework.Controls.MetroCheckBox metroCheckBox5;
        private MetroFramework.Controls.MetroCheckBox metroCheckBox6;
        private MetroFramework.Controls.MetroComboBox metroComboBox1;
    }
}

