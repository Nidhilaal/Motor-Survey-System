import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class LoaderService {

  constructor(private spinner: NgxSpinnerService) { }

  showLoader(): void {
    this.spinner.show();
  }

  hideLoader(): void {
    this.spinner.hide();
  }
}
