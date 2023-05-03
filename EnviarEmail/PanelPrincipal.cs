using System.Windows.Forms;

namespace EnviarEmail
{
    public partial class PanelPrincipal : Form
    {
        public PanelPrincipal()
        {
            InitializeComponent();
            UC_Algo uc = new UC_Algo();

            panel1.Controls.Clear();
            uc.Visible = true;
            uc.Dock = DockStyle.Fill;
            panel1.Controls.Add(uc);
            
        }
    }
}
