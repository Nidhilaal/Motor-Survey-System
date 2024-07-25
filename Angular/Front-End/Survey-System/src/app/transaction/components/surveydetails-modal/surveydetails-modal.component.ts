import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { MotorClmSurDtlEntity } from '../../../shared/models/transaction/MotorClmSurDtlEntity';
import { ApiService } from '../../../services/api.service';
import { AlertService } from '../../../services/alert.service';
import { CurrencyPipe } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-surveydetails-modal',
  templateUrl: './surveydetails-modal.component.html',
  styleUrl: './surveydetails-modal.component.css',
  
})
export class SurveydetailsModalComponent implements OnInit  {
  @Input() currency?: number;
  @Input() currencyCode?: string;
  @Input() surUid?: number;
  @Input() codeData?: any;
  @Input() isEditMode?: boolean;
  @Output() listReload: EventEmitter<void> = new EventEmitter<void>();

  motorClmSurDtlEntity : MotorClmSurDtlEntity = new MotorClmSurDtlEntity();

  ddlItemCode: { cmDisplay: string, cmCode: string, cmDesc: string}[] = []; 
  ddlSurdType: { cmDisplay: string, cmCode: string, cmDesc: string}[] = []; 

  constructor(public activeModal: NgbActiveModal,private apiService:ApiService,private alertService:AlertService ,private router:Router){}
  
  ngOnInit(): void {
    if(sessionStorage.getItem('userType') === null){
      this.router.navigate(['/account/login']);
    }else{
      this.intialiseModal();

    }
  }
  
  intialiseModal(){
    const itemCode = 'SURD_ITEM_CODE';
    this.apiService.getCodes(itemCode).subscribe((response: any[]) => {
      this.ddlItemCode = response.map(item => ({
        cmDisplay: item.CM_DISPLAY,
        cmCode: item.CM_CODE,
        cmDesc: item.CM_DESC
      }));
      if (this.codeData) {
        const selectedItem = this.ddlItemCode.find(item => item.cmDesc === this.codeData.SURD_ITEM_CODE);
        if (selectedItem) {
          this.motorClmSurDtlEntity.surdItemCode = selectedItem.cmCode;  
        }
      }
    });
  
    const type = 'SURD_TYPE';
    this.apiService.getCodes(type).subscribe((response: any[]) => {
      this.ddlSurdType = response.map(item => ({
        cmDisplay: item.CM_DISPLAY,
        cmCode: item.CM_CODE,
        cmDesc: item.CM_DESC
      }));

      if (this.codeData) {
        console.log(this.codeData.SURD_TYPE);
        const selectedType = this.ddlSurdType.find(item => item.cmDesc === this.codeData.SURD_TYPE);
        if (selectedType) {
          this.motorClmSurDtlEntity.surdType = selectedType.cmCode;
        
        } 
      }
    });
  
    if(this.codeData)  {
      this.motorClmSurDtlEntity.surdUid = this.codeData.SURD_UID;
      this.motorClmSurDtlEntity.surdItemCode = this.codeData.SURD_ITEM_CODE;
      this.motorClmSurDtlEntity.surdType = this.codeData.SURD_TYPE;
      this.motorClmSurDtlEntity.surdUnitPrice = this.codeData.SURD_UNIT_PRICE;
      this.motorClmSurDtlEntity.surdQuantity = this.codeData.SURD_QUANTITY;
      this.motorClmSurDtlEntity.surdFcAmt = this.codeData.SURD_FC_AMT;
      this.motorClmSurDtlEntity.surdLcAmt = this.codeData.SURD_LC_AMT;
    }
  }

  onSubmit() {
   if(this.isEditMode){
  
    this.apiService.updateSurveyDetails(this.motorClmSurDtlEntity).subscribe({
      next: (response: string) => {
        if (response.trim() === 'true') {
          this.apiService.getErrorCode('102').subscribe((response: any) => {
            this.alertService.showAlert(response[0].ERR_TYPE, response[0].ERR_DESC, 'success');
          });
          this.listReload.emit();
        } 
      },
      error: (error: any) => {
        console.error('Error:', error);
        this.alertService.showAlert('Error', error, 'error');
      }
    });

   }else{
    this.apiService.GetSurdUidSequence().subscribe({
      next: (surdUid: number) => {
        this.motorClmSurDtlEntity.surdUid = surdUid;
        if(this.surUid){
          this.motorClmSurDtlEntity.surdSurUid= this.surUid;
        }
        console.log(this.motorClmSurDtlEntity.surdSurUid);
        this.apiService.saveSurveyDetails(this.motorClmSurDtlEntity).subscribe({
          next: (response: string) => {
            if (response.trim() === 'true') {
              this.apiService.getErrorCode('101').subscribe((response: any) => {
                this.alertService.showAlert(response[0].ERR_TYPE, response[0].ERR_DESC, 'success');
              });
              this.listReload.emit();
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
  closeModal(){
    this.activeModal.dismiss();
  }
  checkDuplicateSurveyDetails(){
    if(this.surUid){
      this.motorClmSurDtlEntity.surdSurUid= this.surUid;
    }
   
    this.apiService.checkDuplicateSurveyDetails(this.motorClmSurDtlEntity).subscribe({
      next: (response: string) => {
        if (response.trim() === 'true') {
          this.apiService.getErrorCode('201').subscribe((response: any) => {
            this.alertService.showAlert(response[0].ERR_TYPE, response[0].ERR_DESC, 'warning');
          });
          this.motorClmSurDtlEntity.surdItemCode='';
        } 
      },
      error: (error: any) => {
        console.error('Error:', error);
        this.alertService.showAlert('Error', error, 'error');
      }
    });
  }
  calculateFCAmount() {
    const unitPrice = this.motorClmSurDtlEntity.surdUnitPrice;
    const quantity = this.motorClmSurDtlEntity.surdQuantity;

    const fcAmount = unitPrice * quantity;
    this.motorClmSurDtlEntity.surdFcAmt = fcAmount;

    if (unitPrice != null && quantity != null) {
      const fcAmount = unitPrice * quantity;

      this.motorClmSurDtlEntity.surdFcAmt = fcAmount;
      if(this.currency){

        this.motorClmSurDtlEntity.surdLcAmt = fcAmount * this.currency;
        console.log(   this.motorClmSurDtlEntity.surdLcAmt);

      }       
    }  
  }
  onReset() {
    this.intialiseModal();

    if(!this.isEditMode){
   
      this.motorClmSurDtlEntity=  new MotorClmSurDtlEntity();
      console.log(this.motorClmSurDtlEntity);
    }
  }
}
