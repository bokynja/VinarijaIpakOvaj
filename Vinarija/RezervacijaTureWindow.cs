using System.Data;

namespace Vinarija
{
    internal class RezervacijaTureWindow
    {
        private DataRowView row;

        public RezervacijaTureWindow(DataRowView row)
        {
            this.row = row;
        }
    }
}