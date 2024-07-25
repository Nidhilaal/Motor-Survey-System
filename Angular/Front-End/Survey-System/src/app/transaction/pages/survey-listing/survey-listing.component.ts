import { Component, ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';
import { AlertService } from '../../../services/alert.service';
import { ApiService } from '../../../services/api.service';

@Component({
  selector: 'app-survey-listing',
  templateUrl: './survey-listing.component.html',
  styleUrl: './survey-listing.component.css',
  encapsulation: ViewEncapsulation.None

})
export class SurveyListingComponent {
  data: any[] = [];
  count: number = 0;
  currentPage: number = 1;
  pageSize: number = 5; 
  userType?: string | null; 

  constructor(private apiService: ApiService, private alertService: AlertService, private router: Router ) { }

  ngOnInit(): void {
    if(sessionStorage.getItem('userType') === null){
      this.router.navigate(['/account/login']);
    }else{
      this.fetchData();

    }

  }

  fetchData() {
    const offset = (this.currentPage - 1) * this.pageSize;
    const userId = sessionStorage.getItem('userId');

    if(userId){
      this.apiService.getSurveyList(userId).subscribe((response: any) => {
        this.data = response;
        this.count = this.data.length;   
      });
    }
 
  }
  setPage(event: any) {
    this.currentPage = event.offset + 1;
    this.fetchData();
  }

  editSurvey(surUid?: string) 
  {
    if(surUid){
      this.router.navigate(['/survey',surUid]);  
    }else{
     
    }
  
  }


}
