using System;
using System.Collections.Generic;
using System.Linq;

using ClassLibraryBankCards;
namespace _14LAB
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Инициализация банка (List)
            List<SortedDictionary<int, BankCard>> bank = new List<SortedDictionary<int, BankCard>>();

            // Заполнение отделений банка
            for (int i = 0; i < 3; i++) // Три отделения банка
            {
                var branch = new SortedDictionary<int, BankCard>();
                int cardCount = 6; // Количество карт в каждом отделении
                int cardTypesCount = 4; // Количество типов карт

                for (int j = 0; j < cardCount; j++)
                {
                    BankCard card;
                    if (j % cardTypesCount == 0)
                    {
                        card = new BankCard();
                    }
                    else if (j % cardTypesCount == 1)
                    {
                        card = new DebitCard();
                    }
                    else if (j % cardTypesCount == 2)
                    {
                        card = new YouthCard();
                    }
                    else
                    {
                        card = new CreditCard();
                    }
                    card.RandomInit();
                    branch.Add(card.id.Number, card);
                }
                bank.Add(branch);
            }

            //Меню
            int answer = 1;
            while (answer != 10)
            {
                try
                {
                    Console.WriteLine("\nМеню:");
                    Console.WriteLine("1. Распечатать данные коллекции:");
                    Console.WriteLine("2. Карты, со сроком действия больше 2025 (выборка данных Where)");
                    Console.WriteLine("3. Операции над множествами (Union, Except, Intersect)");
                    Console.WriteLine("4. Сумма баланса по дебетовым картам (Sum)");
                    Console.WriteLine("5. Максимальный лимит и минимальный срок погашения по кредитным картам (Max, Min)");
                    Console.WriteLine("6. Средний кэшбек по молодёжным картам (Average)");
                    Console.WriteLine("7. Группировка карт по году действия (группировка данных Group by)");
                    Console.WriteLine("8. Вычисление количества лет до истечения действия карты (новый тип Let)");
                    Console.WriteLine("9. Соединение класса DebitCard с классом Account (Join)");
                    Console.WriteLine("10. Выход");
                    Console.Write("Выберите вариант: \n");

                    answer = int.Parse(Console.ReadLine());
                    switch (answer)
                    {
                        case 1:
                            BankOperations.PrintCollections(bank);
                            break;
                        case 2:
                            BankOperations.SelectDataUsingLINQ(bank);
                            BankOperations.SelectDataUsingExtensionMethods(bank);
                            break;
                        case 3:
                            BankOperations.SetOperations(bank);
                            break;
                        case 4:
                            BankOperations.SumBalanceUsingLINQ(bank);
                            BankOperations.SumBalanceUsingExtensionMethods(bank);
                            break;
                        case 5:
                            BankOperations.MinMaxCreditCardUsingLINQ(bank);
                            BankOperations.MinMaxCreditCardUsingExtensionMethods(bank);
                            break;
                        case 6:
                            BankOperations.AverageCashbackUsingLINQ(bank);
                            BankOperations.AverageCashbackUsingExtensionMethods(bank);
                            break;
                        case 7:
                            BankOperations.GroupDataUsingLINQ(bank);
                            BankOperations.GroupDataUsingExtensionMethods(bank);
                            break;
                        case 8:
                            BankOperations.CreateNewTypeUsingLINQ(bank);
                            BankOperations.CreateNewTypeUsingExtensionMethods(bank);
                            break;
                        case 9:
                            BankOperations.JoinDataUsingLINQ(bank);
                            BankOperations.JoinDataUsingExtensionMethods(bank);
                            break;
                        case 10:
                            Console.WriteLine("Программа завершена");
                            break;
                        default:
                            Console.WriteLine("Неверный выбор, попробуйте снова");
                            break;
                    }
                }
                catch (Exception ex)
                { Console.WriteLine(ex.Message); }
            }
        }     
    }
}