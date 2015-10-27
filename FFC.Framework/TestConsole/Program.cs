using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
    class Program
    {
        public static FFCEntities context = new FFCEntities();
        public static Random random = new Random();

        static void Main(string[] args)
        {
            //AddTestDataInvoke();
            RTest.Test();

            Console.ReadLine();
        }

        public static void AddTestDataInvoke()
        {
            //Creating segment Data
            List<DaySegment> listDaySegment = new List<DaySegment>()
            {
                new DaySegment(){StartTime = new TimeSpan(6,0,0), EndTime = new TimeSpan(8,0,0), SegmentRecordCount = 30},
                new DaySegment(){StartTime = new TimeSpan(8,0,0), EndTime = new TimeSpan(10,0,0), SegmentRecordCount = 30},
                new DaySegment(){StartTime = new TimeSpan(10,0,0), EndTime = new TimeSpan(12,0,0), SegmentRecordCount = 20},
                new DaySegment(){StartTime = new TimeSpan(12,0,0), EndTime = new TimeSpan(14,0,0), SegmentRecordCount = 20},
                new DaySegment(){StartTime = new TimeSpan(14,0,0), EndTime = new TimeSpan(16,0,0), SegmentRecordCount = 20},
                new DaySegment(){StartTime = new TimeSpan(16,0,0), EndTime = new TimeSpan(18,0,0), SegmentRecordCount = 30},
                new DaySegment(){StartTime = new TimeSpan(18,0,0), EndTime = new TimeSpan(20,0,0), SegmentRecordCount = 30}
            };

            DateTime beginTest = DateTime.Now;

            DateTime begin = DateTime.Today.AddDays(-365);
            DateTime end = DateTime.Today;
            AddTestData(1, listDaySegment, begin, end);

            Console.WriteLine("Time Taken " + (DateTime.Now - beginTest).ToString());

        }

        private static void AddTestData(int branchId, List<DaySegment> listDaySegment, DateTime begin, DateTime end)
        {
            //Need to loop through each day
            //DateTime begin = DateTime.Today.AddDays(-365);
            //DateTime end = DateTime.Today;

            int count = 1;
            List<Transaction> finalTransactionList = new List<Transaction>();
            List<DateTime> transactionTimeList = new List<DateTime>();

            for (DateTime date = begin; date <= end; date = date.AddDays(1))
            {
                Console.WriteLine(" ------ Day ------------" + count);

                //Loop through each segment day
                foreach (var segment in listDaySegment)
                {
                    Console.WriteLine("-------Segment ---------" + segment.StartTime.ToString() + " To " + segment.EndTime.ToString());

                    int segmentValue = segment.SegmentRecordCount;
                    int segmentCount = random.Next(segmentValue - 10, segmentValue + 10);

                    //Add records for a segment on a day
                    for (int i = 0; i < segmentCount; i++)
                    {
                        //Get the random transaction time
                        DateTime transactionTime = date.Add(segment.StartTime.Add(new TimeSpan(0, random.Next(1, 120), random.Next(1, 60))));
                        Console.WriteLine(transactionTime.ToString());
                        transactionTimeList.Add(transactionTime);

                    }
                }

                count++;
            }

            transactionTimeList.Sort();

            List<Transaction> finalList = GetTransactionList(branchId, transactionTimeList);

            int transactionCount = finalList.Count;
            int batchSize = 2000;
            int j = 0;
            int batchIterations = transactionCount / batchSize;
            int remaning = transactionCount % batchSize;

            Console.WriteLine("Total record count is " + transactionCount);
            for (int i = 0; i < batchIterations; i++)
            {
                DateTime batchStart = DateTime.Now;

                context.Dispose();
                context = new FFCEntities();
                context.Configuration.AutoDetectChangesEnabled = false;

                context.Transactions.AddRange(finalList.GetRange(j, batchSize));
                context.SaveChanges();

                Console.WriteLine(String.Concat(j, " - ", j + batchSize, " Records inserted. Time Taken = ", (DateTime.Now - batchStart).ToString()));
                j = j + batchSize;
            }

            //Adding remaining records
            context.Dispose();
            context = new FFCEntities();
            context.Configuration.AutoDetectChangesEnabled = false;

            context.Transactions.AddRange(finalList.GetRange(j, remaning - 1));
            context.SaveChanges();

            Console.WriteLine(String.Concat(finalList.Count, " Transaction Are Updated"));
        }

        private static List<Transaction> GetTransactionList(int branchId, List<DateTime> transctionDateTime)
        {
            List<Transaction> finalTransactionList = new List<Transaction>();

            foreach (var dateTime in transctionDateTime)
            {
                int transactionProductCount = random.Next(1, 8);

                //Transaction Detail list for the transaction
                List<TransactionDetail> transactionDetailList = new List<TransactionDetail>();
                Transaction transaction = new Transaction()
                {
                    BranchID = branchId,
                    CustomerID = 1,
                    EmployeeID = 1,
                    TransactionDate = dateTime
                };

                List<int> productIdList = new List<int>();

                for (int i = 1; i <= transactionProductCount; i++)
                {
                    int productId = 0;

                    do
                    {
                        productId = random.Next(1, 20);
                        if (!productIdList.Contains(productId))
                        {
                            break;
                        }

                    } while (true);

                    short quantity = Convert.ToInt16(random.Next(1, 20));

                    productIdList.Add(productId);

                    TransactionDetail transactionDetail = new TransactionDetail { ProductID = productId, Quantity = quantity };
                    transactionDetailList.Add(transactionDetail);
                }

                transaction.TransactionDetails = transactionDetailList;
                finalTransactionList.Add(transaction);
            }

            return finalTransactionList;
        }
    }
}

