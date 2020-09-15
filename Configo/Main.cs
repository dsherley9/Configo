using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Configuration;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Configo
{
    public partial class Main : Form
    {

        private ExcelDao _excelDao;
        private ExcelWorkbook _excelWorkbook;
        private ExcelSheet _activeSheet;
        public TreeView ConfigTree => configTree;


        public Main(ExcelDao excelDao)
        {
            InitializeComponent();
            InitializeFormFields();
            _excelDao = excelDao;
        }

        private void InitializeFormFields()
        {
            InitializeValueTypesComboBox();
            InitializeNodeTypesComboBox();
        }

        private void InitializeValueTypesComboBox()
        {
            cmbValueType.DataSource = Enum.GetNames(typeof(JSONType)).Where(x => x != JSONType.none.ToString()).ToList();
        }

        private void InitializeNodeTypesComboBox()
        {
            cmbNodeType.DataSource = Enum.GetNames(typeof(NodeType)).Where(x => x != JSONType.none.ToString()).ToList();
        }

        private void InitializeBoundToDropdown()
        {
            cmbValue.DataSource = _activeSheet.Data.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToList();
            ChangePropertyDropDownState(chkBoundTo.Checked);
        }

        private void btnChooseFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog(); 
            if (file.ShowDialog() != DialogResult.OK) { return; }

            var selectedFile = new ExcelWorkbook()
            {
                Path = file.FileName,
                Name = Path.GetFileNameWithoutExtension(file.FileName),
                Extension = Path.GetExtension(file.FileName)
            };

            if (!selectedFile.IsValid())
            {
                string acceptableExts = Constants.AcceptableExtensions.Aggregate("", (acc, ext) =>
                {
                    if (!string.IsNullOrWhiteSpace(acc)) { acc += " \n "; }
                    acc += $"\u2022 {ext}";
                    return acc;
                });
                MessageBox.Show($"File type ({selectedFile.Extension}) is not supported. The following file types are supported: \n {acceptableExts}", "Unsupported File Type", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                _excelWorkbook = selectedFile;
               PopulateSheetNames();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex?.Message?.ToString());
            }
        }

        private void PopulateDGV()
        {
            dgvData.Visible = true;
            dgvData.DataSource = _activeSheet.Data;
        }

        private void PopulateSheetNames()
        {
            _excelWorkbook.ExcelSheets = _excelDao.GetSheets(_excelWorkbook.Path, _excelWorkbook.Extension);
            cmbSheetNames.DataSource = _excelWorkbook.ExcelSheets;
        }

        private void CmbSheetNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            _activeSheet = (ExcelSheet)comboBox.SelectedItem;
            _activeSheet.Data = _excelDao.GetExcelSheetContents(_excelWorkbook.Path, _excelWorkbook.Extension, $"SELECT * FROM [{_activeSheet.Name}]");
            PopulateDGV();
            InitializeBoundToDropdown();
        }

        private bool ValidInput()
        {
            bool validInput = true;

            // Will instead move validation to Node maybe??
            var selectedNode = configTree?.SelectedNode as ConfigNode;

            if ((selectedNode == null || selectedNode.PropertyType != JSONType.array) && string.IsNullOrWhiteSpace(cmbProperty.Text))
            {
                // Will eventually do errors in labels and hide labels once valid.... for now though.
                MessageBox.Show($"JSON object cannot have an empty property value!", "Missing Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                validInput = false;
            }

            if (string.IsNullOrWhiteSpace(cmbValueType.Text))
            {
                MessageBox.Show($"You must select a type!", "Missing Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                validInput = false;
            }

            return validInput;
        }


        private (bool success, string msg) TrySetNodeValues(ConfigNode node)
        {
            node.PropertyIsBound = chkBoundTo.Checked;
            node.Property = cmbProperty.Text;
            node.PropertyType = (JSONType)Enum.Parse(typeof(JSONType), cmbValueType?.SelectedItem?.ToString());
            node.Value = cmbValue?.Text?.ToString();

            return (success: true, msg: null);
        }

        private (bool success, string msg) TrySetNodeValues(ExcelNode node)
        {
            node.PropertyIsBound = chkBoundTo.Checked;
            node.Property = cmbProperty.Text;
            node.PropertyType = (JSONType)Enum.Parse(typeof(JSONType), cmbValueType?.SelectedItem?.ToString());
            node.Value = cmbValue?.Text?.ToString();
            node.ExcelSheet = _activeSheet;

            return (success: false, msg: "You must select a file!");
        }

        private void BtnAddNode_Click(object sender, EventArgs e)
        {
            if (!ValidInput()) { return; }
            var selectedType = ConfigHelper.GetEnumValueByString<NodeType>(cmbNodeType?.Text);
            var node = ConfigHelper.CreateNodeType<ConfigNode>(selectedType);
            dynamic nodeSpecific = Convert.ChangeType(node, node.GetType());

            (bool success, string msg) setNodeValues = TrySetNodeValues(nodeSpecific);
            if (!setNodeValues.success) {
                if (!string.IsNullOrWhiteSpace(setNodeValues.msg))
                {
                    MessageBox.Show(setNodeValues.msg, "Failed! :(", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("An error occured while attempting to create node!", "Failed! :(", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            };

            var parentNode = configTree.SelectedNode as ConfigNode;
            if (parentNode != null)
            {
                if (!parentNode.IsAValidParent())
                {
                    MessageBox.Show($"Type ({node.PropertyType}) cannot be a child of the selected node ({parentNode.PropertyType}) type.", "Invalid Action", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                configTree.SelectedNode.Nodes.Add(node);
            }
            else
            {
                configTree.Nodes.Add(node);
            }

            ResetFormInput();
        }

        private void ResetFormInput()
        {
            cmbValue.Text = "";
            cmbValueType.Text = "";
            cmbProperty.Text = "";
        }

        private void ConfigTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeView tree = (TreeView)sender;
            var selectedNode = tree?.SelectedNode;
            var isNodeSelected = (selectedNode != null);
            ChangeDeletedNodeButtonState(isNodeSelected);
            ChangeDeselectNodeButtonState(isNodeSelected);
            ChangeSelectedNodeLabelState(isNodeSelected, selectedNode);
        }

        private void ChangeSelectedNodeLabelState(bool isNodeSelected, TreeNode node = null)
        {
            if (isNodeSelected)
            {
                lblSelectedNode.Text = node?.Text ?? "Error";
            } 
            else
            {
                lblSelectedNode.Text = "Root";
            }
        }

        private void ChangeDeselectNodeButtonState(bool isNodeSelected)
        {
            if (isNodeSelected)
            {
                btnDeselectNode.Enabled = true;
            }
            else
            {
                btnDeselectNode.Enabled = false;
            }
        }

        private void ChangeDeletedNodeButtonState(bool isNodeSelected)
        {
            if (isNodeSelected)
            {
                btnDeleteNode.BackColor = Color.Coral;
                btnDeleteNode.ForeColor = Color.White;
                btnDeleteNode.Enabled = true;
            }
            else
            {
                btnDeleteNode.BackColor = default; // SystemColors.Control;
                btnDeleteNode.ForeColor = default; // SystemColors.ControlText;
                btnDeleteNode.Enabled = false;
            }
        }

        private void ChangePropertyDropDownState(bool isBoundEnabled)
        {
            cmbProperty.DropDownStyle = isBoundEnabled ? ComboBoxStyle.DropDown : ComboBoxStyle.Simple;
            cmbProperty.DataSource = isBoundEnabled ? _activeSheet?.Data.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToList() : null;
        }

        private void ChangeBoundToDropDownState(bool shouldDisplay)
        {
            lblBoundTo.Visible = shouldDisplay;
            cmbValue.Visible = shouldDisplay;
            
            if (!shouldDisplay)
            {
                cmbValue.DataSource = null;
                cmbValue.Text = null;
            }
        }

        private void ChkBoundTo_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = (CheckBox)sender;
            ChangePropertyDropDownState(checkBox.Checked);
        }

        private void CmbJsonTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            var comboBox = (ComboBox)sender;
            var selectedType = (JSONType)Enum.Parse(typeof(JSONType), comboBox?.SelectedItem?.ToString());
            var acceptsBindings = Constants.AcceptsValues.Contains(selectedType);
            ChangeBoundToDropDownState(acceptsBindings);
        }

        private void BtnViewJson_Click(object sender, EventArgs e)
        {
            var jsonForm = new JSONForm(this);
            jsonForm.Show();
        }

        private void btnDeleteNode_Click(object sender, EventArgs e)
        {
            var selectedNode = configTree?.SelectedNode;
            if (selectedNode == null)
            {
                MessageBox.Show("Select a node to delete first! ;)", "Nothing Selected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            configTree.Nodes.Remove(selectedNode);
            configTree.SelectedNode = null;
            ChangeDeletedNodeButtonState(false);
            ChangeDeselectNodeButtonState(false);
            ChangeSelectedNodeLabelState(false);
        }

        private void btnDeselectNode_Click(object sender, EventArgs e)
        {
            configTree.SelectedNode = null;
            ChangeDeletedNodeButtonState(false);
            ChangeDeselectNodeButtonState(false);
            ChangeSelectedNodeLabelState(false);
        }
    }
}
