﻿using System;
using System.Windows.Forms;
using CefSharp.Example;

namespace CefSharp.WinForms.Example
{
    public partial class Browser : Form, IExampleView
    {
        public event Action<object, string> UrlActivated;

        public event EventHandler BackActivated
        {
            add { backButton.Click += value; }
            remove { backButton.Click -= value; }
        }

        public event EventHandler ForwardActivated
        {
            add { forwardButton.Click += value; }
            remove { forwardButton.Click -= value; }
        }

        public event EventHandler TestResourceLoadActivated
        {
            add { testResourceLoadMenuItem.Click += value; }
            remove { testResourceLoadMenuItem.Click -= value; }
        }

        public event EventHandler TestSchemeLoadActivated
        {
            add { testSchemeLoadMenuItem.Click += value; }
            remove { testSchemeLoadMenuItem.Click -= value; }
        }

        public event EventHandler TestExecuteScriptActivated
        {
            add { testExecuteScriptMenuItem.Click += value; }
            remove { testExecuteScriptMenuItem.Click -= value; }
        }

        public event EventHandler TestEvaluateScriptActivated
        {
            add { testEvaluateScriptMenuItem.Click += value; }
            remove { testEvaluateScriptMenuItem.Click -= value; }
        }

        public event EventHandler TestBindActivated
        {
            add { testBindMenuItem.Click += value; }
            remove { testBindMenuItem.Click -= value; }
        }

        public event EventHandler TestConsoleMessageActivated
        {
            add { testConsoleMessageMenuItem.Click += value; }
            remove { testConsoleMessageMenuItem.Click -= value; }
        }

        public event EventHandler TestTooltipsActivated
        {
            add { testTooltipsMenuItem.Click += value; }
            remove { testTooltipsMenuItem.Click -= value; }
        }

        public event EventHandler ExitActivated
        {
            add { exitToolStripMenuItem.Click += value; }
            remove { exitToolStripMenuItem.Click -= value; }
        }

        private readonly WebView web_view;

        public Browser()
        {
            InitializeComponent();
            Text = "CefSharp";

            web_view = new WebView("https://github.com/ataranto/CefSharp", new BrowserSettings());
            web_view.Dock = DockStyle.Fill;
            toolStripContainer.ContentPanel.Controls.Add(web_view);

            var presenter = new ExamplePresenter(web_view, this,
                invoke => Invoke(invoke));
        }

        public void SetTitle(string title)
        {
            Text = title;
        }

        public void SetTooltip(string tooltip)
        {

        }

        public void SetAddress(string address)
        {
            urlTextBox.Text = address;
        }

        public void SetCanGoBack(bool can_go_back)
        {
            backButton.Enabled = can_go_back;
        }

        public void SetCanGoForward(bool can_go_forward)
        {
            forwardButton.Enabled = can_go_forward;
        }

        public void SetIsLoading(bool is_loading)
        {
            goButton.Text = is_loading ?
                "Stop" :
                "Go";
            goButton.Image = is_loading ?
                Properties.Resources.nav_plain_red :
                Properties.Resources.nav_plain_green;

            HandleToolStripLayout();
        }

        public void ExecuteScript(string script)
        {
            web_view.ExecuteScript(script);
        }

        public object EvaluateScript(string script)
        {
            return web_view.EvaluateScript(script);
        }

        public void DisplayOutput(string output)
        {
            outputLabel.Text = output;
        }

        private void HandleToolStripLayout(object sender, LayoutEventArgs e)
        {
            HandleToolStripLayout();
        }

        private void HandleToolStripLayout()
        {
            var width = toolStrip1.Width;
            foreach (ToolStripItem item in toolStrip1.Items)
            {
                if (item != urlTextBox)
                {
                    width -= item.Width - item.Margin.Horizontal;
                }
            }
            urlTextBox.Width = Math.Max(0, width - urlTextBox.Margin.Horizontal - 18);
        }

        private void HandleGoButtonClick(object sender, EventArgs e)
        {
            var handler = this.UrlActivated;
            if (handler != null)
            {
                handler(this, urlTextBox.Text);
            }
        }

        private void UrlTextBoxKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            var handler = UrlActivated;
            if (handler != null)
            {
                handler(this, urlTextBox.Text);
            }
        }

        private void AboutToolStripMenuItemClick(object sender, EventArgs e)
        {
            new AboutBox().ShowDialog();
        }
    }
}