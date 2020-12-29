namespace QLVT_DATHANG
{
    partial class frmTaoLogin
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label mANVLabel;
            System.Windows.Forms.Label hOTENLabel;
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLoginName = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.dataSet = new QLVT_DATHANG.DataSet();
            this.dsnvchuacotkBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dsnvchuacotkTableAdapter = new QLVT_DATHANG.DataSetTableAdapters.dsnvchuacotkTableAdapter();
            this.tableAdapterManager = new QLVT_DATHANG.DataSetTableAdapters.TableAdapterManager();
            this.txtUsername = new DevExpress.XtraEditors.SpinEdit();
            this.cmbHoTen = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.rdCT = new System.Windows.Forms.RadioButton();
            this.rdCN = new System.Windows.Forms.RadioButton();
            this.rdUser = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            mANVLabel = new System.Windows.Forms.Label();
            hOTENLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsnvchuacotkBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUsername.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // mANVLabel
            // 
            mANVLabel.AutoSize = true;
            mANVLabel.Location = new System.Drawing.Point(188, 193);
            mANVLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            mANVLabel.Name = "mANVLabel";
            mANVLabel.Size = new System.Drawing.Size(88, 17);
            mANVLabel.TabIndex = 5;
            mANVLabel.Text = "Mã nhân viên";
            // 
            // hOTENLabel
            // 
            hOTENLabel.AutoSize = true;
            hOTENLabel.Location = new System.Drawing.Point(188, 275);
            hOTENLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            hOTENLabel.Name = "hOTENLabel";
            hOTENLabel.Size = new System.Drawing.Size(52, 17);
            hOTENLabel.TabIndex = 6;
            hOTENLabel.Text = "Họ tên ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(188, 51);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tên đăng nhập";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(188, 115);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Mật khẩu";
            // 
            // txtLoginName
            // 
            this.txtLoginName.Location = new System.Drawing.Point(398, 40);
            this.txtLoginName.Margin = new System.Windows.Forms.Padding(5);
            this.txtLoginName.Name = "txtLoginName";
            this.txtLoginName.Size = new System.Drawing.Size(513, 24);
            this.txtLoginName.TabIndex = 2;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(398, 115);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(5);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(513, 24);
            this.txtPassword.TabIndex = 3;
            // 
            // dataSet
            // 
            this.dataSet.DataSetName = "DataSet";
            this.dataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dsnvchuacotkBindingSource
            // 
            this.dsnvchuacotkBindingSource.DataMember = "dsnvchuacotk";
            this.dsnvchuacotkBindingSource.DataSource = this.dataSet;
            // 
            // dsnvchuacotkTableAdapter
            // 
            this.dsnvchuacotkTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.ChiNhanhTableAdapter = null;
            this.tableAdapterManager.CTDDHTableAdapter = null;
            this.tableAdapterManager.CTPNTableAdapter = null;
            this.tableAdapterManager.CTPXTableAdapter = null;
            this.tableAdapterManager.DatHangTableAdapter = null;
            this.tableAdapterManager.dsnvchuacotkTableAdapter = this.dsnvchuacotkTableAdapter;
            this.tableAdapterManager.KhoTableAdapter = null;
            this.tableAdapterManager.NhanVienTableAdapter = null;
            this.tableAdapterManager.PhieuNhapTableAdapter = null;
            this.tableAdapterManager.PhieuXuatTableAdapter = null;
            this.tableAdapterManager.UpdateOrder = QLVT_DATHANG.DataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            this.tableAdapterManager.VattuTableAdapter = null;
            // 
            // txtUsername
            // 
            this.txtUsername.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.dsnvchuacotkBindingSource, "MANV", true));
            this.txtUsername.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtUsername.Location = new System.Drawing.Point(396, 192);
            this.txtUsername.Margin = new System.Windows.Forms.Padding(5);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtUsername.Size = new System.Drawing.Size(515, 20);
            this.txtUsername.TabIndex = 6;
            this.txtUsername.EditValueChanged += new System.EventHandler(this.txtUsername_EditValueChanged);
            // 
            // cmbHoTen
            // 
            this.cmbHoTen.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.dsnvchuacotkBindingSource, "HOTEN", true));
            this.cmbHoTen.DataSource = this.dsnvchuacotkBindingSource;
            this.cmbHoTen.DisplayMember = "HOTEN";
            this.cmbHoTen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHoTen.FormattingEnabled = true;
            this.cmbHoTen.Location = new System.Drawing.Point(396, 268);
            this.cmbHoTen.Margin = new System.Windows.Forms.Padding(5);
            this.cmbHoTen.Name = "cmbHoTen";
            this.cmbHoTen.Size = new System.Drawing.Size(515, 24);
            this.cmbHoTen.TabIndex = 7;
            this.cmbHoTen.ValueMember = "MANV";
            this.cmbHoTen.SelectedIndexChanged += new System.EventHandler(this.cmbHoTen_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(188, 363);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "Vai trò";
            // 
            // rdCT
            // 
            this.rdCT.AutoSize = true;
            this.rdCT.Location = new System.Drawing.Point(396, 363);
            this.rdCT.Margin = new System.Windows.Forms.Padding(5);
            this.rdCT.Name = "rdCT";
            this.rdCT.Size = new System.Drawing.Size(92, 21);
            this.rdCT.TabIndex = 9;
            this.rdCT.TabStop = true;
            this.rdCT.Text = "CÔNG TY";
            this.rdCT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rdCT.UseVisualStyleBackColor = true;
            // 
            // rdCN
            // 
            this.rdCN.AutoSize = true;
            this.rdCN.Location = new System.Drawing.Point(613, 363);
            this.rdCN.Margin = new System.Windows.Forms.Padding(5);
            this.rdCN.Name = "rdCN";
            this.rdCN.Size = new System.Drawing.Size(111, 21);
            this.rdCN.TabIndex = 10;
            this.rdCN.TabStop = true;
            this.rdCN.Text = "CHI NHÁNH";
            this.rdCN.UseVisualStyleBackColor = true;
            // 
            // rdUser
            // 
            this.rdUser.AutoSize = true;
            this.rdUser.Location = new System.Drawing.Point(854, 363);
            this.rdUser.Margin = new System.Windows.Forms.Padding(5);
            this.rdUser.Name = "rdUser";
            this.rdUser.Size = new System.Drawing.Size(64, 21);
            this.rdUser.TabIndex = 11;
            this.rdUser.TabStop = true;
            this.rdUser.Text = "USER";
            this.rdUser.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(386, 445);
            this.button1.Margin = new System.Windows.Forms.Padding(5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(125, 35);
            this.button1.TabIndex = 12;
            this.button1.Text = "Tạo tài khoản";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(613, 445);
            this.button2.Margin = new System.Windows.Forms.Padding(5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(125, 35);
            this.button2.TabIndex = 13;
            this.button2.Text = "Thoát";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // frmTaoLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 692);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.rdUser);
            this.Controls.Add(this.rdCN);
            this.Controls.Add(this.rdCT);
            this.Controls.Add(this.label3);
            this.Controls.Add(hOTENLabel);
            this.Controls.Add(this.cmbHoTen);
            this.Controls.Add(mANVLabel);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtLoginName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "frmTaoLogin";
            this.Text = "frmTaoLogin";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmTaoLogin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsnvchuacotkBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUsername.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtLoginName;
        private System.Windows.Forms.TextBox txtPassword;
        private DataSet dataSet;
        private System.Windows.Forms.BindingSource dsnvchuacotkBindingSource;
        private DataSetTableAdapters.dsnvchuacotkTableAdapter dsnvchuacotkTableAdapter;
        private DataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private DevExpress.XtraEditors.SpinEdit txtUsername;
        private System.Windows.Forms.ComboBox cmbHoTen;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rdCT;
        private System.Windows.Forms.RadioButton rdCN;
        private System.Windows.Forms.RadioButton rdUser;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}