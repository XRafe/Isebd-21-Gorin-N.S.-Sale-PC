using SalePCServiceDAL.Interfaces;
using SalePCServiceDAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;


namespace SalePCView
{
    public partial class FormSalePCHardware : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public PCHardwareViewModel Model
        {
            set { model = value; }
            get
            {
                return model;
            }
        }
        private readonly IStockService service;
        private PCHardwareViewModel model;
        public FormSalePCHardware(IStockService service)
        {
            InitializeComponent();
            this.service = service;
        }
        private void FormSalePCHardware_Load(object sender, EventArgs e)
        {
            try
            {
                List<StockViewModel> list = service.GetList();
                if (list != null)
                {
                    comboBoxHardware.DisplayMember = "StockName";
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
                MessageBox.Show("Заполните поле количество", "Ошибка",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxHardware.SelectedValue == null)
            {
                MessageBox.Show("Выберите запчасть", "Ошибка", MessageBoxButtons.OK,
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
