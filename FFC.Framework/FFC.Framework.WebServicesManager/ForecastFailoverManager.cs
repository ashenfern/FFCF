using FFC.Framework.Data;
using FFC.Framework.WebServicesManager.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFC.Framework.WebServicesManager
{
    public class ForecastFailoverManager
    {
        //List<BranchItemData> branchItemDataList;
        List<BranchDistributionData> surplusList = new List<BranchDistributionData>();
        List<BranchDistributionData> neededList = new List<BranchDistributionData>();
        public FFCEntities context = new FFCEntities();

        public string GetForecastFailoverResultsFromAlgorithm(List<BranchItemData> branchItemDataList)
        {
            return ForecastFailoverAlgorithm(branchItemDataList);
        }

        public string ForecastFailoverAlgorithm(List<BranchItemData> branchItemDataList)
        {
            //branchItemDataList = new List<BranchItemData>();
            FFCEntities db = new FFCEntities();
            int totalNeeded = 0;
            int totalSurplus = 0;
            int currentNeeded = 0;
            int totalCollected = 0;
            int totalDistributed = 0;
            int totalInHand = 0;
            bool isCollectedFinish = false;
            bool isDistributedFinish = false;
            string Message = String.Empty;

            #region Input Data List For Forecast
            //foreach (var item in db.Branches)
            //{
            //    BranchItemData branchItemData = null;
            //    if (item.BranchID == 1)
            //    {
            //        branchItemData = new BranchItemData() { Branch = item, ExpectedBalance = -10 };
            //    }
            //    else if (item.BranchID == 2)
            //    {
            //        branchItemData = new BranchItemData() { Branch = item, ExpectedBalance = -10 };
            //    }
            //    else if (item.BranchID == 3)
            //    {
            //        branchItemData = new BranchItemData() { Branch = item, ExpectedBalance = 10 };
            //    }
            //    else if (item.BranchID == 4)
            //    {
            //        branchItemData = new BranchItemData() { Branch = item, ExpectedBalance = 10 };
            //    }
            //    else if (item.BranchID == 5)
            //    {
            //        branchItemData = new BranchItemData() { Branch = item, ExpectedBalance = -10 };
            //    }
            //    else
            //    {
            //        throw new ArgumentNullException();
            //    }

            //    branchItemDataList.Add(branchItemData);
            //}
            #endregion

            foreach (var branchItemData in branchItemDataList)
            {
                branchItemData.ExpectedBalance = branchItemData.Now - branchItemData.Forecasted;
                if (branchItemData.ExpectedBalance > 0)
                {
                    surplusList.Add(new BranchDistributionData() { BranchId = branchItemData.Branch.BranchID, BranchName = branchItemData.Branch.BranchName, Amount = branchItemData.ExpectedBalance });
                    totalSurplus = totalSurplus + branchItemData.ExpectedBalance;
                }
                else if (branchItemData.ExpectedBalance < 0)
                {
                    neededList.Add(new BranchDistributionData() { BranchId = branchItemData.Branch.BranchID, BranchName = branchItemData.Branch.BranchName, Amount = Math.Abs(branchItemData.ExpectedBalance) });
                    totalNeeded = totalNeeded + Math.Abs(branchItemData.ExpectedBalance);
                }
            }

            currentNeeded = totalNeeded;

            surplusList = surplusList.OrderByDescending(x => x.Amount).ToList();
            neededList = neededList.OrderByDescending(x => x.Amount).ToList();

            while (surplusList.Count > 0 && currentNeeded > 0 && !isDistributedFinish && totalDistributed != totalNeeded)
            {
                //Go to the first surplus branch
                if (currentNeeded > surplusList[0].Amount)
                {
                    Message = Message + String.Format("Go to {0} Branch and collect {1}. ", surplusList[0].BranchName.ToString(), surplusList[0].Amount.ToString());
                    totalCollected = totalCollected + surplusList[0].Amount;
                    totalInHand = totalInHand + surplusList[0].Amount;
                    currentNeeded = currentNeeded - surplusList[0].Amount;
                }
                else
                {
                    Message = Message + String.Format("Go to {0} Branch and collect {1}. ", surplusList[0].BranchName.ToString(), currentNeeded.ToString());
                    totalCollected = totalCollected + currentNeeded;
                    totalInHand = totalInHand + currentNeeded;
                    currentNeeded = 0;
                    isCollectedFinish = true;
                }

                //Loop thorugh the surplus set and collect if the cost is less
                if (surplusList.Count > 1 && Cost(surplusList[0].BranchId, surplusList[1].BranchId) <= Cost(surplusList[0].BranchId, neededList[0].BranchId) && currentNeeded > 0 && !isCollectedFinish)
                {
                    while (surplusList.Count > 1 && Cost(surplusList[0].BranchId, surplusList[1].BranchId) <= Cost(surplusList[0].BranchId, neededList[0].BranchId) && currentNeeded > 0 && !isCollectedFinish)
                    {
                        //Visit(SC,SNext)
                        if (currentNeeded > surplusList[1].Amount)
                        {
                            Message = Message + String.Format("Then go to {0} Branch and collect {1}. ", surplusList[1].BranchName.ToString(), surplusList[1].Amount.ToString());
                            totalCollected = totalCollected + surplusList[0].Amount;
                            totalInHand = totalInHand + surplusList[1].Amount;
                            currentNeeded = currentNeeded - surplusList[1].Amount;
                        }
                        else
                        {
                            Message = Message + String.Format("Then go to {0} Branch and collect {1}. ", surplusList[1].BranchName.ToString(), surplusList[1].Amount.ToString());
                            totalCollected = totalCollected + currentNeeded;
                            totalInHand = totalInHand + currentNeeded;
                            currentNeeded = 0;
                            isCollectedFinish = true;
                        }

                        //Removing firs surplus element
                        surplusList.RemoveAt(0);
                    }
                }
                else if (surplusList.Count > 1 && Cost(surplusList[0].BranchId, surplusList[1].BranchId) > Cost(surplusList[0].BranchId, neededList[0].BranchId) && !isCollectedFinish)
                {
                    //Removing firs surplus element
                    surplusList.RemoveAt(0);
                }

                //Distributing

                //If collected all from the surplus branches or collected total needed the items from the surplus branches
                if (totalCollected == totalSurplus || totalCollected == totalNeeded)
                {
                    //Visit each needed branch TODO Shortest path algorithm
                    foreach (var branch in neededList)
                    {
                        if (branch.Amount >= totalInHand)
                        {
                            //Distrbute to the branch
                            Message = Message + String.Format("Go to {0} Branch , and distribute {1}. ", branch.BranchName, totalInHand);
                            totalInHand = 0;
                            //currentNeeded = currentNeeded - totalInHand;
                            totalDistributed = totalDistributed + totalInHand;
                            isDistributedFinish = true;
                            break;
                        }
                        else
                        {
                            totalInHand = totalInHand - branch.Amount;
                            //currentNeeded = currentNeeded - Math.Abs(branch.Amount);
                            totalDistributed = totalDistributed + branch.Amount;
                            Message = Message + String.Format("Go to {0} Branch , and distribute {1}. ", branch.BranchName, branch.Amount);
                        }
                    }
                }
                else if (neededList[0].Amount <= totalInHand)
                {
                    //Distrbute to the branch
                    Message = Message + String.Format("Go to {0} Branch , and distribute {1}.", neededList[0].BranchName, neededList[0].Amount);
                    totalInHand = totalInHand - neededList[0].Amount;
                    totalDistributed = totalDistributed + neededList[0].Amount;
                }

                if (neededList.Count > 1 && Cost(neededList[0].BranchId, neededList[1].BranchId) <= Cost(neededList[0].BranchId, surplusList[0].BranchId) && totalInHand >= neededList[1].Amount && !isDistributedFinish)
                {
                    while (neededList.Count > 1 && Cost(neededList[0].BranchId, neededList[1].BranchId) <= Cost(neededList[0].BranchId, surplusList[0].BranchId) && totalInHand >= neededList[1].Amount && !isDistributedFinish)
                    {
                        totalInHand = totalInHand - neededList[1].Amount;
                        //currentNeeded = currentNeeded - neededList[1].Amount;
                        totalDistributed = totalDistributed + neededList[1].Amount;
                        Message = Message + String.Format("Go to {0} Branch , and distribute {1}. ", neededList[1].BranchName, neededList[1].Amount);
                        //j = j + 1;
                        neededList.RemoveAt(0);
                    }

                    if (neededList.Count > 0)
                    {
                        neededList.RemoveAt(0);
                    }
                }
                else if (neededList.Count > 0 && totalInHand >= neededList[0].Amount)//if (Cost(neededList[0].BranchId, neededList[1].BranchId) > Cost(neededList[0].BranchId, surplusList[0].BranchId) || totalInHand < currentNeeded)
                {
                    neededList.RemoveAt(0);
                    //j = j + 1;
                }

            }

            return Message;
        }

        private decimal? Cost(int branch1, int branch2)
        {
            return context.sp_GetBranchesCostBySourceAndDestination(branch1, branch2).FirstOrDefault();
        }
    }
}

