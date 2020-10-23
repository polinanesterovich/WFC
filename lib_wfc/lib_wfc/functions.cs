using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace lib_wfc
{
    [ServiceContract]
    public interface functions
    {
        [OperationContract]
        List<Action> GetActions();

        [OperationContract]
        List<Service> GetServices();

        [OperationContract]
        Person GetPerson(int id_client);

        [OperationContract]
        Order GetOrder(int id_order, int id_client, string token);

        [OperationContract]
        bool AddOrder(Order order, string token);

        [OperationContract]
        List<Order> GetAllOrders(int id_client, string token);

        [OperationContract]
        void Registration(string name, int phone, string email, DateTime birth, string pas);

        [OperationContract]
        Person Autorization(string login, string pas);

        [OperationContract]
        int OrderPrice(Order order);

        [OperationContract]
        bool SaveOrder(int id_order, int id_client, Order order, string token);

        [OperationContract]
        bool SavePerson(int id_client, Person person, string token);
    }
}
