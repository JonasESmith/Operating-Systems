﻿namespace Semaphores
{
    partial class PhilProblem
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
            this.ConsoleLogger = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // ConsoleLogger
            // 
            this.ConsoleLogger.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ConsoleLogger.Location = new System.Drawing.Point(0, 0);
            this.ConsoleLogger.Name = "ConsoleLogger";
            this.ConsoleLogger.Size = new System.Drawing.Size(314, 484);
            this.ConsoleLogger.TabIndex = 0;
            this.ConsoleLogger.Text = "";
            // 
            // PhilProblem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 484);
            this.Controls.Add(this.ConsoleLogger);
            this.Name = "PhilProblem";
            this.Text = "Problem";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PhilProblem_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox ConsoleLogger;
    }
}

