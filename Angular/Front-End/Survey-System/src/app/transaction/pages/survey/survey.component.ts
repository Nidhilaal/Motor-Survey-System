import { Component, OnInit, ViewEncapsulation, LOCALE_ID } from '@angular/core';
import { MotorClmSurHdrEntity } from '../../../shared/models/transaction/MotorClmSurHdrEntity';
import { NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { SurveydetailsModalComponent } from '../../components/surveydetails-modal/surveydetails-modal.component';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiService } from '../../../services/api.service';
import { AlertService } from '../../../services/alert.service';
import { MotorClaimEntity } from '../../../shared/models/transaction/MotorClaimEntity';


@Component({
  selector: 'app-survey',
  templateUrl: './survey.component.html',
  styleUrl: './survey.component.css',
  encapsulation:ViewEncapsulation.None
})
export class SurveyComponent implements OnInit{

  motorClmSurHdrEntity: MotorClmSurHdrEntity = new MotorClmSurHdrEntity();
  motorClaimEntity: MotorClaimEntity = new MotorClaimEntity();
  isEditMode: boolean = false;
  uid: number = 0;
  userId = sessionStorage.getItem('userId');
  userType = sessionStorage.getItem('userType');

  currencies: { display: string, value: string ,cmValue:string}[] = []; 
  
  data: any[] = [];
  count: number = 0;
  currentPage: number = 1;
  pageSize: number = 5; 
 
  constructor(private modalService: NgbModal, private route: ActivatedRoute, private apiService: ApiService, private alertService:AlertService,private router: Router){}

  ngOnInit(): void {

    if(sessionStorage.getItem('userType') === null){
      this.router.navigate(['/account/login']);
    }else{
      this.route.params.subscribe(params => {
        this.uid = parseInt(params['uid'], 10);
  
      });
      this.intialiseForm();
    }
  
    
  } 
 intialiseForm(){
  const code = 'SUR_CURR';
  this.apiService.getCodes(code).subscribe((response: any[]) => {
    this.currencies = response.map(item => ({
      display: item.CM_DISPLAY,
      value: item.CM_CODE,
      cmValue: item.CM_VALUE
    }));
  });

  

  this.apiService.getSurveyHeaderByClmUid(this.uid).subscribe((response: any) => {
    if (response && response.length > 0) {
      const firstRow = response[0]; 
      this.motorClmSurHdrEntity.surclmNo = firstRow.CLM_NO;
      this.motorClmSurHdrEntity.surclmUid = firstRow.CLM_UID;
      this.motorClmSurHdrEntity.surChassisNo = firstRow.CLM_VEH_CHASSIS_NO;
      this.motorClmSurHdrEntity.surEngineNo = firstRow.CLM_VEH_ENGINE_NO;
      this.motorClmSurHdrEntity.surRegnNo = firstRow.CLM_VEH_REGN_NO;
    }else{

      this.isEditMode = true;
      this.apiService.getSurveyHeaderBySurUid(this.uid).subscribe((response: any) => {
        if (response && response.length > 0) {
          const firstRow = response[0]; 
          console.log("isnide");
          this.motorClmSurHdrEntity.surUid = firstRow.SUR_UID;
          this.motorClmSurHdrEntity.surNo = firstRow.SUR_NO;
          this.motorClmSurHdrEntity.surclmNo = firstRow.SUR_CLM_NO;
          this.motorClmSurHdrEntity.surclmUid = firstRow.SUR_CLM_UID;
          this.motorClmSurHdrEntity.surChassisNo = firstRow.SUR_CHASSIS_NO;
          this.motorClmSurHdrEntity.surEngineNo = firstRow.SUR_ENGINE_NO;
          this.motorClmSurHdrEntity.surRegnNo = firstRow.SUR_REGN_NO;
          this.motorClmSurHdrEntity.surCurr = firstRow.SUR_CURR;
          this.motorClmSurHdrEntity.surStatus = firstRow.SUR_STATUS;
        }
      });
      this.fetchData(this.uid);     
    }
  });
 }
  fetchData(surdSurUid: number) {
    const offset = (this.currentPage - 1) * this.pageSize;

    this.apiService.getSurveyDetailsList(surdSurUid).subscribe((response: any) => {
      this.data = response;
      this.count = this.data.length; 

      this.calculateFcSum();
      this.calculateLcSum();  
    });
  }
  setPage(event: any) {
    this.currentPage = event.offset + 1;
    if(this.uid){
      this.fetchData(this.uid);
    }
  }
  onSubmit() {
    const successMsg = this.isEditMode ? '102' : '101';
    this.motorClmSurHdrEntity.surStatus = this.isEditMode && this.data.length ? 'S': 'P';

    if (this.motorClmSurHdrEntity.surFcAmt === null) {
      this.motorClmSurHdrEntity.surFcAmt = 0;
    }
  
    if (this.motorClmSurHdrEntity.surLcAmt === null) {
      this.motorClmSurHdrEntity.surLcAmt = 0;
    }
  
    if(!this.isEditMode){
         
      this.apiService.GetSurUidSequence().subscribe({
        next: (surUid: number) => {
          this.motorClmSurHdrEntity.surUid = surUid;

          this.motorClmSurHdrEntity.surCrBy = this.userId ? this.userId : '';
          this.motorClmSurHdrEntity.surCrDt = new Date;
             
          this.motorClmSurHdrEntity.surUid = surUid;   
          console.log('inside:',surUid);    
          this.motorClmSurHdrEntity.surNo = `S/${new Date().getFullYear()}/${this.motorClmSurHdrEntity.surUid.toString().padStart(6, '0')}`; 
          this.apiService.saveSurveyHeader(this.motorClmSurHdrEntity).subscribe({
            next: (response: string) => {
              if (response.trim() === 'true') {
                this.apiService.getErrorCode(successMsg).subscribe((response: any) => {
                  this.alertService.showAlert(response[0].ERR_TYPE, response[0].ERR_DESC, 'success');
                
                });
                this.motorClaimEntity.clmSurCrYn = 'Y';
                this.motorClaimEntity.clmSurNo =this.motorClmSurHdrEntity.surNo;
                this.motorClaimEntity.clmUid = this.uid;
                
                this.apiService.UpdateSurveyCreated(this.motorClaimEntity).subscribe({
                  next: (response: string) => {
                    if (response.trim() === 'true') {
                      this.apiService.getErrorCode(successMsg).subscribe((response: any) => {
                        this.alertService.showAlert(response[0].ERR_TYPE, response[0].ERR_DESC, 'success');
                      
                      });
            
                    } 
                  },
                  error: (error: any) => {
                    console.error('Error:', error);
                    this.alertService.showAlert('Error', error, 'error');
                  }
                });
                this.router.navigate(['/survey',surUid]);     
              } 
            },
            error: (error: any) => {
              console.error('Error:', error);
              this.alertService.showAlert('Error', error, 'error');
            }
          });       
        },
        error: (error: any) => {
          this.alertService.showAlert('Error', error, 'error');
        }
      });



     
    } else{
      this.apiService.updateSurveyHeader(this.motorClmSurHdrEntity).subscribe({
        next: (response: string) => {
          if (response.trim() === 'true') {
            this.apiService.getErrorCode(successMsg).subscribe((response: any) => {
              this.alertService.showAlert(response[0].ERR_TYPE, response[0].ERR_DESC, 'success');
            
            });
            this.router.navigate(['/survey',this.motorClmSurHdrEntity.surUid]);     
          } 
        },
        error: (error: any) => {
          console.error('Error:', error);
          this.alertService.showAlert('Error', error, 'error');
        }
      });
    }
    

  }
 
  openAddSurveyDetailsModal(row?:any) {
    const modalOptions: NgbModalOptions = {
     size:'lg',
     backdrop: 'static'
    };
    const modalRef = this.modalService.open(SurveydetailsModalComponent, modalOptions);

    const selectedCurrency = this.currencies.find(currency => currency.value === this.motorClmSurHdrEntity.surCurr);
    modalRef.componentInstance.surUid = this.motorClmSurHdrEntity.surUid;
    if (selectedCurrency) {
      modalRef.componentInstance.currency = selectedCurrency.cmValue;
      modalRef.componentInstance.currencyCode = selectedCurrency.value;
    }
    if(row){
      modalRef.componentInstance.isEditMode = true; 
      modalRef.componentInstance.codeData = row;  
       
    }
    modalRef.componentInstance.listReload.subscribe(() => {
    this.modalService.dismissAll();
    this.fetchData(this.uid);  
    });
  }

  calculateFcSum(): void {
    this.motorClmSurHdrEntity.surFcAmt = this.data.reduce((sum, item) => sum + item.SURD_FC_AMT, 0);
  }

  calculateLcSum(): void {
    this.motorClmSurHdrEntity.surLcAmt = this.data.reduce((sum, item) => sum + item.SURD_LC_AMT, 0);
  }

  deleteSurveyDetails(surdUid: number ) {
    this.alertService.showConfirmationDialog().then((confirmed) => {
      if (confirmed) {
        this.apiService.deleteSurveyDetails(surdUid).subscribe({
          next: (response: any) => {
            if (response === 'true') {
              this.apiService.getErrorCode('103').subscribe((response: any) => {
                console.log(response);
                this.alertService.showAlert(response[0].ERR_TYPE, response[0].ERR_DESC,'success');
               });     
              this.fetchData(this.uid);            
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
  onReset(){

    if(!this.isEditMode){
      this.motorClmSurHdrEntity = new MotorClmSurHdrEntity();
    }else{
      this.intialiseForm();

    }
  }
}
