import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserMasterEntity } from '../../../shared/models/master/UserMasterEntity';
import { Router } from '@angular/router';
import { AlertService } from '../../../services/alert.service';
import { ApiService } from '../../../services/api.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit {

  userMaster: UserMasterEntity = new UserMasterEntity('','','','','',new Date(),'',new Date());
  
  constructor(private alertService: AlertService ,fb: FormBuilder,private apiService: ApiService, private router: Router) { 
  
  }

  ngOnInit(): void {
    sessionStorage.clear();
  }

  login() {
    this.apiService.Validatelogin(this.userMaster).subscribe({
      next: (response) => {

      if (response === 'true') {

       this.apiService.GetUserType(this.userMaster).subscribe(userType => {

        sessionStorage.setItem('userType', userType); 
        sessionStorage.setItem('userId', this.userMaster.userId); 

          if (userType === 'U') {
            this.router.navigate(['/account/home']);
          } else if (userType === 'S') {
            this.router.navigate(['/account/dashboard']);
          } else {
          }
        });
       
      } else {
        this.alertService.showAlert('Login Failed','Invalid Credentials', 'error');
        this.router.navigate(['/login']);
                
      }
    },
    error: (error) => {
      this.alertService.showAlert('Error', 'Failed to login.','error');
     
    }
   
  });
  }
}
