using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfServer
{
    [ServiceContract]
    public interface IDataInputService
    {
        [OperationContract]
        int CreateUser(User user);

        [OperationContract]
        int Login(User user);

        [OperationContract]
        List<Time> GetTimes(int userId);

        [OperationContract]
        void AddTime(Time time, int userId);

        [OperationContract]
        List<string> Projects();

        [OperationContract]
        decimal CalculateEarnings(int id);
    }

    [DataContract]
    public class User
    {
        string name;
        string passwort;

        [DataMember]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [DataMember]
        public string Passwort
        {
            get { return passwort; }
            set { passwort = value; }
        }
    }

    [DataContract]
    public class Time
    {
        DateTime start;
        DateTime end;
        string project;
        int uid;
        int id;

        [DataMember]
        public DateTime Start
        {
            get { return Start; }
            set { start = value; }
        }

        [DataMember]
        public DateTime End
        {
            get { return end; }
            set { end = value; }
        }

        [DataMember]
        public string Project
        {
            get { return project; }
            set { project = value; }
        }

        [DataMember]
        public int uId
        {
            get { return uid; }
            set { uid = value; }
        }

        [DataMember]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
    }
}
