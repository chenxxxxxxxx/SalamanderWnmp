﻿using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace SalamanderWnmp.UI
{
    /// <summary>
    /// JSPanel.xaml 的交互逻辑
    /// </summary>
    public partial class CodePanel : Window
    {
        public CodePanel()
        {
            InitializeComponent();
        }


        private void title_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
            e.Handled = true;
        }

        /// <summary>
        /// 编程语言枚举
        /// </summary>
        public enum ProgramLan
        {
            JavaScript,
            PHP,
            Go
        }

        public event PropertyChangedEventHandler PropertyChanged;
        // 选择的编程语言
        private ProgramLan selectedLan = ProgramLan.JavaScript;

        public ProgramLan SelectedLan
        {
            get
            {
                return this.selectedLan;
            }
            set
            {
                this.selectedLan = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("SelectedLan"));
                }
            }
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            e.Handled = true;
        }

        private void btnRun_Click(object sender, RoutedEventArgs e)
        {
            string code = this.txtCode.Text;
            if(string.IsNullOrEmpty(code))
            {
                MessageBox.Show("请输入代码");
                return;
            }
            this.txtOutput.Text = RunNode(code);
        }

        /// <summary>
        /// 运行js脚本
        /// </summary>
        /// <param name="code"></param>
        private string RunNode(string code)
        {
            Process scriptProc = new Process();
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = "node.exe";
            info.Arguments = "-e " + String.Format("\"{0}\"", code);
            info.RedirectStandardError = true;
            info.RedirectStandardOutput = true;
            info.UseShellExecute = false;
            info.CreateNoWindow = true;
            scriptProc.StartInfo = info;
            try
            {
                scriptProc.Start();
            }
            catch(Exception ex)
            {
                MessageBox.Show("node未安装或者未设置node环境变量！");
                return "";
            }
            string outStr = scriptProc.StandardOutput.ReadToEnd();
            // 有错误，读取错误信息
            if(String.IsNullOrEmpty(outStr))
            {
                outStr = scriptProc.StandardError.ReadToEnd();
            }
            scriptProc.Close();
            return outStr;

        }

        private void txtCode_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Tab)
            {
                int index = txtCode.SelectionStart;
                //txtCode.
            }
            e.Handled = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.lanList.DataContext = this;
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
