using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WcfStudenthandlerdb
{
    [DataContract]
    public class Student
    {
        [DataMember]
        public int Id;

        [DataMember]
        public string Navn;

    }
}