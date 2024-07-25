import { NgModule, } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AccountRoutingModule } from './account-routing.module';
import { LoginComponent } from './pages/login/login.component';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HomeComponent } from './pages/home/home.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { UserNavbarComponent } from './components/user-navbar/user-navbar.component';
import { SurveyorNavbarComponent } from './components/surveyor-navbar/surveyor-navbar.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgxSpinnerModule } from 'ngx-spinner';

@NgModule({
  declarations: [
    LoginComponent,
    HomeComponent,
    DashboardComponent,
    UserNavbarComponent,
    SurveyorNavbarComponent
  ],exports:[
    UserNavbarComponent,
    SurveyorNavbarComponent
    
  ],
  imports: [
    CommonModule,
    AccountRoutingModule,
    NgxDatatableModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    NgxSpinnerModule
    

  ]
})
export class AccountModule { }
