using SalePCServiceDAL.BindingModels;
using SalePCServiceDAL.Interfaces;
using SalePCServiceDAL.ViewModels;
using System;
using System.Windows.Forms;

namespace SalePCView
{
    public partial class FormHardware : Form
    {

        public int Id { set { id = value; } }
        private int? id;
        public FormHardware()
        {
            InitializeComponent();
        }

        private void FormHardware_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    HardwareViewModel view = APIClient.GetRequest<HardwareViewModel>("api/Hardware/Get/" + id.Value);
                    textBoxHardwareName.Text = view.HardwareName;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxHardwareName.Text))
            {
                MessageBox.Show("Заполните название ингредиента", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (id.HasValue)
                {
                    APIClient.PostRequest<HardwareBindingModel,
                    bool>("api/Hardware/UpdElement", new HardwareBindingModel
                    {
                        Id = id.Value,
                        HardwareName = textBoxHardwareName.Text
                    });
                }
                else
                {
                    APIClient.PostRequest<HardwareBindingModel, bool>("api/Hardware/AddElement", new HardwareBindingModel
                    {
                        HardwareName = textBoxHardwareName.Text
                    });
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