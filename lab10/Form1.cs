using System;
using System.IO;
using System.Windows.Forms;

namespace lab10
{
    public partial class lab10 : Form
    {
        private string source_path = "";
        private string target_path = "";
        private ListBox list_box_files;
        private Button button_select_source;
        private Button button_select_target;
        private Button button_copy;
        private Label label_source_path;
        private Label label_target_path;

        public lab10()
        {
            initialize_component();
        }

        private void initialize_component()
        {
            this.Text = "Файловый менеджер";
            this.Size = new System.Drawing.Size(600, 450);
            this.StartPosition = FormStartPosition.CenterScreen;

            list_box_files = new ListBox
            {
                Location = new System.Drawing.Point(12, 90),
                Size = new System.Drawing.Size(560, 200),
                SelectionMode = SelectionMode.MultiExtended
            };

            button_select_source = new Button
            {
                Text = "Выбрать исходную папку",
                Location = new System.Drawing.Point(12, 12),
                Width = 250,
            };
            button_select_source.Click += button_select_source_click;

            button_select_target = new Button
            {
                Text = "Выбрать целевую папку",
                Location = new System.Drawing.Point(270, 12),
                Width = 250,
            };
            button_select_target.Click += button_select_target_click;

            button_copy = new Button
            {
                Text = "Копировать выделенные файлы",
                Location = new System.Drawing.Point(12, 310),
                Width = 560,
                Height = 40,
            };
            button_copy.Click += button_copy_click;

            label_source_path = new Label
            {
                AutoSize = true,
                Location = new System.Drawing.Point(12, 50),
                Text = "Исходная папка: не выбрана"
            };

            label_target_path = new Label
            {
                AutoSize = true,
                Location = new System.Drawing.Point(12, 70),
                Text = "Целевая папка: не выбрана"
            };

            Controls.Add(list_box_files);
            Controls.Add(button_select_source);
            Controls.Add(button_select_target);
            Controls.Add(button_copy);
            Controls.Add(label_source_path);
            Controls.Add(label_target_path);
        }

        private void button_select_source_click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    source_path = dialog.SelectedPath;
                    label_source_path.Text = "Исходная папка: " + source_path;
                    load_directory(source_path);
                }
            }
        }

        private void button_select_target_click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    target_path = dialog.SelectedPath;
                    label_target_path.Text = "Целевая папка: " + target_path;
                }
            }
        }

        private void load_directory(string path)
        {
            try
            {
                list_box_files.Items.Clear();
                foreach (string file in Directory.GetFiles(path))
                {
                    list_box_files.Items.Add(file);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки: " + ex.Message);
            }
        }

        private void button_copy_click(object sender, EventArgs e)
        {
            if (list_box_files.SelectedItems.Count == 0)
            {
                MessageBox.Show("Ничего не выбрано.");
                return;
            }

            if (string.IsNullOrEmpty(target_path))
            {
                MessageBox.Show("Не указана целевая папка.");
                return;
            }

            foreach (string src_file in list_box_files.SelectedItems)
            {
                string file_name = Path.GetFileName(src_file);
                string dest_file = Path.Combine(target_path, file_name);

                try
                {
                    File.Copy(src_file, dest_file, overwrite: true);
                    MessageBox.Show($"Файл {file_name} скопирован");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка копирования {file_name}: {ex.Message}");
                }
            }
        }
    }
}