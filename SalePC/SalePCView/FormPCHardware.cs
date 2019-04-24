using SalePCServiceDAL.Interfaces;
using SalePCServiceDAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SalePCView
{
    public partial class FormPCHardware : Form
    {
        public PCHardwareViewModel Model
        {
            set { model = value; }
            get
            {
                return model;
            }
        }
        private PCHardwareViewModel model;

        public FormPCHardware()
        {
            InitializeComponent();
        }
        private void FormPCHardware_Load(object sender, EventArgs e)
        {
            try
            {
                List<HardwareViewModel> list = APIClient.GetRequest<List<HardwareViewModel>>("api/Hardware/GetList");
                if (list != null)
                {
                    comboBoxHardware.DisplayMember = "HardwareName";
                    comboBoxHardware.ValueMember = "Id";
                    comboBoxHardware.DataSource = list;
                    comboBoxHardware.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
            if (model != null)
            {
                comboBoxHardware.Enabled = false;
                comboBoxHardware.SelectedValue = model.HardwareId;
                textBoxCount.Text = model.Count.ToString();
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
            try
            {
                if (model == null)
                {
                    model = new PCHardwareViewModel
                    {
                        HardwareId = Convert.ToInt32(comboBoxHardware.SelectedValue),
                        HardwareNames = comboBoxHardware.Text,
                        Count = Convert.ToInt32(textBoxCount.Text)
                    };
                }
                else
                {
                    model.Count = Convert.ToInt32(textBoxCount.Text);
                }
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