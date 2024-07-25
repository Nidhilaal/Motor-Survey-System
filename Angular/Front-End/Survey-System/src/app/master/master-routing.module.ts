import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from '../account/pages/dashboard/dashboard.component';
import { HomeComponent } from '../account/pages/home/home.component';
import { CodesmasterListingComponent } from './pages/codesmaster-listing/codesmaster-listing.component';
import { ErrorcodesmasterListingComponent } from './pages/errorcodesmaster-listing/errorcodesmaster-listing.component';
import { UsermasterListingComponent } from './pages/usermaster-listing/usermaster-listing.component';
import { CodesmasterModalComponent } from './components/codesmaster-modal/codesmaster-modal.component';

const routes: Routes = [
  {
    path: 'home',
    component:HomeComponent
  },
  {
    path: 'codesmaster-listing',
    component: CodesmasterListingComponent
  },
  {
    path: 'errorcodesmaster-listing',
    component: ErrorcodesmasterListingComponent
  },
  {
    path: 'usermaster-listing',
    component: UsermasterListingComponent
  },
  {
    path: 'codesmaster-modal',
    component: CodesmasterModalComponent
  },
  {
    path: '',
    component:HomeComponent
  }
]

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MasterRoutingModule { }
