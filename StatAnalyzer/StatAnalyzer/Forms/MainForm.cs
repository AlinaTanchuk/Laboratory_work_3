using System;
using System.Drawing;
using System.Windows.Forms;
using StatAnalyzer.Forms;

namespace StatAnalyzer
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            InitializeModuleCards();
        }

        private void InitializeModuleCards()
        {
            // Наполнение карточек
            buttonsPanel.Controls.Add(CreateModuleCard(
                "👥 Численность населения России",
                "Вариант 5",
                "Анализ за 15 лет: прирост, убыль,\nпрогноз методом скользящей средней",
                Color.FromArgb(52, 152, 219),
                (s, e) => OpenForm(new Variant5Form())), 0, 0);

            buttonsPanel.Controls.Add(CreateModuleCard(
                "💰 Медианная зарплата",
                "Вариант 8",
                "Зарплаты мужчин и женщин за 15 лет:\nмакс./мин. % роста, прогноз",
                Color.FromArgb(46, 204, 113),
                (s, e) => OpenForm(new Variant8Form())), 1, 0);

            buttonsPanel.Controls.Add(CreateModuleCard(
                "🏠 Цены на первичное жильё",
                "Вариант 9",
                "1, 2, 3-комнатные квартиры за 15 лет:\nподорожание, удешевление, прогноз",
                Color.FromArgb(155, 89, 182),
                (s, e) => OpenForm(new Variant9Form())), 0, 1);

            buttonsPanel.Controls.Add(CreateModuleCard(
                "🦠 Инфекционные заболевания",
                "Вариант 15",
                "Динамика инфекций за 15 лет:\nснижение заболеваемости, прогноз",
                Color.FromArgb(231, 76, 60),
                (s, e) => OpenForm(new Variant15Form())), 1, 1);
        }


        /// Фабричный метод создания карточки модуля.
        /// Принцип единственной ответственности (SRP): логика создания карточки изолирована.
        private Panel CreateModuleCard(string title, string subtitle, string description,
            Color accentColor, EventHandler onClick)
        {
            var card = new Panel
            {
                Margin = new Padding(10),
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Cursor = Cursors.Hand
            };

            // Тень через рамку
            card.Paint += (s, e) =>
            {
                var rect = new Rectangle(0, 0, card.Width - 1, card.Height - 1);
                using (var pen = new Pen(Color.FromArgb(220, 225, 235), 1))
                    e.Graphics.DrawRectangle(pen, rect);
            };

            // Цветная полоса слева
            var accent = new Panel
            {
                Width = 6,
                Dock = DockStyle.Left,
                BackColor = accentColor
            };

            var content = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(16, 14, 16, 14),
                BackColor = Color.Transparent
            };

            var lblTitle = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 13f, FontStyle.Bold),
                ForeColor = accentColor,
                Height = 28,
                Dock = DockStyle.Top,
                AutoSize = false
            };

            var lblSubtitle = new Label
            {
                Text = subtitle,
                Font = new Font("Segoe UI", 9f, FontStyle.Bold),
                ForeColor = Color.FromArgb(60, 60, 60),
                Height = 22,
                Dock = DockStyle.Top,
                AutoSize = false
            };

            var lblDesc = new Label
            {
                Text = description,
                Font = new Font("Segoe UI", 8.5f),
                ForeColor = Color.FromArgb(110, 110, 110),
                Dock = DockStyle.Fill,
                AutoSize = false
            };

            var btnOpen = new Button
            {
                Text = "Открыть →",
                Height = 32,
                Dock = DockStyle.Bottom,
                BackColor = accentColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9f, FontStyle.Bold),
                Cursor = Cursors.Hand
            };

            btnOpen.FlatAppearance.BorderSize = 0;
            btnOpen.Click += onClick;

            content.Controls.Add(lblDesc);
            content.Controls.Add(lblSubtitle);
            content.Controls.Add(lblTitle);
            content.Controls.Add(btnOpen);

            card.Controls.Add(content);
            card.Controls.Add(accent);

            // Hover-эффект
            card.MouseEnter += (s, e) => card.BackColor = Color.FromArgb(248, 250, 255);
            card.MouseLeave += (s, e) => card.BackColor = Color.White;

            return card;
        }

        private void OpenForm(Form form)
        {
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Show();
        }
    }
}