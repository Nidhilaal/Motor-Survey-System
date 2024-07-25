import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { ApiService } from '../../../services/api.service';
import { NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { AlertService } from '../../../services/alert.service';
import { ErrorcodesmasterModalComponent } from '../../components/errorcodesmaster-modal/errorcodesmaster-modal.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-errorcodesmaster-listing',
  templateUrl: './errorcodesmaster-listing.component.html',
  styleUrl: './errorcodesmaster-listing.component.css',
  encapsulation: ViewEncapsulation.None,

})
export class ErrorcodesmasterListingComponent implements OnInit {
  data: any[] = [];
  count: number = 0;
  currentPage: number = 1;
  pageSize: number = 5; 
 
  constructor(private apiService: ApiService, private alertService: AlertService, private modalService: NgbModal,private router:Router) { }

  ngOnInit(): void {
    if(sessionStorage.getItem('userType') === null){
      this.router.navigate(['/account/login']);
    }else{
      this.fetchData();
    }
   
  }

  fetchData() {
    const offset = (this.currentPage - 1) * this.pageSize;

    this.apiService.getErrorCodesMasterList().subscribe((response: any) => {
      this.data = response;
      this.count = this.data.length;   
    });
  }
  setPage(event: any) {
    this.currentPage = event.offset + 1;
    this.fetchData();
  }

  openAddErrorCodeModal(row?:any) {
    const modalOptions: NgbModalOptions = {
     size:'lg',
     backdrop: 'static'
    };
    const modalRef = this.modalService.open(ErrorcodesmasterModalComponent, modalOptions);

    if(row){
      modalRef.componentInstance.isEditMode = true; 
      modalRef.componentInstance.errorCodeData = row;  
    }
    modalRef.componentInstance.listReload.subscribe(() => {
    this.modalService.dismissAll();
    this.fetchData();  
    });
  }

  deleteErrorCodesMaster(errCode: string) {
    this.alertService.showConfirmationDialog().then((confirmed) => {
      if (confirmed) {
        this.apiService.deleteErrorCodesMaster(errCode,).subscribe({
          next: (response: any) => {
            if (response === 'true') {

              this.apiService.getErrorCode('103').subscribe((response: any) => {
                console.log(response);
                this.alertService.showAlert(response[0].ERR_TYPE, response[0].ERR_DESC,'success');
               });

              this.fetchData();
            } 
          },
          error: (error: any) => {
            console.error('Error:', error);
            this.alertService.showAlert('Error', error,'error');
          }
        });
      }
    });
  }
}
