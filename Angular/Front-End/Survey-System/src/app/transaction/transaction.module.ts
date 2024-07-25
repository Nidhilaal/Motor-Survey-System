import { LOCALE_ID, NgModule } from '@angular/core';
import { CommonModule, CurrencyPipe, JsonPipe, registerLocaleData } from '@angular/common';

import { TransactionRoutingModule } from './transaction-routing.module';
import { MotorclaimListingComponent } from './pages/motorclaim-listing/motorclaim-listing.component';
import { AccountModule } from '../account/account.module';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { NgbModalModule,  } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MotorclaimComponent } from './pages/motorclaim/motorclaim.component';

import { SurveyHistoryComponent } from './pages/survey-history/survey-history.component';
import { SurveyComponent } from './pages/survey/survey.component';
import { SurveydetailsModalComponent } from './components/surveydetails-modal/surveydetails-modal.component';
import { SurveyListingComponent } from './pages/survey-listing/survey-listing.component';
import { SurveyApprovalComponent } from './pages/survey-approval/survey-approval.component';

@NgModule({
  declarations: [  
    MotorclaimListingComponent,
    MotorclaimComponent,
    SurveyHistoryComponent,
    SurveyComponent,
    SurveydetailsModalComponent,
    SurveyListingComponent,
    SurveyApprovalComponent
    
    
  ],
  imports: [
    CommonModule,
    TransactionRoutingModule,
    AccountModule,
    NgxDatatableModule,
    NgbModalModule,
    FormsModule,
    ReactiveFormsModule,
 
    JsonPipe
  

    
  ]
})
export class TransactionModule {
 
 
 }
