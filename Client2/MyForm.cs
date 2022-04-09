using System.Windows.Forms;

namespace Client2
{
    public partial class MyForm : Form
    {
        private UserControl currentTab;
        private IServices _srv;
        public MyForm()
        {
            InitializeComponent();
            tabLogin.Show();
            tabMain.Visible = false;
            currentTab = tabLogin;
            _srv = Program.GetService();
        }

        public void TryLogin(string name, string password)
        {
            Agency agency = Login(name, password);
            if (agency == null) return;

            currentTab.Hide();
            currentTab = tabMain;
            tabMain.Initialize(agency, _srv);
            currentTab.Show();
        }

        private Agency Login(string name, string password)
        {
            Agency agency = _srv.getAgencyByName(name);
            if (agency == null)
            {
                MessageBox.Show("No agency with this name");
                return null;
            }
            if (!password.Equals(agency.Password))
            {
                MessageBox.Show("Wrong password!");
                return null;
            }
            return agency;
        }

        public void SetLoginTab()
        {
            currentTab.Hide();
            currentTab = tabLogin;
            currentTab.Show();
        }
    }
}
