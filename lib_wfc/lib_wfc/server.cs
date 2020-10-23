using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ServiceModel;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Drawing;
using System.Diagnostics;
using System.IO;




namespace lib_wfc
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class server : functions
    {
        public bool check_token(string token)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["myDB"].ConnectionString;
            MySqlConnection conn = new MySqlConnection(connectionString);

            conn.Open();

            string sql = "select * from mydb.person where token = @token;";

            // Создать объект Command.
            MySqlCommand cmd = new MySqlCommand();
            Person person = new Person();

            cmd.Parameters.AddWithValue("@token", token);
            
            // Сочетать Command с Connection.
            cmd.Connection = conn;
            cmd.CommandText = sql;

            DbDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                person.id = Convert.ToInt32(reader[0].ToString());
                person.name = reader[1].ToString();
                person.phone = Convert.ToInt32(reader[2].ToString());
                person.email = reader[3].ToString();
                person.birthday = Convert.ToDateTime(reader[5].ToString());
                person.password = reader[4].ToString();

                Console.WriteLine("id: " + reader[0].ToString() + "-- name: " + reader[1].ToString() + "-- phone: " +
                    reader[2].ToString() + "-- email: " + reader[3].ToString() + "-- birthday: " + reader[5].ToString() +
                    "-- password: " + reader[4].ToString());
            }

            conn.Close();

            if (person.id != 0)
            {
                return true;
            }

            else
            {
                return false;
            }
            
        }

        public  List<Order> GetAllOrders(int id_client, string token)
        {
            bool check = check_token(token);
            List<Order> orders = new List<Order>();

            if (check == true)
            {

                var connectionString = ConfigurationManager.ConnectionStrings["myDB"].ConnectionString;
                MySqlConnection conn = new MySqlConnection(connectionString);

                string obj = "order";
                int count = Count(obj, id_client);

                if (count == 0)
                {
                    return orders;
                }
                else
                {
                    conn.Open();
                    string sql = "SELECT * FROM mydb.order where id_client = @id_client;";

                    // Создать объект Command.
                    MySqlCommand cmd = new MySqlCommand();


                    cmd.Parameters.AddWithValue("@id_client", id_client);
                    // Сочетать Command с Connection.
                    cmd.Connection = conn;
                    cmd.CommandText = sql;

                    DbDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Order order = new Order();
                        order.id = Convert.ToInt32(reader[0].ToString());
                        order.adress_from = reader[1].ToString();
                        order.adress_to = reader[2].ToString();
                        order.quick_price = Convert.ToInt32(reader[4].ToString());
                        order.date = Convert.ToDateTime(reader[5].ToString());
                        order.documents = Convert.ToInt32(reader[7].ToString());
                        order.weight = Convert.ToInt32(reader[6].ToString());
                        order.lenght = Convert.ToInt32(reader[8].ToString());
                        order.width = Convert.ToInt32(reader[9].ToString());
                        order.height = Convert.ToInt32(reader[10].ToString());
                        order.card = Convert.ToInt32(reader[11].ToString());
                        order.comment = reader[12].ToString();
                        order.status = Convert.ToInt32(reader[13].ToString());
                        order.price = Convert.ToInt32(reader[17].ToString());


                        orders.Add(order);
                    }

                }
                conn.Close();

                return orders;
            }

            else
            {
                return orders;
            }
        }
        public bool AddOrder(Order order, string token)
        {
            bool check = check_token(token);
            if (check == true)
            {
                try
                {
                    var connectionString = ConfigurationManager.ConnectionStrings["myDB"].ConnectionString;
                    MySqlConnection conn = new MySqlConnection(connectionString);

                    conn.Open();
                    string sql = "Insert into mydb.order (adress_from, adress_to, id_client, quick_price, date, weight, documents, lenght, width, height, card, comment, status, price) "
                                            + " values (@adress_from, @adress_to, @id_client, @quick_price, @date, @weight, @documents, @lenght, @width, @height, @card, @comment, @status, @price) ";

                    // Создать объект Command.
                    MySqlCommand cmd = new MySqlCommand();


                    cmd.Parameters.AddWithValue("@adress_from", order.adress_from);
                    cmd.Parameters.AddWithValue("@adress_to", order.adress_to);
                    cmd.Parameters.AddWithValue("@id_client", order.id_client);
                    cmd.Parameters.AddWithValue("@quick_price", order.quick_price);
                    cmd.Parameters.AddWithValue("@date", order.date);
                    cmd.Parameters.AddWithValue("@weight", order.weight);
                    cmd.Parameters.AddWithValue("@documents", order.documents);
                    cmd.Parameters.AddWithValue("@lenght", order.lenght);
                    cmd.Parameters.AddWithValue("@width", order.width);
                    cmd.Parameters.AddWithValue("@height", order.height);
                    cmd.Parameters.AddWithValue("@card", order.card);
                    cmd.Parameters.AddWithValue("@comment", order.comment);
                    cmd.Parameters.AddWithValue("@status", order.status);
                    cmd.Parameters.AddWithValue("@price", order.price);
                    // Сочетать Command с Connection.
                    cmd.Connection = conn;
                    cmd.CommandText = sql;

                    DbDataReader reader = cmd.ExecuteReader();



                    conn.Close();

                    return true;
                }

                catch
                {
                    return false;
                }
            }

            else
            {
                return false;
            }
        }
        public int OrderPrice(Order order)
        {
            int price = 500;

            if (order.quick_price == 1)
            {
                price = price + 500;
            }

            order.height = 0;


            if (order.weight >= 100)
            {
                price = price + 100;
            }

            if (order.weight >= 500)
            {
                price = price + 500;
            }

            if (order.weight >= 1000)
            {
                price = price + 1000;
            }



            if (order.lenght >= 100)
            {
                price = price + 100;
            }

            if (order.lenght >= 500)
            {
                price = price + 500;
            }

            if (order.lenght >= 1000)
            {
                price = price + 1000;
            }


            if (order.width >= 100)
            {
                price = price + 100;
            }

            if (order.width >= 500)
            {
                price = price + 500;
            }

            if (order.width >= 1000)
            {
                price = price + 1000;
            }


            if (order.height >= 100)
            {
                price = price + 100;
            }

            if (order.height >= 500)
            {
                price = price + 500;
            }

            if (order.height >= 1000)
            {
                price = price + 1000;
            }

            return price;
        }
        public  Order GetOrder(int id_order, int id_client, string token)
        {
            Order order = new Order();
            bool check = check_token(token);
            if (check == true)
            {


                var connectionString = ConfigurationManager.ConnectionStrings["myDB"].ConnectionString;
                MySqlConnection conn = new MySqlConnection(connectionString);
                string obj = "order";
                int count = Count(obj, id_client);


                if (count == 0)
                {
                    return order;
                }
                else
                {
                    conn.Open();
                    string sql = "SELECT * FROM mydb.order where id_client = @id_client and id = @id_order;";


                    MySqlCommand cmd = new MySqlCommand();

                    cmd.Parameters.AddWithValue("@id_order", id_order);
                    cmd.Parameters.AddWithValue("@id_client", id_client);

                    cmd.Connection = conn;
                    cmd.CommandText = sql;

                    DbDataReader reader = cmd.ExecuteReader();


                    while (reader.Read())
                    {
                        order.adress_from = reader[1].ToString();
                        order.adress_to = reader[2].ToString();
                        order.quick_price = Convert.ToInt32(reader[4].ToString());
                        order.date = Convert.ToDateTime(reader[5]);
                        order.documents = Convert.ToInt32(reader[7].ToString());
                        order.weight = Convert.ToInt32(reader[6].ToString());
                        order.lenght = Convert.ToInt32(reader[8].ToString());
                        order.width = Convert.ToInt32(reader[9].ToString());
                        order.height = Convert.ToInt32(reader[10].ToString());
                        order.card = Convert.ToInt32(reader[11].ToString());
                        order.comment = reader[12].ToString();
                        order.status = Convert.ToInt32(reader[13].ToString());
                        order.price = Convert.ToInt32(reader[17].ToString());

                        Console.WriteLine("id: " + reader[0].ToString() + "-- adress_from: " + reader[1].ToString() + "-- adress_to: " +
                            reader[2].ToString() + "-- quick_price: " + reader[4].ToString() + "-- date: " + reader[5].ToString() +
                            "-- documents: " + reader[7].ToString() + "-- weight: " + reader[6].ToString() +
                            "-- lenght: " + reader[8].ToString() + "-- width: " + reader[9].ToString() + "-- height: " + reader[10].ToString()
                            + "-- card: " + reader[11].ToString() + "-- comment: " + reader[12].ToString() + "-- status: " + reader[13].ToString()
                            + "-- price: " + reader[17].ToString()
                            );

                    }


                }

                conn.Close();
                return order;
            }

            else
            {
                return order;
            }
        }
        public bool SaveOrder(int id_order, int id_client, Order order, string token)
        {
            bool check = check_token(token);

            if (check == true)
            {

                var connectionString = ConfigurationManager.ConnectionStrings["myDB"].ConnectionString;
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();

                string sql = "UPDATE mydb.order SET adress_from = @adress_from, adress_to = @adress_to," +
                    "quick_price = @quick_price, date = @date, weight = @weight, documents = @documents, " +
                    "lenght = @lenght, width = @width, height = @height, card = @card, comment = @comment," +
                    "status = @status, price = @price WHERE id_client = @id_client and id = @id_order;";


                MySqlCommand cmd = new MySqlCommand();
                cmd.Parameters.AddWithValue("@adress_from", order.adress_from);
                cmd.Parameters.AddWithValue("@adress_to", order.adress_to);
                cmd.Parameters.AddWithValue("@quick_price", order.quick_price);
                cmd.Parameters.AddWithValue("@date", order.date);
                cmd.Parameters.AddWithValue("@weight", order.weight);
                cmd.Parameters.AddWithValue("@documents", order.documents);
                cmd.Parameters.AddWithValue("@lenght", order.lenght);
                cmd.Parameters.AddWithValue("@width", order.width);
                cmd.Parameters.AddWithValue("@height", order.height);
                cmd.Parameters.AddWithValue("@card", order.card);
                cmd.Parameters.AddWithValue("@comment", order.comment);
                cmd.Parameters.AddWithValue("@status", order.status);
                cmd.Parameters.AddWithValue("@price", order.price);


                cmd.Parameters.AddWithValue("@id_order", id_order);
                cmd.Parameters.AddWithValue("@id_client", id_client);

                cmd.Connection = conn;
                cmd.CommandText = sql;

                DbDataReader reader = cmd.ExecuteReader();

                conn.Close();

                return true;
            }
             else
            {
                return false;
            }
        }

        public Person GetPerson(int id_client)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["myDB"].ConnectionString;
            MySqlConnection conn = new MySqlConnection(connectionString);
            string obj = "person";
            int count = Count(obj, id_client);
            Person person = new Person();

            if (count == 0)
            {
                return person;
            }
            else
            {
                conn.Open();
                string sql = "SELECT * FROM mydb.person where id = @id_client and datedel is null;";

                // Создать объект Command.
                MySqlCommand cmd = new MySqlCommand();

                cmd.Parameters.AddWithValue("@id_client", id_client);
                // Сочетать Command с Connection.
                cmd.Connection = conn;
                cmd.CommandText = sql;

                DbDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    person.name = reader[1].ToString();
                    person.phone = Convert.ToInt32(reader[2].ToString());
                    person.email = reader[3].ToString();
                    person.birthday = Convert.ToDateTime(reader[5].ToString());
                    person.password = reader[4].ToString();

                    Console.WriteLine("id: " + reader[0].ToString() + "-- name: " + reader[1].ToString() + "-- phone: " +
                        reader[2].ToString() + "-- email: " + reader[3].ToString() + "-- birthday: " + reader[5].ToString() +
                        "-- password: " + reader[4].ToString());

                }
            }
            conn.Close();
            return person;
        }


        public List<Action> GetActions()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["myDB"].ConnectionString;
            MySqlConnection conn = new MySqlConnection(connectionString);
            List<Action> actions = new List<Action>();
            string obj = "services";
            int id_client = 0;
            int count = Count(obj, id_client);

            if (count == 0)
            {
                return actions;
            }

            else
            {
                conn.Open();
                string sql = "Select * From actions";

                // Создать объект Command.
                MySqlCommand cmd = new MySqlCommand();



                // Сочетать Command с Connection.
                cmd.Connection = conn;
                cmd.CommandText = sql;

                DbDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Action action = new Action();
                    action.id = Convert.ToInt32(reader[0].ToString());
                    action.name = reader[1].ToString();
                    action.description = reader[2].ToString();
                    action.start = Convert.ToDateTime(reader[3].ToString());
                    action.finish = Convert.ToDateTime(reader[4].ToString());
                    action.sale = Convert.ToInt32(reader[5].ToString());

                    actions.Add(action);

                }
            }
            conn.Close();
            return actions;
        }

        public  List<Service> GetServices()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["myDB"].ConnectionString;
            MySqlConnection conn = new MySqlConnection(connectionString);
            List<Service> services = new List<Service>();
            string obj = "services";
            int id_client = 0;
            int count = Count(obj, id_client);

            if (count == 0)
            {
                return services;
            }

            else
            {
                conn.Open();
                string sql = "Select * From services";

                // Создать объект Command.
                MySqlCommand cmd = new MySqlCommand();



                // Сочетать Command с Connection.
                cmd.Connection = conn;
                cmd.CommandText = sql;

                DbDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Service service = new Service();
                    service.id = Convert.ToInt32(reader[0].ToString());
                    service.price = Convert.ToInt32(reader[1].ToString());
                    service.description = reader[2].ToString();



                    services.Add(service);


                }
            }
            conn.Close();
            return services;
        }

        public void Registration(string name, int phone, string email, DateTime birth, string pas)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["myDB"].ConnectionString;
            MySqlConnection conn = new MySqlConnection(connectionString);

            conn.Open();
            string sql = "Insert into mydb.person (name, phone, email, birthday, password, token) "
                                    + " values (@name, @phone, @email, @birth, @pas, @token) ";

            MySqlCommand cmd = new MySqlCommand();

            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@phone", phone);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@birth", birth);
            cmd.Parameters.AddWithValue("@pas", pas);
            cmd.Parameters.AddWithValue("@token", RandomString());

            cmd.Connection = conn;
            cmd.CommandText = sql;

            DbDataReader reader = cmd.ExecuteReader();

            Console.WriteLine("Данные занесены в базу");

            conn.Close();

        }
        public  Person Autorization(string login, string pas)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["myDB"].ConnectionString;
            MySqlConnection conn = new MySqlConnection(connectionString);

            Person person = new Person();
            conn.Open();
            int log;
            try
            {
                 log = Convert.ToInt32(login);
            }
            catch
            {
                 log = 0;
            }




            string pass = pas;

            string sql = "SELECT * FROM mydb.person where phone = @login and password = @pas;";

            // Создать объект Command.
            MySqlCommand cmd = new MySqlCommand();


            cmd.Parameters.AddWithValue("@login", log);
            cmd.Parameters.AddWithValue("@pas", pass);
            // Сочетать Command с Connection.
            cmd.Connection = conn;
            cmd.CommandText = sql;

            DbDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                person.id = Convert.ToInt32(reader[0].ToString());
                person.name = reader[1].ToString();
                person.phone = Convert.ToInt32(reader[2].ToString());
                person.email = reader[3].ToString();
                person.birthday = Convert.ToDateTime(reader[5].ToString());
                person.password = reader[4].ToString();

            }
            conn.Close();

            person.token = GetToken(person.id);

            return person;

        }

        public string GetToken(int id_client)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["myDB"].ConnectionString;
            MySqlConnection conn = new MySqlConnection(connectionString);

            conn.Open();

            string sql = "UPDATE mydb.person SET token = @token where id = @id_client;";

            // Создать объект Command.
            MySqlCommand cmd = new MySqlCommand();
            string token = RandomString();

            cmd.Parameters.AddWithValue("@token", token);
            cmd.Parameters.AddWithValue("@id_client", id_client);
            // Сочетать Command с Connection.
            cmd.Connection = conn;
            cmd.CommandText = sql;

            DbDataReader reader2 = cmd.ExecuteReader();

            conn.Close();

            return token;
        }
        public string RandomString()
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < 30; i++)
            {
                //Генерируем число являющееся латинским символом в юникоде
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                //Конструируем строку со случайно сгенерированными символами
                builder.Append(ch);
            }
            return builder.ToString();
        }

        public bool SavePerson(int id_client, Person person, string token)
        {
            bool check = check_token(token);
            if (check == true)
            {
                var connectionString = ConfigurationManager.ConnectionStrings["myDB"].ConnectionString;
                MySqlConnection conn = new MySqlConnection(connectionString);

                conn.Open();
                string sql = "UPDATE mydb.person SET name = @name, phone = @phone," +
                    "email = @email, password = @password, birthday = @birthday where id = @id_client;";


                MySqlCommand cmd = new MySqlCommand();
                cmd.Parameters.AddWithValue("@name", person.name);
                cmd.Parameters.AddWithValue("@phone", person.phone);
                cmd.Parameters.AddWithValue("@email", person.email);
                cmd.Parameters.AddWithValue("@password", person.password);
                cmd.Parameters.AddWithValue("@birthday", person.birthday);

                cmd.Parameters.AddWithValue("@id_client", id_client);

                cmd.Connection = conn;
                cmd.CommandText = sql;

                DbDataReader reader = cmd.ExecuteReader();
                conn.Close();

                return true;
            }

            else
            {
                return false;
            }

        }


      
        public int Count(string obj, int id_client)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["myDB"].ConnectionString;
            MySqlConnection conn = new MySqlConnection(connectionString);

            conn.Open();
            string sql = null;


            if (id_client == 0)
            {
                sql = "Select count(*) From " + obj;
            }

            else
            {
                sql = "SELECT count(*) FROM mydb.order where id_client = @id_client;";

            }

            // Создать объект Command.
            MySqlCommand cmd = new MySqlCommand();


            cmd.Parameters.AddWithValue("@id_client", id_client);
            // Сочетать Command с Connection.
            cmd.Connection = conn;
            cmd.CommandText = sql;

            DbDataReader reader = cmd.ExecuteReader();

            reader.Read();

            int count = Convert.ToInt32(reader[0]);
            // Закрыть соединение.
            conn.Close();
            // Уничтожить объект, освободить ресурс.
            conn.Dispose();
            return count;

        }

      
    }
}
