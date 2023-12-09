using System;

namespace WorkTimeControl.Report.Helper
{
    public class Mediator
    {
        private static int _ListBoxIndex;
        public int ListBoxIndex
        {
            get { return _ListBoxIndex; }
            set { _ListBoxIndex = value; }
        }

        private static Guid _UserId;
        public Guid UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }
    }
}
