using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;





namespace lib_wfc
{
    [DataContract]
    public class Action
    {
       [DataMember]
        public int id;
        [DataMember]
        public string name;
        [DataMember]
        public string description;
        [DataMember]
        public DateTime start;
        [DataMember]
        public DateTime finish;
        [DataMember]
        public int sale;
        [DataMember]
        public DateTime datadel;
        
    }


    [DataContract]
    public class Service
    {
        [DataMember]
        public int id;
        [DataMember]
        public int price;
        [DataMember]
        public string description;
        [DataMember]
        public int acting;
        [DataMember]
        public DateTime datadel;
    }

    [DataContract]
    public class Person
    {
        [DataMember]
        public int id;
        [DataMember]
        public string name;
        [DataMember]
        public int phone;
        [DataMember]
        public string email;
        [DataMember]
        public string password;
        [DataMember]
        public DateTime birthday;
        [DataMember]
        public string token;
        [DataMember]
        public DateTime datadel;
    }

    [DataContract]
    public class Order
    {
        [DataMember]
        public int id;
        [DataMember]
        public string adress_from;
        [DataMember]
        public string adress_to;
        [DataMember]
        public int id_client;
        [DataMember]
        public int quick_price;
        [DataMember]
        public DateTime date;
        [DataMember]
        public int weight;
        [DataMember]
        public int documents;
        [DataMember]
        public int lenght;
        [DataMember]
        public int width;
        [DataMember]
        public int height;
        [DataMember]
        public int card;
        [DataMember]
        public string comment;
        [DataMember]
        public int status;
        [DataMember]
        public int price;
        [DataMember]
        public DateTime datadel;
    }
}
