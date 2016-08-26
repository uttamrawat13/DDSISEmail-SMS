using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDMailSmsWeb.Classes
{
    static class Source
    {
        private static string _GlobalDeptID;
        public static string GlobalDeptID
        {
            get
            {
                return _GlobalDeptID;
            }
            set
            {
                _GlobalDeptID = value;
            }
        }

        private static string _InboxID;
        public static string InboxID
        {
            get
            {
                return _InboxID;
            }
            set
            {
                _InboxID = value;
            }
        }
        private static string _ReplySentfrom;
        public static string ReplySentfrom
        {
            get
            {
                return _ReplySentfrom;
            }
            set
            {
                _ReplySentfrom = value;
            }
        }

        private static string _ReplySentto;
        public static string ReplySentto
        {
            get
            {
                return _ReplySentto;
            }
            set
            {
                _ReplySentto = value;
            }
        }


        private static string _ReplySentSubject;
        public static string ReplySentSubject
        {
            get
            {
                return _ReplySentSubject;
            }
            set
            {
                _ReplySentSubject = value;
            }
        }


        private static string _ReplySentBody;
        public static string ReplySentBody
        {
            get
            {
                return _ReplySentBody;
            }
            set
            {
                _ReplySentBody = value;
            }
        }

        public static Boolean SOrL(string studentno, string leadID)
        {
            Boolean status = true;
            if (studentno == string.Empty && leadID != string.Empty)
            {
                status = false;
            }
            return status;
        }




    }
}
