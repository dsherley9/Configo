namespace Configo
{
    partial class Main
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
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.btnChooseFile = new System.Windows.Forms.Button();
            this.cmbSheetNames = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.configTree = new System.Windows.Forms.TreeView();
            this.btnAddNode = new System.Windows.Forms.Button();
            this.btnViewJson = new System.Windows.Forms.Button();
            this.btnDeleteNode = new System.Windows.Forms.Button();
            this.cmbJsonTypes = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbBoundTo = new System.Windows.Forms.ComboBox();
            this.lblBoundTo = new System.Windows.Forms.Label();
            this.cmbProperty = new System.Windows.Forms.ComboBox();
            this.chkBoundTo = new System.Windows.Forms.CheckBox();
            this.btnDeselectNode = new System.Windows.Forms.Button();
            this.lblSelectNodeTitle = new System.Windows.Forms.Label();
            this.lblSelectedNode = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvData
            // 
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Location = new System.Drawing.Point(28, 83);
            this.dgvData.Name = "dgvData";
            this.dgvData.Size = new System.Drawing.Size(592, 517);
            this.dgvData.TabIndex = 5;
            // 
            // btnChooseFile
            // 
            this.btnChooseFile.Location = new System.Drawing.Point(28, 26);
            this.btnChooseFile.Name = "btnChooseFile";
            this.btnChooseFile.Size = new System.Drawing.Size(145, 32);
            this.btnChooseFile.TabIndex = 3;
            this.btnChooseFile.Text = "Choose and Read File";
            this.btnChooseFile.UseVisualStyleBackColor = true;
            this.btnChooseFile.Click += new System.EventHandler(this.btnChooseFile_Click);
            // 
            // cmbSheetNames
            // 
            this.cmbSheetNames.FormattingEnabled = true;
            this.cmbSheetNames.Location = new System.Drawing.Point(350, 33);
            this.cmbSheetNames.Name = "cmbSheetNames";
            this.cmbSheetNames.Size = new System.Drawing.Size(269, 21);
            this.cmbSheetNames.TabIndex = 6;
            this.cmbSheetNames.SelectedIndexChanged += new System.EventHandler(this.CmbSheetNames_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(301, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Sheets:";
            // 
            // configTree
            // 
            this.configTree.Location = new System.Drawing.Point(685, 177);
            this.configTree.Name = "configTree";
            this.configTree.Size = new System.Drawing.Size(589, 423);
            this.configTree.TabIndex = 8;
            this.configTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.ConfigTree_AfterSelect);
            // 
            // btnAddNode
            // 
            this.btnAddNode.Location = new System.Drawing.Point(1095, 36);
            this.btnAddNode.Name = "btnAddNode";
            this.btnAddNode.Size = new System.Drawing.Size(179, 68);
            this.btnAddNode.TabIndex = 9;
            this.btnAddNode.Text = "Add Node";
            this.btnAddNode.UseVisualStyleBackColor = true;
            this.btnAddNode.Click += new System.EventHandler(this.BtnAddNode_Click);
            // 
            // btnViewJson
            // 
            this.btnViewJson.Location = new System.Drawing.Point(685, 606);
            this.btnViewJson.Name = "btnViewJson";
            this.btnViewJson.Size = new System.Drawing.Size(589, 39);
            this.btnViewJson.TabIndex = 10;
            this.btnViewJson.Text = "View JSON";
            this.btnViewJson.UseVisualStyleBackColor = true;
            this.btnViewJson.Click += new System.EventHandler(this.BtnViewJson_Click);
            // 
            // btnDeleteNode
            // 
            this.btnDeleteNode.Enabled = false;
            this.btnDeleteNode.Location = new System.Drawing.Point(987, 143);
            this.btnDeleteNode.Name = "btnDeleteNode";
            this.btnDeleteNode.Size = new System.Drawing.Size(287, 28);
            this.btnDeleteNode.TabIndex = 11;
            this.btnDeleteNode.Text = "Delete Node";
            this.btnDeleteNode.UseVisualStyleBackColor = true;
            this.btnDeleteNode.Click += new System.EventHandler(this.btnDeleteNode_Click);
            // 
            // cmbJsonTypes
            // 
            this.cmbJsonTypes.FormattingEnabled = true;
            this.cmbJsonTypes.Location = new System.Drawing.Point(910, 49);
            this.cmbJsonTypes.Name = "cmbJsonTypes";
            this.cmbJsonTypes.Size = new System.Drawing.Size(179, 21);
            this.cmbJsonTypes.TabIndex = 12;
            this.cmbJsonTypes.SelectedIndexChanged += new System.EventHandler(this.CmbJsonTypes_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(907, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Type:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(682, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Property:";
            // 
            // cmbBoundTo
            // 
            this.cmbBoundTo.FormattingEnabled = true;
            this.cmbBoundTo.Location = new System.Drawing.Point(748, 83);
            this.cmbBoundTo.Name = "cmbBoundTo";
            this.cmbBoundTo.Size = new System.Drawing.Size(341, 21);
            this.cmbBoundTo.TabIndex = 15;
            // 
            // lblBoundTo
            // 
            this.lblBoundTo.AutoSize = true;
            this.lblBoundTo.Location = new System.Drawing.Point(685, 86);
            this.lblBoundTo.Name = "lblBoundTo";
            this.lblBoundTo.Size = new System.Drawing.Size(57, 13);
            this.lblBoundTo.TabIndex = 16;
            this.lblBoundTo.Text = "Bound To:";
            // 
            // cmbProperty
            // 
            this.cmbProperty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cmbProperty.FormattingEnabled = true;
            this.cmbProperty.Location = new System.Drawing.Point(685, 49);
            this.cmbProperty.Name = "cmbProperty";
            this.cmbProperty.Size = new System.Drawing.Size(207, 21);
            this.cmbProperty.TabIndex = 18;
            // 
            // chkBoundTo
            // 
            this.chkBoundTo.AutoSize = true;
            this.chkBoundTo.Location = new System.Drawing.Point(731, 29);
            this.chkBoundTo.Name = "chkBoundTo";
            this.chkBoundTo.Size = new System.Drawing.Size(76, 17);
            this.chkBoundTo.TabIndex = 19;
            this.chkBoundTo.Text = "Bound To:";
            this.chkBoundTo.UseVisualStyleBackColor = true;
            this.chkBoundTo.CheckedChanged += new System.EventHandler(this.ChkBoundTo_CheckedChanged);
            // 
            // btnDeselectNode
            // 
            this.btnDeselectNode.Enabled = false;
            this.btnDeselectNode.Location = new System.Drawing.Point(685, 143);
            this.btnDeselectNode.Name = "btnDeselectNode";
            this.btnDeselectNode.Size = new System.Drawing.Size(287, 28);
            this.btnDeselectNode.TabIndex = 20;
            this.btnDeselectNode.Text = "Deselect Node";
            this.btnDeselectNode.UseVisualStyleBackColor = true;
            this.btnDeselectNode.Click += new System.EventHandler(this.btnDeselectNode_Click);
            // 
            // lblSelectNodeTitle
            // 
            this.lblSelectNodeTitle.AutoSize = true;
            this.lblSelectNodeTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectNodeTitle.Location = new System.Drawing.Point(684, 120);
            this.lblSelectNodeTitle.Name = "lblSelectNodeTitle";
            this.lblSelectNodeTitle.Size = new System.Drawing.Size(118, 20);
            this.lblSelectNodeTitle.TabIndex = 21;
            this.lblSelectNodeTitle.Text = "Selected Node:";
            // 
            // lblSelectedNode
            // 
            this.lblSelectedNode.AutoSize = true;
            this.lblSelectedNode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedNode.Location = new System.Drawing.Point(808, 120);
            this.lblSelectedNode.Name = "lblSelectedNode";
            this.lblSelectedNode.Size = new System.Drawing.Size(44, 20);
            this.lblSelectedNode.TabIndex = 22;
            this.lblSelectedNode.Text = "Root";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1293, 653);
            this.Controls.Add(this.lblSelectedNode);
            this.Controls.Add(this.lblSelectNodeTitle);
            this.Controls.Add(this.btnDeselectNode);
            this.Controls.Add(this.chkBoundTo);
            this.Controls.Add(this.cmbProperty);
            this.Controls.Add(this.lblBoundTo);
            this.Controls.Add(this.cmbBoundTo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbJsonTypes);
            this.Controls.Add(this.btnDeleteNode);
            this.Controls.Add(this.btnViewJson);
            this.Controls.Add(this.btnAddNode);
            this.Controls.Add(this.configTree);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbSheetNames);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.btnChooseFile);
            this.Name = "Main";
            this.Text = "Main";
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.Button btnChooseFile;
        private System.Windows.Forms.ComboBox cmbSheetNames;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TreeView configTree;
        private System.Windows.Forms.Button btnAddNode;
        private System.Windows.Forms.Button btnViewJson;
        private System.Windows.Forms.Button btnDeleteNode;
        private System.Windows.Forms.ComboBox cmbJsonTypes;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbBoundTo;
        private System.Windows.Forms.Label lblBoundTo;
        private System.Windows.Forms.ComboBox cmbProperty;
        private System.Windows.Forms.CheckBox chkBoundTo;
        private System.Windows.Forms.Button btnDeselectNode;
        private System.Windows.Forms.Label lblSelectNodeTitle;
        private System.Windows.Forms.Label lblSelectedNode;
    }
}