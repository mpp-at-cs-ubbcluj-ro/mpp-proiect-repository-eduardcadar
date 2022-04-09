using System.Windows.Forms;

namespace Client2
{
    public partial class TabLogin : UserControl
    {
        public TabLogin()
        {
            InitializeComponent();
        }

        private void ButtonLogin_Click(object sender, System.EventArgs e)
        {
            var parent = this.Parent as MyForm;
            string name = textBoxUsername.Text;
            string password = textBoxPassword.Text;
            parent.TryLogin(name, password);
        }
    }
}
