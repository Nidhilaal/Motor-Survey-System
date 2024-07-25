import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { ApiService } from '../../../services/api.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-survey-history',
  templateUrl: './survey-history.component.html',
  styleUrl: './survey-history.component.css',
  encapsulation: ViewEncapsulation.None
})
export class SurveyHistoryComponent implements OnInit{
  data: any[] = [];
  count: number = 0;
  currentPage: number = 1;
  pageSize: number = 5; 
 
  constructor(private apiService: ApiService,private router:Router) { }

  ngOnInit(): void {
    if(sessionStorage.getItem('userType') === null){
      this.router.navigate(['/account/login']);
    }else{
      this.fetchData();

    }
    
  }

  fetchData() {
    const offset = (this.currentPage - 1) * this.pageSize;

    this.apiService.getSurveyHistoryList().subscribe((response: any) => {
      this.data = response;
      this.count = this.data.length;   
    });
  }
  setPage(event: any) {
    this.currentPage = event.offset + 1;
    this.fetchData();
  }

 
}
