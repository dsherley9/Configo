using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            InitializeConfigTypesComboBox();
        }

        private void InitializeConfigTypesComboBox()
        {
            cmbJsonTypes.DataSource = Constants.JSONTypes;
        }

        private void InitializeBoundToDropdown()
        {
            cmbBoundTo.DataSource = _activeSheet.Data.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToList();
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

        private void BtnAddNode_Click(object sender, EventArgs e)
        {
            var node = new ExcelNode()
            {
                PropertyIsBoundToColumn = chkBoundTo.Checked,
                Property = cmbProperty.Text,
                PropertyType = (string)cmbJsonTypes.SelectedItem,
                Value = (string)cmbBoundTo.SelectedItem,
                ExcelSheet = _activeSheet
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
        }

        private void ConfigTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeView tree = (TreeView)sender;
            var node = (ExcelNode)tree.SelectedNode;
            return;
        }

        private void ChangePropertyDropDownState(bool isBoundEnabled)
        {
            cmbProperty.DropDownStyle = isBoundEnabled ? ComboBoxStyle.DropDown : ComboBoxStyle.Simple;
            cmbProperty.DataSource = isBoundEnabled ? _activeSheet?.Data.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToList() : null;
        }

        private void ChangeBoundToDropDownState(bool shouldDisplay)
        {
            lblBoundTo.Visible = shouldDisplay;
            cmbBoundTo.Visible = shouldDisplay;
            
            if (!shouldDisplay)
            {
                cmbBoundTo.DataSource = null;
                cmbBoundTo.Text = null;
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
            var selectedType = comboBox.SelectedItem;
            var acceptsBindings = Constants.AcceptsValues.Contains(selectedType);
            ChangeBoundToDropDownState(acceptsBindings);
        }

        private void BtnViewJson_Click(object sender, EventArgs e)
        {
            var jsonForm = new JSONForm(this);
            jsonForm.Show();
        }
    }
}
