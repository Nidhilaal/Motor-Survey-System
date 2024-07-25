import { NgModule } from '@angular/core';
import { CommonModule, JsonPipe } from '@angular/common';
import { MasterRoutingModule } from './master-routing.module';
import { CodesmasterListingComponent } from './pages/codesmaster-listing/codesmaster-listing.component';
import { AccountModule } from '../account/account.module';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { NgbModalModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ErrorcodesmasterListingComponent } from './pages/errorcodesmaster-listing/errorcodesmaster-listing.component';
import { UsermasterListingComponent } from './pages/usermaster-listing/usermaster-listing.component';
import { ErrorcodesmasterModalComponent } from './components/errorcodesmaster-modal/errorcodesmaster-modal.component';
import { UsermasterModalComponent } from './components/usermaster-modal/usermaster-modal.component';
import { CodesmasterModalComponent } from './components/codesmaster-modal/codesmaster-modal.component';

@NgModule({
  declarations: [
    CodesmasterListingComponent,
    CodesmasterModalComponent,
    ErrorcodesmasterListingComponent,
    UsermasterListingComponent,
    ErrorcodesmasterModalComponent,
    UsermasterModalComponent  
  ],
  imports: [
    CommonModule,
    MasterRoutingModule,    
    FormsModule,
    ReactiveFormsModule,
    AccountModule,
    NgxDatatableModule,
    NgbModalModule,
    JsonPipe

  ]
})
export class MasterModule { }
