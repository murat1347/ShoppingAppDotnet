import { Component, OnInit } from '@angular/core';
import {DashboardService} from "./dashboard.service";
import {Dashboard} from "../../model/dasboard";

@Component({
  selector: 'app-home',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  isLoadingResult: boolean = true;

  saleSummaryChartData?: HorizontalChartData[] =[]

  purchaseSummaryChartData?: HorizontalChartData[] =[]

  mostProfitChartData? : HorizontalChartData[] = []

  mostLossChartData? : HorizontalChartData[] = []

  monthlyIncomes? : HorizontalChartData[] = []

  view: [number,number] = [700,400];

  colorScheme = {
    domain: ['#5AA454', '#A10A28', '#C7B42C', '#AAAAAA']
  };

  dashboard : Dashboard = {};

  constructor(
    private _dashboardService: DashboardService
  ) { }

  ngOnInit(): void {
    this.getDashboardReport();
  }

  getDashboardReport(){
    this.isLoadingResult = true;
    this._dashboardService.getDashboard().subscribe(d=> {
      this.dashboard = d
      this.saleSummaryChartData = this.dashboard.saleSummary?.map(s=> new HorizontalChartData(s.productName,s.totalSale));
      this.purchaseSummaryChartData = this.dashboard.purchaseSummary?.map(s=> new HorizontalChartData(s.productName,s.totalPurchase));
      this.mostProfitChartData = this.dashboard.mostProfit?.map(s=> new HorizontalChartData(s.productName,s.profit));
      this.mostLossChartData = this.dashboard.mostLoss?.map(s=> new HorizontalChartData(s.productName,-1 * s.profit));
      this.monthlyIncomes = this.dashboard.monthlyIncomes?.map(s=> new HorizontalChartData(getMonthName(s.month) + " ",s.income));
      this.isLoadingResult = false;
    });
  }

  refresh(){
    this._dashboardService.invalidateCache().subscribe(_=>this.getDashboardReport());
  }

}


export default class HorizontalChartData{

  constructor(public name: string, public value: number) {
  }
}

function getMonthName(month:number){
  const d = new Date();
  d.setMonth(month-1);
  const monthName = d.toLocaleString("en", {month: "long"});
  return monthName;
}
