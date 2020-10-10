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
            this.cmbValueType = new System.Windows.Forms.ComboBox();
            this.lblValueType = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbValue = new System.Windows.Forms.ComboBox();
            this.lblBoundTo = new System.Windows.Forms.Label();
            this.cmbProperty = new System.Windows.Forms.ComboBox();
            this.chkPropertyBoundTo = new System.Windows.Forms.CheckBox();
            this.btnDeselectNode = new System.Windows.Forms.Button();
            this.lblSelectNodeTitle = new System.Windows.Forms.Label();
            this.lblSelectedNode = new System.Windows.Forms.Label();
            this.lblNodeType = new System.Windows.Forms.Label();
            this.cmbNodeType = new System.Windows.Forms.ComboBox();
            this.chkValueBoundTo = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvData
            // 
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Location = new System.Drawing.Point(652, 85);
            this.dgvData.Name = "dgvData";
            this.dgvData.Size = new System.Drawing.Size(592, 559);
            this.dgvData.TabIndex = 5;
            // 
            // btnChooseFile
            // 
            this.btnChooseFile.Location = new System.Drawing.Point(652, 28);
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
            this.cmbSheetNames.Location = new System.Drawing.Point(974, 35);
            this.cmbSheetNames.Name = "cmbSheetNames";
            this.cmbSheetNames.Size = new System.Drawing.Size(269, 21);
            this.cmbSheetNames.TabIndex = 6;
            this.cmbSheetNames.SelectedIndexChanged += new System.EventHandler(this.CmbSheetNames_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(925, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Sheets:";
            // 
            // configTree
            // 
            this.configTree.Location = new System.Drawing.Point(18, 176);
            this.configTree.Name = "configTree";
            this.configTree.Size = new System.Drawing.Size(589, 423);
            this.configTree.TabIndex = 8;
            this.configTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.ConfigTree_AfterSelect);
            // 
            // btnAddNode
            // 
            this.btnAddNode.Location = new System.Drawing.Point(428, 35);
            this.btnAddNode.Name = "btnAddNode";
            this.btnAddNode.Size = new System.Drawing.Size(179, 68);
            this.btnAddNode.TabIndex = 9;
            this.btnAddNode.Text = "Add Node";
            this.btnAddNode.UseVisualStyleBackColor = true;
            this.btnAddNode.Click += new System.EventHandler(this.BtnAddNode_Click);
            // 
            // btnViewJson
            // 
            this.btnViewJson.Location = new System.Drawing.Point(18, 605);
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
            this.btnDeleteNode.Location = new System.Drawing.Point(320, 142);
            this.btnDeleteNode.Name = "btnDeleteNode";
            this.btnDeleteNode.Size = new System.Drawing.Size(287, 28);
            this.btnDeleteNode.TabIndex = 11;
            this.btnDeleteNode.Text = "Delete Node";
            this.btnDeleteNode.UseVisualStyleBackColor = true;
            this.btnDeleteNode.Click += new System.EventHandler(this.btnDeleteNode_Click);
            // 
            // cmbValueType
            // 
            this.cmbValueType.FormattingEnabled = true;
            this.cmbValueType.Location = new System.Drawing.Point(281, 82);
            this.cmbValueType.Name = "cmbValueType";
            this.cmbValueType.Size = new System.Drawing.Size(141, 21);
            this.cmbValueType.TabIndex = 12;
            this.cmbValueType.SelectedIndexChanged += new System.EventHandler(this.CmbJsonTypes_SelectedIndexChanged);
            // 
            // lblValueType
            // 
            this.lblValueType.AutoSize = true;
            this.lblValueType.Location = new System.Drawing.Point(278, 66);
            this.lblValueType.Name = "lblValueType";
            this.lblValueType.Size = new System.Drawing.Size(64, 13);
            this.lblValueType.TabIndex = 13;
            this.lblValueType.Text = "Value Type:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Property:";
            // 
            // cmbValue
            // 
            this.cmbValue.FormattingEnabled = true;
            this.cmbValue.Location = new System.Drawing.Point(21, 82);
            this.cmbValue.Name = "cmbValue";
            this.cmbValue.Size = new System.Drawing.Size(254, 21);
            this.cmbValue.TabIndex = 15;
            // 
            // lblBoundTo
            // 
            this.lblBoundTo.AutoSize = true;
            this.lblBoundTo.Location = new System.Drawing.Point(18, 63);
            this.lblBoundTo.Name = "lblBoundTo";
            this.lblBoundTo.Size = new System.Drawing.Size(37, 13);
            this.lblBoundTo.TabIndex = 16;
            this.lblBoundTo.Text = "Value:";
            // 
            // cmbProperty
            // 
            this.cmbProperty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cmbProperty.FormattingEnabled = true;
            this.cmbProperty.Location = new System.Drawing.Point(21, 35);
            this.cmbProperty.Name = "cmbProperty";
            this.cmbProperty.Size = new System.Drawing.Size(254, 21);
            this.cmbProperty.TabIndex = 18;
            // 
            // chkPropertyBoundTo
            // 
            this.chkPropertyBoundTo.AutoSize = true;
            this.chkPropertyBoundTo.Enabled = false;
            this.chkPropertyBoundTo.Location = new System.Drawing.Point(73, 14);
            this.chkPropertyBoundTo.Name = "chkPropertyBoundTo";
            this.chkPropertyBoundTo.Size = new System.Drawing.Size(76, 17);
            this.chkPropertyBoundTo.TabIndex = 19;
            this.chkPropertyBoundTo.Text = "Bound To:";
            this.chkPropertyBoundTo.UseVisualStyleBackColor = true;
            this.chkPropertyBoundTo.Visible = false;
            this.chkPropertyBoundTo.CheckedChanged += new System.EventHandler(this.ChkBoundTo_CheckedChanged);
            // 
            // btnDeselectNode
            // 
            this.btnDeselectNode.Enabled = false;
            this.btnDeselectNode.Location = new System.Drawing.Point(18, 142);
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
            this.lblSelectNodeTitle.Location = new System.Drawing.Point(17, 119);
            this.lblSelectNodeTitle.Name = "lblSelectNodeTitle";
            this.lblSelectNodeTitle.Size = new System.Drawing.Size(118, 20);
            this.lblSelectNodeTitle.TabIndex = 21;
            this.lblSelectNodeTitle.Text = "Selected Node:";
            // 
            // lblSelectedNode
            // 
            this.lblSelectedNode.AutoSize = true;
            this.lblSelectedNode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedNode.Location = new System.Drawing.Point(141, 119);
            this.lblSelectedNode.Name = "lblSelectedNode";
            this.lblSelectedNode.Size = new System.Drawing.Size(44, 20);
            this.lblSelectedNode.TabIndex = 22;
            this.lblSelectedNode.Text = "Root";
            // 
            // lblNodeType
            // 
            this.lblNodeType.AutoSize = true;
            this.lblNodeType.Location = new System.Drawing.Point(278, 14);
            this.lblNodeType.Name = "lblNodeType";
            this.lblNodeType.Size = new System.Drawing.Size(63, 13);
            this.lblNodeType.TabIndex = 24;
            this.lblNodeType.Text = "Node Type:";
            // 
            // cmbNodeType
            // 
            this.cmbNodeType.FormattingEnabled = true;
            this.cmbNodeType.Location = new System.Drawing.Point(281, 34);
            this.cmbNodeType.Name = "cmbNodeType";
            this.cmbNodeType.Size = new System.Drawing.Size(141, 21);
            this.cmbNodeType.TabIndex = 23;
            // 
            // chkValueBoundTo
            // 
            this.chkValueBoundTo.AutoSize = true;
            this.chkValueBoundTo.Enabled = false;
            this.chkValueBoundTo.Location = new System.Drawing.Point(73, 63);
            this.chkValueBoundTo.Name = "chkValueBoundTo";
            this.chkValueBoundTo.Size = new System.Drawing.Size(76, 17);
            this.chkValueBoundTo.TabIndex = 25;
            this.chkValueBoundTo.Text = "Bound To:";
            this.chkValueBoundTo.UseVisualStyleBackColor = true;
            this.chkValueBoundTo.Visible = false;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 658);
            this.Controls.Add(this.chkValueBoundTo);
            this.Controls.Add(this.lblNodeType);
            this.Controls.Add(this.cmbNodeType);
            this.Controls.Add(this.lblSelectedNode);
            this.Controls.Add(this.lblSelectNodeTitle);
            this.Controls.Add(this.btnDeselectNode);
            this.Controls.Add(this.chkPropertyBoundTo);
            this.Controls.Add(this.cmbProperty);
            this.Controls.Add(this.lblBoundTo);
            this.Controls.Add(this.cmbValue);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblValueType);
            this.Controls.Add(this.cmbValueType);
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
        private System.Windows.Forms.ComboBox cmbValueType;
        private System.Windows.Forms.Label lblValueType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbValue;
        private System.Windows.Forms.Label lblBoundTo;
        private System.Windows.Forms.ComboBox cmbProperty;
        private System.Windows.Forms.CheckBox chkPropertyBoundTo;
        private System.Windows.Forms.Button btnDeselectNode;
        private System.Windows.Forms.Label lblSelectNodeTitle;
        private System.Windows.Forms.Label lblSelectedNode;
        private System.Windows.Forms.Label lblNodeType;
        private System.Windows.Forms.ComboBox cmbNodeType;
        private System.Windows.Forms.CheckBox chkValueBoundTo;
    }
}