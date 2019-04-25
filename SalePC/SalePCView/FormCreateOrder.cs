using SalePCServiceDAL.BindingModels;
using SalePCServiceDAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SalePCView
{
    public partial class FormCreateOrder : Form
    {
        public FormCreateOrder()
        {
            InitializeComponent();
        }
        private void FormCreateOrder_Load(object sender, EventArgs e)
        {
            try
            {
                List<ClientViewModel> listC = APIClient.GetRequest<List<ClientViewModel>>("api/Client/GetList");
                comboBoxClient.DisplayMember = "ClientFIO";
                comboBoxClient.ValueMember = "Id";
                comboBoxClient.DataSource = listC;
                comboBoxClient.SelectedItem = null;

                List<PCViewModel> listPC = APIClient.GetRequest<List<PCViewModel>>("api/PC/GetList");
                comboBoxSalePC.DisplayMember = "PCName";
                comboBoxSalePC.ValueMember = "Id";
                comboBoxSalePC.DataSource = listPC;
                comboBoxSalePC.SelectedItem = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
        private void CalcSum()
        {
            if (comboBoxSalePC.SelectedValue != null &&
           !string.IsNullOrEmpty(textBoxCount.Text))
            {
                try
                {
                    int id = Convert.ToInt32(comboBoxSalePC.SelectedValue);
                    PCViewModel PC = APIClient.GetRequest<PCViewModel>("api/PC/Get/" + id);
                    int count = Convert.ToInt32(textBoxCount.Text);
                    textBoxSum.Text = (count * PC.Price).ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
        }
        private void textBoxCount_TextChanged(object sender, EventArgs e)
        {
            CalcSum();
        }
        private void comboBoxPC_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcSum();
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxClient.SelectedValue == null)
            {
                MessageBox.Show("Выберите заказчика", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (comboBoxSalePC.SelectedValue == null)
            {
                MessageBox.Show("Выберите комплектующие", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                APIClient.PostRequest<OrderBindingModel,
                bool>("api/Client/UpdElement", new OrderBindingModel
                {
                    ClientId = Convert.ToInt32(comboBoxClient.SelectedValue),
                    PCId = Convert.ToInt32(comboBoxSalePC.SelectedValue),
                    Count = Convert.ToInt32(textBoxCount.Text),
                    Sum = 10000
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