namespace GetCardData
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
            this.getCardBtn = new System.Windows.Forms.Button();
            this.cardFolderPathText = new System.Windows.Forms.TextBox();
            this.cardBrowserBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.cardPicture = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.nameText = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.idText = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.typeText = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cardTypeText = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.abilityText = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.levelText = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.attackText = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.defenceText = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.attibuteText = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.rankText = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.pendulumText = new System.Windows.Forms.TextBox();
            this.isEffectChk = new System.Windows.Forms.CheckBox();
            this.cardInfoGroup = new System.Windows.Forms.GroupBox();
            this.descriptionText = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.editBtn = new System.Windows.Forms.Button();
            this.saveBtn = new System.Windows.Forms.Button();
            this.getAllBtn = new System.Windows.Forms.Button();
            this.monsterTableAdapter = new GetCardData.CDBDataSetTableAdapters.MonsterTableAdapter();
            this.trapTableAdapter = new GetCardData.CDBDataSetTableAdapters.TrapTableAdapter();
            this.spellTableAdapter = new GetCardData.CDBDataSetTableAdapters.SpellTableAdapter();
            this.cdbDataSet = new GetCardData.CDBDataSet();
            this.dataSourceTableAdapter = new GetCardData.CDBDataSetTableAdapters.DataSourceTableAdapter();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cardList = new System.Windows.Forms.DataGridView();
            this.colCardId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.cardPicture)).BeginInit();
            this.cardInfoGroup.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cdbDataSet)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cardList)).BeginInit();
            this.SuspendLayout();
            // 
            // getCardBtn
            // 
            this.getCardBtn.Location = new System.Drawing.Point(697, 377);
            this.getCardBtn.Name = "getCardBtn";
            this.getCardBtn.Size = new System.Drawing.Size(75, 23);
            this.getCardBtn.TabIndex = 0;
            this.getCardBtn.Text = "Get Data";
            this.getCardBtn.UseVisualStyleBackColor = true;
            this.getCardBtn.Click += new System.EventHandler(this.getCardBtn_Click);
            // 
            // cardFolderPathText
            // 
            this.cardFolderPathText.Location = new System.Drawing.Point(15, 379);
            this.cardFolderPathText.Name = "cardFolderPathText";
            this.cardFolderPathText.Size = new System.Drawing.Size(122, 20);
            this.cardFolderPathText.TabIndex = 3;
            // 
            // cardBrowserBtn
            // 
            this.cardBrowserBtn.Location = new System.Drawing.Point(143, 377);
            this.cardBrowserBtn.Name = "cardBrowserBtn";
            this.cardBrowserBtn.Size = new System.Drawing.Size(72, 23);
            this.cardBrowserBtn.TabIndex = 4;
            this.cardBrowserBtn.Text = "Browser";
            this.cardBrowserBtn.UseVisualStyleBackColor = true;
            this.cardBrowserBtn.Click += new System.EventHandler(this.cardBrowserBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 363);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Card Folder";
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            this.folderBrowserDialog.ShowNewFolderButton = false;
            // 
            // richTextBox
            // 
            this.richTextBox.Location = new System.Drawing.Point(6, 21);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.Size = new System.Drawing.Size(219, 306);
            this.richTextBox.TabIndex = 6;
            this.richTextBox.Text = "";
            // 
            // cardPicture
            // 
            this.cardPicture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardPicture.Location = new System.Drawing.Point(12, 24);
            this.cardPicture.Name = "cardPicture";
            this.cardPicture.Size = new System.Drawing.Size(100, 138);
            this.cardPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.cardPicture.TabIndex = 7;
            this.cardPicture.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(119, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Name";
            // 
            // nameText
            // 
            this.nameText.Location = new System.Drawing.Point(160, 21);
            this.nameText.Name = "nameText";
            this.nameText.ReadOnly = true;
            this.nameText.Size = new System.Drawing.Size(121, 20);
            this.nameText.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(119, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(16, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Id";
            // 
            // idText
            // 
            this.idText.Location = new System.Drawing.Point(160, 47);
            this.idText.Name = "idText";
            this.idText.ReadOnly = true;
            this.idText.Size = new System.Drawing.Size(121, 20);
            this.idText.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(119, 76);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Type";
            // 
            // typeText
            // 
            this.typeText.Location = new System.Drawing.Point(160, 73);
            this.typeText.Name = "typeText";
            this.typeText.ReadOnly = true;
            this.typeText.Size = new System.Drawing.Size(60, 20);
            this.typeText.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(119, 102);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Card Type";
            // 
            // cardTypeText
            // 
            this.cardTypeText.Location = new System.Drawing.Point(181, 99);
            this.cardTypeText.Name = "cardTypeText";
            this.cardTypeText.ReadOnly = true;
            this.cardTypeText.Size = new System.Drawing.Size(100, 20);
            this.cardTypeText.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(119, 128);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Ability";
            // 
            // abilityText
            // 
            this.abilityText.Location = new System.Drawing.Point(160, 125);
            this.abilityText.Name = "abilityText";
            this.abilityText.ReadOnly = true;
            this.abilityText.Size = new System.Drawing.Size(121, 20);
            this.abilityText.TabIndex = 9;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(119, 154);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "Effect";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(119, 180);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(33, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "Level";
            // 
            // levelText
            // 
            this.levelText.Location = new System.Drawing.Point(160, 177);
            this.levelText.Name = "levelText";
            this.levelText.ReadOnly = true;
            this.levelText.Size = new System.Drawing.Size(60, 20);
            this.levelText.TabIndex = 9;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(119, 206);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(28, 13);
            this.label10.TabIndex = 8;
            this.label10.Text = "ATK";
            // 
            // attackText
            // 
            this.attackText.Location = new System.Drawing.Point(160, 203);
            this.attackText.Name = "attackText";
            this.attackText.ReadOnly = true;
            this.attackText.Size = new System.Drawing.Size(60, 20);
            this.attackText.TabIndex = 9;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(119, 232);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(28, 13);
            this.label11.TabIndex = 8;
            this.label11.Text = "DEF";
            // 
            // defenceText
            // 
            this.defenceText.Location = new System.Drawing.Point(160, 229);
            this.defenceText.Name = "defenceText";
            this.defenceText.ReadOnly = true;
            this.defenceText.Size = new System.Drawing.Size(60, 20);
            this.defenceText.TabIndex = 9;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(119, 258);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(46, 13);
            this.label12.TabIndex = 8;
            this.label12.Text = "Attribute";
            // 
            // attibuteText
            // 
            this.attibuteText.Location = new System.Drawing.Point(171, 255);
            this.attibuteText.Name = "attibuteText";
            this.attibuteText.ReadOnly = true;
            this.attibuteText.Size = new System.Drawing.Size(110, 20);
            this.attibuteText.TabIndex = 9;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(119, 284);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(33, 13);
            this.label13.TabIndex = 8;
            this.label13.Text = "Rank";
            // 
            // rankText
            // 
            this.rankText.Location = new System.Drawing.Point(160, 281);
            this.rankText.Name = "rankText";
            this.rankText.ReadOnly = true;
            this.rankText.Size = new System.Drawing.Size(40, 20);
            this.rankText.TabIndex = 9;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(119, 310);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(81, 13);
            this.label14.TabIndex = 8;
            this.label14.Text = "PendulumScale";
            // 
            // pendulumText
            // 
            this.pendulumText.Location = new System.Drawing.Point(206, 307);
            this.pendulumText.Name = "pendulumText";
            this.pendulumText.ReadOnly = true;
            this.pendulumText.Size = new System.Drawing.Size(75, 20);
            this.pendulumText.TabIndex = 9;
            // 
            // isEffectChk
            // 
            this.isEffectChk.AutoSize = true;
            this.isEffectChk.Enabled = false;
            this.isEffectChk.Location = new System.Drawing.Point(160, 154);
            this.isEffectChk.Name = "isEffectChk";
            this.isEffectChk.Size = new System.Drawing.Size(15, 14);
            this.isEffectChk.TabIndex = 10;
            this.isEffectChk.UseVisualStyleBackColor = true;
            // 
            // cardInfoGroup
            // 
            this.cardInfoGroup.Controls.Add(this.descriptionText);
            this.cardInfoGroup.Controls.Add(this.abilityText);
            this.cardInfoGroup.Controls.Add(this.isEffectChk);
            this.cardInfoGroup.Controls.Add(this.cardPicture);
            this.cardInfoGroup.Controls.Add(this.pendulumText);
            this.cardInfoGroup.Controls.Add(this.label3);
            this.cardInfoGroup.Controls.Add(this.label14);
            this.cardInfoGroup.Controls.Add(this.nameText);
            this.cardInfoGroup.Controls.Add(this.rankText);
            this.cardInfoGroup.Controls.Add(this.label4);
            this.cardInfoGroup.Controls.Add(this.label13);
            this.cardInfoGroup.Controls.Add(this.idText);
            this.cardInfoGroup.Controls.Add(this.attibuteText);
            this.cardInfoGroup.Controls.Add(this.label5);
            this.cardInfoGroup.Controls.Add(this.label12);
            this.cardInfoGroup.Controls.Add(this.typeText);
            this.cardInfoGroup.Controls.Add(this.defenceText);
            this.cardInfoGroup.Controls.Add(this.label6);
            this.cardInfoGroup.Controls.Add(this.label11);
            this.cardInfoGroup.Controls.Add(this.cardTypeText);
            this.cardInfoGroup.Controls.Add(this.attackText);
            this.cardInfoGroup.Controls.Add(this.label7);
            this.cardInfoGroup.Controls.Add(this.label10);
            this.cardInfoGroup.Controls.Add(this.label8);
            this.cardInfoGroup.Controls.Add(this.levelText);
            this.cardInfoGroup.Controls.Add(this.label9);
            this.cardInfoGroup.Location = new System.Drawing.Point(231, 22);
            this.cardInfoGroup.Name = "cardInfoGroup";
            this.cardInfoGroup.Size = new System.Drawing.Size(293, 337);
            this.cardInfoGroup.TabIndex = 11;
            this.cardInfoGroup.TabStop = false;
            this.cardInfoGroup.Text = "Card Info";
            // 
            // descriptionText
            // 
            this.descriptionText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.descriptionText.Location = new System.Drawing.Point(12, 175);
            this.descriptionText.Name = "descriptionText";
            this.descriptionText.ReadOnly = true;
            this.descriptionText.Size = new System.Drawing.Size(100, 148);
            this.descriptionText.TabIndex = 11;
            this.descriptionText.Text = "";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.richTextBox);
            this.groupBox2.Location = new System.Drawing.Point(541, 22);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(231, 337);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Data on Web";
            // 
            // editBtn
            // 
            this.editBtn.Location = new System.Drawing.Point(449, 377);
            this.editBtn.Name = "editBtn";
            this.editBtn.Size = new System.Drawing.Size(75, 23);
            this.editBtn.TabIndex = 0;
            this.editBtn.Text = "Edit";
            this.editBtn.UseVisualStyleBackColor = true;
            this.editBtn.Click += new System.EventHandler(this.editBtn_Click);
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(368, 377);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(75, 23);
            this.saveBtn.TabIndex = 0;
            this.saveBtn.Text = "Save";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // getAllBtn
            // 
            this.getAllBtn.Location = new System.Drawing.Point(616, 377);
            this.getAllBtn.Name = "getAllBtn";
            this.getAllBtn.Size = new System.Drawing.Size(75, 23);
            this.getAllBtn.TabIndex = 0;
            this.getAllBtn.Text = "Get All";
            this.getAllBtn.UseVisualStyleBackColor = true;
            this.getAllBtn.Click += new System.EventHandler(this.getAllBtn_Click);
            // 
            // monsterTableAdapter
            // 
            this.monsterTableAdapter.ClearBeforeFill = true;
            // 
            // trapTableAdapter
            // 
            this.trapTableAdapter.ClearBeforeFill = true;
            // 
            // spellTableAdapter
            // 
            this.spellTableAdapter.ClearBeforeFill = true;
            // 
            // cdbDataSet
            // 
            this.cdbDataSet.DataSetName = "CDBDataSet";
            this.cdbDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dataSourceTableAdapter
            // 
            this.dataSourceTableAdapter.ClearBeforeFill = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cardList);
            this.groupBox1.Location = new System.Drawing.Point(15, 21);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 338);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Card List";
            // 
            // cardList
            // 
            this.cardList.AllowUserToAddRows = false;
            this.cardList.AllowUserToDeleteRows = false;
            this.cardList.AllowUserToResizeColumns = false;
            this.cardList.AllowUserToResizeRows = false;
            this.cardList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.cardList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCardId,
            this.colStatus});
            this.cardList.Location = new System.Drawing.Point(6, 22);
            this.cardList.MultiSelect = false;
            this.cardList.Name = "cardList";
            this.cardList.ReadOnly = true;
            this.cardList.RowHeadersVisible = false;
            this.cardList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.cardList.Size = new System.Drawing.Size(188, 306);
            this.cardList.TabIndex = 0;
            this.cardList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.cardList_CellClick);
            // 
            // colCardId
            // 
            this.colCardId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colCardId.HeaderText = "Card";
            this.colCardId.Name = "colCardId";
            this.colCardId.ReadOnly = true;
            this.colCardId.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // colStatus
            // 
            this.colStatus.HeaderText = "";
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            this.colStatus.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colStatus.Width = 30;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 451);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.cardInfoGroup);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cardBrowserBtn);
            this.Controls.Add(this.cardFolderPathText);
            this.Controls.Add(this.getAllBtn);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.editBtn);
            this.Controls.Add(this.getCardBtn);
            this.Name = "Form1";
            this.Text = "Card Editor";
            ((System.ComponentModel.ISupportInitialize)(this.cardPicture)).EndInit();
            this.cardInfoGroup.ResumeLayout(false);
            this.cardInfoGroup.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cdbDataSet)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cardList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button getCardBtn;
        private System.Windows.Forms.TextBox cardFolderPathText;
        private System.Windows.Forms.Button cardBrowserBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.RichTextBox richTextBox;
        private System.Windows.Forms.PictureBox cardPicture;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox nameText;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox idText;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox typeText;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox cardTypeText;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox abilityText;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox levelText;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox attackText;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox defenceText;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox attibuteText;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox rankText;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox pendulumText;
        private System.Windows.Forms.CheckBox isEffectChk;
        private System.Windows.Forms.GroupBox cardInfoGroup;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button editBtn;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.Button getAllBtn;
        private CDBDataSetTableAdapters.MonsterTableAdapter monsterTableAdapter;
        private CDBDataSetTableAdapters.TrapTableAdapter trapTableAdapter;
        private CDBDataSetTableAdapters.SpellTableAdapter spellTableAdapter;
        private CDBDataSet cdbDataSet;
        private CDBDataSetTableAdapters.DataSourceTableAdapter dataSourceTableAdapter;
        private System.Windows.Forms.RichTextBox descriptionText;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView cardList;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCardId;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colStatus;
    }
}

