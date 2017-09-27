﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfStudenthandlerdb
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        IList<Student> GetAllStudents();

        [OperationContract]
        Student GetStudentById(int id);

        [OperationContract]
        IList<Student> GetStudentsByName(string navn);

        [OperationContract]
        int AddStudent(string navn, int id);

    }
}
