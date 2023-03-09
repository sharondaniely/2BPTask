import { Component } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ModalComponent } from './components/report-modal/modal.component';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = '2BP task';

  constructor(private modalService: NgbModal) { }

  openModal() {
    const modalRef = this.modalService.open(ModalComponent);

  }
}
