using SalePCServiceDAL.BindingModels;
using SalePCServiceDAL.Interfaces;
using SalePCServiceDAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SalePCView
{
    public partial class FormPutOnStock : Form
    {

        public FormPutOnStock()
        {
            InitializeComponent();
        }
        private void FormPutOnStock_Load(object sender, EventArgs e)
        {
            try
            {
                List<HardwareViewModel> listI = APIClient.GetRequest<List<HardwareViewModel>>("api/Hardware/GetList");
                if (listI != null)
                {
                    comboBoxHardware.DisplayMember = "HardwareName";
                    comboBoxHardware.ValueMember = "Id";
                    comboBoxHardware.DataSource = listI;
                    comboBoxHardware.SelectedItem = null;
                }
                List<StockViewModel> listS = APIClient.GetRequest<List<StockViewModel>>("api/Stock/GetList");
                if (listS != null)
                {
                    comboBoxStock.DisplayMember = "StockName";
                    comboBoxStock.ValueMember = "Id";
                    comboBoxStock.DataSource = listS;
                    comboBoxStock.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxHardware.SelectedValue == null)
            {
                MessageBox.Show("Выберите компонент", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (comboBoxStock.SelectedValue == null)
            {
                MessageBox.Show("Выберите склад", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                APIClient.PostRequest<StockHardwareBindingModel, bool>("api/Main/PutComponentOnStock", new StockHardwareBindingModel
                {
                    HardwareId = Convert.ToInt32(comboBoxHardware.SelectedValue),
                    StockId = Convert.ToInt32(comboBoxStock.SelectedValue),
                    Count = Convert.ToInt32(textBoxCount.Text)
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение",
               MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}