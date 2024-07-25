import { Component, OnInit, ViewEncapsulation } from '@angular/core';

import { NgbModal, NgbModalOptions} from '@ng-bootstrap/ng-bootstrap';
import { AlertService } from '../../../services/alert.service';
import { ApiService } from '../../../services/api.service';
import { CodesmasterModalComponent } from '../../components/codesmaster-modal/codesmaster-modal.component';
import { Router } from '@angular/router';


@Component({
  selector: 'app-codesmaster-listing',
  templateUrl: './codesmaster-listing.component.html',
  styleUrl: './codesmaster-listing.component.css',
  encapsulation: ViewEncapsulation.None,
})
export class CodesmasterListingComponent implements OnInit {
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

    this.apiService.getCodesMasterList().subscribe((response: any) => {
      this.data = response;
      this.count = this.data.length;   
    });
  }
  setPage(event: any) {
    this.currentPage = event.offset + 1;
    this.fetchData();
  }

  openAddCodeModal(row?:any) {
    const modalOptions: NgbModalOptions = {
     size:'lg',
     backdrop: 'static'
    };
    const modalRef = this.modalService.open(CodesmasterModalComponent, modalOptions);

    if(row){
      modalRef.componentInstance.isEditMode = true; 
      modalRef.componentInstance.codeData = row;  
    }
    modalRef.componentInstance.listReload.subscribe(() => {
    this.modalService.dismissAll();
    this.fetchData();  
    });
  }

  deleteCodesMaster(cmCode: string, cmType: string) {
    this.alertService.showConfirmationDialog().then((confirmed) => {
      if (confirmed) {
        this.apiService.deleteCodesMaster(cmCode, cmType).subscribe({
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

