import { Component, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { UserMasterEntity } from '../../../shared/models/master/UserMasterEntity';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { AlertService } from '../../../services/alert.service';
import { ApiService } from '../../../services/api.service';
import { Router } from '@angular/router';
import { tap } from 'rxjs';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-usermaster-modal',
  templateUrl: './usermaster-modal.component.html',
  styleUrl: './usermaster-modal.component.css'
})
export class UsermasterModalComponent {

  @Output() listReload: EventEmitter<void> = new EventEmitter<void>();
  @Input() isEditMode?: boolean; 
  @Input() userData: any;
  @ViewChild('testForm') testForm!: NgForm;

  userId = sessionStorage.getItem('userId');

  userMasterEntity: UserMasterEntity= new UserMasterEntity();

  constructor(private alertService:AlertService, private apiService:ApiService, public activeModal: NgbActiveModal, private router:Router){}

  ngOnInit(): void {
    if(sessionStorage.getItem('userType') === null){
      this.router.navigate(['/account/login']);
    }else{
      this.intialiseForm();

    }
  }
  intialiseForm(){

    if(this.userData){
      this.isEditMode = true;     
      this.userMasterEntity.userId = this.userData.USER_ID;
      this.userMasterEntity.userName = this.userData.USER_NAME;
      this.userMasterEntity.userPassword = this.userData.USER_PASSWORD;
      this.userMasterEntity.userType = this.userData.USER_TYPE === 'Surveyor' ? 'S' : 'U';
  
    }
   }
  closeModal() {
    this.activeModal.dismiss();
  }

  onSubmit(){
    this.userMasterEntity.userCrBy = this.userId ? this.userId : '';
    this.userMasterEntity.userCrDt = new Date;
    this.userMasterEntity.userUpBy = this.userId ? this.userId : '';
    this.userMasterEntity.userUpDt = new Date;
   
    const method = this.isEditMode ? 'UpdateUserMaster' : 'SaveUserMaster';
    const successMsg = this.isEditMode ? '102' : '101';

    const observable = this.isEditMode ? 
      this.apiService[method](this.userMasterEntity).pipe(
        tap(() => this.listReload.emit())
      ) : 
      this.apiService[method](this.userMasterEntity).pipe(
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
  onUserIdChange() {
    this.apiService.checkDuplicateErrorCodesMaster(this.userMasterEntity.userId).subscribe({
      next: (response: any) => {
        if (response === 'true') {
          this.userMasterEntity.userId = '';
          this.apiService.getErrorCode('201').subscribe((response: any) => {
            console.log(response);
            this.alertService.showAlert(response[0].ERR_TYPE, response[0].ERR_DESC,'warning');
           });            
        } 
      },
     
    });
  }  
  onReset(){
    if(this.isEditMode){
      this.intialiseForm();
    }else{
      
      this.testForm.resetForm();
    }
  }
}
