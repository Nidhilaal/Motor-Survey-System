import { Component, ViewEncapsulation } from '@angular/core';
import { ApiService } from '../../../services/api.service';
import { NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { AlertService } from '../../../services/alert.service';
import { UsermasterModalComponent } from '../../components/usermaster-modal/usermaster-modal.component';

@Component({
  selector: 'app-usermaster-listing',
  templateUrl: './usermaster-listing.component.html',
  styleUrl: './usermaster-listing.component.css',
  encapsulation: ViewEncapsulation.None,

})
export class UsermasterListingComponent {
  data: any[] = [];
  count: number = 0;
  currentPage: number = 1;
  pageSize: number = 5; 
 
  constructor(private apiService: ApiService, private alertService: AlertService, private modalService: NgbModal) { }

  ngOnInit(): void {
    
    this.fetchData();
  }

  fetchData() {
    const offset = (this.currentPage - 1) * this.pageSize;

    this.apiService.getUserMasterList().subscribe((response: any) => {
      this.data = response;
      this.count = this.data.length;   
    });
  }
  setPage(event: any) {
    this.currentPage = event.offset + 1;
    this.fetchData();
  }

  openAddUserModal(row?:any) {
    const modalOptions: NgbModalOptions = {
     size:'lg',
    // backdrop: 'static'
    };
    const modalRef = this.modalService.open(UsermasterModalComponent, modalOptions);

    if(row){
      modalRef.componentInstance.isEditMode = true; 
      modalRef.componentInstance.userData = row;  
    }
    modalRef.componentInstance.listReload.subscribe(() => {
    this.modalService.dismissAll();
    this.fetchData();  
    });
  }

  deleteUserMaster(userId: string) {
    this.alertService.showConfirmationDialog().then((confirmed) => {
      if (confirmed) {
        this.apiService.DeleteUserMaster(userId).subscribe({
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
