using System.Data;

namespace Vinarija
{
    internal class ProizvodWindow
    {
        private DataRowView row;

        public ProizvodWindow(DataRowView row)
        {
            this.row = row;
        }
    }
}