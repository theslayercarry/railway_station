namespace railway_station
{
    partial class other_tables
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label_routes = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label_tickets = new System.Windows.Forms.Label();
            this.label_trains = new System.Windows.Forms.Label();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.BackgroundImage = global::railway_station.Properties.Resources.button_color4;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.label4);
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1090, 100);
            this.panel3.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Book Antiqua", 18.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(15, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(699, 53);
            this.label4.TabIndex = 18;
            this.label4.Text = "Реализация добавления, редактирования и удаления";
            // 
            // label_routes
            // 
            this.label_routes.BackColor = System.Drawing.Color.Transparent;
            this.label_routes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label_routes.Font = new System.Drawing.Font("Book Antiqua", 17.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_routes.ForeColor = System.Drawing.Color.White;
            this.label_routes.Location = new System.Drawing.Point(13, 27);
            this.label_routes.Name = "label_routes";
            this.label_routes.Size = new System.Drawing.Size(369, 32);
            this.label_routes.TabIndex = 16;
            this.label_routes.Text = "Маршруты";
            this.label_routes.Click += new System.EventHandler(this.label_routes_Click);
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::railway_station.Properties.Resources.button_color4;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label_tickets);
            this.panel1.Controls.Add(this.label_trains);
            this.panel1.Controls.Add(this.label_routes);
            this.panel1.Location = new System.Drawing.Point(0, 175);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(385, 441);
            this.panel1.TabIndex = 17;
            // 
            // label_tickets
            // 
            this.label_tickets.BackColor = System.Drawing.Color.Transparent;
            this.label_tickets.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label_tickets.Font = new System.Drawing.Font("Book Antiqua", 17.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_tickets.ForeColor = System.Drawing.Color.White;
            this.label_tickets.Location = new System.Drawing.Point(13, 132);
            this.label_tickets.Name = "label_tickets";
            this.label_tickets.Size = new System.Drawing.Size(369, 32);
            this.label_tickets.TabIndex = 18;
            this.label_tickets.Text = "Билеты";
            this.label_tickets.Click += new System.EventHandler(this.label_tickets_Click);
            // 
            // label_trains
            // 
            this.label_trains.BackColor = System.Drawing.Color.Transparent;
            this.label_trains.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label_trains.Font = new System.Drawing.Font("Book Antiqua", 17.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_trains.ForeColor = System.Drawing.Color.White;
            this.label_trains.Location = new System.Drawing.Point(13, 79);
            this.label_trains.Name = "label_trains";
            this.label_trains.Size = new System.Drawing.Size(369, 32);
            this.label_trains.TabIndex = 17;
            this.label_trains.Text = "Поезда";
            this.label_trains.Click += new System.EventHandler(this.label_trains_Click);
            // 
            // other_tables
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1088, 614);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "other_tables";
            this.Text = "Работа с таблицами";
            this.Load += new System.EventHandler(this.other_tables_Load);
            this.panel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label_routes;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label_tickets;
        private System.Windows.Forms.Label label_trains;
        private System.Windows.Forms.Label label4;
    }
}