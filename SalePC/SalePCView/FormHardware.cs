using SalePCServiceDAL.BindingModels;
using SalePCServiceDAL.Interfaces;
using SalePCServiceDAL.ViewModels;
using System;
using System.Windows.Forms;
using Unity;

namespace SalePCView
{
    public partial class FormHardware : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }
        private readonly IHardwareService service;
        private int? id;
        public FormHardware(IHardwareService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void FormHardware_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    HardwareViewModel view = service.GetElement(id.Value);
                    if (view != null)
                    {
                        textBoxHardwareName.Text = view.HardwareName;
                    }
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
                MessageBox.Show("Заполните название запчасти", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (id.HasValue)
                {
                    service.UpdElement(new HardwareBindingModel
                    {
                        Id = id.Value,
                        HardwareName = textBoxHardwareName.Text
                    });
                }
                else
                {
                    service.AddElement(new HardwareBindingModel
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
