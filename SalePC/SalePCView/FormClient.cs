using SalePCServiceDAL.BindingModels;
using SalePCServiceDAL.ViewModels;
using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;
namespace SalePCView
{
    public partial class FormClient : Form
    {
        public int Id { set { id = value; } }
        private int? id;
        public FormClient()
        {
            InitializeComponent();
        }
        private void FormClient_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    ClientViewModel client =
                   APIClient.GetRequest<ClientViewModel>("api/Client/Get/" + id.Value);
                    textBoxFIO.Text = client.ClientFIO;
                    textBoxMail.Text = client.Mail;
                    dataGridView.DataSource = client.Messages;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[1].Visible = false;
                    dataGridView.Columns[4].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.Fill;
                }
                catch (Exception ex)
                {
                    while (ex.InnerException != null)
                    {
                        ex = ex.InnerException;
                    }
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }
            }
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxFIO.Text))
            {
                MessageBox.Show("Заполните ФИО", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            string fio = textBoxFIO.Text;
            string mail = textBoxMail.Text;
            if (!string.IsNullOrEmpty(mail))
            {
                if (!Regex.IsMatch(mail, @"(\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)"))
                {
                    MessageBox.Show("Неверный формат для электронной почты", "Ошибка",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            if (id.HasValue)
            {
                APIClient.PostRequest<ClientBindingModel,
               bool>("api/Client/UpdElement", new ClientBindingModel
               {
                   Id = id.Value,
                   ClientFIO = fio,
                   Mail = mail
               });
            }
            else
            {
                APIClient.PostRequest<ClientBindingModel,
               bool>("api/Client/AddElement", new ClientBindingModel
               {
                   ClientFIO = fio,
                   Mail = mail
               });
            }
            MessageBox.Show("Сохранение прошло успешно", "Сообщение",
           MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;
            Close();
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
