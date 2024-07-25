import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TransactionRoutingModule } from './transaction/transaction-routing.module';

const routes: Routes = [
  {
    path: "account",
    loadChildren: () => import('./account/account-routing.module').then((m) => m.AccountRoutingModule)
  }, 
  {
    path: "master",
    loadChildren: () => import('./master/master-routing.module').then((m) => m.MasterRoutingModule)
  },
  {
    path: "transaction",
    loadChildren: () => import('./transaction/transaction-routing.module').then((m) => m.TransactionRoutingModule)
  }, 
  {
    path: "",
    loadChildren: () => import('./account/account-routing.module').then((m) => m.AccountRoutingModule)
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
