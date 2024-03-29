﻿using GTA6Game.UserControls.Overlay.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GTA6Game.UserControls.Messages
{
    /// <summary>
    /// Interaction logic for MessageOk.xaml
    /// </summary>
    public partial class MessageOk : ModalContentBase
    {
        private Modal<object> Modal;

        public MessageOk(string description, string title)
        {
            InitializeComponent();
            TbDescription.Text = description;
            TbTitle.Text = title;
        }

        public override void InjectModalVM(BaseModal modal)
        {
            Modal = (Modal<object>)modal;
        }

        private void TbOk_Click(object sender, RoutedEventArgs e)
        {
            Modal.CloseModal(false, null);
        }
    }
}
