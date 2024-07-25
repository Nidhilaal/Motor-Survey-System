import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MotorclaimListingComponent } from './pages/motorclaim-listing/motorclaim-listing.component';
import { MotorclaimComponent } from './pages/motorclaim/motorclaim.component';
import { SurveyHistoryComponent } from './pages/survey-history/survey-history.component';
import { SurveyComponent } from './pages/survey/survey.component';
import { SurveyListingComponent } from './pages/survey-listing/survey-listing.component';
import { SurveyApprovalComponent } from './pages/survey-approval/survey-approval.component';

const routes: Routes = [
  {
    path: 'motorclaim-listing',
   component:MotorclaimListingComponent
  },
  {
    path: 'motorclaim',
   component:MotorclaimComponent
  },
  {
    path: 'survey/:uid', 
    component: SurveyComponent
  },
 
  {
    path: 'motorclaim/:clmUid',
    component: MotorclaimComponent 
  },
  {
    path: 'survey-history',
   component:SurveyHistoryComponent
  },
  {
    path: 'survey-approval',
   component:SurveyApprovalComponent
  },
  {
    path: 'survey-approval/:clmUid',
   component:SurveyApprovalComponent
  },

  {
    path: 'survey-listing',
   component:SurveyListingComponent
  },
 
  {
    path: '',
    component:MotorclaimListingComponent
  }
 
]

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TransactionRoutingModule { }
