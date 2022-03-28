using System.Windows.Forms;

namespace AgentiiDeTurism.src.controller
{
    public partial class TabLogin : UserControl
    {
        public TabLogin()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, System.EventArgs e)
        {
            var parent = this.Parent as MyForm;
            string name = textBoxUsername.Text;
            string password = textBoxPassword.Text;
            parent.TryLogin(name, password);
        }
    }
}
