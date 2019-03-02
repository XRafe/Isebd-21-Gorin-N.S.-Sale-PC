using SalePCServiceDAL.BindingModels;
using SalePCServiceDAL.Interfaces;
using SalePCServiceDAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;


namespace SalePCView
{
    public partial class FormPC : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public int Id { set { id = value; } }
        private readonly IPCService service;
        private int? id;
        private List<PCHardwareViewModel> PCHardwares;
        public FormPC(IPCService service)
        {
            InitializeComponent();
            this.service = service;
        }
        private void FormSalePC_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    PCViewModel view = service.GetElement(id.Value);
                    if (view != null)
                    {
                        textBoxName.Text = view.PCName;
                        textBoxPrice.Text = view.Price.ToString();
                        PCHardwares = view.PCHardwares;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
            else
            {
                PCHardwares = new List<PCHardwareViewModel>();
            }
        }
        private void LoadData()
        {
            try
            {
                if (PCHardwares != null)
                {
                    dataGridView.DataSource = null;
                    dataGridView.DataSource = PCHardwares;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[1].Visible = false;
                    dataGridView.Columns[2].Visible = false;
                    dataGridView.Columns[3].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormSalePCHardware>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (form.Model != null)
                {
                    if (id.HasValue)
                    {
                        form.Model.PCId = id.Value;
                    }
                    PCHardwares.Add(form.Model);
                }
                LoadData();
            }
        }
        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = Container.Resolve<FormSalePCHardware>();
                form.Model =
               PCHardwares[dataGridView.SelectedRows[0].Cells[0].RowIndex];
                if (form.ShowDialog() == DialogResult.OK)
                {
                    PCHardwares[dataGridView.SelectedRows[0].Cells[0].RowIndex] =
                   form.Model;
                    LoadData();
                }
            }
        }
        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo,
               MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {

                        PCHardwares.RemoveAt(dataGridView.SelectedRows[0].Cells[0].RowIndex);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                       MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }
        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (PCHardwares == null || PCHardwares.Count == 0)
            {
                MessageBox.Show("Заполните комплектующие", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                List<PCHardwareBindingModel> SalePCHardwareBM = new List<PCHardwareBindingModel>();
                for (int i = 0; i < PCHardwares.Count; ++i)
                {
                    SalePCHardwareBM.Add(new PCHardwareBindingModel
                    {
                        Id = PCHardwares[i].Id,
                        PCId = PCHardwares[i].PCId,
                        HardwareId = PCHardwares[i].HardwareId,
                        Count = PCHardwares[i].Count
                    });
                }
                if (id.HasValue)
                {
                    service.UpdElement(new PCBindingModel
                    {
                        Id = id.Value,
                        PCName = textBoxName.Text,
                        Price = Convert.ToInt32(textBoxPrice.Text),
                        PCHardwares = SalePCHardwareBM
                    });
                }
                else
                {
                    service.AddElement(new PCBindingModel
                    {
                        PCName = textBoxName.Text,
                        Price = Convert.ToInt32(textBoxPrice.Text),
                        PCHardwares = SalePCHardwareBM
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
