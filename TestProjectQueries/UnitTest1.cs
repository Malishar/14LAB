using ClassLibraryBankCards;
using _14LAB;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace BankTests
{
    [TestClass]
    public class BankTests
    {
        [TestMethod]
        public void SelectDataUsingLINQ_Returns_Cards_With_Expiry_Date_After_2025()
        {
            // Arrange
            var bank = new List<SortedDictionary<int, BankCard>>
        {
            new SortedDictionary<int, BankCard>
            {
                { 1, new BankCard { Number = "1000", Owner = "Геннадий Борисов", Date = 2024 } },
                { 2, new BankCard { Number = "2000", Owner = "Сергей Борисов", Date = 2026 } },
                { 3, new BankCard { Number = "3000", Owner = "Геннадий Пашин", Date = 2027 } }
            },
            new SortedDictionary<int, BankCard>
            {
                { 4, new BankCard { Number = "4000", Owner = "Геннадий Харитонов", Date = 2025 } },
                { 5, new BankCard { Number = "5000", Owner = "Олег Борисов", Date = 2024 } }
            }
        };

            // Act
            var result = BankOperations.SelectDataUsingLINQ(bank);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count); // Expected count of cards with expiry date after 2025

            foreach (var card in result)
            {
                Assert.IsTrue(card.Date > 2025); // Expiry date should be after 2025
            }
        }

        [TestMethod]
        public void SelectDataUsingLINQ_ShouldReturnEmpty_WhenNoCardsAfter2025()
        {
            // Arrange
            var bank = new List<SortedDictionary<int, BankCard>>
        {
            new SortedDictionary<int, BankCard>
            {
                { 1, new BankCard { Number = "123", Owner = "Alice", Date = 2024 } },
                { 2, new BankCard { Number = "124", Owner = "Bob", Date = 2024 } }
            },
            new SortedDictionary<int, BankCard>
            {
                { 3, new BankCard { Number = "125", Owner = "Charlie", Date = 2023 } },
                { 4, new BankCard { Number = "126", Owner = "David", Date = 2023 } }
            }
        };

            // Act
            var result = BankOperations.SelectDataUsingLINQ(bank);

            // Assert
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void SelectDataUsingMethod_Returns_Cards_With_Expiry_Date_After_2025()
        {
            // Arrange
            var bank = new List<SortedDictionary<int, BankCard>>
        {
            new SortedDictionary<int, BankCard>
            {
                { 1, new BankCard { Number = "1000", Owner = "Геннадий Борисов", Date = 2024 } },
                { 2, new BankCard { Number = "2000", Owner = "Сергей Борисов", Date = 2026 } },
                { 3, new BankCard { Number = "3000", Owner = "Геннадий Пашин", Date = 2027 } }
            },
            new SortedDictionary<int, BankCard>
            {
                { 4, new BankCard { Number = "4000", Owner = "Геннадий Харитонов", Date = 2025 } },
                { 5, new BankCard { Number = "5000", Owner = "Олег Борисов", Date = 2024 } }
            }
        };

            // Act
            var result = BankOperations.SelectDataUsingExtensionMethods(bank);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);

            foreach (var card in result)
            {
                Assert.IsTrue(card.Date > 2025);
            }
        }

        [TestMethod]
        public void SelectDataUsingExtensionMethods_ShouldReturnEmpty_WhenNoCardsAfter2025()
        {
            // Arrange
            var bank = new List<SortedDictionary<int, BankCard>>
        {
            new SortedDictionary<int, BankCard>
            {
                { 1, new BankCard { Number = "123", Owner = "Alice", Date = 2024 } },
                { 2, new BankCard { Number = "124", Owner = "Bob", Date = 2024 } }
            },
            new SortedDictionary<int, BankCard>
            {
                { 3, new BankCard { Number = "125", Owner = "Charlie", Date = 2023 } },
                { 4, new BankCard { Number = "126", Owner = "David", Date = 2023 } }
            }
        };

            // Act
            var result = BankOperations.SelectDataUsingExtensionMethods(bank);

            // Assert
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void SumBalanceUsingLINQ_Returns_Correct_Sum()
        {
            // Arrange
            var bank = new List<SortedDictionary<int, BankCard>>
        {
            new SortedDictionary<int, BankCard>
            {
                { 1, new DebitCard { Number = "2000 0000 4000 3000", Owner = "Иван Петров", Date = 2024, id = new IdNumber { Number = 1 }, Balance = 1000 } },
                { 2, new DebitCard { Number = "2000 7000 4000 3000", Owner = "Пётр Сидоров", Date = 2025, id = new IdNumber { Number = 2 }, Balance = 1500 } }
            },
            new SortedDictionary<int, BankCard>
            {
                { 3, new DebitCard { Number = "3000 0000 4000 3000", Owner = "Сергей Иванов", Date = 2024, id = new IdNumber { Number = 3 }, Balance = 2000 } }
            }
        };

            double expectedSum = 1000 + 1500 + 2000;

            // Act
            double actualSum = BankOperations.SumBalanceUsingLINQ(bank);

            // Assert
            Assert.AreEqual(expectedSum, actualSum);
        }

        [TestMethod]
        public void SumBalanceUsingMethod_Returns_Correct_Sum()
        {
            // Arrange
            var bank = new List<SortedDictionary<int, BankCard>>
        {
            new SortedDictionary<int, BankCard>
            {
                { 1, new DebitCard { Number = "2000 0000 4000 3000", Owner = "Иван Петров", Date = 2024, id = new IdNumber { Number = 1 }, Balance = 1000 } },
                { 2, new DebitCard { Number = "2000 7000 4000 3000", Owner = "Пётр Сидоров", Date = 2025, id = new IdNumber { Number = 2 }, Balance = 1500 } }
            },
            new SortedDictionary<int, BankCard>
            {
                { 3, new DebitCard { Number = "3000 0000 4000 3000", Owner = "Сергей Иванов", Date = 2024, id = new IdNumber { Number = 3 }, Balance = 2000 } }
            }
        };

            double expectedSum = 1000 + 1500 + 2000;

            // Act
            double actualSum = BankOperations.SumBalanceUsingExtensionMethods(bank);

            // Assert
            Assert.AreEqual(expectedSum, actualSum, "SumBalanceUsingLINQ did not return the correct sum.");
        }

        [TestMethod]
        public void AverageCashbackUsingLINQ_ShouldReturnCorrectValue()
        {
            // Arrange
            var bank = new List<SortedDictionary<int, BankCard>>
        {
            new SortedDictionary<int, BankCard>
            {
                { 1, new YouthCard { Cashback = 3.5 } },
                { 2, new YouthCard { Cashback = 4.0 } }
            },
            new SortedDictionary<int, BankCard>
            {
                { 1, new YouthCard { Cashback = 4.5 } },
                { 2, new YouthCard { Cashback = 5.0 } },
                { 3, new YouthCard { Cashback = 5.5 } }
            }
        };

            // Act
            double result = BankOperations.AverageCashbackUsingLINQ(bank);

            // Assert
            Assert.AreEqual(4.5, result, 0.001, "The average cashback calculated using LINQ is incorrect.");
        }

        [TestMethod]
        public void AverageCashbackUsingMethod_ShouldReturnCorrectValue()
        {
            // Arrange
            var bank = new List<SortedDictionary<int, BankCard>>
        {
            new SortedDictionary<int, BankCard>
            {
                { 1, new YouthCard { Cashback = 3.5 } },
                { 2, new YouthCard { Cashback = 4.0 } }
            },
            new SortedDictionary<int, BankCard>
            {
                { 1, new YouthCard { Cashback = 4.5 } },
                { 2, new YouthCard { Cashback = 5.0 } },
                { 3, new YouthCard { Cashback = 5.5 } }
            }
        };

            // Act
            double result = BankOperations.AverageCashbackUsingExtensionMethods(bank);

            // Assert
            Assert.AreEqual(4.5, result, 0.001, "The average cashback calculated using LINQ is incorrect.");
        }

        [TestMethod]
        public void MinMaxCreditCardUsingLINQ_ShouldReturnCorrectValues()
        {
            // Arrange
            // Arrange
            var bank = new List<SortedDictionary<int, BankCard>>
        {
            new SortedDictionary<int, BankCard>
            {
                { 1, new CreditCard { Limit = 5000, RepaymentTerm = 12 } },
                { 2, new DebitCard { Balance = 10000 } } // Добавляем дебетовую карту, чтобы убедиться, что метод работает только с кредитными картами
            },
            new SortedDictionary<int, BankCard>
            {
                { 1, new CreditCard { Limit = 6000, RepaymentTerm = 9 } },
                { 2, new CreditCard { Limit = 8000, RepaymentTerm = 8 } }
            }
        };

            // Act
            var result = BankOperations.MinMaxCreditCardUsingLINQ(bank);

            // Assert
            Assert.AreEqual(8000, result.MaxLimit);
            Assert.AreEqual(8, result.MinRepaymentTerm);
        }

        [TestMethod]
        public void MinMaxCreditCardUsingExtensionMethods_ShouldReturnCorrectValues()
        {
            // Arrange
            var bank = new List<SortedDictionary<int, BankCard>>
        {
            new SortedDictionary<int, BankCard>
            {
                { 1, new CreditCard { Limit = 5000, RepaymentTerm = 12 } },
                { 2, new DebitCard { Balance = 10000 } }
            },
            new SortedDictionary<int, BankCard>
            {
                { 1, new CreditCard { Limit = 6000, RepaymentTerm = 9 } },
                { 2, new CreditCard { Limit = 8000, RepaymentTerm = 8 } }
            }
        };

            // Act
            var result = BankOperations.MinMaxCreditCardUsingExtensionMethods(bank);

            // Assert
            Assert.AreEqual(8000, result.MaxLimit);
            Assert.AreEqual(8, result.MinRepaymentTerm);
        }

        [TestMethod]
        public void GroupDataUsingLINQTest()
        {
            // Arrange
            List<SortedDictionary<int, BankCard>> bank = GetTestBankData();

            // Act
            var groupedByDate = BankOperations.GroupDataUsingLINQ(bank);

            // Assert
            Assert.IsNotNull(groupedByDate);
            Assert.IsTrue(groupedByDate.Any());
        }

        [TestMethod]
        public void GroupDataUsingExtensionMethodsTest()
        {
            // Arrange
            List<SortedDictionary<int, BankCard>> bank = GetTestBankData();

            // Act
            var groupedByDateMethod = BankOperations.GroupDataUsingExtensionMethods(bank);

            // Assert
            Assert.IsNotNull(groupedByDateMethod);
            Assert.IsTrue(groupedByDateMethod.Any());
        }

        private List<SortedDictionary<int, BankCard>> GetTestBankData()
        {
            // Создаем некоторые тестовые данные для банка
            var bank = new List<SortedDictionary<int, BankCard>>();

            var branch1 = new SortedDictionary<int, BankCard>
        {
            { 1, new BankCard { Number = "1111 1111 1111 1111", Owner = "John Doe", Date = 2023 } },
            { 2, new BankCard { Number = "2222 2222 2222 2222", Owner = "Jane Smith", Date = 2023 } },
            { 3, new BankCard { Number = "3333 3333 3333 3333", Owner = "Bob Johnson", Date = 2024 } }
        };

            var branch2 = new SortedDictionary<int, BankCard>
        {
            { 4, new BankCard { Number = "4444 4444 4444 4444", Owner = "Alice Brown", Date = 2024 } },
            { 5, new BankCard { Number = "5555 5555 5555 5555", Owner = "Sam Wilson", Date = 2022} }
        };

            bank.Add(branch1);
            bank.Add(branch2);

            return bank;
        }

        [TestMethod]
        public void CreateNewTypeUsingLINQTest()
        {
            // Arrange
            var bank = new List<SortedDictionary<int, BankCard>>
        {
            new SortedDictionary<int, BankCard>
            {
                { 1, new BankCard { Number = "1111 1111 1111 1111", Owner = "John Doe", Date = 2023 } },
                { 2, new BankCard { Number = "2222 2222 2222 2222", Owner = "Jane Smith", Date = 2023 } }
            },
            new SortedDictionary<int, BankCard>
            {
                { 3, new BankCard { Number = "3333 3333 3333 3333", Owner = "Bob Johnson", Date = 2024 } }
            }
        };

            // Act
            var result = BankOperations.CreateNewTypeUsingLINQ(bank);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void CreateNewTypeUsingMethodTest()
        {
            // Arrange
            var bank = new List<SortedDictionary<int, BankCard>>
        {
            new SortedDictionary<int, BankCard>
            {
                { 1, new BankCard { Number = "1111 1111 1111 1111", Owner = "John Doe", Date = 2023 } },
                { 2, new BankCard { Number = "2222 2222 2222 2222", Owner = "Jane Smith", Date = 2023 } }
            },
            new SortedDictionary<int, BankCard>
            {
                { 3, new BankCard { Number = "3333 3333 3333 3333", Owner = "Bob Johnson", Date = 2024 } }
            }
        };

            // Act
            var result = BankOperations.CreateNewTypeUsingExtensionMethods(bank);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void TestJoinDataUsingLINQ()
        {
            var bank = new List<SortedDictionary<int, BankCard>>
        {
            new SortedDictionary<int, BankCard>(),
            new SortedDictionary<int, BankCard>(),
            new SortedDictionary<int, BankCard>()
        };

            var result = BankOperations.JoinDataUsingLINQ(bank);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void TestJoinDataUsingExtensionMethods()
        {
            var bank = new List<SortedDictionary<int, BankCard>>
        {
            new SortedDictionary<int, BankCard>(),
            new SortedDictionary<int, BankCard>(),
            new SortedDictionary<int, BankCard>()
        };

            var result = BankOperations.JoinDataUsingExtensionMethods(bank);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Any());
        }
    }
    [TestClass]
    public class CardQueriesTests
    {
        [TestMethod]
        public void SetOperations_ShouldReturnCorrectUnionResult()
        {
            // Arrange
            var bank = CreateTestBank();

            // Act
            var result = BankOperations.SetOperations(bank);

            // Assert
            StringAssert.Contains(result, "Результат объединения:");
            StringAssert.Contains(result, "Number: 2000 0000 4000 3000, Owner: Иван Петров, Date: 2024");
            StringAssert.Contains(result, "Number: 2000 7000 4000 3000, Owner: Пётр Сидоров, Date: 2025");
        }

        [TestMethod]
        public void SetOperations_ShouldReturnCorrectExceptResult()
        {
            // Arrange
            var bank = CreateTestBank();

            // Act
            var result = BankOperations.SetOperations(bank);

            // Assert
            StringAssert.Contains(result, "Результат исключения:");
            StringAssert.Contains(result, "Первое отделение, исключая второе и третье отделения:");
            StringAssert.Contains(result, "Второе отделение, исключая первое и третье отделения:");
            StringAssert.Contains(result, "Третье отделение, исключая первое и второе отделения:");
        }

        [TestMethod]
        public void SetOperations_ShouldReturnCorrectIntersectResult()
        {
            // Arrange
            var bank = CreateTestBank();

            // Act
            var result = BankOperations.SetOperations(bank);

            // Assert
            StringAssert.Contains(result, "Результаты пересечения:");
            StringAssert.Contains(result, "Первое отделение и второе отделение пересекаются в:");
            StringAssert.Contains(result, "Первое отделение и третье отделение пересекаются в:");
            StringAssert.Contains(result, "Второе отделение и третье отделение пересекаются в:");
        }

        private List<SortedDictionary<int, BankCard>> CreateTestBank()
        {
            var bank = new List<SortedDictionary<int, BankCard>>
        {
            new SortedDictionary<int, BankCard>(),
            new SortedDictionary<int, BankCard>(),
            new SortedDictionary<int, BankCard>()
        };

            BankCard dc1 = new BankCard { Number = "2000 0000 4000 3000", Owner = "Иван Петров", Date = 2024, id = new IdNumber { Number = 6 } };
            BankCard dc2 = new BankCard { Number = "2000 7000 4000 3000", Owner = "Пётр Сидоров", Date = 2025, id = new IdNumber { Number = 7 } };

            bank[0].Add(dc1.id.Number, dc1);
            bank[1].Add(dc2.id.Number, dc2);

            return bank;
        }
    }
}