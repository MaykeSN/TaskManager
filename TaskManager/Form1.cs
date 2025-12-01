using System;
using System.Linq;
using System.Windows.Forms;

namespace TaskManager
{
    public partial class Form1 : Form
    {
        private readonly TaskRepository _repo;

        public Form1()
        {
            InitializeComponent();
            _repo = new TaskRepository();
            cmbStatus.SelectedIndex = 0;
            LoadTasks();

            // Eventos
            btnCreate.Click += BtnCreate_Click;
            btnEdit.Click += BtnEdit_Click;
            btnDelete.Click += BtnDelete_Click;
            dgvTasks.SelectionChanged += DgvTasks_SelectionChanged;
        }

        private void LoadTasks()
        {
            var tasks = _repo.GetAll();
            dgvTasks.DataSource = tasks;
        }

        private Task GetSelectedTask()
        {
            if (dgvTasks.SelectedRows.Count == 0) return null;
            return dgvTasks.SelectedRows[0].DataBoundItem as Task;
        }

        private void ClearInputs()
        {
            txtTitle.Clear();
            txtDescription.Clear();
            cmbStatus.SelectedIndex = 0;
        }

        private void BtnCreate_Click(object sender, EventArgs e)
        {
            var task = new Task(txtTitle.Text, txtDescription.Text)
            {
                Status = cmbStatus.SelectedItem?.ToString() ?? "PENDENTE"
            };
            _repo.Add(task);
            LoadTasks();
            ClearInputs();
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            var task = GetSelectedTask();
            if (task == null) return;

            task.Title = txtTitle.Text;
            task.Description = txtDescription.Text;
            task.Status = cmbStatus.SelectedItem?.ToString() ?? task.Status;

            _repo.Update(task);
            LoadTasks();
            ClearInputs();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            var task = GetSelectedTask();
            if (task == null) return;

            _repo.Delete(task.Id);
            LoadTasks();
            ClearInputs();
        }

        private void DgvTasks_SelectionChanged(object sender, EventArgs e)
        {
            var task = GetSelectedTask();
            if (task == null) return;

            txtTitle.Text = task.Title;
            txtDescription.Text = task.Description;
            cmbStatus.SelectedItem = task.Status;
        }
    }
}
