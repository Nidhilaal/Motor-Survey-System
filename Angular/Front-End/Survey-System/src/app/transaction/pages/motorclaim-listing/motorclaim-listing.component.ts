import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { ApiService } from '../../../services/api.service';
import { AlertService } from '../../../services/alert.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Router } from '@angular/router';
import {provideNativeDateAdapter} from '@angular/material/core';
import { NgbPaginationNumberContext } from '@ng-bootstrap/ng-bootstrap/pagination/pagination';


@Component({
  selector: 'app-motorclaim-listing',
  templateUrl: './motorclaim-listing.component.html',
  styleUrl: './motorclaim-listing.component.css',
  encapsulation: ViewEncapsulation.None
 
})
export class MotorclaimListingComponent implements  OnInit{
  data: any[] = [];
  count: number = 0;
  currentPage: number = 1;
  pageSize: number = 5; 
  userType?: string | null; 

  constructor(private apiService: ApiService, private alertService: AlertService, private router: Router ) { }

  ngOnInit(): void {
    this.userType = sessionStorage.getItem('userType');

    if(sessionStorage.getItem('userType') === null){
      this.router.navigate(['/account/login']);
    }else{
      this.fetchData();

    }
  }

  fetchData() {
    const offset = (this.currentPage - 1) * this.pageSize;

    this.apiService.getMotorClaimList().subscribe((response: any) => {
      this.data = response;
      this.count = this.data.length;   
    });
  }
  setPage(event: any) {
    this.currentPage = event.offset + 1;
    this.fetchData();
  }

  addMotorClaim(clmUid?: string) 
  {
    if(clmUid){
      this.router.navigate(['/motorclaim',clmUid]);
    }else{
      this.router.navigate(['/motorclaim']);  
    }
  
  }

  deleteMotorClaim(clmUid: number) {
    this.alertService.showConfirmationDialog().then((confirmed) => {
      if (confirmed) {
        this.apiService.deleteMotorClaim(clmUid).subscribe({
          next: (response: any) => {
            if (response === 'true') {

              this.apiService.getErrorCode('103').subscribe((response: any) => {
                console.log(response);
                this.alertService.showAlert(response[0].ERR_TYPE, response[0].ERR_DESC,'success');
               });

              this.fetchData();
            } 
          },
          error: (error: any) => {
            console.error('Error:', error);
            this.alertService.showAlert('Error', error,'error');
          }
        });
      }
    });
  }

  startSurvey(clmUid: number){
    this.router.navigate(['/transaction/survey', clmUid]);
  }
  viewSurvey(clmUid: number){
    this.router.navigate(['/transaction/survey-approval', clmUid]);
  }
}
