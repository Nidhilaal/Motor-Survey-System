import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { CodesMasterEntity } from '../../../shared/models/master/CodesMasterEntity';
import { AlertService } from '../../../services/alert.service';
import { ApiService } from '../../../services/api.service';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { tap } from 'rxjs';
import { NgForm } from '@angular/forms';
import { timingSafeEqual } from 'crypto';
import { Router } from '@angular/router';

@Component({
  selector: 'app-codesmaster-modal',
  templateUrl: './codesmaster-modal.component.html',
  styleUrl: './codesmaster-modal.component.css'
})
export class CodesmasterModalComponent implements OnInit {

  @Output() listReload: EventEmitter<void> = new EventEmitter<void>();
  @Input() isEditMode: boolean = false; 
  @Input() codeData: any;
  
  codesMasterEntity: CodesMasterEntity = new CodesMasterEntity();
  cmActiveCheckbox?: boolean;
  @ViewChild('testForm') testForm!: NgForm;

  constructor(private apiService: ApiService, private alertService: AlertService, 
    public activeModal: NgbActiveModal, 
    private router: Router){}

  ngOnInit(): void {

    if(sessionStorage.getItem('userType') === null){
      this.router.navigate(['/account/login']);
    }else{
      this.intialiseForm();

    }


  }

  intialiseForm(){
  this.codesMasterEntity = this.isEditMode ? 
      new CodesMasterEntity(
        this.codeData.CM_CODE,
        this.codeData.CM_TYPE,
        this.codeData.CM_DESC,
        this.codeData.CM_VALUE,
        '',
        new Date(),
        '',
        new Date(),
        
      ) :
      new CodesMasterEntity();
      this.cmActiveCheckbox = this.isEditMode ? this.codeData.CM_ACTIVE_YN === 'Yes' : false;
}
  onSubmit() {
    if(!this.codesMasterEntity.cmValue){
      this.codesMasterEntity.cmValue = 0;
    }
    const method = this.isEditMode ? 'UpdateCodesMaster' : 'SaveCodesMaster';
    const successMsg = this.isEditMode ? '102' : '101';

    this.codesMasterEntity.cmActiveYn = this.cmActiveCheckbox ? 'Y' : 'N';
    const observable = this.isEditMode ? 
      this.apiService[method](this.codesMasterEntity).pipe(
        tap(() => this.listReload.emit())
      ) : 
      this.apiService[method](this.codesMasterEntity).pipe(
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

  closeModal() {
    this.activeModal.dismiss();
  }

checkDuplicateCodesMaster(event: any){
  if(!this.codesMasterEntity.cmValue){
    this.codesMasterEntity.cmValue = 0;
  }
  const inputField = event.target;

  this.apiService.checkDuplicateCodesMaster(this.codesMasterEntity).subscribe({
    next: (response: string) => {
      if (response.trim() === 'true') {
        this.apiService.getErrorCode('201').subscribe((response: any) => {
          this.alertService.showAlert(response[0].ERR_TYPE, response[0].ERR_DESC, 'warning');
        
        });
       inputField.value = '';
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
   
if(this.codeData){
  this.codesMasterEntity.cmType = this.codeData.CM_TYPE;
  this.codesMasterEntity.cmCode = this.codeData.CM_CODE;
  this.codesMasterEntity.cmDesc = this.codeData.CM_DESC;
  this.codesMasterEntity.cmValue = this.codeData.CM_VALUE;
  this.cmActiveCheckbox = this.codeData.CM_ACTIVE_YN === 'Yes' ? true : false;



}
  }else{
    this.testForm.resetForm();

  }

}

  }

