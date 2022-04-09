using System.Windows.Forms;

namespace Client
{
    public partial class TabLogin : UserControl
    {
        public TabLogin()
        {
            InitializeComponent();
        }

        private void ButtonLogin_Click(object sender, System.EventArgs e)
        {
            var parent = Parent as MyForm;
            string name = textBoxUsername.Text;
            string password = textBoxPassword.Text;
            parent.TryLogin(name, password);
        }
    }
}
