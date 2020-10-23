using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using MySql.Data.MySqlClient;
using System.ServiceModel;
using System.Diagnostics;
using client.ServiceReference1;
using lib_wfc;

namespace client
{
    class Program
    {
        public class Action
        {
            public int id;
            public string name;
            public string description;
            public DateTime start;
            public DateTime finish;
            public int sale;
            public DateTime datadel;
        }

        public class Order
        {
            public int id;
            public string adress_from;
            public string adress_to;
            public int id_client;
            public int quick_price;
            public DateTime date;
            public int weight;
            public int width;
            public int lenght;
            public int height;
            public int documents;
            public int card;
            public string comment;
            public int status;
            public int price;
            public DateTime datadel;
        }

        public class Service
        {
            public int id;
            public int price;
            public string description;
            public int acting;
            public DateTime datadel;
        }

        public class Person
        {
            public int id;
            public string name;
            public int phone;
            public string email;
            public string password;
            public string token;
            public DateTime birthday;
            public DateTime datadel;
        }

        public class Orders
        {
            public int global_id = 0;
            public List<Order> orders = new List<Order>();
            public List<Order> GetOrders()
            {
                return orders;
            }

            public void AddOrders(Order order)
            {
                global_id++;
                order.id = global_id;
                orders.Add(order);
            }
        }



        public class Services
        {
       /*     public List<Service> services = new List<Service>();
            public List<Service> GetServices()
            {
                return services;
            }*/
        }

        public class Persons
        {
            public int global_id = 0;
            public List<Person> persons = new List<Person>();


        }
        static void Main()
        {
           
               functionsClient functions = new functionsClient("BasicHttpBinding_functions");
            string start;

            while (true)
            {
                Console.WriteLine("1 - Авторизация\n2 - Регистрация");

                start = Console.ReadLine();

                if (start == "")
                {
                    Console.WriteLine("Введите корректное значение");
                    continue;
                }

                int st = Convert.ToInt32(start);



                if (st > 2)
                {
                    Console.WriteLine("Введите корректное значение");
                    continue;
                }

                break;

            }


            lib_wfc.Person client = new lib_wfc.Person();
            



            if (start == "1")
            {
                while (true)
                {
                   
                        Console.WriteLine("Введите логин:");
                        string login = Console.ReadLine();

                        Console.WriteLine("Введите пароль:");
                        string pass = Console.ReadLine();


                        client = functions.Autorization(login, pass);

                    

                    if(client.phone == 0)
                    {
                        Console.WriteLine("Неправильный логин! Введите номер телефона");
                        continue;
                    }

                    break;
                }
                
                

                if (client.id == 0)
                {
                    Console.WriteLine("Неправильный логин или пароль");
                    Main();
                }
            }

            if (start == "2")
            {
                Console.WriteLine("Введите имя:");
                string name = Console.ReadLine();

                int phone;
                while (true)
                {
                    Console.WriteLine("Введите номер телефона:");

                    if (!int.TryParse(Console.ReadLine(), out phone))
                    {
                        Console.WriteLine("Введите корректные данные");
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }


                Console.WriteLine("Введите email:");
                string email = Console.ReadLine();

                Console.WriteLine("Введите дату рождения:");
                DateTime birth = Convert.ToDateTime(Console.ReadLine());

                Console.WriteLine("Введите пароль:");
                string pass = Console.ReadLine();


                functions.Registration(name, phone, email, birth, pass);

                Console.WriteLine("Пройдите авторизацию");
                Main();
            }




            
            Orders orders = new Orders();
            string item = "0";
            Console.WriteLine("\nДобрый день!");
            while (item != "5")
            {
                Console.WriteLine("Что вы хотите сделать?: \n" +
                    "1 - Посмотреть акции\n" +
                    "2 - Посмотреть услуги\n" +
                    "3 - Заказы\n" +
                    "4 - Личный кабинет\n" +
                    "5 - Выход\n");
                item = Console.ReadLine();

                if (item == "1")
                {
                  
                    lib_wfc.Action[] action_list = functions.GetActions();


                    for (int i = 0; i < action_list.Length; i++)
                    {
                        Console.WriteLine("id: " + action_list[i].id + " name: " + action_list[i].name + " description: " + action_list[i].description + " start: " + action_list[i].start + " finish: " + action_list[i].finish + " sale: " +
                             +action_list[i].sale);
                    }



                    Console.Read();

                }

                else if (item == "2")
                {

                    lib_wfc.Service[] service_list = functions.GetServices();
                   
                    for (int i = 0; i < service_list.Length; i++)
                    {
                        Console.WriteLine("id: " + service_list[i].id + " description: " + service_list[i].description + " price: " + service_list[i].price);
                    }




                    Console.Read();
                }

                else if (item == "3")
                {

                    Console.WriteLine("Что вы хотите сделать?: \n" +
                    "1 - Посмотреть заказы\n" +
                    "2 - Добавить заказ\n" +
                    "3 - Редактировать заказ\n" +
                    "4 - Изменить статус заказа\n" +
                    "5 - Главное меню\n");
                    item = Console.ReadLine();
                    if (item == "1")
                    {
                        int id_client = client.id;
                        lib_wfc.Order[] order_list = functions.GetAllOrders(id_client);

                        for (int i = 0; i < order_list.Length; i++)
                        {
                            Console.WriteLine("id: " + order_list[i].id + " adress from: " + order_list[i].adress_from + " adress to: " + order_list[i].adress_to + " quick: " + order_list[i].quick_price + " date: " + order_list[i].date + " weight: " +
                                 +order_list[i].weight + " documents: " + order_list[i].documents + " lenght: " + order_list[i].lenght + " width: " + order_list[i].width + " height: " + order_list[i].height + " card: " +
                                  +order_list[i].card + " comment: " + order_list[i].comment + " ststus: " + order_list[i].status + " price: " + order_list[i].price);
                        }


                        Console.Read();

                    }
                    if (item == "2")
                    {
                       lib_wfc.Order new_order = new lib_wfc.Order();



                        new_order.id_client = client.id;

                        Console.WriteLine("Введите место отправки:");
                        new_order.adress_from = Console.ReadLine();

                        Console.WriteLine("Введите место доставки:");
                        new_order.adress_to = Console.ReadLine();

                        Console.WriteLine("Введите дату доставки:");
                        string date = Console.ReadLine();
                        new_order.date = DateTime.Parse(date);

                        while (true)
                        {
                            Console.WriteLine("Нужна ли быстрая доставка? 1 - да, 0 - нет");
                            string num = Console.ReadLine();
                            if (num == "1")
                            {
                                new_order.quick_price = 1;
                                break;
                            }
                            if (num == "0")
                            {
                                new_order.quick_price = 0;
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Введите корректные данные");
                                continue;
                            }
                        }

                        while (true)
                        {
                            Console.WriteLine("Документы? 1 - да, 0 - нет");
                            string num = Console.ReadLine();
                            if (num == "1")
                            {
                                new_order.documents = 1;

                                new_order.weight = 0;
                                new_order.lenght = 0;
                                new_order.width = 0;
                                new_order.height = 0;

                                break;
                            }
                            if (num == "0")
                            {
                                new_order.documents = 0;

                                Console.WriteLine("Введите ширину(м):");
                                string weight = Console.ReadLine();
                                new_order.weight = Int32.Parse(weight);

                                Console.WriteLine("Введите длину(м):");
                                string lenght = Console.ReadLine();
                                new_order.lenght = Int32.Parse(lenght);

                                Console.WriteLine("Введите высоту(м):");
                                string height = Console.ReadLine();
                                new_order.height = Int32.Parse(height);

                                Console.WriteLine("Введите вес(кг):");
                                string width = Console.ReadLine();
                                new_order.width = Int32.Parse(width);

                                break;
                            }
                            else
                            {
                                Console.WriteLine("Введите корректные данные");
                                continue;
                            }
                        }

                        while (true)
                        {
                            Console.WriteLine("Оплата по карте? 1 - да, 0 - нет");
                            string num = Console.ReadLine();
                            if (num == "1")
                            {
                                new_order.card = 1;
                                break;
                            }
                            if (num == "0")
                            {
                                new_order.card = 1;
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Введите корректные данные");
                                continue;
                            }
                        }

                        Console.WriteLine("Вы можете оставить комментарий:");
                        new_order.comment = Console.ReadLine();

                        new_order.status = 1;

                        new_order.price = functions.OrderPrice(new_order);

                        Console.WriteLine("Стоимость заказа: " + new_order.price + " р.");

                        bool serv_ans = functions.AddOrder(new_order);

                        if (serv_ans == true)
                        {
                            Console.WriteLine("Заказ успешно добавлен");
                        }

                        else
                        {
                            Console.WriteLine("Ошибка добавления заказа");
                        }


                    }
                    if (item == "3")
                    {
                        Console.WriteLine("Выберите id заказа, который нужно изменить:");

                        //conn.Open();
                        //try
                        //{
                        int id_client = client.id;

                        lib_wfc.Order[] order_list = functions.GetAllOrders(id_client);
                        

                        for (int i = 0; i < order_list.Length; i++)
                        {
                            Console.WriteLine("id: " + order_list[i].id + " adress from: " + order_list[i].adress_from + " adress to: " + order_list[i].adress_to + " quick: " + order_list[i].quick_price + " date: " + order_list[i].date + " weight: " +
                                 +order_list[i].weight + " documents: " + order_list[i].documents + " lenght: " + order_list[i].lenght + " width: " + order_list[i].width + " height: " + order_list[i].height + " card: " +
                                  +order_list[i].card + " comment: " + order_list[i].comment + " ststus: " + order_list[i].status + " price: " + order_list[i].price);
                        }



                        int id_order;

                        while (!int.TryParse(Console.ReadLine(), out id_order))
                        {
                            Console.WriteLine("Введите корректный id");
                        }


                        try
                        {
                            lib_wfc.Order order = functions.GetOrder(id_order, id_client);
                            

                            while (true)
                            {

                                Console.WriteLine("Место отправки заказа. Текущее значение - " + order.adress_from + ". Изменить на: \n (1 - не изменять)");
                                string answer = Console.ReadLine();
                                if (answer != "1")
                                {
                                    if (answer.Length == 0)
                                    {
                                        Console.WriteLine("Введите корректные данные");
                                        continue;
                                    }

                                    else
                                    {
                                        order.adress_from = answer;
                                        break;
                                    }
                                }
                                else
                                {

                                    break;
                                }
                            }

                            while (true)
                            {
                                Console.WriteLine("Место доставки заказа. Текущее значение - " + order.adress_to + ". Изменить на: \n (1 - не изменять)");
                                string answer = Console.ReadLine();
                                if (answer != "1")
                                {
                                    if (answer.Length == 0)
                                    {
                                        Console.WriteLine("Введите корректные данные");
                                        continue;
                                    }

                                    else
                                    {
                                        order.adress_to = answer;
                                        break;
                                    }
                                }
                                else
                                {

                                    break;
                                }
                            }

                            while (true)
                            {
                                Console.WriteLine("Нужна ли быстрая доставка? Текущее значение - " + order.quick_price + ". Изменить на (0 - нужна, 2 - не нужна): \n (1 - не изменять)");
                                int answer;
                                if (!int.TryParse(Console.ReadLine(), out answer))
                                {
                                    Console.WriteLine("Введите корректные данные");
                                    continue;
                                }

                                else
                                {
                                    if (answer == 1)
                                    {

                                        break;
                                    }
                                    if (answer == 0)
                                    {
                                        order.quick_price = 500;
                                        break;
                                    }
                                    if (answer == 2)
                                    {
                                        order.quick_price = 0;
                                        break;
                                    }
                                    if (answer > 2)
                                    {
                                        Console.WriteLine("Введите корректные данные");
                                        continue;
                                    }
                                }

                            }

                            while (true)
                            {
                                Console.WriteLine("Дата доставки заказа. Текущее значение - " + order.date + ". Изменить на (дд.мм.гггг): \n (1 - не изменять)");
                                string answer = Console.ReadLine();
                                if (answer != "1")
                                {
                                    if (answer.Length == 0)
                                    {
                                        Console.WriteLine("Введите корректные данные");
                                        continue;
                                    }

                                    else
                                    {
                                        order.date = Convert.ToDateTime(answer);
                                        break;
                                    }
                                }
                                else
                                {

                                    break;
                                }
                            }

                            while (true)
                            {
                                Console.WriteLine("Изменить вид посылки. 2 - документы, 0 - посылка: \n (1 - не изменять)");
                                int answer;
                                if (!int.TryParse(Console.ReadLine(), out answer))
                                {
                                    Console.WriteLine("Введите корректные данные");
                                    continue;
                                }

                                else
                                {
                                    if (answer == 1)
                                    {
                                        break;
                                    }

                                    if (answer == 0)
                                    {
                                        order.documents = 0;

                                        Console.WriteLine("Введите вес посылки (кг)");
                                        order.weight = Convert.ToInt32(Console.ReadLine());

                                        Console.WriteLine("Введите длину посылки (м)");
                                        order.lenght = Convert.ToInt32(Console.ReadLine());

                                        Console.WriteLine("Введите ширину посылки (м)");
                                        order.width = Convert.ToInt32(Console.ReadLine());

                                        Console.WriteLine("Введите высоту посылки (м)");
                                        order.height = Convert.ToInt32(Console.ReadLine());

                                        break;
                                    }
                                    if (answer == 2)
                                    {
                                        order.documents = 1;

                                        order.weight = 0;
                                        order.lenght = 0;
                                        order.width = 0;
                                        order.height = 0;

                                        break;
                                    }
                                    if (answer > 2)
                                    {
                                        Console.WriteLine("Введите корректные данные");
                                        continue;
                                    }
                                }
                            }



                            while (true)
                            {
                                Console.WriteLine("Изменить вид оплаты. Текущее значение - " + order.card + ". Изменить на (0 - по карте, 2 - наличными): \n (1 - не изменять)");
                                int answer;
                                if (!int.TryParse(Console.ReadLine(), out answer))
                                {
                                    Console.WriteLine("Введите корректные данные");
                                    continue;
                                }

                                else
                                {
                                    if (answer == 1)
                                    {

                                        break;
                                    }
                                    if (answer == 0)
                                    {
                                        order.card = 1;
                                        break;
                                    }
                                    if (answer == 2)
                                    {
                                        order.card = 0;
                                        break;
                                    }
                                    if (answer > 2)
                                    {
                                        Console.WriteLine("Введите корректные данные");
                                        continue;
                                    }
                                }

                            }


                            while (true)
                            {

                                Console.WriteLine("Изменить комментарий. Текущее значение - " + order.comment + ". Изменить на: \n (1 - не изменять)");
                                string answer = Console.ReadLine();
                                if (answer != "1")
                                {
                                    if (answer.Length == 0)
                                    {
                                        Console.WriteLine("Введите корректные данные");
                                        continue;
                                    }

                                    else
                                    {
                                        order.comment = answer;
                                        break;
                                    }
                                }
                                else
                                {

                                    break;
                                }
                            }

                            order.price = functions.OrderPrice(order);

                            functions.SaveOrder(id_order, id_client, order);

                            Console.WriteLine("Данные успешно изменены");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Error: " + e);
                            Console.WriteLine(e.StackTrace);
                        }

                        Console.Read();


                    }
                    if (item == "4")
                    {
                        Console.WriteLine("Выберите id заказа, статус которого нужно изменить:");


                        try
                        {
                            int id_client = client.id;

                            lib_wfc.Order[] order_list = functions.GetAllOrders(id_client);

                            

                            for (int i = 0; i < order_list.Length; i++)
                            {
                                Console.WriteLine("id: " + order_list[i].id + " adress from: " + order_list[i].adress_from + " adress to: " + order_list[i].adress_to + " quick: " + order_list[i].quick_price + " date: " + order_list[i].date + " weight: " +
                                     +order_list[i].weight + " documents: " + order_list[i].documents + " lenght: " + order_list[i].lenght + " width: " + order_list[i].width + " height: " + order_list[i].height + " card: " +
                                      +order_list[i].card + " comment: " + order_list[i].comment + " ststus: " + order_list[i].status + " price: " + order_list[i].price);
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Error: " + e);
                            Console.WriteLine(e.StackTrace);
                        }


                        int id_order;

                        while (!int.TryParse(Console.ReadLine(), out id_order))
                        {
                            Console.WriteLine("Введите корректный id");
                        }


                        try
                        {
                            int id_client = client.id;
                            lib_wfc.Order order = functions.GetOrder(id_order, id_client);
                            string status = "нет значения";

                            if (order.status == 1)
                            {
                                status = "выполняется";
                            }
                            if (order.status == 0)
                            {
                                status = "отменен";
                            }
                            if (order.status == 2)
                            {
                                status = "выполнен";
                            }

                            while (true)
                            {
                                Console.WriteLine("Статус вашего заказа - " + status + " \nИзменить на (0 - отменен, 2 - выполнен)");
                                int answer;
                                if (!int.TryParse(Console.ReadLine(), out answer))
                                {
                                    Console.WriteLine("Введите корректные данные");
                                    continue;
                                }

                                else
                                {
                                    if (answer == 0)
                                    {
                                        order.status = 0;
                                        break;
                                    }
                                    if (answer == 2)
                                    {
                                        order.status = 2;
                                        break;
                                    }
                                    if (answer > 2)
                                    {
                                        Console.WriteLine("Введите корректные данные");
                                        continue;
                                    }
                                }
                            }

                            functions.SaveOrder(id_order, id_client, order);

                            Console.WriteLine("Данные успешно изменены");


                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Error: " + e);
                            Console.WriteLine(e.StackTrace);
                        }

                        Console.Read();
                    }
                }

                else if (item == "4")
                {
                    Console.WriteLine("Что вы хотите сделать?: \n" +
                    "1 - Показать информацию о себе\n" +
                    "2 - Редактировать информацию о себе\n" +
                    "3 - Главное меню\n");
                    item = Console.ReadLine();

                    if (item == "1")
                    {

                        try
                        {
                            int id_client = client.id;
                            lib_wfc.Person person = functions.GetPerson(id_client);
                            Console.WriteLine("ФИО - " + person.name + " phone: " + person.phone + " db: " + person.birthday + " email: " + person.email);


                            Console.WriteLine("Нажмите любую клавишу");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Error: " + e);
                            Console.WriteLine(e.StackTrace);
                        }

                        Console.Read();
                    }
                    if (item == "2")
                    {

                        try
                        {
                            int id_client = client.id;

                            Console.WriteLine("Редактируемые данные:\n");

                            lib_wfc.Person person = functions.GetPerson(id_client);


                            while (true)
                            {

                                Console.WriteLine("ФИО. Текущее значение - " + person.name + ". Изменить на: \n (1 - не изменять)");
                                string text = Console.ReadLine();
                                if (text != "1")
                                {
                                    if (text.Length == 0)
                                    {
                                        Console.WriteLine("Введите корректные данные");
                                        continue;
                                    }

                                    else
                                    {
                                        person.name = text;
                                        break;
                                    }
                                }
                                else
                                {

                                    break;
                                }
                            }




                            while (true)
                            {

                                Console.WriteLine("Номер телефона. Текущее значение - " + person.phone + ". Изменить на: \n (1 - не изменять)");
                                int text;
                                if (!int.TryParse(Console.ReadLine(), out text))
                                {
                                    Console.WriteLine("Введите корректные данные");
                                    continue;
                                }
                                else
                                {
                                    person.phone = text;
                                    break;
                                }
                            }

                            while (true)
                            {
                                Console.WriteLine("Email. Текущее значение - " + person.email + ". Изменить на: \n (1 - не изменять)");
                                string text = Console.ReadLine();
                                if (text != "1")
                                {
                                    if (text.Length == 0)
                                    {
                                        Console.WriteLine("Введите корректные данные");
                                        continue;
                                    }

                                    else
                                    {
                                        person.email = text;
                                        break;
                                    }
                                }
                                else
                                {

                                    break;
                                }
                            }


                            while (true)
                            {
                                Console.WriteLine("Дата рождения. Текущее значение - " + person.birthday + ". Изменить на (дд.мм.гггг): \n (1 - не изменять)");
                                string text = Console.ReadLine();
                                if (text != "1")
                                {
                                    if (text.Length == 0)
                                    {
                                        Console.WriteLine("Введите корректные данные");
                                        continue;
                                    }

                                    else
                                    {
                                        person.birthday = Convert.ToDateTime(text);
                                        break;
                                    }
                                }
                                else
                                {

                                    break;
                                }
                            }



                            while (true)
                            {
                                Console.WriteLine("Изменить пароль (enter): \n (1 - не изменять)");
                                string text = Console.ReadLine();

                                string pass1;
                                string pass2;

                                Console.WriteLine("Введите старый пароль");

                                pass1 = Console.ReadLine();

                                if (text != "1")
                                {
                                    if (pass1 == person.password)
                                    {
                                        Console.WriteLine("Введите новый пароль");

                                        pass2 = Console.ReadLine();
                                        if (pass2.Length == 0)
                                        {
                                            Console.WriteLine("Введите корректные данные");
                                            continue;
                                        }

                                        else
                                        {
                                            Console.WriteLine("Повторите пароль");
                                            string pass3 = Console.ReadLine();
                                            if (pass3 == pass2)
                                            {
                                                person.password = pass2;
                                                break;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Пароли не совпадают");
                                                continue;
                                            }

                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Введен неверный пароль");
                                        continue;
                                    }
                                }



                            }

                            functions.SavePerson(id_client, person);

                            Console.WriteLine("Данные успешно изменены");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Error: " + e);
                            Console.WriteLine(e.StackTrace);
                        }

                        Console.Read();
                    }
                }




                Console.ReadLine();
            }
            Main();



        }
    }
}
