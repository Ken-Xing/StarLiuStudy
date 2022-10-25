using ReadAndSaveCSVFile;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReadAndSaveFile
{
    public partial class PossibleDuplicateDateForm : Form
    {
        #region
        private CSVFileHelper _cSVFileHelper=new CSVFileHelper();
        #endregion

        public PossibleDuplicateDateForm()
        {
            InitializeComponent();
        }

        private void PossibleDuplicateDateForm_Load(object sender, EventArgs e)
        {
            this.dgvDuplicateDataTable.DataSource = this._cSVFileHelper.DuplicateDataTable.Copy();
            this.dgvNotDuplicateDataTable.DataSource= this._cSVFileHelper.NotDuplicateDataTable.Copy();
            this.dgvPossibleDuplicateDataTable.DataSource = this._cSVFileHelper.PossibleDuplicateDataTable.Copy();
        }
    }
}
