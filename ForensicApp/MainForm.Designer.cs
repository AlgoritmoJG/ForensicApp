namespace ForensicApp
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            button1 = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            lblStatus = new Label();
            rtbPromptEnviado = new RichTextBox();
            rtbRespuestaAPI = new RichTextBox();
            txtObjetivosInvestigacion = new TextBox();
            cmbTipoSistemaa = new ComboBox();
            cmbNaturalezaIncidente = new ComboBox();
            label4 = new Label();
            label5 = new Label();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(316, 237);
            button1.Name = "button1";
            button1.Size = new Size(129, 36);
            button1.TabIndex = 0;
            button1.Text = "Generar Analysis";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = SystemColors.ButtonFace;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(116, 20);
            label1.TabIndex = 3;
            label1.Text = "Tipo de Sistema";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = SystemColors.ButtonFace;
            label2.Location = new Point(394, 9);
            label2.Name = "label2";
            label2.Size = new Size(171, 20);
            label2.TabIndex = 4;
            label2.Text = "Naturaleza del Incidente";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = SystemColors.ButtonFace;
            label3.Location = new Point(316, 77);
            label3.Name = "label3";
            label3.Size = new Size(200, 20);
            label3.TabIndex = 5;
            label3.Text = "Objetivos de la Investigacion";
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.ForeColor = SystemColors.ButtonFace;
            lblStatus.Location = new Point(342, 288);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(57, 20);
            lblStatus.TabIndex = 6;
            lblStatus.Text = "Estado:";
            // 
            // rtbPromptEnviado
            // 
            rtbPromptEnviado.BackColor = SystemColors.InfoText;
            rtbPromptEnviado.ForeColor = SystemColors.InactiveBorder;
            rtbPromptEnviado.Location = new Point(34, 156);
            rtbPromptEnviado.Name = "rtbPromptEnviado";
            rtbPromptEnviado.Size = new Size(255, 370);
            rtbPromptEnviado.TabIndex = 7;
            rtbPromptEnviado.Text = "";
            // 
            // rtbRespuestaAPI
            // 
            rtbRespuestaAPI.BackColor = SystemColors.InactiveCaptionText;
            rtbRespuestaAPI.ForeColor = SystemColors.InactiveBorder;
            rtbRespuestaAPI.Location = new Point(474, 156);
            rtbRespuestaAPI.Name = "rtbRespuestaAPI";
            rtbRespuestaAPI.Size = new Size(347, 401);
            rtbRespuestaAPI.TabIndex = 8;
            rtbRespuestaAPI.Text = "";
            // 
            // txtObjetivosInvestigacion
            // 
            txtObjetivosInvestigacion.Location = new Point(316, 100);
            txtObjetivosInvestigacion.Name = "txtObjetivosInvestigacion";
            txtObjetivosInvestigacion.Size = new Size(207, 27);
            txtObjetivosInvestigacion.TabIndex = 9;
            txtObjetivosInvestigacion.TextChanged += txtObjetivosInvestigacion_TextChanged;
            // 
            // cmbTipoSistemaa
            // 
            cmbTipoSistemaa.FormattingEnabled = true;
            cmbTipoSistemaa.Location = new Point(134, 26);
            cmbTipoSistemaa.Name = "cmbTipoSistemaa";
            cmbTipoSistemaa.Size = new Size(245, 28);
            cmbTipoSistemaa.TabIndex = 10;
            // 
            // cmbNaturalezaIncidente
            // 
            cmbNaturalezaIncidente.FormattingEnabled = true;
            cmbNaturalezaIncidente.Location = new Point(556, 32);
            cmbNaturalezaIncidente.Name = "cmbNaturalezaIncidente";
            cmbNaturalezaIncidente.Size = new Size(247, 28);
            cmbNaturalezaIncidente.TabIndex = 11;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = SystemColors.ButtonFace;
            label4.Location = new Point(34, 133);
            label4.Name = "label4";
            label4.Size = new Size(113, 20);
            label4.TabIndex = 12;
            label4.Text = "Pront Generado";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ForeColor = SystemColors.ButtonFace;
            label5.Location = new Point(598, 133);
            label5.Name = "label5";
            label5.Size = new Size(104, 20);
            label5.TabIndex = 13;
            label5.Text = "Respuesta api:";
            // 
            // button2
            // 
            button2.Location = new Point(304, 311);
            button2.Name = "button2";
            button2.Size = new Size(164, 55);
            button2.TabIndex = 14;
            button2.Text = "Generar Documento de Word";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(304, 372);
            button3.Name = "button3";
            button3.Size = new Size(164, 60);
            button3.TabIndex = 15;
            button3.Text = "Generar Presentacion ";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Location = new Point(333, 481);
            button4.Name = "button4";
            button4.Size = new Size(94, 45);
            button4.TabIndex = 16;
            button4.Text = "Cerrar app";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(0, 0, 64);
            ClientSize = new Size(833, 578);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(cmbNaturalezaIncidente);
            Controls.Add(cmbTipoSistemaa);
            Controls.Add(txtObjetivosInvestigacion);
            Controls.Add(rtbRespuestaAPI);
            Controls.Add(rtbPromptEnviado);
            Controls.Add(lblStatus);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            Text = "MainForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label lblStatus;
        private RichTextBox rtbPromptEnviado;
        private RichTextBox rtbRespuestaAPI;
        private TextBox txtObjetivosInvestigacion;
        private ComboBox cmbTipoSistemaa;
        private ComboBox cmbNaturalezaIncidente;
        private Label label4;
        private Label label5;
        private Button button2;
        private Button button3;
        private Button button4;
    }
}