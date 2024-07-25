import { BrowserModule, provideClientHydration } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { AccountRoutingModule } from './account/account-routing.module';
import { AccountModule } from './account/account.module';
import { HttpClientModule } from '@angular/common/http';
import { MasterModule } from './master/master.module';
import { MasterRoutingModule } from './master/master-routing.module';
import { NgxSpinnerModule, NgxSpinnerService } from 'ngx-spinner';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { TransactionModule } from './transaction/transaction.module';
import { TransactionRoutingModule } from './transaction/transaction-routing.module';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { NgModule } from '@angular/core';
import { CommonModule, CurrencyPipe } from '@angular/common';


@NgModule({
  declarations: [
    AppComponent,        
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule,
    AccountRoutingModule,
    AccountModule,
    HttpClientModule, 
    MasterModule,
    MasterRoutingModule,
    TransactionModule,
    TransactionRoutingModule,
    BrowserAnimationsModule,
    NgxSpinnerModule,
    CommonModule   
           
  ],
  providers: [
    provideClientHydration(),
    provideAnimationsAsync(), 
  ],
  bootstrap: [AppComponent]
})
export class AppModule { 

}
