import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { MotorClmSurHdrEntity } from '../../../shared/models/transaction/MotorClmSurHdrEntity';
import { MotorClaimEntity } from '../../../shared/models/transaction/MotorClaimEntity';
import { ApiService } from '../../../services/api.service';
import { AlertService } from '../../../services/alert.service';
import { ActivatedRoute, Route, Router } from '@angular/router';


@Component({
  selector: 'app-survey-approval',
  templateUrl: './survey-approval.component.html',
  styleUrl: './survey-approval.component.css',
  encapsulation:ViewEncapsulation.None

})
export class SurveyApprovalComponent implements OnInit {

  data: any[] = [];
  count: number = 0;
  currentPage: number = 1;
  pageSize: number = 5; 

  motorClmSurHdrEntity: MotorClmSurHdrEntity = new MotorClmSurHdrEntity();
  motorClaimEntity: MotorClaimEntity = new MotorClaimEntity();
  surClmUid?: number;
  userId = sessionStorage.getItem('userId');
  userType = sessionStorage.getItem('userType');

 constructor(private apiService: ApiService, private alertService:AlertService, private route:ActivatedRoute, private router:Router){}

  ngOnInit(): void {
    if(sessionStorage.getItem('userType') === null){
      this.router.navigate(['/account/login']);
    }else{
      this.route.params.subscribe(params => {
        this.surClmUid = parseInt(params['clmUid'], 10);
  
      });
      if(this.surClmUid ){
        this.apiService.getSurveyHeaderBySurClmUid(this.surClmUid).subscribe((response: any) => {
          if (response && response.length > 0) {
            const firstRow = response[0]; 
            
            this.motorClaimEntity.clmPolNo = firstRow.SUR_CLM_NO;
            this.motorClmSurHdrEntity.surUid = firstRow.SUR_UID;
            this.motorClmSurHdrEntity.surNo = firstRow.SUR_NO;
            this.motorClmSurHdrEntity.surclmNo = firstRow.SUR_CLM_NO;
            this.motorClmSurHdrEntity.surChassisNo = firstRow.SUR_CHASSIS_NO;
            this.motorClmSurHdrEntity.surEngineNo = firstRow.SUR_ENGINE_NO;
            this.motorClmSurHdrEntity.surRegnNo = firstRow.SUR_REGN_NO;
            this.motorClmSurHdrEntity.surCurr = firstRow.SUR_CURR;
            this.motorClmSurHdrEntity.surStatus = firstRow.SUR_STATUS;
            this.motorClmSurHdrEntity.surApprSts = firstRow.SUR_APPR_STS;
            this.motorClmSurHdrEntity.surFcAmt = firstRow.SUR_FC_AMT;
            this.motorClmSurHdrEntity.surLcAmt = firstRow.SUR_LC_AMT;
          }
          this.fetchData( this.motorClmSurHdrEntity.surUid);
        });
       
      }

    } 
  }

  fetchData(surdSurUid: number) {
    const offset = (this.currentPage - 1) * this.pageSize;

    this.apiService.getSurveyDetailsList(surdSurUid).subscribe((response: any) => {
      this.data = response;
      this.count = this.data.length; 

    });
  }
  setPage(event: any) {
    this.currentPage = event.offset + 1;
    if( this.motorClmSurHdrEntity.surUid){
      this.fetchData( this.motorClmSurHdrEntity.surUid);
    }
  }
  handleApproval(action: string) {
    this.motorClmSurHdrEntity.surApprSts = action;
    if (this.userId !== null) {
        this.motorClmSurHdrEntity.surApprBy = this.userId;
    }
    this.motorClmSurHdrEntity.surApprDt = new Date();

    this.apiService.updateSurveyStatus(this.motorClmSurHdrEntity).subscribe({
      next: (response: string) => {
        if (response.trim() === 'true') {
          if(action ==='A'){
            this.motorClaimEntity.clmSurApprYn = 'Y';
            this.apiService.getErrorCode('105').subscribe((response: any) => {
              this.alertService.showAlert('Approved', response[0].ERR_DESC, 'success');
            
            });
          }else if(action ==='R'){
            this.motorClaimEntity.clmSurApprYn = 'N';
            this.apiService.getErrorCode('302').subscribe((response: any) => {
              this.alertService.showAlert('Rejected', response[0].ERR_DESC, 'error');
            
            });
          }
        } 
        this.motorClaimEntity.clmNo = this.motorClmSurHdrEntity.surclmNo;
 
        this.apiService.updateApprovalSts(this.motorClaimEntity).subscribe({
          next: (response: any) => {

          },
          error: (updateError: any) => {
            console.error('Error updating approval status:', updateError);
          }
        });
        this.router.navigate(['/survey-approval',this.surClmUid]);
        
      },
      error: (error: any) => {
        console.error('Error:', error);
        this.alertService.showAlert('Error', error, 'error');
      }
    });
    
  }
}


