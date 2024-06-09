using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibraryBankCards;
namespace _14LAB
{
    public  class BankOperations
    {
        //Метод печати коллекции
        public static void PrintCollections(List<SortedDictionary<int, BankCard>> bank)
        {
            for (int i = 0; i < bank.Count; i++)
            {
                foreach (var card in bank[i].Values)
                {
                    card.Show();
                }
            }
        }

        // Метод для выборки данных (Where)
        public static List<BankCard> SelectDataUsingLINQ(List<SortedDictionary<int, BankCard>> bank)
        {
            Console.WriteLine("Карты, срок действия которых больше 2025 года:");
            var linqQuery = from branch in bank
                            from card in branch.Values
                            where card.Date > 2025
                            select card;
            Console.WriteLine("\nLINQ-запрос:");
            foreach (var card in linqQuery)
            {
                card.Show();
            }
            return linqQuery.ToList();
        }

        public static List<BankCard> SelectDataUsingExtensionMethods(List<SortedDictionary<int, BankCard>> bank)
        {
            Console.WriteLine("Карты, срок действия которых больше 2025 года:");
            var methodQuery = bank.SelectMany(branch => branch.Values)
                                  .Where(card => card.Date > 2025);
            Console.WriteLine("\nМетоды расширения:");
            foreach (var card in methodQuery)
            {
                card.Show();
            }
            return methodQuery.ToList();
        }


        // Метод для операций над множествами (Union, Except, Intersect)
        public static string SetOperations(List<SortedDictionary<int, BankCard>> bank)
        {
            StringBuilder resultBuilder = new StringBuilder();

            BankCard dc1 = new BankCard { Number = "2000 0000 4000 3000", Owner = "Иван Петров", Date = 2024, id = new IdNumber { Number = 1 } };
            BankCard dc2 = new BankCard { Number = "2000 7000 4000 3000", Owner = "Пётр Сидоров", Date = 2025, id = new IdNumber { Number = 2 } };

            bank[0].Add(dc1.id.Number, dc1);
            bank[1].Add(dc2.id.Number, dc2);

            // Union
            var unionResult = bank[0].Values.Union(bank[1].Values).Union(bank[2].Values);
            resultBuilder.AppendLine("\nРезультат объединения:");
            foreach (var card in unionResult)
            {
                resultBuilder.AppendLine($"Number: {card.Number}, Owner: {card.Owner}, Date: {card.Date}");
            }

            // Except
            var exceptResult1 = bank[0].Values.Except(bank[1].Values).Except(bank[2].Values);
            var exceptResult2 = bank[1].Values.Except(bank[0].Values).Except(bank[2].Values);
            var exceptResult3 = bank[2].Values.Except(bank[0].Values).Except(bank[1].Values);

            resultBuilder.AppendLine("\nРезультат исключения:");
            resultBuilder.AppendLine("Первое отделение, исключая второе и третье отделения:");
            foreach (var card in exceptResult1)
            {
                resultBuilder.AppendLine($"Number: {card.Number}, Owner: {card.Owner}, Date: {card.Date}");
            }
            resultBuilder.AppendLine("Второе отделение, исключая первое и третье отделения:");
            foreach (var card in exceptResult2)
            {
                resultBuilder.AppendLine($"Number: {card.Number}, Owner: {card.Owner}, Date: {card.Date}");
            }
            resultBuilder.AppendLine("Третье отделение, исключая первое и второе отделения:");
            foreach (var card in exceptResult3)
            {
                resultBuilder.AppendLine($"Number: {card.Number}, Owner: {card.Owner}, Date: {card.Date}");
            }

            // Intersect
            var intersectResult1 = bank[0].Values.Intersect(bank[1].Values);
            var intersectResult2 = bank[0].Values.Intersect(bank[2].Values);
            var intersectResult3 = bank[1].Values.Intersect(bank[2].Values);

            resultBuilder.AppendLine("\nРезультаты пересечения:");
            resultBuilder.AppendLine("Первое отделение и второе отделение пересекаются в:");
            foreach (var card in intersectResult1)
            {
                resultBuilder.AppendLine($"Number: {card.Number}, Owner: {card.Owner}, Date: {card.Date}");
            }
            resultBuilder.AppendLine("Первое отделение и третье отделение пересекаются в:");
            foreach (var card in intersectResult2)
            {
                resultBuilder.AppendLine($"Number: {card.Number}, Owner: {card.Owner}, Date: {card.Date}");
            }
            resultBuilder.AppendLine("Второе отделение и третье отделение пересекаются в:");
            foreach (var card in intersectResult3)
            {
                resultBuilder.AppendLine($"Number: {card.Number}, Owner: {card.Owner}, Date: {card.Date}");
            }

            return resultBuilder.ToString();
        }

        public static double SumBalanceUsingLINQ(List<SortedDictionary<int, BankCard>> bank)
        {
            var totalBalance = (from branch in bank
                                from card in branch.Values
                                where card is DebitCard
                                select (card as DebitCard).Balance).Sum();

            Console.WriteLine("\nLINQ-запрос:");
            Console.WriteLine($"Общий баланс: {totalBalance}");
            return totalBalance;
        }

        public static double SumBalanceUsingExtensionMethods(List<SortedDictionary<int, BankCard>> bank)
        {
            // Метод расширения
            var debitCards = bank.SelectMany(branch => branch.Values)
                                 .OfType<DebitCard>();

            double sumBalance = debitCards.Sum(card => card.Balance);

            Console.WriteLine($"\nМетод расширения:");
            Console.WriteLine($"Сумма баланса: {sumBalance}");
            return sumBalance;
        }


        //Максимальный лимит и минимальный срок погашения по кредитным картам
        public static (double MaxLimit, int MinRepaymentTerm) MinMaxCreditCardUsingLINQ(List<SortedDictionary<int, BankCard>> bank)
        {
            // LINQ-запрос
            var creditCards = from branch in bank
                              from card in branch.Values
                              where card is CreditCard
                              select card as CreditCard;

            var maxLimit = creditCards.Max(card => card.Limit);
            var minRepaymentTerm = creditCards.Min(card => card.RepaymentTerm);
            Console.WriteLine("\nLINQ-запрос:");
            Console.WriteLine($"Максимальный лимит: {maxLimit}");
            Console.WriteLine($"Минимальный срок погашения: {minRepaymentTerm}");
            return (maxLimit, minRepaymentTerm);
        }

        public static (double MaxLimit, int MinRepaymentTerm) MinMaxCreditCardUsingExtensionMethods(List<SortedDictionary<int, BankCard>> bank)
        {
            // Метод расширения
            var creditCards = bank
            .SelectMany(branch => branch.Values)
            .OfType<CreditCard>();

            var maxLimit = creditCards.Max(card => card.Limit);
            var minRepaymentTerm = creditCards.Min(card => card.RepaymentTerm);
            Console.WriteLine("\nМетод расширения:");
            Console.WriteLine($"Максимальный лимит: {maxLimit}");
            Console.WriteLine($"Минимальный срок погашения: {minRepaymentTerm}");
            return (maxLimit, minRepaymentTerm);
        }


        //Средний кэшбек по молодёжным картам 
        public static double AverageCashbackUsingLINQ(List<SortedDictionary<int, BankCard>> bank)
        {
            var averageCashback = (from branch in bank
                                   from card in branch.Values
                                   where card is YouthCard
                                   select (card as YouthCard).Cashback)
                                   .Average();

            Console.WriteLine($"\nLINQ-запрос:");
            Console.WriteLine($"Средний кэшбек: {averageCashback}");
            return averageCashback;
        }


        public static double AverageCashbackUsingExtensionMethods(List<SortedDictionary<int, BankCard>> bank)
        {
            // Метод расширения
            var youthCards = bank.SelectMany(branch => branch.Values)
                                 .OfType<YouthCard>();

            double avgCashback = youthCards.Average(card => card.Cashback);
            Console.WriteLine($"\nМетод расширения:");
            Console.WriteLine($"Средний кэшбек: {avgCashback}");
            return avgCashback;
        }

        // Метод для группировки данных (Group by)
        public static IEnumerable<IGrouping<int, BankCard>> GroupDataUsingLINQ(List<SortedDictionary<int, BankCard>> bank)
        {
            // Группировка по году действия карты
            IEnumerable<IGrouping<int, BankCard>> groupedByDate = from branch in bank
                                                                  from card in branch.Values
                                                                  group card by card.Date;

            Console.WriteLine("\nLINQ-запрос:");
            foreach (var group in groupedByDate)
            {
                Console.WriteLine($"Год: {group.Key}, Количество: {group.Count()}");
                foreach (var card in group)
                {
                    card.Show();
                }
            }

            return groupedByDate;
        }

        public static IEnumerable<IGrouping<int, BankCard>> GroupDataUsingExtensionMethods(List<SortedDictionary<int, BankCard>> bank)
        {
            // Использование методов расширения
            IEnumerable<IGrouping<int, BankCard>> groupedByDateMethod = bank.SelectMany(branch => branch.Values)
                                                                            .GroupBy(card => card.Date);

            Console.WriteLine("\nМетоды расширения:");
            foreach (var group in groupedByDateMethod)
            {
                Console.WriteLine($"Год: {group.Key}, Количество: {group.Count()}");
                foreach (var card in group)
                {
                    card.Show();
                }
            }
            return groupedByDateMethod;
        }


        // Вычисление количества лет до истечения действия карты (Let)
        public static IEnumerable<object> CreateNewTypeUsingLINQ(List<SortedDictionary<int, BankCard>> bank)
        {
            // Создание нового типа с использованием Let в LINQ-запросах
            var linqQuery = from branch in bank
                            from card in branch.Values
                            let newType = new { card.Number, card.Owner, YearLeft = card.Date - DateTime.Now.Year }
                            select newType;

            foreach (var item in linqQuery)
            {
                Console.WriteLine($"Номер: {item.Number}, Владелец: {item.Owner}, до истечения срока карты: {item.YearLeft} лет");
            }

            return linqQuery.Cast<object>();
        }

        public static IEnumerable<object> CreateNewTypeUsingExtensionMethods(List<SortedDictionary<int, BankCard>> bank)
        {
            // Использование методов расширения
            var methodQuery = bank.SelectMany(branch => branch.Values)
                                  .Select(card => new { card.Number, card.Owner, YearLeft = card.Date - DateTime.Now.Year });

            foreach (var item in methodQuery)
            {
                Console.WriteLine($"Номер: {item.Number}, Владелец: {item.Owner}, до истечения срока карты: {item.YearLeft} лет");
            }

            return methodQuery.Cast<object>();
        }


        // Метод для соединения (Join)
        public static IEnumerable<object> JoinDataUsingLINQ(List<SortedDictionary<int, BankCard>> bank)
        {
            DebitCard dc1 = new DebitCard { Number = "2000 0000 4000 3000", Owner = "Иван Петров", Date = 2024, id = new IdNumber { Number = 1 }, Balance = 10000, };
            DebitCard dc2 = new DebitCard { Number = "2000 7000 4000 3000", Owner = "Пётр Сидоров", Date = 2025, id = new IdNumber { Number = 2 }, Balance = 20000, };
            DebitCard dc3 = new DebitCard { Number = "2000 0000 4000 6000", Owner = "Владимир Смирнов", Date = 2026, id = new IdNumber { Number = 3 }, Balance = 15000, };
            bank[0].Add(dc1.id.Number, dc1);
            bank[1].Add(dc1.id.Number, dc2);
            bank[2].Add(dc1.id.Number, dc3);

            // Для примера добавим новый тип данных - Account (аккаунт с балансом)
            List<Account> accounts = new List<Account>();
            Account a1 = new Account(10000, 2, 6);
            Account a2 = new Account(20000, 1, 7);
            Account a3 = new Account(15000, 3, 8);
            accounts.Add(a1);
            accounts.Add(a2);
            accounts.Add(a3);
            var joinQuery = from branch in bank
                            from card in branch.Values
                            where card is DebitCard
                            join account in accounts on ((DebitCard)card).Balance equals account.Balance
                            select new
                            {
                                CardOwner = card.Owner,
                                Balance = account.Balance,
                                NumberOfDeposits = account.DepositCount,
                                DepositInterestRate = account.DepositInterestRate
                            };
            Console.WriteLine("\nLINQ-запрос:");
            foreach (var item in joinQuery)
            {
                Console.WriteLine($"Держатель карты: {item.CardOwner}, Баланс: {item.Balance}, Количество вкладов: {item.NumberOfDeposits}, Процент по вкладам: {item.DepositInterestRate}");
            }
            return joinQuery.ToList();
        }

        public static IEnumerable<object> JoinDataUsingExtensionMethods(List<SortedDictionary<int, BankCard>> bank)
        {
            DebitCard dc1 = new DebitCard { Number = "2000 0000 4000 3000", Owner = "Иван Петров", Date = 2024, id = new IdNumber { Number = 1 }, Balance = 10000, };
            DebitCard dc2 = new DebitCard { Number = "2000 7000 4000 3000", Owner = "Пётр Сидоров", Date = 2025, id = new IdNumber { Number = 2 }, Balance = 20000, };
            DebitCard dc3 = new DebitCard { Number = "2000 0000 4000 6000", Owner = "Владимир Смирнов", Date = 2026, id = new IdNumber { Number = 3 }, Balance = 15000, };
            bank[0].Add(dc1.id.Number, dc1);
            bank[1].Add(dc1.id.Number, dc2);
            bank[2].Add(dc1.id.Number, dc3);

            // Для примера добавим новый тип данных - Account (аккаунт с балансом)
            List<Account> accounts = new List<Account>();
            Account a1 = new Account (10000, 2, 6 );
            Account a2 = new Account (20000, 1, 7 );
            Account a3 = new Account (15000, 3, 8 );
            accounts.Add(a1);
            accounts.Add(a2);
            accounts.Add(a3);
            var joinMethodQuery = bank.SelectMany(branch => branch.Values)
                                      .OfType<DebitCard>()
                                      .Join(accounts,
                                            card => card.Balance,
                                            account => account.Balance,
                                            (card, account) => new
                                            {
                                                CardOwner = card.Owner,
                                                Balance = account.Balance,
                                                NumberOfDeposits = account.DepositCount,
                                                DepositInterestRate = account.DepositInterestRate
                                            });

            Console.WriteLine("\nМетод расширения:");
            foreach (var item in joinMethodQuery)
            {
                Console.WriteLine($"Держатель карты: {item.CardOwner}, Баланс: {item.Balance}, Количество вкладов: {item.NumberOfDeposits}, Процент по вкладам: {item.DepositInterestRate}");
            }
            return joinMethodQuery.ToList();
        }
    }
}
