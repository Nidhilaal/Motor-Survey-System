import { Component, OnInit, ViewChild } from '@angular/core';
import { MotorClaimEntity } from '../../../shared/models/transaction/MotorClaimEntity';
import { provideNativeDateAdapter } from '@angular/material/core';
import { ApiService } from '../../../services/api.service';
import { AlertService } from '../../../services/alert.service';
import { ActivatedRoute, Router } from '@angular/router';
import { formatDate } from '@angular/common';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-motorclaim',
  templateUrl: './motorclaim.component.html',
  styleUrl: './motorclaim.component.css',
  providers: [provideNativeDateAdapter()]
})
export class MotorclaimComponent implements OnInit {

  motorClaimEntity: MotorClaimEntity = new MotorClaimEntity();
  isEditMode: boolean = false;
  clmUid?: number;
  formattedClmPolFmDt?: string;
  formattedClmPolToDt?: string;
  formattedClmPolIntmDt?: string;
  formattedClmPolLossDt?: string;
  formattedClmRegToDt?: string;
  isValid: boolean = false;
  @ViewChild('testForm') testForm!: NgForm;

  constructor(private apiService:ApiService, private route: ActivatedRoute, private alertService: AlertService,private router: Router ){}

  ngOnInit(): void {
    if(sessionStorage.getItem('userType') === null){
      this.router.navigate(['/account/login']);
    }else{
      this.route.params.subscribe(params => {
        this.clmUid = parseInt(params['clmUid'], 10);
        });
       
        this.intialiseForm();
    
    } 
  }

  getToday(): string {
    return new Date().toISOString().split('T')[0]
  }
intialiseForm(){
  if(this.clmUid){
    this.isEditMode = true;   
    
    this.apiService.getMotorClaim(this.clmUid).subscribe((response: any) => {
      if (response && response.length > 0) {
        const firstRow = response[0]; 
        this.motorClaimEntity.clmUid = firstRow.CLM_UID;
        this.motorClaimEntity.clmNo = firstRow.CLM_NO;
        this.motorClaimEntity.clmPolNo = firstRow.CLM_POL_NO;
        this.formattedClmPolFmDt = firstRow.CLM_POL_FM_DT.split('T')[0];
        this.formattedClmPolToDt = firstRow.CLM_POL_TO_DT.split('T')[0];
        this.formattedClmPolLossDt = firstRow.CLM_LOSS_DT.split('T')[0];
        this.formattedClmPolIntmDt = firstRow.CLM_INTM_DT.split('T')[0];
        this.formattedClmRegToDt= firstRow.CLM_REG_DT.split('T')[0];
        this.motorClaimEntity.clmPolRepNo = firstRow.CLM_POL_REP_NO;
        this.motorClaimEntity.clmLossDesc = firstRow.CLM_LOSS_DESC;
        this.motorClaimEntity.clmVehChassisNo = firstRow.CLM_VEH_CHASSIS_NO;
        this.motorClaimEntity.clmVehEngineNo = firstRow.CLM_VEH_ENGINE_NO;
        this.motorClaimEntity.clmVehRegnNo = firstRow.CLM_VEH_REGN_NO;
        this.motorClaimEntity.clmVehValue = firstRow.CLM_VEH_VALUE;
        this.motorClaimEntity.clmSurCrYn = firstRow.CLM_SUR_CR_YN;
      }
    });
  }else{
    this.formattedClmRegToDt = this.getToday();
  }
}

 setPolicyToDate(event: any) {
  const fromDate = new Date(event);
  const toDate = new Date(fromDate.getFullYear() + 1, fromDate.getMonth(), fromDate.getDate());

  this.formattedClmPolToDt = formatDate(toDate, 'yyyy-MM-dd', 'en-US');
}
getMaxDate(): string {
  const today = new Date();
  const formattedToday = new Date().toISOString().split('T')[0];
  if(this.formattedClmPolToDt)
    {
      return today < new Date(this.formattedClmPolToDt) ? formattedToday : this.formattedClmPolToDt;

    }
    else{
      return '';
    }
}


  onSubmit() {
    if(!this.motorClaimEntity.clmVehValue){
      this.motorClaimEntity.clmVehValue = 0;
    }

    if (this.formattedClmPolToDt && this.formattedClmPolFmDt && 
      this.formattedClmPolLossDt && this.formattedClmPolIntmDt && this.formattedClmRegToDt) {
      this.motorClaimEntity.clmPolFmDt = new Date(this.formattedClmPolFmDt);
      this.motorClaimEntity.clmPolToDt = new Date(this.formattedClmPolToDt);
      this.motorClaimEntity.clmLossDt = new Date(this.formattedClmPolLossDt);
      this.motorClaimEntity.clmIntmDt= new Date(this.formattedClmPolIntmDt);
      this.motorClaimEntity.clmRegDt= new Date(this.formattedClmRegToDt);
    }

    if(this.isEditMode)
    {
      console.log(this.motorClaimEntity.clmVehValue);

      this.apiService.updateMotorClaim(this.motorClaimEntity).subscribe({
        next: (response: string) => {
          if (response.trim() === 'true') {
            this.apiService.getErrorCode('102').subscribe((response: any) => {
              this.alertService.showAlert(response[0].ERR_TYPE, response[0].ERR_DESC, 'success');
            });
            this.router.navigate(['/motorclaim', this.motorClaimEntity.clmUid ]);

          } 
        },
        error: (error: any) => {
          console.error('Error:', error);
          this.alertService.showAlert('Error', error, 'error');
        }
      });
    }else{
      this.apiService.GetClaimUidSequence().subscribe({
        next: (clmUid: number) => {
          this.motorClaimEntity.clmUid = clmUid;
  
        
          this.motorClaimEntity.clmNo = `C/${new Date().getFullYear()}/${this.motorClaimEntity.clmUid.toString().padStart(6, '0')}`;
          this.apiService.saveMotorClaim(this.motorClaimEntity).subscribe({
            next: (response: string) => {
              if (response.trim() === 'true') {
                this.apiService.getErrorCode('101').subscribe((response: any) => {
                  this.alertService.showAlert(response[0].ERR_TYPE, response[0].ERR_DESC, 'success');
                });
                this.router.navigate(['/motorclaim', this.motorClaimEntity.clmUid ]);
  
              } 
            },
            error: (error: any) => {
              console.error('Error:', error);
              this.alertService.showAlert('Error', error, 'error');
            }
          });
        },
        error: (error: any) => {
          console.error('Error fetching UID:', error);
          this.alertService.showAlert('Error', error, 'error');
        }
      });
    }


   
  }
  checkDuplicateMotorClaim(){
    console.log(this.motorClaimEntity.clmPolRepNo);
    this.apiService.checkDuplicateMotorClaim(this.motorClaimEntity.clmPolRepNo).subscribe({
      next: (response: string) => {
        if (response.trim() === 'true') {
          this.apiService.getErrorCode('201').subscribe((response: any) => {
            this.alertService.showAlert(response[0].ERR_TYPE, response[0].ERR_DESC, 'warning');
          });
          this.motorClaimEntity.clmPolRepNo = '';
        } 
      },
      error: (error: any) => {
        console.error('Error:', error);
        this.alertService.showAlert('Error', error, 'error');
      }
    });
  }
  onReset() {
    if(this.isEditMode){
      this.intialiseForm();
  
    }else{

      this.testForm.resetForm(); // Reset the form using the resetForm method

      setTimeout(() => {
        this.formattedClmRegToDt = this.getToday();
      }, 0);  


    
    }


    
  }
}
