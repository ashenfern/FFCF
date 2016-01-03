using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole.FFAlgo
{
    public class ForecastFailover
    {
        List<BranchItemData> branchItemDataList;
        List<BranchDistributionData> surplusList = new List<BranchDistributionData>();
        List<BranchDistributionData> neededList = new List<BranchDistributionData>();

        public void Algorithm()
        {
            branchItemDataList = new List<BranchItemData>();
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
            foreach (var item in db.Branches)
            {
                BranchItemData branchItemData = null;
                if (item.BranchID == 1)
                {
                    branchItemData = new BranchItemData() { branch = item, ExpectedBalance = 15 };
                }
                else if (item.BranchID == 2)
                {
                    branchItemData = new BranchItemData() { branch = item, ExpectedBalance = 10 };
                }
                else if (item.BranchID == 3)
                {
                    branchItemData = new BranchItemData() { branch = item, ExpectedBalance = -15 };
                }
                else if (item.BranchID == 4)
                {
                    branchItemData = new BranchItemData() { branch = item, ExpectedBalance = -15 };
                }
                else
                {
                    throw new ArgumentNullException();
                }

                branchItemDataList.Add(branchItemData);
            }
            #endregion

            foreach (var branchItemData in branchItemDataList)
            {
                if (branchItemData.ExpectedBalance > 0)
                {
                    surplusList.Add(new BranchDistributionData(){BranchId = branchItemData.branch.BranchID, Amount = branchItemData.ExpectedBalance});
                    totalSurplus = totalSurplus + branchItemData.ExpectedBalance;
                }
                else if (branchItemData.ExpectedBalance < 0)
                {
                     neededList.Add(new BranchDistributionData(){BranchId = branchItemData.branch.BranchID, Amount = Math.Abs(branchItemData.ExpectedBalance)});
                    totalNeeded = totalNeeded + Math.Abs(branchItemData.ExpectedBalance);
                }
            }

            currentNeeded = totalNeeded;

            surplusList = surplusList.OrderByDescending(x => x.Amount).ToList();
            neededList = neededList.OrderByDescending(x => x.Amount).ToList();

            int firstSuplusBranch = surplusList[0].BranchId;
            int firstSurplusBranchValue = surplusList[0].Amount;

            if (currentNeeded >= firstSurplusBranchValue)
            {
                //Collect first Value from branch first
                totalCollected = firstSurplusBranchValue;
                totalInHand = firstSurplusBranchValue;
                currentNeeded = totalNeeded - firstSurplusBranchValue;

                Message = String.Format("first go to branch {0} and collect {1}", firstSuplusBranch, firstSurplusBranchValue);

                while (surplusList.Count > 0 && currentNeeded > 0 && !isDistributedFinish)
                {
                    if (Cost(surplusList[0].BranchId, surplusList[1].BranchId) < Cost(surplusList[0].BranchId, neededList[0].BranchId) && currentNeeded > 0)
                    {
                        //Loop thorugh the surplus set and collect if the cost is less
                        while (Cost(surplusList[0].BranchId, surplusList[1].BranchId) < Cost(surplusList[0].BranchId, neededList[0].BranchId) && currentNeeded > 0 && !isCollectedFinish)
                        {
                            //Visit(SC,SNext)
                            if(currentNeeded >  surplusList[1].Amount )
                            {
                                Message = Message + String.Format("Then go to Branch {0} and collect {1}", surplusList[1].BranchId.ToString(), surplusList[1].Amount.ToString());
                                totalCollected = totalCollected + surplusList[1].Amount;
                                totalInHand = totalInHand + surplusList[1].Amount;
                                currentNeeded = currentNeeded - surplusList[1].Amount;
                            }
                            else
                            {
                                Message = Message + String.Format("Then go to Branch {0} and collect {1}", surplusList[1].BranchId.ToString(), surplusList[1].Amount.ToString());
                                totalCollected = totalCollected + currentNeeded;
                                totalInHand = totalInHand + currentNeeded;
                                currentNeeded = 0;
                                isCollectedFinish = true;
                            }

                            //Removing firs surplus element
                            surplusList.RemoveAt(0);

                        }
                    }
                    //Distributing
                    else if(true)
                    {
                        //Loop through the needed list and distribute if the cost is low
                        while (Cost(neededList[0].BranchId, neededList[1].BranchId) < Cost(neededList[0].BranchId, surplusList[0].BranchId) && totalInHand > currentNeeded && !isDistributedFinish)
                        {
                            //Visit(SC,SNext)
                            if (currentNeeded > neededList[1].Amount)
                            {
                                Message = Message + String.Format("Then go to Branch {0} and collect {1}", neededList[0].BranchId.ToString(), neededList[1].Amount.ToString());
                                totalCollected = totalCollected + surplusList[1].Amount;
                                totalInHand = totalInHand + surplusList[1].Amount;
                                currentNeeded = currentNeeded - surplusList[1].Amount;
                            }
                            else
                            {
                                Message = Message + String.Format("Then go to Branch {0} and collect {1}", surplusList[1].BranchId.ToString(), surplusList[1].Amount.ToString());
                                totalCollected = totalCollected + surplusList[1].Amount;
                                totalInHand = totalInHand + surplusList[1].Amount;
                                currentNeeded = currentNeeded - surplusList[1].Amount;
                                isDistributedFinish = true;
                            }

                            //go to next surplus node
                        }
                    }
                }
            }
            else
            {
                totalCollected = currentNeeded;
                totalInHand = currentNeeded;
                //Distribute among the needed nodes
            }

        }

        private int Cost(int branch1, int branch2)
        {
            return branch1 * 100 + branch2 * 100;
        }
    }
}
