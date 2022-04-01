export class Dashboard{


  constructor(
    public totalIncome?: number,
    public totalCost? : number,
    public profit? : number,
    public saleSummary? : SaleSummaryResult[],
    public purchaseSummary? : PurchaseSummaryResult[],
    public criticalStock? : number,
    public mostProfit? : MostProfitProductResult[],
    public mostLoss? : MostProfitProductResult[],
    public categoryIncomes? : CategoryIncomeResult[],
    public monthlyIncomes?: MonthlyIncomeResult[]
  ) {
    this.profit = totalIncome! - totalCost!;
  }

}

export interface SaleSummaryResult{
  productName : string,
  totalSale : number
}

export interface PurchaseSummaryResult{
  productName : string,
  totalPurchase : number
}

export interface MostProfitProductResult{
  productName : string,
  profit : number
}

export interface CategoryIncomeResult {
  categoryName : string,
  totalIncome : number
}

export interface MonthlyIncomeResult{
  year : number,
  month : number,
  income : number,
}
