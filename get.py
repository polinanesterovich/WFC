#-*- coding: utf8 -*-
from zeep import Client
import datetime
# from datetime import datetime
import timestring

client = Client('http://localhost:8733/Design_Time_Addresses/lib_wfc/Service1/?wsdl')
isAuth = 0
userСhoice = 0
global TOKEN
TOKEN = ""


def price_def(order):
    order_price = 500
    if order['quick_price'] == 500:
        order_price += 500
    prices = [2000, 1000, 500, 100]
    params = [int(order['lenght']), int(order['width']), int(order['height']), int(order['weight'])]
    for param in params:
        for price in prices:
            if param > price:
                order_price += price
                break
    return order_price

def print_services(services): 
    print("="*45)
    for service in services:
        print("%s - %s - %s" % (service["id"], service["description"], service["price"]))

def print_actions(actions):
    print('='*45)
    for action in actions:
        print("%s - %s - %s - %s" % (action["id"], action["name"], action["description"], action["sale"]))

def print_orders(orders):
    print('='*45)
    for order in orders:
        print("%s - %s - %s - %s - %s - %s" % (order["id"], order["adress_from"], order["adress_to"], order["date"], order["price"], order["status"]))  



while(True):
    if (isAuth == 0):
        {
            print("Что будем делать? \n1-Просмотреть список услуг \n2-Просмотреть список акций \n3-Авторизация \n4-Выход из программы")
        }
    userСhoice = int(input())
    if (userСhoice == 1):
        services = client.service.GetServices()
        print_services(services)
        
    if (userСhoice == 2):
        actions = client.service.GetActions()
        print_actions(actions)
    if (userСhoice == 3):
        login = input("Введите логин\n")
        pwd = input("Введите пароль\n")
        
        person = client.service.Autorization(login, pwd)
        TOKEN = person["token"]
        

        if (person["id"] != 0):

            print("Авторизация пройдена")
            client.id = person["id"]
            isAuth = 1
            
        else:
            print("Авторизация не пройдена")
    if (userСhoice == 4):
        exit(0)

    
    if (userСhoice == 5 and isAuth == 1):
        myOrders = client.service.GetAllOrders(person["id"], TOKEN)
        
        if myOrders == None:
            print("Заказов нет")
        else:
            print_orders(myOrders)

    if (userСhoice == 6 and isAuth == 1):
          
        order = {}
    
        print(" ")
        while True:
            print("Место отправки (сообщение должно содержать больше 5 символов):")
            adress_from = input()
            if len(adress_from) == 0 or len(adress_from) < 5:
                print(" ")
                print ("Введите корректные данные")
                continue
            else:
                order['adress_from'] = adress_from
                break
        print(" ")
        while True:
            print("Место доставки:")

            adress_to = input()
            if len(adress_to) == 0 or len(adress_to) < 5:
                print(" ")
                print ("Введите корректные данные! (сообщение должно содержать больше 5 символов)")
                continue
            else:
                order['adress_to'] = adress_to
                break
        print(" ")

        while True:
            print("Дата доставки (в формате дд.мм.ггг):")

            date = input()
            if len(date) == 0 or len(date) < 9:
                print(" ")
                print ("Введите корректные данные в формате дд.мм.гггг")
                continue
            else:
                order['date'] = datetime.datetime.strptime(date, '%d.%m.%Y')
                

                break
            
        print(" ")

        while True:
            print("Нужна ли быстрая доставка? да - 1, нет - 0:")
            quick_price = input()
            if len(quick_price) == 0 or int(quick_price) > 1 :
                print(" ")
                print ("Введите корректные данные")
                continue
            if quick_price == '1':
                order['quick_price'] = 500
            
                break
            else:
                order['quick_price'] = 0
                break

        while True:
            print(" ")
            print("Документы? (0 - нет, 1 - да):")
            s = input()
            if len(s) == 0:
                print(" ")
                print ("Не может быть пустым!")
                continue
            if s == '1':
                order['documents'] = s
                order['lenght'] = 0
                order['width'] = 0
                order['height'] = 0
                order['weight'] = 0
                
                break
            else:
                order['documents'] = 0
                
                print(" ")
                print("Длина (см):")
                order['lenght'] = input()
            
                print(" ")
                print("Ширина (см):")
                order['width'] = input()
            
                print(" ")
                print("Высота (см):")
                order['height'] = input()
            
                print(" ")
                print("Вес (кг):")
                order['weight'] = input()
            
                break

        while True:
            print(" ")
            print("Оплата по карте? (0 - да, 1 - нет):")
            card = input()
            if len(card) == 0 or int(card) > 1:
                    print(" ")
                    print ("Введите корректные данные!")
                    continue
            if card == '0' or card == '1':
                order['card'] = s
                break
            else:
                order['card'] = 0
                break
        print(" ")
        print("Вы можете оставить комментарий к заказу:")
        order['comment'] = input()
        order['status'] = '1'
        order['id_client'] = int(person["id"])
        order['price'] = price_def(order)

        ch = client.service.AddOrder(order, TOKEN)
        
        if(ch == True):
            {
                print("Успешно")
            }
        else:
            {
                print("Заказ не добавлен")
            }


    if (userСhoice == 7 and isAuth == 1):
        print(" ")
        print("Выберите id редатируемого заказа из списка:")
        myOrders = client.service.GetAllOrders(int(person["id"]), TOKEN)
        print_orders(myOrders)

        id_order = input()

        order = client.service.GetOrder(int(id_order), int(person["id"]), TOKEN)

        for row in order:   
        
            if row == "adress_from":
                print()
                while True:
                    print('Изменить место отправки (1 - без изменений)')
                    print(" ")
                    item = input('старое значение: "' + str(order["adress_from"]) + '", изменить на: ')
                    if item != '1':
                        if len(item) < 5 or len(item) == 0:
                            print("Введите корректные данные")
                            continue
                        else:
                            order["adress_from"] = item
                            break
                    if item == '1':
                        order["adress_from"] = order["adress_from"]
                        break
                    if len(item) == 0:
                        print(" ")
                        print ("Введите корректные данные")
                        continue
            if row == "adress_to":
                print()
                while True:
                    print('Изменить место доставки (1 - без изменений)')
                    print(" ")
                    item = input('старое значение: "' + str(order["adress_to"]) + '", изменить на: ')
                    if len(item) == 0:
                            print(" ")
                            print ("Введите корректные данные")
                            continue
                    if item != '1':
                        if len(item) < 5:
                            print("Введите корректные данные")
                            continue
                        else:
                            order["adress_to"] = item
                            break
                    if item == '1':
                        order["adress_to"] = order["adress_to"]
                        break
            if row == "quick_price":
                while True:
                    print(" ")
                    print('Изменить срочность доставки (1 - без изменений)')
                    print(" ")
                    item = input('изменить на 2 - срочно, 0 - не срочно: ')
                    if item == '0':
                        order["quick_price"] = 0
                        break
                    if item == '2':
                        order["quick_price"] = 500
                        
                        break
                    if len(item) == 0:
                        print(" ")
                        print ("Не может быть пустым!")
                        continue
                    if item == '1':
                        break
            if row == "date": 
                print()
                while True:
                    print('Изменить дату отправки (1 - без изменений)')
                    print(" ")
                    item = input('старое значение: "' + str(order["date"]) + '", изменить на: ')
                    if item != '1':
                        if len(item)< 9:
                            print("Введите корректные данные")
                            continue
                        else:
                            order["date"] = datetime.datetime.strptime(item, '%d.%m.%Y')
                            break
                    if len(item) == 0:
                        print(" ")
                        print ("Введите корректные данные")
                        continue
                    if item == '1':
                        order["date"] = order["date"]
                        break
            if row == "documents": 
                print()
                while True:
                    print('Изменить вид поссылки  (1 - без изменений)')
                    print(" ")
                    item = input('изменить на документы - 0, посылка - 2: ')
                    if len(item) == 0 or int(item) > 2:
                            print(" ")
                            print ("Введите корректные данные")
                            continue
                    if item == '1':
                        break
                    if item == '0':
                        order["documents"] = 1
                        order["weight"] = 0
                        order["lenght"] = 0
                        order["height"] = 0
                        order["width"] = 0
                        
                        break
                    if item == '2':
                        order["documents"] = 0
                        
                        print(" ")
                        print('Введите вес посылки (кг):')
                        order["weight"] = input()
                    
                        print(" ")
                        print('Введите ширину посылки (см):')
                        order["width"] = input()
                        
                        print(" ")
                        print('Введите высоту посылки (см):')
                        order["height"] = input()
                    
                        print(" ")
                        print('Введите длину посылки (см):')
                        order["lenght"] = input()
                    
                        break
            if row == "card": 
                print()
                while True:
                    print('Изменить способ оплаты  (1 - без изменений)')      
                    print(" ")
                    item = input(' изменить на наличные - 0, карта - 2: ')
                    if len(item) == 0 or int(item) > 2:
                        print(" ")
                        print ("Введите корректные данные")
                        continue
                    if item == '0':
                        order["card"] = 0
                        break
                    if item == '2':
                        order["card"] = 1
                        break
                    if item == '1':
                        order["card"] = order["card"]
                        break
            if row == "status":
                order["status"] = 1
            if row == "comment": 
                print()
                print('Изменить комментарий (1 - без изменений)')      
                print(" ")
                item = input('старое значение: "' + order["comment"] + '", изменить на: ')
                if item == '1':
                    order["comment"] = order["comment"]
                    break
                else:
                    order["comment"] = item
        order_price = 500
        if order['quick_price'] == 500:
            order_price += 500
        prices = [2000, 1000, 500, 100]
        params = [int(order['lenght']), int(order['width']), int(order['height']), int(order['weight'])]
        for param in params:
            for price in prices:
                if param > price:
                    order_price += price
                    break
        order["price"] = order_price


        try:
            ch = client.service.SaveOrder(id_order, person["id"], order, TOKEN)
            if (ch == True):
                print("Успешно")
            else:
                print("Ошибка")
        except:

            print("Ошибка")
        
    
    if (userСhoice == 8 and isAuth == 1):
        print(" ")
        print("Выберите id редатируемого заказа из списка:")
        myOrders = client.service.GetAllOrders(int(person["id"]), TOKEN)
        print_orders(myOrders)

        id_order = input()

        order = client.service.GetOrder(int(id_order), int(person["id"]), TOKEN)
        if (order == None):
            print("Ошибка! Токен не совпадает")
        try:    
            for row in order:   
                if row == "status":
                    print()
                    print('Изменить статус заказа (1 - без изменений)')
                    if str(order["status"]) == "None":
                        st = "нет статуса"
                    if str(order["status"]) == "1":
                        st = "выполняется"
                    if str(order["status"]) == "0":
                        st = "отменен"
                    if str(order["status"]) == "2":
                        st = "выполнен"
                    print(" ")
                    item = input('старое значение: "' + str(st) + '", изменить на: (0 - отменен, 2 - выполнен) ')
                    order["status"] = int(item)  

                if row == "adress_from":
                    order["adress_from"] = order["adress_from"]

                if row == "adress_to":
                    order["adress_to"] = order["adress_to"]

                if row == "quick_price":
                    order["quick_price"] = order["quick_price"]

                if row == "date": 
                    order["date"] = order["date"]

                if row == "documents": 
                    order["documents"] = order["documents"]
                    order["weight"] = order["weight"]
                    order["lenght"] = order["lenght"]
                    order["height"] = order["height"]
                    order["width"] = order["width"]

                if row == "card": 
                    order["card"] = order["card"]
                    
                if row == "comment": 
                    order["comment"] = order["comment"]     
            
            try:
                ch = client.service.SaveOrder(id_order, person["id"], order)

                print("Успешно")
            except:

                print("Ошибка")
        except:
            isAuth = 0

    if (userСhoice == 9 and isAuth == 1):
       
        print("ФИО: " + person["name"] + " " + "Дата рождения: " + str(person["birthday"]) + " " + "Телефон: " + str(person["phone"]) + " Почта: " +  person["email"] + " Пароль: " + person["password"] )

    if (userСhoice == 10 and isAuth == 1):

        for row in person:   
            if row == 'name':
                while True:
                    print()
                    print(" ")
                    print('Изменить имя (1 - без изменений)')
                    print(" ")
                    item = input('старое значение: "' + str(person["name"]) + '", изменить на: ')
                    if len(item) == 0:
                        print("Ведите корректные данные")
                        continue
                    if item != '1':
                        person["name"] = item
                        break
                    else:
                        break

            if row == "phone":
                while True:
                    print()
                    print('Сменить номер телефона (1 - без изменений)')
                    print(" ")
                    item = input('старое значение: "' + str(person["phone"]) + '", изменить на: ')
                    if len(item) == 0:
                        print("Ведите корректные данные")
                        continue
                    if item != '1':
                        person["phone"] = item
                        break
                    else:
                        break
            if row == "email":
                while True:
                    print()
                    print('Сменить email (1 - без изменений)')
                    print(" ")
                    item = input('старое значение: "' + str(person["email"]) + '", изменить на: ')
                    if len(item) == 0:
                        print("Ведите корректные данные")
                        continue
                    if item != '1':
                        person["email"] = item
                        break
                    else:
                        break
            if row == "birthday":
                while True:
                    print()
                    print('Изменить дату рождения (1 - без изменений)')
                    print(" ")
                    item = input('старое значение: "' + str(person["birthday"]) + '", изменить на: ')
                    if len(item) == 0:
                            print("Ведите корректные данные")
                            continue
                    if item != '1':
                        person["birthday"] = datetime.datetime.strptime(item, '%d.%m.%Y')
                        break
                    else:
                        break
            if row == "password":
                print()
                print('Сменить пароль? (Enter - сменить, 1 - без изменений)')
                item = input()
                if item != '1': 
                        print(" ")          
                        print("Смена пароля")
                        
                        while True:
                            print(" ")
                            print("Введите старый пароль")
                            pas = input()

                            if person["password"] == pas:
                                    print(" ")
                                    print("Введите новый пароль (не меньше 4 символов):")
                                    pas1 = input()
                                    print(" ")
                                    if not len(pas1) > 4: 
                                        print(" ")
                                        print ("Неправильная команда!")
                                        continue
                                    print("Повторите пароль:")
                                    pas2 = input()
                                    if pas1 == pas2:
                                        person["password"] = pas1
                                        print(" ")
                                        print("Пароль сменен:")
                                        break
                            else:
                                print(" ")
                                print("Неправильный пароль")
                                print()
                                continue
                else:
                    person["password"] = person["password"]
        
        ch = client.service.SavePerson(person["id"], person, TOKEN)
        if (ch == True):
            print("Успешно")
        else:

            print("Ошибка")

    if (userСhoice == 11 and isAuth == 1):
        isAuth = 0

    if (isAuth == 1):

        
        print("Меню\n1 - Просмотреть список услуг \n2 - Просмотреть список акций \n4 - Выход из программы \n5 - Мои заказы" +
        "\n6 - Создать заказ \n7 - Изменить заказ \n8 - Изменить статус заказа \n9 - Информация профиля \n10 - Изменить данные профиля \n11 - Выйти")
       
  
    
        
