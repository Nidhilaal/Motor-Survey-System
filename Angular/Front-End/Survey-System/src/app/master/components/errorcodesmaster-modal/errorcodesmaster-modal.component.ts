import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ErrorMasterEntity } from '../../../shared/models/master/ErrorMasterEnitity';
import { ApiService } from '../../../services/api.service';
import { tap } from 'rxjs';
import { AlertService } from '../../../services/alert.service';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-errorcodesmaster-modal',
  templateUrl: './errorcodesmaster-modal.component.html',
  styleUrl: './errorcodesmaster-modal.component.css'
})
export class ErrorcodesmasterModalComponent implements OnInit {

  @Output() listReload: EventEmitter<void> = new EventEmitter<void>();
  @Input() isEditMode?: boolean; 
  @Input() errorCodeData: any;
  errorMasterEntity: ErrorMasterEntity= new ErrorMasterEntity();

  @ViewChild('testForm') testForm!: NgForm; 


  constructor(private alertService:AlertService, private apiService:ApiService, public activeModal: NgbActiveModal, private router:Router){}

  ngOnInit(): void {
    if(sessionStorage.getItem('userType') === null){
      this.router.navigate(['/account/login']);
    }else{
      this.intialiseForm();

    }
  }
  closeModal() {
    this.activeModal.dismiss();
  }
 intialiseForm(){

  if(this.errorCodeData){
    this.isEditMode = true;
    
    this.errorMasterEntity.errType = this.errorCodeData.ERR_TYPE;
    this.errorMasterEntity.errCode = this.errorCodeData.ERR_CODE;
    this.errorMasterEntity.errDesc = this.errorCodeData.ERR_DESC;

  }
 }
  onSubmit(){
    const method = this.isEditMode ? 'UpdateErrorCodesMaster' : 'SaveErrorCodesMaster';
    const successMsg = this.isEditMode ? '102' : '101';

    const observable = this.isEditMode ? 
      this.apiService[method](this.errorMasterEntity).pipe(
        tap(() => this.listReload.emit())
      ) : 
      this.apiService[method](this.errorMasterEntity).pipe(
        tap(() => this.listReload.emit())
      );

    observable.subscribe({
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
  }
  onReset() {

    if(this.isEditMode){
      this.intialiseForm();
    }else{
      
      this.testForm.resetForm();

    }

  }
  onErrCodeChange() {
    this.apiService.checkDuplicateErrorCodesMaster(this.errorMasterEntity.errCode).subscribe({
      next: (response: any) => {
        if (response === 'true') {
          this.errorMasterEntity.errCode = '';
          this.apiService.getErrorCode('201').subscribe((response: any) => {
            console.log(response);
            this.alertService.showAlert(response[0].ERR_TYPE, response[0].ERR_DESC,'warning');
           });            
        } 
      },
     
    });
  }  
}
