import { Component, OnInit } from '@angular/core';
import { ContentService, TypeService, AuthenticationService } from '../services';
import { Content, Type } from '../models';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { Form, FormGroup, NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  closeResult = '';
  public contentData: Content[];
  public typeData: Type[];
  public popContentData : Content = new Content();
  public labelColor = {"Blue": "badge-primary", "Green": "badge-success", "Red": "badge-danger"}

  constructor(private authenticationService: AuthenticationService, private contentService: ContentService, private typeService: TypeService, private modalService: NgbModal, private toastr: ToastrService) { }

  ngOnInit() {
    this.getContents();
    this.getTypes();
  }
 
  getContents() {
    this.contentService.getContents().subscribe((data: Content[]) => {
      this.contentData = data;
    }, error => {
      this.toastr.error(error);
    })
  }

  getTypes() {
    this.typeService.getTypes().subscribe((data: Type[]) => {
      this.typeData = data;
    })
  }

  onSubmit(contentForm: NgForm)
  {
    if(contentForm.form.valid)
    {
      if(this.popContentData.ContentType == "0")
      {
        this.toastr.error("Please select the required type field");
      }
      else
      {
        if(this.popContentData.ContentId)
        {
          this.contentService.updateContent(this.popContentData).subscribe((data: Content) => {
            this.modalService.dismissAll();
            this.getContents();
            this.toastr.success('', 'Content updated successfully');
          }, error => {
            this.toastr.error(error);
          });
        }
        else
        {
          this.contentService.createContent(this.popContentData).subscribe((data: Content) => {
            this.getContents();
            this.modalService.dismissAll();
            this.toastr.success('', 'Content added successfully');
          }, error => {
            this.toastr.error(error);
          });
        }
      }      
    }
    else
    {
      this.toastr.info("Please enter the required details");
    }   
  }

  openModal(content, contentData?: Content) {
    if(contentData)
    {
      this.contentService.getContentById(contentData.ContentId).subscribe((data: Content) => {
        this.popContentData = data;
      }, error => {
        this.toastr.error(error);
      });
    }    
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' }).result.then((result) => {
      this.closeResult = `Closed with: ${result}`;
    }, (reason) => {
      this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
    });
  }

  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking on a backdrop';
    } else {
      return `with: ${reason}`;
    }
  }

  openContentForm(content)
  {
    this.popContentData = new Content();
    this.popContentData.ContentType = "0";
    this.openModal(content)
  }

  deleteContent(contentData?: Content)
  {
    this.contentService.deleteContent(contentData.ContentId).subscribe((data: Content) => {
      this.getContents();
      this.toastr.info('', 'Content delete successfully');
    }, error => {
      this.toastr.error(error);
    });
  }

  logout()
  {
    this.authenticationService.logout();
  }
}
